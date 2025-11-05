using UnityEngine;

public abstract class UIState : MonoBehaviour
{
    [SerializeField] protected StateManager _stateManager;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}