using System;
using UnityEngine;
using UnityEngine.UI;

public class FightUI : UIState
{
    [SerializeField]
    private Button _idleButton;

    [SerializeField]
    private Button _buildButton;

    private void Awake()
    {
        Debug.Log("FightUI Awake");

        _idleButton.onClick.AddListener(OnIdleButtonClicked);
        _buildButton.onClick.AddListener(OnBuildButtonClicked);
    }
    private void OnIdleButtonClicked()
    {
        Debug.Log("FightUI: OnIdleButtonClicked");
        _stateManager.SwitchToState<IdleState, IdleStateContext>();
    }

    private void OnBuildButtonClicked()
    {
        Debug.Log("FightUI: OnBuildButtonClicked");
        _stateManager.SwitchToState<BuildState, BuildStateContext>();
    }
}
