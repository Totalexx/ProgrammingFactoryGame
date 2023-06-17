using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class MeltScript : MonoBehaviour
{
    public InventorySlot meltSlot;
    public InventorySlot finishSlot;
    public TMP_Text timer;
    public List<MeltScriptableObject> recipes;

    private ItemScriptableObject itemEnd;
    private float timeMelt;
    private float copyTimeMelt;
    private bool isMeltable;
    private bool wereInForeach;
    private bool isStartTimer;
    private void Start()
    {
        //meltSlot = GetComponent<InventorySlot>();
        //for (var i = 0; i < meltPanel.childCount; i++)
        //{
        //    if (meltPanel.GetChild(i).GetComponent<MeltSlot>() != null)
        //        slots.Add(meltPanel.GetChild(i).GetComponent<MeltSlot>());
        //}
    }
    private void Update()
    {
        if (meltSlot.item != null && !wereInForeach)
        {
            wereInForeach = true;
            foreach (MeltScriptableObject recipe in recipes)
            {
                if (meltSlot.item == recipe.itemStart)
                {
                    isMeltable = true;
                    itemEnd = recipe.itemEnd;
                    timeMelt = recipe.timeMelt;
                    copyTimeMelt = timeMelt;
                }
            }
        }

        if (isMeltable && meltSlot.amount > 0)
        {
            if (!isStartTimer)
            {
                isStartTimer = true;
                timeMelt = copyTimeMelt;
            }
            timeMelt -= Time.deltaTime;
            timer.text = Mathf.Round(timeMelt).ToString() + "s";
            if (timeMelt < 0)
            {
                meltSlot.amount--;
                isStartTimer = false;
                AddItem();
            }
        }

        if (meltSlot.item == null)
        {
            wereInForeach = false;
            isMeltable = false;
        }
            
    }

    public void AddItem()
    {
        if (finishSlot.item == null)
        {
            finishSlot.item = itemEnd;
            finishSlot.amount = 1;
            finishSlot.icon.GetComponent<Image>().sprite = itemEnd.icon;
            finishSlot.itemAmount.text = "1";
        }
        else
        {
            finishSlot.amount++;
            finishSlot.itemAmount.text = finishSlot.amount.ToString();
        }
        
    }
}
