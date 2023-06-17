using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour, IDragHandler
{
    // Start is called before the first frame update
    private InventorySlot handSlot;
    private Transform player;
    void Start()
    {
        handSlot = GetComponent<InventorySlot>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (handSlot.item != null)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 1;
            handSlot.transform.position = mousePos;
        }
        else
        {
            handSlot.icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Следует");
        if (handSlot.isEmpty)
            return;
        GetComponent<RectTransform>().position = Input.mousePosition;
    }
}
