using System;
using UnityEngine;
using UnityEngine.UI;

public class IdleUI : UIState
{
    [SerializeField] 
    private Button _fightButton;

    [SerializeField] 
    private Button _buildButton;

    private void Awake()
    {
        Debug.Log("IdleUI Awake");

        _fightButton.onClick.AddListener(OnFightButtonClicked);
        _buildButton.onClick.AddListener(OnBuildButtonClicked);
    }
    private void OnFightButtonClicked()
    {
        Debug.Log("IdleUI: OnFightButtonClicked");
        _stateManager.SwitchToState<FightState, FightStateContext>();
    }

    private void OnBuildButtonClicked()
    {
        Debug.Log("IdleUI: OnBuildButtonClicked");
        _stateManager.SwitchToState<BuildState, BuildStateContext>();
    }
}