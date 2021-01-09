using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    public Inventory wildItemsInventory;
    public FloatVariable floatingItemIndex;
    public ItemObjectRuntimeSet wildItemsSubset;
    [Space]
    public ItemType spawnItemType;
    // [MinMaxRange(1,10)] public RangedFloat numSpawnRange;
    [Space]
    public Transform itemsParent; 
    public ItemObject itemPrefab;
    [Space]
    public UnityEvent ItemPickupRequest;

    private List<Item> spawnList = new List<Item>();

    void Start()
    {
        ClearSpawns();

        // make master list of ingredients able to spawn
        PopulateSpawnList();

        // spawn some items
        SpawnItems();
    }

    void OnDisable()
    {
        ClearSpawns();
    }

    void PopulateSpawnList()
    {
        spawnList.Clear();
        List<Item> masterList = wildItemsInventory.GetMasterList();
        foreach (Item item in masterList)
        {
            if (item.GetItemType() == spawnItemType)
                spawnList.Add(item);
        }
    }

    void SpawnItems()
    {
        int randomNumSpawns = Random.Range(1, 4);

        for (int i = 0; i < randomNumSpawns; i++)
        {
            int randomItemIndex = Random.Range(0,spawnList.Count); // gen new index each item

            ItemObject ui = ItemObject.Instantiate(itemPrefab, itemsParent.transform);
            // random item from spawn list
            Item uniqueItem = Object.Instantiate(spawnList[randomItemIndex]);

            wildItemsInventory.AddItem(uniqueItem);
            ui.SetItem(uniqueItem);

            wildItemsSubset.Add(ui);
        }
    }

    void ClearSpawns()
    {
        wildItemsInventory.ClearInventory();

        for (int i = wildItemsSubset.items.Count - 1; i >= 0 ; i--)
        {
            Debug.Log("wild Items Subset size: " + wildItemsSubset.items.Count);
            ItemObject item = wildItemsSubset.items[i];
            
            item.Destroy();
        }
    }

    #region Event Responses

    public void OnNewDay()
    {
        ClearSpawns();
        SpawnItems();
    }

    #endregion
}
