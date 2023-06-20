using RobotEntity;
using UnityEngine;

public class AssemblingInventoryHolder : InventoryHolder
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void PutItem(ItemScriptableObject item, int amount)
    {
        throw new System.NotImplementedException();
    }

    public override RobotItem TakeItem()
    {
        throw new System.NotImplementedException();
    }
}
