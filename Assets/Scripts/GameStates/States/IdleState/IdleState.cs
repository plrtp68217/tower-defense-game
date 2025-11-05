using System.Diagnostics;

public sealed class IdleState : StateBase<IdleStateContext>
{
    private readonly UIState _ui;

    public IdleState(StateManager stateManager)
        : base(stateManager)
    {
        _ui = stateManager.IdleUI;
    }

    public override void OnEnter()
    {
        _ui.Show();
    }

    public override void OnExit()
    {
        _ui.Hide();
    }
}
