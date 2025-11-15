using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerGenerator: MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private Terrain _map;

    [Header("Tower Settings")]
    [SerializeField] private float _minDistanceBetweenTowers = 0.1f;

    [Header("Team Border")]
    [Range(0.1f, 0.9f)]
    [SerializeField] private float _teamBorder = 0.5f;

    [Header("Noise Settings")]
    [SerializeField] private float _noiseScale = 5f;
    [SerializeField] private float _threshold  = 0.3f;
    [SerializeField] private Vector2 _offset;

    private int _teamBorderX;
    private int _mapWidth;
    private int _mapHeight;

    private void Start()
    {
        _mapWidth  = Mathf.RoundToInt(_map.terrainData.size.x);
        _mapHeight = Mathf.RoundToInt(_map.terrainData.size.z);

        _teamBorderX = Mathf.RoundToInt(_mapWidth * _teamBorder);
    }

    public List<Vector3> GenerateTowers(Team team)
    {
        List<Vector3> candidates = new();
        List<Vector3> approvedСandidates = new();

        int startX = team == Team.Blue ? 0            : _teamBorderX;
        int endX   = team == Team.Blue ? _teamBorderX : _mapWidth;

        for (int x = startX; x < endX; x++)
        {
            for (int z = 0; z < _mapHeight; z++)
            {
                float noiseValue = GetPerlinNoiseValue(x, z);

                Debug.Log(noiseValue);

                if (noiseValue > _threshold)
                {
                    candidates.Add(new Vector3(x, 0, z));
                }
            }
        }

        foreach (Vector3 candidate in candidates)
        {
            bool tooClose = false;

            foreach (Vector3 approvedСandidate in approvedСandidates)
            {
                if (Vector3.Distance(candidate, approvedСandidate) < _minDistanceBetweenTowers)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                approvedСandidates.Add(candidate);
            }
        }

        return approvedСandidates;
    }

    private float GetPerlinNoiseValue(float x, float z)
    {
        float sampleX = (x + _offset.x) / _noiseScale;
        float sampleZ = (z + _offset.y) / _noiseScale;

        return Mathf.PerlinNoise(sampleX, sampleZ);
    }
}
