using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPanelManager : MonoBehaviour
{
    public GameObject craftingObject;
    public Transform craftingPanel;
    public List<CraftSlot> slots = new List<CraftSlot>();

    void Start()
    {
        for (var i = 0; i < craftingPanel.childCount; i++)
        {
            if (craftingPanel.GetChild(i).GetComponent<CraftSlot>() != null)
                slots.Add(craftingPanel.GetChild(i).GetComponent<CraftSlot>());
        }
        craftingObject.SetActive(false);
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
