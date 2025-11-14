using UnityEngine;

public class Unit : UnitEntityBase
{
    private readonly float arrivalThreshold = 0.1f;
    private Tower _target;

    public void SetTargetTower(Tower targetTower)
    {
        _target = targetTower;
    }

    private void Update()
    {
        if (_target == null) return;

        Vector3 targetPosition = _target.Center;

        bool isUnitInTower = Vector3.Distance(transform.position, targetPosition) <= arrivalThreshold;

        if (isUnitInTower)
        {
            DealDamage(_target);
            Destroy(gameObject);
            return; 
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            _data.MoveSpeed * Time.deltaTime  
        );

        transform.LookAt(targetPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent(out Unit otherUnit);

        if (otherUnit == null) return;

        otherUnit.TakeDamage(Damage, Team);
    }
}
