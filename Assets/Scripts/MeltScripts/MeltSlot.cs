using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeltSlot : MonoBehaviour
{
    public MeltScriptableObject[] recipes;
    public ItemScriptableObject item;
    public int amount;
    public GameObject icon;
    public TMP_Text itemAmount;

    private void Awake()
    {
        icon = transform.GetChild(0).gameObject;
        itemAmount = transform.GetChild(1).GetComponent<TMP_Text>();
    }
    void Start()
    {
        amount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void MeltObject()
    //{
    //    foreach (var recipe in recipes)
    //    {
    //        if (recipe.itemStart == item)
    //        {

    //        }
    //    }
    //}
}
