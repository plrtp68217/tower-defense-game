using TMPro;
using UnityEngine;

public class TowerUI: EntityUI
{
    [SerializeField] private Tower _tower;
    [SerializeField] private GameObject _text;

    private TextMeshProUGUI _textMeshPro;

    private readonly FontStyles _fontStyle = FontStyles.Bold | FontStyles.UpperCase;
    private readonly int _fontSize = 36;
    private readonly Color _fontColor = Color.black;

    public override void UpdateText()
    {
        _textMeshPro.text = "level: " + _tower.TowerData.Level;
    }

    private void Awake()
    {
        _textMeshPro = _text.GetComponent<TextMeshProUGUI>();
        _textMeshPro.fontStyle = _fontStyle;
        _textMeshPro.fontSize = _fontSize;
        _textMeshPro.color = _fontColor;
    }

    private void Start()
    {
        Quaternion rotation = _mainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    private void Update()
    {
        UpdateText();
    }
}