public sealed class IdleState : StateBase<IdleStateContext>
{
    public IdleState(StateManager stateManager)
        : base(stateManager) { }

    public override void OnEnter() => _stateManager.HideVisual();
}
