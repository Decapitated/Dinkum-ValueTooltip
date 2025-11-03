using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static MelonLoader.MelonLogger;

namespace ValueTooltipMod.Patches
{
    [HarmonyLib.HarmonyPatch]
    static internal class PatchedInventory
    {
        [HarmonyPatch(typeof(Inventory), "fillHoverDescription")]
        [HarmonyPostfix]
        private static void fillHoverDescription(ref Inventory __instance, ref InventorySlot rollOverSlot)
        {
            InventoryItem itemInSlot = rollOverSlot.itemInSlot;
            string description = GetDescription(itemInSlot);
            int num = CalculateValues(rollOverSlot);
            if (num != -1)
            {
                __instance.InvDescriptionText.text = description + GetDisplayString(num);
                num = 0;
            }
        }

        public static int CalculateValues(InventorySlot slot)
        {
            GiveNPC give = GiveNPC.give;
            int num = 0;
            InventoryItem itemInSlot = slot.itemInSlot;
            if (itemInSlot.isDeed || itemInSlot.getItemId() == Inventory.Instance.moneyItem.getItemId())
            {
                return -1;
            }
            num = ((itemInSlot.isStackable && !itemInSlot.isATool && !itemInSlot.isPowerTool && !itemInSlot.hasFuel) ? (itemInSlot.value * slot.stack) : itemInSlot.value);
            if (!slot.isDisabledForGive() && give.giveWindowOpen)
            {
                switch (give.giveMenuTypeOpen)
                {
                    case GiveNPC.currentlyGivingTo.SellToTuckshop:
                    case GiveNPC.currentlyGivingTo.SellToBugComp:
                    case GiveNPC.currentlyGivingTo.SellToFishingComp:
                        num = Mathf.RoundToInt((float)num * 2.5f);
                        break;
                    case GiveNPC.currentlyGivingTo.SellToTrapper:
                        num = Mathf.RoundToInt((float)num * 2f);
                        break;
                    case GiveNPC.currentlyGivingTo.SellToJimmy:
                        num = Mathf.RoundToInt((float)num * 1.5f);
                        break;
                    case GiveNPC.currentlyGivingTo.Tech:
                        num = Mathf.RoundToInt((float)num * 6f);
                        break;
                    case GiveNPC.currentlyGivingTo.Sell:
                        if ((bool)itemInSlot.relic)
                        {
                            num /= 4;
                        }
                        break;
                }
            }
            num += Mathf.RoundToInt((float)num / 20f * (float)LicenceManager.manage.allLicences[8].getCurrentLevel());
            return num;
        }

        public static string GetDescription(InventoryItem item)
        {
            Inventory instance = Inventory.Instance;
            return item.getItemDescription(instance.getInvItemId(item));
        }

        public static string GetDisplayString(int value)
        {
            return $"\n <sprite=11>{value}";
        }
    }
}
