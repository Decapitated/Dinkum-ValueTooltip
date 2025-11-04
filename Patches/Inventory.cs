using HarmonyLib;
using UnityEngine;
using static GiveNPC;

namespace ValueTooltipMod.Patches
{
    [HarmonyLib.HarmonyPatch]
    static internal class PatchedInventory
    {
        [HarmonyPatch(typeof(Inventory), "fillHoverDescription")]
        [HarmonyPostfix]
        private static void FillHoverDescription(ref InventorySlot rollOverSlot)
        {
            int value = CalculateValues(rollOverSlot);
            Core.Instance.valueText.text = $"<sprite=11>{value}";
        }

        // Altered version of GiveNPC.getDollarValueOfGiveSlots()
        public static int CalculateValues(InventorySlot inventorySlot)
        {
            int moneyOffer = 0;
            if (inventorySlot.itemNo == -1)
            {
                return -1;
            }
            if (inventorySlot.itemInSlot.hasFuel || inventorySlot.itemInSlot.hasColourVariation)
            {
                moneyOffer += inventorySlot.itemInSlot.value;
            }
            else if (inventorySlot.getGiveAmount() == 0)
            {
                if (GiveNPC.give.giveMenuTypeOpen == currentlyGivingTo.Sell && (bool)inventorySlot.itemInSlot.relic)
                {
                    moneyOffer += inventorySlot.itemInSlot.value * inventorySlot.stack / 4;
                }
                else
                {
                    moneyOffer += inventorySlot.itemInSlot.value * inventorySlot.stack;
                }
            }
            else if (GiveNPC.give.giveMenuTypeOpen == currentlyGivingTo.Sell && (bool)inventorySlot.itemInSlot.relic)
            {
                moneyOffer += inventorySlot.itemInSlot.value * inventorySlot.getGiveAmount() / 4;
            }
            else
            {
                moneyOffer += inventorySlot.itemInSlot.value * inventorySlot.getGiveAmount();
            }
            moneyOffer += Mathf.RoundToInt((float)moneyOffer / 20f * (float)LicenceManager.manage.allLicences[8].getCurrentLevel());
            if (GiveNPC.give.giveMenuTypeOpen == currentlyGivingTo.SellToBugComp)
            {
                moneyOffer = Mathf.RoundToInt((float)moneyOffer * 2.5f);
            }
            else if (GiveNPC.give.giveMenuTypeOpen == currentlyGivingTo.SellToFishingComp)
            {
                moneyOffer = Mathf.RoundToInt((float)moneyOffer * 2.5f);
            }
            else if (GiveNPC.give.giveMenuTypeOpen == currentlyGivingTo.SellToTuckshop)
            {
                moneyOffer = Mathf.RoundToInt((float)moneyOffer * 2.5f);
            }
            else if (GiveNPC.give.giveMenuTypeOpen == currentlyGivingTo.SellToTrapper)
            {
                moneyOffer = Mathf.RoundToInt((float)moneyOffer * 2f);
            }
            else if (GiveNPC.give.giveMenuTypeOpen == currentlyGivingTo.SellToJimmy)
            {
                moneyOffer = Mathf.RoundToInt((float)moneyOffer * 1.5f);
            }
            else if (GiveNPC.give.giveMenuTypeOpen == currentlyGivingTo.Tech)
            {
                moneyOffer = Mathf.RoundToInt((float)moneyOffer * 6f);
            }
            if (moneyOffer <= -1)
            {
                moneyOffer *= -1;
            }
            if (moneyOffer > BankMenu.billion)
            {
                moneyOffer = BankMenu.billion;
            }
            return moneyOffer;
        }
    }
}
