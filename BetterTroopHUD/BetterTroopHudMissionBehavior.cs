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
        DisplayDebugMessage("[DEBUG] EarlyStart: called");
        
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

    // public override void OnMissionScreenInitialize()
    // {
    //     base.OnMissionScreenInitialize();
    //     
    //     DisplayDebugMessage("[DEBUG] OnMissionScreenInitialize: called");
    //     
    //     // todo Implement _isInDeployment
    //     // this._isInDeployement = base.Mission.GetMissionBehavior<BattleDeploymentHandler>() != null;
    //     // if (this._isInDeployement)
    //     // {
    //     //     this._deploymentMissionView = base.Mission.GetMissionBehavior<DeploymentMissionView>();
    //     //     if (this._deploymentMissionView != null)
    //     //     {
    //     //         DeploymentMissionView deploymentMissionView = this._deploymentMissionView;
    //     //         deploymentMissionView.OnDeploymentFinish = (OnPlayerDeploymentFinishDelegate)Delegate.Combine(deploymentMissionView.OnDeploymentFinish, new OnPlayerDeploymentFinishDelegate(this.OnDeploymentFinish));
    //     //     }
    //     // }
    // }
    
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

    // private void OnDeploymentFinish()
    // {
    //     this._isInDeployement = false;
    //     DeploymentMissionView deploymentMissionView = this._deploymentMissionView;
    //     deploymentMissionView.OnDeploymentFinish = (OnPlayerDeploymentFinishDelegate)Delegate.Remove(deploymentMissionView.OnDeploymentFinish, new OnPlayerDeploymentFinishDelegate(this.OnDeploymentFinish));
    // }
    
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