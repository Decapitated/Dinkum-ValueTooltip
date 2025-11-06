using MelonLoader;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[assembly: MelonInfo(typeof(ValueTooltipMod.Core), "ValueTooltipMod", "1.0.0", "Decapitated", null)]
[assembly: MelonGame("James Bendon", "Dinkum")]

namespace ValueTooltipMod
{
    public class Core : MelonMod
    {
        static public Core Instance;

        internal TextMeshProUGUI valueText;
        internal TextMeshProUGUI sellValueText;

        public override void OnInitializeMelon()
        {
            Instance = this;
            DivineDinkum.Core.Instance.OnSceneReady.Subscribe(() => MelonCoroutines.Start(Setup()));
        }

        private IEnumerator Setup()
        {
            MelonCoroutines.Start(SetupTooltip());
            MelonCoroutines.Start(SetupSellValue());
            yield break;
        }

        private IEnumerator SetupTooltip()
        {
            #region Root
            var layoutObj = new GameObject()
            {
                name = "Value"
            };

            var layout = layoutObj.AddComponent<HorizontalLayoutGroup>();
            layout.childControlWidth = false;            // Let children define their own width
            layout.childControlHeight = true;            // Let layout group control height to keep things aligned
            layout.childForceExpandWidth = true;         // Stretch children horizontally to fill available space
            layout.childForceExpandHeight = false;       // Don’t force vertical stretching

            var layoutRect = layoutObj.GetComponent<RectTransform>();
            layoutRect.anchorMin = new Vector2(0, 1);   // Anchor to top-left corner horizontally (0) and top vertically (1)
            layoutRect.anchorMax = new Vector2(1, 1);   // Stretch horizontally across the parent width
            layoutRect.pivot = new Vector2(0.0f, 1);    // Pivot at the top-center
            layoutRect.anchoredPosition = Vector2.zero; // Position relative to parent (top)
            layoutRect.sizeDelta = new Vector2(0, 30);  // Explicit height (30 units). Width = 0 lets it stretch with anchors
            #endregion
            #region Text
            var textObj = new GameObject()
            {
                name = "Text"
            };

            valueText = textObj.AddComponent<TextMeshProUGUI>();
            valueText.fontSize = 24;                                       // Font size
            valueText.color = new Color(0.4057f, 0.3395f, 0.2392f);        // Text color
            valueText.text = "<size=80%><sprite=11> <size=100%>999999999"; // Set sample text so TMP can calculate preferred size
            valueText.textWrappingMode = TextWrappingModes.NoWrap;         // Prevent multi-line wrapping for now

            var textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0, 0);     // Anchor to bottom-left of parent
            textRect.anchorMax = new Vector2(1, 1);     // Stretch to fill vertically and horizontally
            textRect.pivot = new Vector2(0, 0.5f);      // Pivot on the left-middle for natural text alignment
            textRect.anchoredPosition = Vector2.zero;   // No offset from parent

            var layoutElement = textObj.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24;

            textObj.transform.SetParent(layoutObj.transform);
            #endregion

            var invDesc = DivineDinkum.CursorCanvas.Instance.InventoryDescriptions;
            layoutObj.transform.SetParent(invDesc.transform);
            layoutObj.SetActive(true);

            yield break;
        }
        
        private IEnumerator SetupSellValue()
        {
            #region Root
            var layoutObj = new GameObject()
            {
                name = "SellValue"
            };

            var layout = layoutObj.AddComponent<HorizontalLayoutGroup>();
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;
            layout.childAlignment = TextAnchor.MiddleCenter;

            var layoutRect = layoutObj.GetComponent<RectTransform>();
            layoutRect.pivot = new Vector2(0.5f, 1f);
            layoutRect.anchorMin = new Vector2(0.5f, 0f);
            layoutRect.anchorMax = new Vector2(0.5f, 0f);
            layoutRect.sizeDelta = new Vector2(500f, 50f);
            layoutRect.anchoredPosition = Vector2.zero;

            var giveWindow = DivineDinkum.Canvas.Instance.GiveNPCWindow;
            layoutObj.transform.SetParent(giveWindow.transform, false);
            #endregion
            #region Text
            var textObj = new GameObject()
            {
                name = "Text"
            };

            sellValueText = textObj.AddComponent<TextMeshProUGUI>();
            sellValueText.fontSize = 32;
            sellValueText.color = Color.white;
            sellValueText.text = "<size=80%><sprite=11> <size=100%>0";
            sellValueText.textWrappingMode = TextWrappingModes.NoWrap;

            var textRect = textObj.GetComponent<RectTransform>();
            textRect.pivot = new Vector2(0.5f, 0.5f);

            textObj.transform.SetParent(layoutObj.transform);
            #endregion

            yield break;
        }
    }
}