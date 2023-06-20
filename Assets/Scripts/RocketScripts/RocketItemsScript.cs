using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketItemsScript : MonoBehaviour
{
    public int finishAmount;
    public bool isEnoughResources;

    private Transform canvas;
    private Transform listRocketPanel;
    private List<InventorySlot> rocketSlots = new List<InventorySlot>();
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.GetChild(0);
        listRocketPanel = canvas.GetChild(0);
        isEnoughResources = false;
        for (var i = 0; i < listRocketPanel.childCount; i++)
        {
            rocketSlots.Add(listRocketPanel.GetChild(i).GetComponent<InventorySlot>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        var isEnough = false;
        foreach (var slot in rocketSlots)
        {
            if (slot.amount < finishAmount)
            {
                isEnough = false;
                break;
            }
            isEnough = true;
        }

        if (isEnough)
        {
            foreach (var slot in rocketSlots)
            {
                slot.amount = 0;
            }
            isEnoughResources = true;
        }
    }
}
