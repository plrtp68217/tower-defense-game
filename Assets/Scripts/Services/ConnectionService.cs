using System.Collections.Generic;
using UnityEngine;

public class ConnectionService : MonoBehaviour
{
    private Dictionary<Tower, List<Tower>> _connections;

    private void Awake()
    {
        _connections = new();
    }

    public void AddConnection(Tower fromTower, Tower toTower)
    {
        if (_connections.ContainsKey(fromTower) == false)
        {
            _connections[fromTower] = new List<Tower>();
        }

        _connections[fromTower].Add(toTower);
    }

    public bool RemoveConnection(Tower fromTower, Tower toTower)
    {
        if (_connections.ContainsKey(fromTower))
        {
            List<Tower> connections = _connections[fromTower];

            bool removed = connections.Remove(toTower);

            return removed;
        }

        return false;
    }

    public List<Tower> GetTargetsForSource(Tower sourceTower)
    {
        if (_connections.ContainsKey(sourceTower) == false)
        {
            return new List<Tower>();
        }

        return _connections[sourceTower];
    }

    public Dictionary<Tower, List<Tower>> GetAllConnections()
    {
        return _connections;
    }

    public bool IsConnectionBlocked(Tower fromTower, Tower toTower)
    {
        Vector3 fromPos = fromTower.WorldPosition;
        Vector3 toPos = toTower.WorldPosition;

        List<Vector3> pointsAlongLine = GetPointsAlongLine(fromPos, toPos, 0.1f);

        foreach (Vector3 point in pointsAlongLine)
        {
            if (Vector3.Distance(point, fromPos) < 1f || Vector3.Distance(point, toPos) < 1f)
                continue;

            if (IsPointBlocked(point, fromTower, toTower))
            {
                return true;
            }
        }

        return false;
    }

    private List<Vector3> GetPointsAlongLine(Vector3 from, Vector3 to, float step)
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

    private bool IsPointBlocked(Vector3 worldPosition, Tower fromTower, Tower toTower)
    {
        Collider[] colliders = Physics.OverlapSphere(worldPosition, 0.3f);

        foreach (Collider collider in colliders)
        {
            if (collider.transform.position == fromTower.WorldPosition || collider.transform.position == toTower.WorldPosition)
            {
                continue;
            }

            IPlacable placable = collider.GetComponent<IPlacable>();

            if (placable != null)
                return true;
        }

        return false;
    }
}
