using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonScript : MonoBehaviour
{
    public Transform canvasTasks;
    // Start is called before the first frame update
    void Start()
    {
        canvasTasks.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePanel()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        canvasTasks.gameObject.SetActive(true);
    }
}
