using System.Collections.Generic;
using UnityEngine;


public class ConnectionService : MonoBehaviour
{
    private readonly IList<Connection> _connections = new List<Connection>();
    private readonly Collider[] _collidersBuffer = new Collider[128];

    [field: SerializeField] public GameObject LinePrefab { get; set; }
    [field: SerializeField] public GameObject DebugSpherePrefab { get; set; }

    private readonly List<GameObject> _debugSpheres = new();

    public void AddConnection(Connection connection)
    {
        if (_connections.Contains(connection) == false)
        {
            _connections.Add(connection);
        }
    }

    public void RemoveConnection(Connection connection)
    {
        if (connection == null) return;

        Tower tower = connection.StartTower;
        Tower targetTower = connection.TargetTower;

        tower.RemoveTarget(targetTower);

        _connections.Remove(connection);

        Destroy(connection.gameObject);
    }

    public bool IsConnectionBlocked(Tower fromTower, Tower toTower)
    {
        if (fromTower == null || toTower == null) return true;

        Vector3 fromPos = fromTower.Center;
        Vector3 toPos = toTower.Center;

        const float sphereRadius = 0.3f;
        var pointsAlongLine = GetPointsAlongLine(fromPos, toPos);

        foreach (Vector3 point in pointsAlongLine)
        {
            if (Vector3.Distance(point, fromPos) < 1f || Vector3.Distance(point, toPos) < 1f)
            {
                continue;
            }

            if (IsPointBlocked(point, fromTower, toTower, sphereRadius))
            {
                return true;
            }
        }

        return false;
    }

    private IReadOnlyList<Vector3> GetPointsAlongLine(Vector3 from, Vector3 to, float step = 0.1f)
    {
        List<Vector3> points = new();

        float distance = Vector3.Distance(from, to);
        int stepsCount = Mathf.CeilToInt(distance / step);

        for (int i = 0; i <= stepsCount; i++)
        {
            float t = (float)i / stepsCount;
            Vector3 point = Vector3.Lerp(from, to, t);
            points.Add(point);
        }

        return points;
    }

    private bool IsPointBlocked(Vector3 worldPosition, Tower fromTower, Tower toTower, float sphereRadius = 0.1f)
    {
        var n = Physics.OverlapSphereNonAlloc(worldPosition, sphereRadius, _collidersBuffer);

        for (int i = 0; i < n; i++)
        {
            Collider collider = _collidersBuffer[i];

            if (collider.transform.position == fromTower.WorldPosition || collider.transform.position == toTower.WorldPosition)
            {
                continue;
            }

            if (collider.TryGetComponent<IPlacable>(out var _))
            {
                return true;
            }
        }

        return false;
    }

    private void RemoveAllDebugSpheres()
    {
        foreach (GameObject sphere in _debugSpheres)
        {
            if (sphere != null)
            {
                Destroy(sphere);
            }
        }
        _debugSpheres.Clear();
    }

    private void DrawDebugSpheres(IReadOnlyList<Vector3> points, float sphereRadius)
    {
        if (DebugSpherePrefab == null)
        {
            Debug.LogWarning("Debug sphere prefab is not assigned!");
            return;
        }

        RemoveAllDebugSpheres();

        foreach (Vector3 point in points)
        {
            GameObject sphere = Instantiate(DebugSpherePrefab, point, Quaternion.identity);
            sphere.name = "DebugSphere";

            sphere.transform.localScale = 2f * sphereRadius * Vector3.one; // Диаметр = 2 × радиус
            _debugSpheres.Add(sphere);
            Destroy(sphere, 2f);
        }
    }
}
