using UnityEngine;

public abstract class EntityUI : MonoBehaviour
{
    [SerializeField] protected Camera _mainCamera;
    public virtual void UpdateText() { }
}