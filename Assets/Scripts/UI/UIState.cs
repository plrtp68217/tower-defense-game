using UnityEngine;

public abstract class UIState : MonoBehaviour
{
    [SerializeField] protected StateManager _stateManager;

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}