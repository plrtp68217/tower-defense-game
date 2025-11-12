using System;
using UnityEngine;
using UnityEngine.UI;

public class IdleUI : UIState
{
    [SerializeField] 
    private Button _buildButton;

    [SerializeField] private GameObject _smallTowerPrefab;


    private void Awake()
    {
        Debug.Log("IdleUI Awake");

        _buildButton.onClick.AddListener(() => OnBuildButtonClicked(_smallTowerPrefab));
    }

    private void OnBuildButtonClicked(GameObject prefab)
    {
        Debug.Log("IdleUI: OnBuildButtonClicked");
        _stateManager.SwitchToState<BuildState, BuildStateContext>(new() { Prefab = prefab });
    }
}