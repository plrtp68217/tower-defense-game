using System.Collections.Generic;
using UnityEngine;

public class Tower : TowerEntityBase, IPreviewable
{
    [field: SerializeField] public TowerData Data { get; set; }

    public Team Team { get; set; }
    public string Name { get; set; }

    private Tower() {}
    public static Tower FromData(TowerData td) => new() 
    {
        Instance            = Object.Instantiate(td.Prefab),
        Data                = td,
        Prefab              = td.Prefab,
        Name                = td.EntityName,
        Size                = td.Size,
        Health              = td.MaxHealth,
        Team                = td.EntityTeam,

        _previewMaterial    = td.PreviewMaterial,
    };

    private readonly List<Renderer> _previewObjectRenderers             = new();
    private readonly Dictionary<Renderer, Material> _originalMaterials  = new();

    private Material _previewMaterial;
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

        _previewObjectRenderers.AddRange(Instance.GetComponentsInChildren<Renderer>());

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