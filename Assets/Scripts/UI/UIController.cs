using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private StateManager _stateManager;

    [Header("UI States")]
    [SerializeField] private IdleUI _idleUI;
    [SerializeField] private BuildUI _buildUI;
    [SerializeField] private FightUI _fightUI;

    private UIState _currentUI;

    private void Awake()
    {
        _idleUI.Hide();
        _buildUI.Hide();
        _fightUI.Hide();

        _stateManager.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(Type stateType)
    {
        if (_currentUI != null)
        {
            _currentUI.Hide();
        }

        _currentUI = stateType switch
        {
            Type t when t == typeof(IdleState) => _idleUI,
            Type t when t == typeof(BuildState) => _buildUI,
            Type t when t == typeof(FightState) => _fightUI,
            _ => null
        };

        if (_currentUI != null)
        {
            _currentUI.Show();
        }
    }
}