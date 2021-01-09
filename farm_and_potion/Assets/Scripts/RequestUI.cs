using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class RequestUI : ItemContainerUI
{
    public ItemObjectRuntimeSet requestItemsSubset;
    [Space]
    public TextMeshProUGUI requestText;
    [TextArea] public string defaultRequestBoardText;
    [Space]
    public UnityEvent SaveTheFloatingItemEvent;

    private List<Request> masterRequestsList = new List<Request>();
    private List<Request> availableRequests = new List<Request>(); // only those not completed yet
    [SerializeField] Request currentRequest;

    void OnValidate()
    {
        PopulateMasterRequestList();
    }

    void OnEnable()
    {
        availableRequests = PopulateAvailableRequestsList();
        GetAnotherAvailableRequest();
        ResetBoardForNewRequest();
    }

    void OnDisable()
    {
        ResetAllRequestsCompletion(); // for prototype testing
    }

    // TODO: duplicate code from CraftingUI (kind of)
    protected override void AddItem(ItemObject itemObject)
    {
        base.AddItem(itemObject);
        requestItemsSubset.Add(itemObject);
    }

    // TODO: duplicate code from CraftingUI (kind of)
    protected override void RemoveItem(ItemObject itemObject)
    {
        base.RemoveItem(itemObject);
        requestItemsSubset.Remove(itemObject);
    }

    #region Event Responses

    // TODO: duplicate code from CraftingUI
    public void ItemDropResponse()
    {
        ItemSlot dropSlot = slots[(int)dropSlotIndex.value];
        ItemObject floatingItem = GetFloatingItem();

        // TODO: probably not the best way to do this
        if (floatingItem.GetItem().GetItemType().name.Equals("Potion"))
        {
            // Just drop the item when dropSlot is empty
            if (dropSlot.isEmpty()) 
            {
                AddItem(floatingItem);
                AddItemToSlot(floatingItem, dropSlot);
            } 
            // Swap the two items if the dropSlot isn't empty
            else if (!dropSlot.isEmpty())
            {
                // save the existing item first
                // and get the start slot from the itemObject; maybe not use GetComponent?

                ItemObject existingItem = dropSlot.GetComponentInChildren<ItemObject>();
                floatingItemMasterIndex.value = inventorySet.GetIndex(existingItem); // save item

                RemoveItem(existingItem);
                AddItem(floatingItem);

                AddItemToSlot(floatingItem, dropSlot);

                SaveTheFloatingItemEvent.Invoke();
            }
        }
    }

    // TODO: duplicate code from CraftingUI
    public void ResetSlotResponse() 
    {
        ItemObject item = GetFloatingItem();
        RemoveItem(item);
        
        SaveTheFloatingItemEvent.Invoke();
    }

    public void GiveClientSolutionResponse()
    {
        // check there's an item being submitted
        if (inventory.items.Count > 0)
        {
            // get the first item for now.... will there ever be more than 1 item to give???
            Item solutionItem = inventory.items[0];
            
            // get ailment points for each ailment met
            List<Ailment> ailmentReqs = currentRequest.GetAilmentRequirements();
            List<Ailment> ailmentReqsMet = solutionItem.GetAilments().Intersect(ailmentReqs).ToList();
            int ailmentPoints = ailmentReqsMet.Count();

            // get stat points for each point over the minimum
            Dictionary<Stat, int> requestStats = currentRequest.GetStatRequirements();
            int statPoints = 0;
            foreach (KeyValuePair<Stat, int> solutionStat in solutionItem.GetStatsDictionary())
            {
                if (requestStats.ContainsKey(solutionStat.Key))
                    statPoints += solutionStat.Value - requestStats[solutionStat.Key];
                else
                    statPoints += solutionStat.Value;
            }

            // output: satisfaction points, some kind of confirmation
            int finalSatisfactionPoints = ailmentPoints + statPoints;
            requestText.text = "The client gave you: " + finalSatisfactionPoints + "/" + currentRequest.GetSatisfactionPoints();

            // set request to be completed & get new
            currentRequest.SetIsCompleted(true);
            availableRequests.Remove(currentRequest);
            GetAnotherAvailableRequest();

            // destory gameobject
            for (int i = requestItemsSubset.items.Count - 1; i >= 0; i--)
            {
                ItemObject item = requestItemsSubset.items[i];
                requestItemsSubset.Remove(item);
                item.Destroy();
            }

            // but don't reset board yet until a new day
        }
    }

    public void ResetBoardForNewRequest()
    {
        if (currentRequest != null && !currentRequest.IsCompleted())
        {
            requestText.text = currentRequest.description;
            // clear inventories and such here?
            inventory.ClearInventory();
            inventorySet.items.Clear();
        }
        // means there's no more new requests
        // TODO: make a different way to check?
        else if (availableRequests.Count == 0 || currentRequest.IsCompleted())
        {
            requestText.text = defaultRequestBoardText;
        }
    }

    #endregion

    private void CalculateStatSatisfactionPoints()
    {

    }

    private void GetAnotherAvailableRequest()
    {
        if (availableRequests.Count > 0)
        {
            int nextReqIndex = Random.Range(0, availableRequests.Count);
            currentRequest = availableRequests[nextReqIndex];
        }
    }

    private List<Request> PopulateAvailableRequestsList()
    {
        List<Request> availableRequests = new List<Request>();

        foreach (Request request in masterRequestsList)
        {
            if (!request.IsCompleted())
                availableRequests.Add(request);
        }

        return availableRequests;
    }

    private void PopulateMasterRequestList()
    {
        string[] assetNames = AssetDatabase.FindAssets("t:Request", new[] { "Assets/Requests" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var request = AssetDatabase.LoadAssetAtPath<Request>(SOpath);
            masterRequestsList.Add(request);
        }
    }

    #region For Testing Purposes

    private void ResetAllRequestsCompletion()
    {
        foreach (Request request in masterRequestsList)
        {
            request.SetIsCompleted(false);
        }
    }

    #endregion
}
