using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildUI : UIState
{
    [Header("UI Buttons")]
    [SerializeField]
    private Button _building1Button;

    [SerializeField]
    private Button _building2Button;

    [SerializeField]
    private Button _building3Button;

    [SerializeField]
    private Button _idleButton;

    [SerializeField]
    private Button _fightButton;

    [Header("Buildings")]
    [SerializeField]
    private TowerData _building1SO;

    [SerializeField]
    private TowerData _building2SO;

    [SerializeField]
    private TowerData _building3SO;

    private void Awake()
    {
        Debug.Log("BuildUI Awake");

        _building1Button.onClick.AddListener(() => OnBuildingSelected(_building1SO));
        _building2Button.onClick.AddListener(() => OnBuildingSelected(_building2SO));
        _building3Button.onClick.AddListener(() => OnBuildingSelected(_building3SO));

        _idleButton.onClick.AddListener(OnIdleButtonClicked);
        _fightButton.onClick.AddListener(OnFightButtonClicked);
    }

    private void OnFightButtonClicked()
    {
        Debug.Log("BuildUI: OnFightButtonClicked");
        _stateManager.SwitchToState<FightState, FightStateContext>();
    }

    private void OnIdleButtonClicked()
    {
        _stateManager.SwitchToState<IdleState, IdleStateContext>();
    }

    private void OnBuildingSelected(TowerData so)
    {
        _stateManager.SwitchToState<IdleState, IdleStateContext>();
        Debug.Log($"UI:{so.Prefab}");
        _stateManager.SwitchToState<BuildState, BuildStateContext>(new() { Data = so });
    }
}