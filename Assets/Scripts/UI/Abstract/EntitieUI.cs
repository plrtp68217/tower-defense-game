using UnityEngine;

public abstract class EntitieUI : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    public virtual void UpdateText() { }
}