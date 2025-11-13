using UnityEngine;

public class Unit : UnitEntityBase
{
    private readonly float arrivalThreshold = 1f;
    private Tower _target;

    public void SetTargetTower(Tower targetTower)
    {
        _target = targetTower;
    }

    public void Update()
    {
        if (_target != null)
        {
            Vector3 targetPosition = _target.Center;

            if (Vector3.Distance(transform.position, targetPosition) <= arrivalThreshold)
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
    }
}
