using UnityEngine;

public class Unit : UnitEntityBase
{
    private readonly float arrivalThreshold = 0.2f;
    private Tower _target;

    public void SetTargetTower(Tower targetTower)
    {
        _target = targetTower;
    }

    public void Update()
    {
        if (_target != null)
        {
            Vector3 targetPosition = _target.transform.position;

            if (Vector3.Distance(transform.position, targetPosition) <= arrivalThreshold)
            {
                Destroy(gameObject);
                return; 
            }


            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                UnitData.MoveSpeed * Time.deltaTime  
            );

            transform.LookAt(targetPosition);
        }
    }
}
