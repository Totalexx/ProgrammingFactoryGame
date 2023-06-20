using RobotEntity;
using UnityEngine;

public abstract class InventoryHolder : MonoBehaviour
{

    public abstract void PutItem(ItemScriptableObject item, int amount);

    public abstract RobotItem TakeItem();
}
