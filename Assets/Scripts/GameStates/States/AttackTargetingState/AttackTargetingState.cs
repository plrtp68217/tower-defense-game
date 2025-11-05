using UnityEngine;

public sealed class AttackTargetingState : StateBase<AttackTargetingStateContext>
{
    private LineRenderer _lineRenderer;
    private GameObject _selectedTower;
    private Vector3 _targetPosition;

    public AttackTargetingState(StateManager stateManager)
        : base(stateManager) { }

    public override void OnEnter()
    {
        // Инициализируем LineRenderer для отображения линии прицеливания
        var lineObj = new GameObject("AttackLine");
        _lineRenderer = lineObj.AddComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;
        _lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        _lineRenderer.material.color = Color.red;
        _lineRenderer.positionCount = 2;

        // Получаем выбранную башню из контекста
        _selectedTower = Context.SelectedTower;

        Debug.Log("Entered AttackTargetingState. Selected tower: " + _selectedTower.name);
    }

    public override void OnUpdate()
    {
        // Получаем позицию курсора на игровом поле
        Vector3 mousePosition = _stateManager.InputManager.GetSelectedMapPosition();
        _targetPosition = mousePosition;

        // Обновляем позицию конечной точки линии
        _lineRenderer.SetPosition(0, _selectedTower.transform.position);
        _lineRenderer.SetPosition(1, _targetPosition);

        // Визуализируем линию только если курсор вне UI
        _lineRenderer.enabled = !_stateManager.InputManager.IsPointerOverUI();
    }

    public override void OnClick()
    {
        if (_stateManager.InputManager.IsPointerOverUI())
            return;

        // Проверяем, есть ли враг в точке клика
        var enemy = FindEnemyAtPosition(_targetPosition);
        if (enemy != null)
        {
            // Запускаем атаку по найденному врагу
            ExecuteAttack(_selectedTower, enemy);

            // Выходим из режима прицеливания
            _stateManager.SwitchToState<IdleState, IdleStateContext>();
        }
        else
        {
            Debug.LogWarning("No enemy found at target position.");
        }
    }

    public override void OnExit()
    {
        // Удаляем линию прицеливания
        if (_lineRenderer != null && _lineRenderer.gameObject != null)
        {
            Object.Destroy(_lineRenderer.gameObject);
        }
    }

    private IPlacable FindEnemyAtPosition(Vector3 position)
    {
        // Здесь реализуем поиск врага в заданной точке
        // Пример реализации через коллайдеры:
        Collider[] hitColliders = Physics.OverlapSphere(position, 1.0f);
        foreach (var collider in hitColliders)
        {
            if (collider.TryGetComponent<IPlacable>(out var enemy))
            {
                return enemy;
            }
        }
        return null;
    }

    private void ExecuteAttack(GameObject tower, IPlacable target)
    {
        // Логика атаки башни по врагу
        Debug.Log($"Tower {tower.name} attacks enemy {target}!");


        // Пример: запускаем анимацию атаки башни
        //var animator = tower.GetComponent<Animator>();
        //if (animator != null)
        //{
        //    animator.SetTrigger("Attack");
        //}
    }
}
