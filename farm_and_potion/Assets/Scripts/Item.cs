using UnityEngine;

// blueprint class for all items
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    [TextArea]
    public string description;
    public Sprite artwork = null;

    [Space]
    // stat values
    public int strength;
    public int luck;
    public int dex;
    public int mag;

    // Use explicitly for the floating variable!
    public void OverwriteItemWithItem(Item newItem)
    {
        name = newItem.name;
        description = newItem.description;
        artwork = newItem.artwork;

        // set stats too
    }
}
