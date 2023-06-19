using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftScriptableObject : ScriptableObject
{
    public ItemScriptableObject item;
    public int craftAmount;
    public float timeCreate;
    public List<CraftResource> resources;
}

[System.Serializable]
public class CraftResource
{
    public ItemScriptableObject item;
    public int craftAmount;
}
