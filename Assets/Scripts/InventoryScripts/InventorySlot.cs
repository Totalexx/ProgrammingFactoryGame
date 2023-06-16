using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject item;
    public int amount;
    public bool isEmpty = true;
    public GameObject icon;
    public TMP_Text itemAmount;
    private Transform dragObject;

    private void Awake()
    {
        dragObject = transform.GetChild(0).transform;
        icon = dragObject.GetChild(0).gameObject;
        itemAmount = dragObject.GetChild(1).GetComponent<TMP_Text>();
    }

    private void Start()
    {
        dragObject = transform.GetChild(0).transform;
        icon = dragObject.GetChild(0).gameObject;
        itemAmount = dragObject.GetChild(1).GetComponent<TMP_Text>();
    }
    public void SetIcon(Sprite _icon)
    {
        icon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        icon.GetComponent<Image>().sprite = _icon;
    }
    
}
