using System.Collections.Generic;
using UnityEngine;

public class Tower : TowerEntityBase, IPreviewable
{
    private readonly List<Renderer> _previewObjectRenderers             = new();
    private readonly Dictionary<Renderer, Material> _originalMaterials  = new();
    private readonly Material _previewMaterial;

    private Color _validPreviewColor    = Color.white;
    private Color _inValidPreviewColor  = Color.red;

    public void RestoreMaterial()
    {
        foreach (var renderer in _previewObjectRenderers)
            if (_originalMaterials.TryGetValue(renderer, out var originalMaterial))
                renderer.material = originalMaterial;

        _originalMaterials.Clear();
    }
    public void SetPreviewMaterial()
    {

        if (_originalMaterials.Count != 0) return;

        _previewObjectRenderers.Clear();
        _originalMaterials.Clear();

        _previewObjectRenderers.AddRange(gameObject.GetComponentsInChildren<Renderer>());

        foreach (var renderer in _previewObjectRenderers)
        {
            _originalMaterials[renderer] = renderer.material;

            renderer.material = _previewMaterial;
            renderer.material.color = _validPreviewColor;
        }
    }

    public void SetPreviewValidity(bool isValid)
    {
        foreach (var renderer in _previewObjectRenderers)
            renderer.material.color = isValid ? _validPreviewColor : _inValidPreviewColor;
    }


}