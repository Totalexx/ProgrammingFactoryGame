using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class MeltScript : MonoBehaviour
{
    public Transform meltPanel;
    public List<MeltSlot> slots = new List<MeltSlot>();
    private void Start()
    {
        for (var i = 0; i < meltPanel.childCount; i++)
        {
            if (meltPanel.GetChild(i).GetComponent<MeltSlot>() != null)
                slots.Add(meltPanel.GetChild(i).GetComponent<MeltSlot>());
        }
    }
    private void Update()
    {
        
    }

    public void Melt()
    {

    }
}
