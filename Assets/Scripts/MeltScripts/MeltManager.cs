using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class MeltManager : MonoBehaviour
{
    public Transform meltPanel;
    public GameObject gameObjectMeltPanel;
    public Transform inventoryPanel;
    public GameObject inventory;
    public GameObject canvas;
    public GameObject craftPanel;
    public GameObject textRecipes;
    public GameObject textMelts;
    public ItemScriptableObject furnaceItem;

    private bool isOpen;
    private float meltTime;
    private List<MeltSlot> meltSlots = new List<MeltSlot>();
    private float maxTime;
    private bool isStartTimer;
    private Camera mainCamera;
    private GameObject Inventory;
    private GameObject CraftPanel;
    private GameObject TextRecipes;
    private GameObject TextMelts;
    private void Start()
    {
        mainCamera = Camera.main;
        Inventory = canvas.transform.Find("Panel").Find("Inventory").Find("Cell").gameObject;
        Debug.Log(Inventory);
        for (var i = 0; i < meltPanel.childCount - 1; i++)
        {
            Debug.Log(i);
            if (meltPanel.GetChild(i).GetComponent<MeltSlot>() != null)
                meltSlots.Add(meltPanel.GetChild(i).GetComponent<MeltSlot>());
        }
        maxTime = 0;
        isStartTimer = false;
        gameObjectMeltPanel.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 cameraPos = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
        isOpen = canvas.GetComponent<InventoryManager>().isOpen;

        if (Input.GetMouseButtonDown(1) &&
            !isOpen &&
            Physics2D.OverlapPoint(mousePos).gameObject.GetComponent<Item>().item == furnaceItem)
        {
            Collider2D colliderBuilding = Physics2D.OverlapPoint(mousePos);
            var isFurnace = colliderBuilding.gameObject.GetComponent<Item>().item == furnaceItem;
            var smth = colliderBuilding.gameObject.GetComponent<MeltManager>();
            Debug.Log(smth);
            if (isFurnace)
            {
                isOpen = !isOpen;
                inventory.SetActive(true);
                gameObjectMeltPanel.SetActive(true);
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

        if (craftPanel.active || !inventory.active)
            gameObjectMeltPanel.SetActive(false);

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