using UnityEngine;

// blueprint class for all items
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    [TextArea]
    public string description;
    public Sprite artwork = null;

    // stat values

}
