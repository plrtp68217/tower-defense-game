using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private StateManager _placementManager;

    [Header("UI Buttons")]
    [SerializeField]
    private Button _escapeButton;

    [SerializeField]
    private Button _building1Button;

    [SerializeField]
    private Button _building2Button;

    [SerializeField]
    private Button _building3Button;

    [Header("Buildings")]
    [SerializeField]
    private SimplePlacableObject _building1SO;

    [SerializeField]
    private SimplePlacableObject _building2SO;

    [SerializeField]
    private SimplePlacableObject _building3SO;

    private void Awake()
    {
        Debug.Log("UIController Awake");

        _escapeButton.onClick.AddListener(OnEscapeButtonClicked);
        _building1Button.onClick.AddListener(() => OnBuildingSelected(_building1SO));
        _building2Button.onClick.AddListener(() => OnBuildingSelected(_building2SO));
        _building3Button.onClick.AddListener(() => OnBuildingSelected(_building3SO));
    }

    private void OnBuildingSelected(IPlacable so)
    {
        if (_placementManager == null)
        {
            Debug.LogWarning("Missing _placementManager in UIController");
            return;
        }
        if (so == null)
        {
            Debug.LogWarning("Missing so in UIController");
            return;
        }

        Debug.Log($"OnBuildingSelected: {so}");
        _placementManager.SwitchToState<BuildState, BuildStateContext>(
            new BuildStateContext() { Object = so }
        );
    }

    private void OnEscapeButtonClicked()
    {
        Debug.Log("Escape button clicked");
        _placementManager?.SwitchToState<IdleState, IdleStateContext>(new IdleStateContext());
    }
}
