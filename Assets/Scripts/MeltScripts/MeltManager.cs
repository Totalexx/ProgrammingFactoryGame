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
    public GameObject canvas;
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
        var panel = canvas.transform.Find("Panel");
        Inventory = panel.Find("Inventory").gameObject;
        CraftPanel = panel.Find("CraftPanel").gameObject;
        TextRecipes = panel.Find("TextRecipes").gameObject;
        TextMelts = panel.Find("TextMelts").gameObject;

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
            Physics2D.OverlapPoint(mousePos)?.gameObject.GetComponent<Item>()?.item == furnaceItem)
        {
            Collider2D colliderBuilding = Physics2D.OverlapPoint(mousePos);
            var isFurnace = colliderBuilding.gameObject.GetComponent<Item>().item == furnaceItem;
            var smth = colliderBuilding.gameObject.GetComponent<MeltManager>();
            Debug.Log(smth);
            if (isFurnace)
            {
                isOpen = !isOpen;
                Inventory.SetActive(true);
                gameObjectMeltPanel.SetActive(true);
                CraftPanel.SetActive(false);
                TextRecipes.SetActive(false);
                TextMelts.SetActive(true);
            }
        }

        if (meltSlots[0].item != null)
        {
            isStartTimer = true;
            meltTime = HowMuchTime(meltSlots[0]);
        }

        if (CraftPanel.active || !Inventory.active)
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