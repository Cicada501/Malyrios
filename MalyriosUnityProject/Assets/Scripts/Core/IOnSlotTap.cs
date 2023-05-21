
namespace Malyrios.Core
{
    public interface IOnSlotTap
    {
        void OnTap(); //if its a Equipment slot it calls the OnTap function of the EquippmentSlot, and if Inventory Slot, it calls the OnTap of the Inventory Slot

    }
}