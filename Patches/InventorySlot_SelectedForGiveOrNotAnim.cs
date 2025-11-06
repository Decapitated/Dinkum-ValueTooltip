using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.Rendering.DebugUI;

namespace ValueTooltipMod.Patches
{
    [HarmonyPatch(typeof(InventorySlot), "selectedForGiveOrNotAnim")]
    internal class InventorySlot_SelectedForGiveOrNotAnim
    {
        static void Postfix()
        {
            var value = DivineDinkum.Utilities.GetSellTotalValue();
            Core.Instance.sellValueText.text = $"<size=80%><sprite=11> <size=100%>{value}";
        }
    }
}
