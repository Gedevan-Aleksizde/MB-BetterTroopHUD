using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using System;

namespace BetterTroopHUD;

public class BetterTroopHudSettings : AttributeGlobalSettings<BetterTroopHudSettings>
{
    public override string Id => "BetterTroopHUD";
    public override string DisplayName => $"Better Troop HUD {typeof(BetterTroopHudSettings).Assembly.GetName().Version.ToString(3)}";
    public override string FolderName => "BetterTroopHUD";
    public override string FormatType => "json";

    private float _updateRate = 5f;

    [SettingPropertyBool("{=BTHUDMCM001}Show Widget", Order = 1, RequireRestart = false, HintText = "{=BTHUDMCM001Hint}Used to hide the widget without exiting the game.")]
    [SettingPropertyGroup("{=BTHUDMCM000}General")]
    public bool ShowTroopStatsWidget { get; set; } = true;

    [SettingPropertyBool("{=BTHUDMCM002}Show additional Markers", Order = 2, RequireRestart = false, HintText = "{=BTHUDMCM002}Enables additional markers that provides more information about your troops.")]
    [SettingPropertyGroup("{=BTHUDMCM000}General")]
    public bool ShowWidgetMarkers { get; set; } = false;

    [SettingPropertyFloatingInteger("{=BTHUDMCM003}Update Rate", 1.5f, 10f, "0.0 seconds", Order = 3, RequireRestart = false, HintText = "{=BTHUDMCM003Hint}Specifies how often the widget should be updated. Will affect performance if set too low.")]
    [SettingPropertyGroup("{=BTHUDMCM000}General")]
    public float UpdateRate
    {
        get => _updateRate;
        set => _updateRate = (float)Math.Round(value, 1);
    }
}