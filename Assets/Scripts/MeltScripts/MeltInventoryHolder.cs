using RobotEntity;

namespace MeltScripts
{
    public class MeltInventoryHolder : InventoryHolder
    {

        private MeltScript _meltScript;
        
        private void Start()
        {
            _meltScript = GetComponent<MeltScript>();
        }

        public override void PutItem(ItemScriptableObject item, int amount)
        {
            var itemCount = _meltScript.meltSlot.amount + amount;
            _meltScript.meltSlot.SetItem(item, itemCount);
        }

        public override RobotItem TakeItem()
        {
            return new RobotItem(_meltScript.finishSlot.item, _meltScript.finishSlot.amount); 
        }
    }
}