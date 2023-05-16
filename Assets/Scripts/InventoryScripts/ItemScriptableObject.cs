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
    public Sprite icon;
    public string ItemDescription;
    public bool isBuilding;
    public ItemType Type;
}
