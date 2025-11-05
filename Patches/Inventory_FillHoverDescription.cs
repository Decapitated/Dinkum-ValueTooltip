using HarmonyLib;

namespace ValueTooltipMod.Patches
{
    [HarmonyPatch(typeof(Inventory), "fillHoverDescription")]
    internal class Inventory_FillHoverDescription
    {
        static void Postfix(ref InventorySlot rollOverSlot)
        {
            int value = DivineDinkum.Utilities.GetSlotValue(rollOverSlot);
            Core.Instance.valueText.text = $"<size=80%><sprite=11> <size=100%>{value}";
        }
    }
}
