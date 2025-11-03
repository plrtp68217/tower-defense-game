using System;
using UnityEngine;
using System.Collections.Generic;

public class DefaultState: IPlacementState
{
    private PlacementSystem _context;

    public PlacementStateType StateType { get; }

    public DefaultState(PlacementSystem context, PlacementStateType stateType)
    {
        _context = context;
        StateType = stateType;
    }

    public void OnEnter(int objectID = -1)
    {
        _context.HideVisual();
    }

    public void OnUpdate()
    {

    }

    public void OnClick()
    {

    }

    public void OnExit()
    {

    }
}