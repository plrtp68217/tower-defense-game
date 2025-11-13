using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : TowerEntityBase, IPreviewable
{
    private readonly List<Renderer> _previewObjectRenderers = new();
    private readonly Dictionary<Renderer, Material> _originalMaterials = new();

    private readonly List<Tower> _targetTowers = new();

    private Color _validPreviewColor = Color.white;
    private Color _inValidPreviewColor = Color.red;

    private UnitDistributor _distributor;

    private void Start()
    {
        _distributor = new(_targetTowers);
        StartCoroutine(SpawnUnits());
    }

    private IEnumerator SpawnUnits(float spawnInterval = 1f)
    {

        foreach (var nextTarget in _distributor.NextTarget())
        {
            if (nextTarget == null)
            {
                yield return new WaitForSeconds(spawnInterval);
                continue;
            }

            var gameObject = Instantiate(TowerData.UnitPrefab, Center, Quaternion.identity);

            if (gameObject.TryGetComponent(out Unit unit))
            {
                unit.UnitData.Team = TowerData.Team;
                unit.SetTargetTower(nextTarget);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void AddTarget(Tower target)
    {
        if (_targetTowers.Contains(target) == false)
        {
            _targetTowers.Add(target);
        }
    }

    public void RemoveTarget(Tower target)
    {
        if (_targetTowers.Contains(target) == true)
        {
            _targetTowers.Remove(target);
        }
    }

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

            renderer.material = TowerData.PreviewMaterial;
            renderer.material.color = _validPreviewColor;
        }
    }

    public void SetPreviewValidity(bool isValid)
    {
        foreach (var renderer in _previewObjectRenderers)
            renderer.material.color = isValid ? _validPreviewColor : _inValidPreviewColor;
    }
}