using UnityEngine;

public abstract class UIState : MonoBehaviour
{
    [SerializeField] protected StateManager _stateManager;

    public virtual void OnShow() { }
    public virtual void OnHide() { }

    public void Show()
    {
        OnShow();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        OnHide();
        gameObject.SetActive(false);
    }
}