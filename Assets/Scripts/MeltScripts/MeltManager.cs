using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MeltManager : MonoBehaviour
{
    public GameObject meltPanel;
    public GameObject canvas;
    public ItemScriptableObject furnaceItem;

    private bool isOpen;
    private float meltTime;
    private List<MeltSlot> meltSlots = new List<MeltSlot>();
    private float maxTime;
    private bool isStartTimer;
    private Camera mainCamera;
    private GameObject inventory;
    private GameObject craftPanel;
    private GameObject textRecipes;
    private GameObject textMelts;
    private GameObject panel;
    
    private void Start()
    {
        mainCamera = Camera.main;
        panel = canvas.transform.Find("Panel").gameObject;
        inventory = panel.transform.Find("Inventory").gameObject;
        craftPanel = panel.transform.Find("CraftPanel").gameObject;
        textRecipes = panel.transform.Find("TextRecipes").gameObject;
        textMelts = panel.transform.Find("TextMelts").gameObject;

        for (var i = 0; i < meltPanel.transform.childCount - 1; i++)
        {
            Debug.Log(i);
            if (meltPanel.transform.GetChild(i).GetComponent<MeltSlot>() != null)
                meltSlots.Add(meltPanel.transform.GetChild(i).GetComponent<MeltSlot>());
        }
        maxTime = 0;
        isStartTimer = false;
        meltPanel.SetActive(false);
        
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 cameraPos = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
        isOpen = canvas.GetComponent<InventoryManager>().isOpen;

        if (Input.GetMouseButtonUp(1) && !isOpen)
        {
            var colliderBuilding = Physics2D.OverlapPoint(mousePos);
            
            if (colliderBuilding != null 
                && colliderBuilding.gameObject.GetComponent<Item>()?.item == furnaceItem
                && colliderBuilding.gameObject == gameObject.transform.parent.gameObject)
            {
                isOpen = !isOpen;
                panel.SetActive(true);
                inventory.SetActive(true);
                meltPanel.SetActive(true);
                craftPanel.SetActive(false);
                textRecipes.SetActive(false);
                textMelts.SetActive(true);
            }
        }

        if (meltSlots[0].item != null)
        {
            isStartTimer = true;
            meltTime = HowMuchTime(meltSlots[0]);
        }

        if (craftPanel.activeInHierarchy || !inventory.activeInHierarchy)
            meltPanel.SetActive(false);

        if (isStartTimer)
        {
            meltTime -= Time.deltaTime;
            maxTime += Time.deltaTime;
            if (meltTime < 0)
            {
                meltTime = maxTime;
                maxTime = 0;
                meltSlots[1].item = meltSlots[0].item;
                meltSlots[0].amount -= 1;
            }
        }
    }

    public void AddItem(InventorySlot slot)
    {
        var a = slot.GetComponent<Button>().onClick;
        Debug.Log(a.GetPersistentMethodName(0));
        if (!slot.item.isBuilding)
        {
            meltSlots[0].item = slot.item;
            meltSlots[0].itemAmount = slot.itemAmount;
            slot.itemAmount.text = "0";
        }
    }

    private float HowMuchTime(MeltSlot slot)
    {
        var item = slot.item;
        for (var i = 0; i < meltSlots[0].recipes.Length; i++)
            if (item == meltSlots[0].recipes[i])
                return meltSlots[0].recipes[i].timeMelt;
        return 0;
    }
}