using System;
using UnityEngine;
using System.Collections.Generic;

public class DefaultState: IPlacementState
{
    private PlacementSystem _context;

    public DefaultState(PlacementSystem context)
    {
        _context = context;
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
