using DivineDinkum;
using MelonLoader;
using System.Collections;
using System.Xml.Linq;
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
        static internal MelonLogger.Instance Logger;

        internal TextMeshProUGUI valueText;

        private AssetBundle assetBundle;
        private GameObject valuePrefab;

        public override void OnInitializeMelon()
        {
            Instance = this;
            DivineDinkum.Core.Instance.OnSceneReady.AddListener(OnSceneReady);
        }

        private void OnSceneReady()
        {
            MelonCoroutines.Start(Setup());
        }

        //private IEnumerator LoadAssets(Action success)
        //{
        //    yield return Utilities.LoadAssetBundle("valuetooltip", (bundle) =>
        //    {
        //        assetBundle = bundle;
        //    });

        //    if (assetBundle == null)
        //    {
        //        LoggerInstance.Error("Failed to load AssetBundle.");
        //        yield break;
        //    }

        //    yield return Utilities.LoadAsset<GameObject>(assetBundle, "Details", (gameObject) =>
        //    {
        //        valuePrefab = gameObject;
        //    });

        //    if (valuePrefab == null)
        //    {
        //        LoggerInstance.Error("Failed to load Details asset.");
        //        yield break;
        //    }
        //    success();
        //}

        private IEnumerator Setup()
        {
            var layoutObj = new GameObject()
            {
                name = "Value Details"
            };
            var layout = layoutObj.AddComponent<HorizontalLayoutGroup>();
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandHeight = true;
            layout.childForceExpandWidth = true;
            layout.childAlignment = TextAnchor.MiddleLeft;

            var sprites = Resources.FindObjectsOfTypeAll<Sprite>();
            var sprite = Array.Find(sprites, (s) => s.name == "smallCurveUI");

            var image = layoutObj.AddComponent<Image>();
            image.sprite = sprite;
            image.type = Image.Type.Sliced;
            image.color = new Color(255f, 215f, 0f);

            var textObj = new GameObject()
            {
                name = "Text"
            };

            valueText = textObj.AddComponent<TextMeshProUGUI>();
            valueText.text = "<sprite=11>999999999";
            valueText.fontSize = 24;
            valueText.color = new Color(0.4057f, 0.3395f, 0.2392f);
            textObj.transform.SetParent(layout.transform);

            var invDesc = DivineDinkum.Core.Instance.InventoryDescriptions;
            yield return Utilities.WaitForGameObject(invDesc);
            layoutObj.transform.SetParent(invDesc.transform);
            layoutObj.SetActive(true);
        }
    }
}