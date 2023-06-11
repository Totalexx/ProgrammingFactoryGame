using System;
using System.Collections;
using System.Collections.Generic;
using RobotEntity;
using UnityEngine;

public class RobotUI : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartProgram()
    {
        Debug.Log("Script start");
        GetComponent<RobotScriptable>().StartProgram();
    }

    public void StopProgram()
    {
        Debug.Log("Script Stop");
    }

    public void OpenCode()
    {
        Debug.Log("Opencode");
    }

    public void RemoveRobot()
    {
        Debug.Log("Remove robot");
    }
    
    private void OnMouseDown()
    {
        var robotMenu = transform.Find("Canvas").Find("RobotMenu").gameObject;
        robotMenu.SetActive(!robotMenu.activeSelf);
    }
}
