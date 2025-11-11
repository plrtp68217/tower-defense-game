using UnityEngine;

public class Connection
{
    public Tower StartTower { get; private set; }
    public Tower TargetTower { get; private set; }
    public LineRenderer Line { get; private set; }

    public Connection(Tower startTower)
    {
        //TargetTower = targetTower;
        //Line = line;
        GameObject lineObj = new()
        {
            name = "Line",
            layer = LayerMask.NameToLayer(Layers.Objects)
        };

        Line = lineObj.AddComponent<LineRenderer>();
        Line.startWidth = 0.5f;
        Line.endWidth = 0.5f;
        Line.material = new Material(Shader.Find("Unlit/Color"))
        {
            color = Color.brown
        };
        Line.positionCount = 2;
        StartTower = startTower;
        Line.SetPosition(0, StartTower.Center);

    }

    public void SetTarget(Tower target)
    {
        if (target == StartTower) return;

        TargetTower = target;
        MoveTo(target.Center);
        MeshCollider collider = Line.gameObject.AddComponent<MeshCollider>();
        Mesh mesh = new();
        Line.BakeMesh(mesh, true);
        collider.sharedMesh = mesh;
        StartTower.AddTarget(target);

    }
    public void Destroy() => Object.Destroy(Line);
    public void MoveTo(Vector3 pos) => Line.SetPosition(1, pos);
}