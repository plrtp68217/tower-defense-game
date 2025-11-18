using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : TowerEntityBase, IPreviewable, IDamagable
{
    private readonly List<Renderer> _previewObjectRenderers = new();
    private readonly Dictionary<Renderer, Material> _originalMaterials = new();

    private readonly List<Tower> _targetTowers = new();

    private Color _validPreviewColor = Color.white;
    private Color _inValidPreviewColor = Color.red;

    private UnitDistributor _distributor;

    private Team _team;
    private int _level;
    private int _unitsCount;

    public Team Team
    {
        get => _team;
        set
        {   
            _team = value;
            OnTeamChanged();
        }
    }

    public int Level { get => _level; set => _level = value; }
    public int UnitsCount { get => _unitsCount; set => _unitsCount = value; }


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

            var gameObject = Instantiate(_data.UnitPrefab, Center, Quaternion.identity);

            if (gameObject.TryGetComponent(out Unit unit))
            {
                unit.Team = Team;
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
        {
            if (_originalMaterials.TryGetValue(renderer, out var originalMaterial))
            {
                renderer.material = originalMaterial;
            }
        }

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

            renderer.material = _data.PreviewMaterial;
            renderer.material.color = _validPreviewColor;
        }
    }

    public void SetPreviewValidity(bool isValid)
    {
        foreach (var renderer in _previewObjectRenderers) 
        {
            renderer.material.color = isValid ? _validPreviewColor : _inValidPreviewColor;
        }
    }

    public void DealDamage(IDamagable damageTarget)
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(int damage, Team team)
    {
        UnitsCount += damage;

        if (UnitsCount == _data.UnitsPerLevel)
        {
            ChangeLevel(team);
        }

        if (Level == 0)
        {
            Team = team;
        }
    }

    private void ChangeLevel(Team team)
    {
        Level = team == Team ? Level + 1 : Level - 1;

        UnitsCount = 0;
    }

    private void OnTeamChanged()
    {
        Level = 1;
        UnitsCount = 0;

        _targetTowers.Clear();

        ConnectionService connectionService = FindFirstObjectByType<ConnectionService>();
        connectionService.ClearConnectionsForTower(this);
    }

    private void Awake()
    {
        _team = _data.Team;
        _level = _data.Level;
        _unitsCount = _data.UnitsCount;
    }
}