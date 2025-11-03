public interface IPlacementState
{
    void OnEnter(int objectID = -1);
    void OnUpdate();
    void OnClick();
    void OnExit();
}

