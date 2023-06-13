using RobotEntity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RobotUI : MonoBehaviour
{
    private GameObject robotMenu;
    private GameObject inventory;
    
    void Start()
    {
        var canvas = transform.Find("Canvas");
        robotMenu = canvas.Find("RobotMenu").gameObject;
        inventory = canvas.Find("Inventory").gameObject;
        GetComponent<RobotInventory>().ItemUpdate += UpdateInventory;
        UpdateInventory();
    }

    void Update()
    {
        
    }

    public void StartProgram()
    {
        ChangeStateRobotMenu();
        Debug.Log("Script start");
        GetComponent<RobotScriptable>().CancellationToken?.Cancel();
        GetComponent<RobotScriptable>().StartProgram();
    }

    public void StopProgram()
    {
        Debug.Log("Script Stop");
        GetComponent<RobotScriptable>().CancellationToken?.Cancel();
        ChangeStateRobotMenu();
    }

    public void OpenCode()
    {
        Debug.Log("Opencode");
        ChangeStateRobotMenu();
    }

    public void RemoveRobot()
    {
        Debug.Log("Remove robot");
        ChangeStateRobotMenu();
    }
    
    private void OnMouseDown()
    {
        ChangeStateRobotMenu();
    }

    private void UpdateInventory()
    {
        var item = GetComponent<RobotInventory>().Item;
        if (item == null)
        {
            inventory.SetActive(false);
            return;
        }
        
        inventory.SetActive(true);
        inventory.transform.GetChild(0).GetComponent<Image>().sprite = item.Item.icon;
        inventory.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Amount + "";
    }
    
    private void ChangeStateRobotMenu()
    {
        robotMenu.SetActive(!robotMenu.activeSelf);   
    }
}
