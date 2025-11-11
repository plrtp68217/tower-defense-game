using UnityEngine;

public class Connection: MonoBehaviour
{
    private LineRenderer _lineRenderer;

    public Tower StartTower { get; private set; }
    public Tower TargetTower { get; private set; }


    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetStart(Tower tower)
    {
        StartTower = tower;
    }

    public void SetTarget(Tower target)
    {
        if (target == StartTower) return;

        TargetTower = target;

        MoveTo(target.Center);

        CreateCollider();

        StartTower.AddTarget(target);
    }

    public void MoveFrom(Vector3 pos) => _lineRenderer.SetPosition(0, pos);
    public void MoveTo(Vector3 pos) => _lineRenderer.SetPosition(1, pos);
    public void Destroy() => Destroy(gameObject);

    private void CreateCollider()
    {
        MeshCollider collider = gameObject.AddComponent<MeshCollider>();
        Mesh mesh = new();
        _lineRenderer.BakeMesh(mesh, true);
        collider.sharedMesh = mesh;
    }
}