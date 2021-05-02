using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class RequestUI : ItemContainerUI
{
    [Space]
    public ItemObjectRuntimeSet requestItemsSubset;
    [Space]
    public TextMeshProUGUI requesterNameText;
    public TextMeshProUGUI requestText;
    [TextArea]
    public string defaultRequestBoardText;
    [Space]
    public UnityEvent SaveTheFloatingItemEvent;
    public UnityEvent SuccessfulItemDropEvent;

    private List<Request> masterRequestsList = new List<Request>();
    private List<Request> availableRequests = new List<Request>(); // only those not completed yet
    [SerializeField] Request currentRequest;

    void OnValidate()
    {
        FillMasterRequestList();
    }

    void OnEnable()
    {
        availableRequests = FillAvailableRequestsList();
        GetAnotherAvailableRequest();
        ResetBoardDisplayForNew();
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

        // TODO: probably not the best way to do this; does this work???
        // if (floatingItem.Item.GetType().Equals("Potion"))
        if (floatingItem.Item is Potion p)
        {
            // Just drop the item when dropSlot is empty
            if (dropSlot.IsEmpty && (floatingItem.Item is Potion)) 
            {
                AddItem(floatingItem);
                AddItemToSlot(floatingItem, dropSlot);
                SuccessfulItemDropEvent.Invoke();
            } 
            // Swap the two items if the dropSlot isn't empty
            else if (!dropSlot.IsEmpty)
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
            // TODO: get the first item for now.... will there ever be more than 1 item to give???
            Potion solutionItem = (Potion)inventory.items[0];
            
            currentRequest.CalculateSolutionPoints(solutionItem);
            // currentRequest.SetCompletedEval(eval);
            DisplayEvaluation();

            // Set request to be completed & get new
            currentRequest.ResetRequestToDefault(); // temp reset
            // currentRequest.SetIsCompleted(true);
            // availableRequests.Remove(currentRequest);
            GetAnotherAvailableRequest();

            // Clear input inventory and subset to prepare for a new request.
            for (int i = requestItemsSubset.items.Count - 1; i >= 0; i--)
            {
                ItemObject item = (ItemObject)requestItemsSubset.items[i];
                requestItemsSubset.Remove(item);
                item.Destroy();
            }
            inventory.ClearInventory();

            // but don't reset board yet until a new day
            // TODO: disable the "give" slot until new request
        }
    }

    public void ResetBoardDisplayForNew()
    {
        if (currentRequest != null && !currentRequest.Completed)
        {
            requestText.text = currentRequest.Description;
            requesterNameText.text = currentRequest.Requester;
        }
        // means there's no more new requests
        // TODO: make a different way to check?
        else if (availableRequests.Count == 0 || currentRequest.Completed)
        {
            requestText.text = defaultRequestBoardText;
        }
    }

    #endregion

    private void DisplayEvaluation()
    {
        requestText.text = $"Score: {currentRequest.FinalScore} / {currentRequest.PerfectScore}";
    }

    private void GetAnotherAvailableRequest()
    {
        if (availableRequests.Count > 0)
        {
            int nextReqIndex = Random.Range(0, availableRequests.Count);
            currentRequest = availableRequests[nextReqIndex];
        }
    }

    private List<Request> FillAvailableRequestsList()
    {
        List<Request> availableRequests = new List<Request>();

        foreach (Request request in masterRequestsList)
        {
            if (!request.Completed)
                availableRequests.Add(request);
        }

        return availableRequests;
    }

    private void FillMasterRequestList()
    {
        string[] assetNames = AssetDatabase.FindAssets("t:Request", new[] { "Assets/ScriptableObjects/Requests" });
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
            request.Completed = false;
        }
    }

    #endregion
}
