using UnityEngine;

public class Connection
{
    public Tower Tower { get; private set; }
    public Tower TargetTower { get; private set; }
    public LineRenderer Line { get; private set; }

    public Connection(Tower tower, Tower targetTower, LineRenderer line)
    {
        Tower = tower;
        TargetTower = targetTower;
        Line = line;
    }
}