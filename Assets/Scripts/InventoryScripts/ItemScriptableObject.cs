using UnityEngine;

public enum ItemType
{
    Building,
    Resource,
    Dron
}

public class ItemScriptableObject : ScriptableObject
{
    public string ItemName;
    public int MaxAmount;
    //public GameObject ItemCollider;
    public Sprite icon;
    public string ItemDescription;
    public ItemType Type;
}
