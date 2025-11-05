using MelonLoader;
using System.Collections;
using TMPro;
using UnityEngine;

[assembly: MelonInfo(typeof(ValueTooltipMod.Core), "ValueTooltipMod", "1.0.0", "Decapitated", null)]
[assembly: MelonGame("James Bendon", "Dinkum")]

namespace ValueTooltipMod
{
    public class Core : MelonMod
    {
        static public Core Instance;

        internal TextMeshProUGUI valueText;

        public override void OnInitializeMelon()
        {
            Instance = this;
            DivineDinkum.Core.Instance.OnSceneReady.AddListener(OnSceneReady);
        }

        private void OnSceneReady()
        {
            MelonCoroutines.Start(Setup());
        }

        private IEnumerator Setup()
        {
            var textObj = new GameObject()
            {
                name = "Value"
            };

            valueText = textObj.AddComponent<TextMeshProUGUI>();
            valueText.fontSize = 18;
            valueText.color = new Color(0.4057f, 0.3395f, 0.2392f);

            var rect = textObj.GetComponent<RectTransform>();
            rect.pivot = Vector2.zero;

            var invDesc = DivineDinkum.Core.Instance.InventoryDescriptions;
            textObj.transform.SetParent(invDesc.transform);
            textObj.SetActive(true);
            return null;
        }
    }
}