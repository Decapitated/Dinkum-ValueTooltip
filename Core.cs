using MelonLoader;

[assembly: MelonInfo(typeof(ValueTooltipMod.Core), "ValueTooltipMod", "1.0.0", "brett", null)]
[assembly: MelonGame("James Bendon", "Dinkum")]

namespace ValueTooltipMod
{
    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
        }
    }
}