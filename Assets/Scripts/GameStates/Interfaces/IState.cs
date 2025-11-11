public interface IState
{
    void OnEnter();
    void OnUpdate();
    void OnClick();
    void OnPressed();
    void OnExit();
}
