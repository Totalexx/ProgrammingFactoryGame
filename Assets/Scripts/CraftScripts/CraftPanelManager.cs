using Humanizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftPanelManager : MonoBehaviour
{
    public List<CraftSlot> slots = new List<CraftSlot>();
    public GameObject canvas;
    public ItemScriptableObject assemblingMachine;
    public bool isListCraftPanelActive;
    public CraftScriptableObject lastCraftItem;

    private GameObject craftingObject;
    private Transform listCraftPanel;
    private Transform craftPanelActiveSlots;
    private Transform craftPanelPassiveSlots;
    private Transform textAssemblingMachine;
    private GameObject button;
    private GameObject inventory;
    private GameObject craftPanel;
    private GameObject textRecipes;
    private GameObject textMelts;
    private GameObject panel;
    private Camera mainCamera;
    private bool isOpen;

    void Start()
    {
        panel = canvas.transform.Find("Panel").gameObject;
        inventory = panel.transform.Find("Inventory").gameObject;
        craftPanel = panel.transform.Find("CraftPanel").gameObject;
        textRecipes = panel.transform.Find("TextRecipes").gameObject;
        textMelts = panel.transform.Find("TextMelts").gameObject;

        mainCamera = Camera.main;
        isOpen = false;
        listCraftPanel = transform.Find("ListCraftPanel");
        craftPanelActiveSlots = transform.Find("CraftPanelActiveSlots");
        craftPanelPassiveSlots = transform.Find("CraftPanelPassiveSlots");
        button = transform.Find("ExitButton").gameObject;
        textAssemblingMachine = transform.Find("TextAssemblingMachine");
        isListCraftPanelActive = true;

        for (var i = 0; i < listCraftPanel.childCount; i++)
        {
            if (listCraftPanel.GetChild(i).GetComponent<CraftSlot>() != null)
                slots.Add(listCraftPanel.GetChild(i).GetComponent<CraftSlot>());
        }
        //craftPanel.SetParent(listCraftPanel);
        listCraftPanel.gameObject.SetActive(false);
        craftPanelActiveSlots.gameObject.SetActive(false);
        craftPanelPassiveSlots.gameObject.SetActive(false);
        button.SetActive(false);
        textAssemblingMachine.gameObject.SetActive(false);
    }

    public void SetActiveCraftPanel(CraftScriptableObject craftItem)
    {
        lastCraftItem = craftItem;
        isListCraftPanelActive = false;
        listCraftPanel.gameObject.SetActive(false);
        craftPanelActiveSlots.gameObject.SetActive(true);
        craftPanelPassiveSlots.gameObject.SetActive(true);
        button.SetActive(true);

        var craftSlots = new List<InventorySlot>();
        
        for (var i = 2; i >= craftItem.resources.Count; i--)
            craftPanelActiveSlots.GetChild(i).gameObject.SetActive(false);

        for (var i = 0; i < craftPanelActiveSlots.childCount; i++)
            if (craftPanelActiveSlots.GetChild(i).gameObject.activeSelf)
                craftSlots.Add(craftPanelActiveSlots.GetChild(i).GetComponent<InventorySlot>());
            else
                craftPanelActiveSlots.GetChild(i).GetComponent<InventorySlot>().item = null;

        for (var i = 0; i < craftItem.resources.Count; i++)
        {
            craftSlots[i].isCraftSlot = true;
            craftSlots[i].item = craftItem.resources[i].item;
            craftSlots[i].icon.GetComponent<Image>().sprite = craftItem.resources[i].item.icon;
            craftSlots[i].icon.GetComponent<Image>().color = new Color(255, 255, 255, 100);
        }
    }

    public void SetActiveListCraftPanel()
    {
        isListCraftPanelActive = true;
        craftPanelActiveSlots.gameObject.SetActive(false);
        craftPanelPassiveSlots.gameObject.SetActive(false);
        button.SetActive(false);
        listCraftPanel.gameObject.SetActive(true);

        for (var i = 0; i < craftPanelActiveSlots.childCount; i++)
        {
            craftPanelActiveSlots.GetChild(i).GetComponent<InventorySlot>().item = null;
            craftPanelActiveSlots.GetChild(i).gameObject.SetActive(true);
        }
            
    }

    
    void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        isOpen = canvas.GetComponent<InventoryManager>().isOpen;

        if (Input.GetMouseButtonUp(1) && !isOpen)
        {
            var colliderBuilding = Physics2D.OverlapPoint(mousePos);

            if (colliderBuilding != null
                && colliderBuilding.gameObject.GetComponent<Item>()?.item == assemblingMachine
                && colliderBuilding.gameObject == gameObject.transform.parent.gameObject)
            {
                isOpen = !isOpen;
                panel.SetActive(true);
                inventory.gameObject.SetActive(true);
                craftPanel.SetActive(false);
                textRecipes.SetActive(false);
                textMelts.SetActive(false);
                textAssemblingMachine.gameObject.SetActive(true);
                if (isListCraftPanelActive)
                    listCraftPanel.gameObject.SetActive(true);
                else
                    SetActiveCraftPanel(lastCraftItem);
            }
        }

        if (craftPanel.activeInHierarchy || !inventory.activeInHierarchy)
        {
            listCraftPanel.gameObject.SetActive(false);
            craftPanelActiveSlots.gameObject.SetActive(false);
            craftPanelPassiveSlots.gameObject.SetActive(false);
            button.SetActive(false);
            textAssemblingMachine.gameObject.SetActive(false);
        }
    }
}
