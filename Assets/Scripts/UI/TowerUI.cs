using TMPro;
using UnityEngine;

public class TowerUI: EntityUI
{
    private Tower _tower;
    private TextMeshPro _textMeshPro;
    public override void UpdateText()
    {
        _textMeshPro.text = "LEVEL: " + _tower.TowerData.Level;
    }
    private void Start()
    {
        _tower = gameObject.GetComponentInParent<Tower>();

        _textMeshPro = gameObject.GetComponentInChildren<TextMeshPro>();
        _textMeshPro.text = "LEVEL: " + _tower.TowerData.Level;
    }
    private void Update()
    {
        Quaternion rotation = _mainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
}