using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.GauntletUI.Mission;
using TaleWorlds.ScreenSystem;
using static BetterTroopHUD.Utils;

namespace BetterTroopHUD;

public class BetterTroopHudMissionBehavior : MissionGauntletBattleUIBase
{
    private GauntletLayer? _gauntletLayer;
    private BetterTroopHudVM? _dataSource;

    public override void EarlyStart()
    {
        base.EarlyStart();
        DisplayDebugMessage(GameTexts.FindText("BTHUD_debug100").ToString());
        
        _dataSource = new BetterTroopHudVM(Mission);
        _gauntletLayer = new GauntletLayer(1);
        _gauntletLayer.LoadMovie("BetterTroopHUD", _dataSource);
        ScreenManager.TopScreen.AddLayer(_gauntletLayer);
    }

    public override void AfterStart()
    {
        base.AfterStart();
        
        DisplayDebugMessage("[DEBUG] AfterStart: called");
        
        _dataSource?.Initialize();
    }
    
    protected override void OnCreateView() => _dataSource.IsAgentStatusAvailable = true;
    protected override void OnDestroyView() => _dataSource.IsAgentStatusAvailable = false;
    public override void OnMissionScreenFinalize()
    {
        base.OnMissionScreenFinalize();
        
        DisplayDebugMessage("[DEBUG] OnMissionScreenFinalize: called");
        
        // Clean up
        ScreenManager.TopScreen.RemoveLayer(_gauntletLayer);
        _gauntletLayer = null;
        _dataSource?.OnFinalize();
        _dataSource = null;
    }    
    public override void OnMissionModeChange(MissionMode oldMissionMode, bool atStart)
    {
        base.OnMissionModeChange(oldMissionMode, atStart);
        _dataSource?.OnMissionModeChange(oldMissionMode, atStart);
    }

    public override void OnMissionScreenTick(float dt)
    {
        base.OnMissionScreenTick(dt);
        
        // _dataSource?.IsInDeployment = _isInDeployment; // todo
        _dataSource?.Tick(dt);
    }

    public override void OnPhotoModeActivated()
    {
        base.OnPhotoModeActivated();
        
        // Hide UI
        _gauntletLayer.UIContext.ContextAlpha = 0f;
    }

    public override void OnPhotoModeDeactivated()
    {
        base.OnPhotoModeDeactivated();
        
        // Un-hide UI
        _gauntletLayer.UIContext.ContextAlpha = 1f;
    }
}