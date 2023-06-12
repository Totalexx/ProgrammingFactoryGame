using System;
using System.Collections;
using System.Collections.Generic;
using RobotEntity;
using UnityEngine;

public class RobotUI : MonoBehaviour
{

    private GameObject robotMenu;
    void Start()
    {
        robotMenu = transform.Find("Canvas").Find("RobotMenu").gameObject;
    }

    void Update()
    {
        
    }

    public void StartProgram()
    {
        ChangeStateRobotMenu();
        Debug.Log("Script start");
        GetComponent<RobotScriptable>().StartProgram();
    }

    public void StopProgram()
    {
        Debug.Log("Script Stop");
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

    private void ChangeStateRobotMenu()
    {
        robotMenu.SetActive(!robotMenu.activeSelf);   
    }
}
