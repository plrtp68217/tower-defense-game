public interface IPlacementState
{
    PlacementStateType StateType { get; }
    void OnEnter(int objectID = -1);
    void OnUpdate();
    void OnClick();
    void OnExit();
}

