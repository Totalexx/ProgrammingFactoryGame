using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class RocketPanelScript : MonoBehaviour
{
    public ItemScriptableObject rocketItem;
    public Transform canvas;

    private Transform listRocketPanel;
    private List<GameObject> objectList = new List<GameObject>();
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
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        panel = canvas.transform.Find("Panel").gameObject;
        inventory = panel.transform.Find("Inventory").gameObject;
        craftPanel = panel.transform.Find("CraftPanel").gameObject;
        textRecipes = panel.transform.Find("TextRecipes").gameObject;
        textMelts = panel.transform.Find("TextMelts").gameObject;

        for (var i = 0; i < transform.childCount; i++)
        {
            objectList.Add(transform.GetChild(i).gameObject);
        }

        foreach (var obj in objectList)
            obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        isOpen = canvas.GetComponent<InventoryManager>().isOpen;

        if (Input.GetMouseButtonUp(1) && !isOpen)
        {
            var colliderBuilding = Physics2D.OverlapPoint(mousePos);

            if (colliderBuilding != null
                && colliderBuilding.gameObject.GetComponent<Item>()?.item == rocketItem
                && colliderBuilding.gameObject == gameObject.transform.parent.gameObject)
            {
                isOpen = !isOpen;
                panel.SetActive(true);
                inventory.gameObject.SetActive(true);
                craftPanel.SetActive(false);
                textRecipes.SetActive(false);
                textMelts.SetActive(false);
                foreach (var obj in objectList)
                    obj.SetActive(true);
            }
        }

        if (craftPanel.activeInHierarchy || !inventory.activeInHierarchy)
        {
            foreach (var obj in objectList)
                obj.SetActive(false);
        }
    }
}
