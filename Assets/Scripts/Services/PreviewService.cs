using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PreviewService : MonoBehaviour
{
    [SerializeField] private BuildingService _buildingService;
    [SerializeField] private Color _validPreviewColor = new(0f, 1f, 0f, 0.3f);
    [SerializeField] private Color _invalidPreviewColor = new(1f, 0f, 0f, 0.3f);
    [SerializeField] private Material _previewMaterial;

    private GameObject _previewObject;
    private readonly IList<Renderer> _previewObjectRenderers = new List<Renderer>();
    private readonly IList<GameObject> _occupiedCellsVisuals = new List<GameObject>();
    private IPlacable _currentPlacable;

    /// <summary>
    /// Отображает предварительный просмотр объекта.
    /// </summary>
    /// <param name="placable">Объект для предпросмотра.</param>
    public void ShowPreview(IPlacable placable)
    {
        if (placable == null)
            return;

        _currentPlacable = placable;

        // Создаём preview-объект
        if (_previewObject == null)
        {
            _previewObject = Instantiate(placable.Prefab);
            _previewObjectRenderers.AddRange(_previewObject.GetComponentsInChildren<Renderer>());

            foreach (var renderer in _previewObjectRenderers)
            {
                renderer.material = _previewMaterial;
                renderer.material.color = _validPreviewColor;
            }
        }

        // Обновляем позицию
        UpdatePreviewPosition();

    }

    /// <summary>
    /// Обновляет позицию preview-объекта.
    /// </summary>
    public void UpdatePreviewPosition()
    {
        if (_previewObject == null || _currentPlacable == null)
            return;

        var worldPos = _buildingService.CellToWorld(_currentPlacable.Position);
        _previewObject.transform.position = worldPos;

        var canPlace = _buildingService.CanPlace(_currentPlacable);
        var color = canPlace ? _validPreviewColor : _invalidPreviewColor;

        foreach (var renderer in _previewObjectRenderers)
            renderer.material.color = color;

    }

    /// <summary>
    /// Скрывает предварительный просмотр.
    /// </summary>
    public void HidePreview()
    {
        if (_previewObject != null)
        {
            Destroy(_previewObject);
            _previewObject = null;
        }

        foreach (var cellVisual in _occupiedCellsVisuals)
            Destroy(cellVisual);

        _occupiedCellsVisuals.Clear();
        _previewObjectRenderers.Clear();

        _currentPlacable = null;
    }

    private void OnDestroy()
    {
        HidePreview();
    }
}
