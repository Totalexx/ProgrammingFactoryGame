using System;
using UnityEngine;

namespace RobotEntity
{
    public class RobotInventory : MonoBehaviour
    {
        public event Action ItemUpdate;
        
        public int MaxAmount { get; } = 5;
        
        private RobotItem _item;
        
        public RobotItem Item
        {
            get => _item;
            set
            {
                _item = value;
                if (_item.Amount == 0)
                    _item = null;
                ItemUpdate?.Invoke();
            }
        }

        private void Start()
        {
            ItemUpdate?.Invoke();
        }

        public void AddItem(RobotItem item)
        {
            if (Item == null)
            {
                if (item.Amount < 0 || item.Amount > MaxAmount)
                    throw new ArgumentException("Невозможно добавить столько предметов в инвентарь");

                Item = item;
                return;
            }
            
            if (!Item.Item.ItemName.Equals(item.Item.ItemName))
                throw new ArgumentException("В инвентаре другой тип предмета");

            if (Item.Amount + item.Amount > MaxAmount)
                throw new ArgumentException("Невозможно добавить столько предметов в инвентарь");
            
            Item.Item = item.Item;
            Item.Amount += item.Amount;
            ItemUpdate?.Invoke();
        }
    }

    public class RobotItem
    {
        public ItemScriptableObject Item;
        public int Amount;

        public RobotItem(ItemScriptableObject item, int amount)
        {
            Item = item;
            Amount = amount;
        }
    }
}