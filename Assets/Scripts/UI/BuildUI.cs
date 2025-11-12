using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildUI : UIState
{
    [Header("UI Buttons")]
    [SerializeField]
    private Button _idleButton;

    private void Awake()
    {
        Debug.Log("BuildUI Awake");


        _idleButton.onClick.AddListener(OnIdleButtonClicked);
    }

    private void OnIdleButtonClicked()
    {
        Debug.Log("BuildUI: OnIdleButtonClicked");
        _stateManager.SwitchToState<IdleState, IdleStateContext>();
    }
}