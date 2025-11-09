using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    private Dictionary<Tower, List<Tower>> _connections;

    public bool AddConnection(Tower fromTower, Tower toTower)
    {
        return false;
    }

    public bool RemoveConnection(Tower fromTower, Tower toTower)
    {
        return false;
    }

    public List<Tower> GetTargetsForSource(Tower sourceTower)
    {
        return new List<Tower>();
    }

    public Dictionary<Tower, List<Tower>> GetAllConnections()
    {
        return _connections;
    }

}
