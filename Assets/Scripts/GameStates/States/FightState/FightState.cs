using UnityEngine;

public sealed class FightState : StateBase<FightStateContext>
{
    private readonly UIState _ui;

    public FightState(StateManager stateManager)
        : base(stateManager)
    {
        _ui = stateManager.FightUI;
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
