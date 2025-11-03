using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlacementStateFactory : IPlacementStateFactory
{
    private readonly PlacementSystem _context;

    public PlacementStateFactory(PlacementSystem context)
    {
        _context = context;
    }

    public IPlacementState CreateState(PlacementStateType stateType)
    {
        return stateType switch
        {
            PlacementStateType.Default => new DefaultState(_context, PlacementStateType.Default),
            PlacementStateType.Build => new BuildState(_context, PlacementStateType.Build),
            _ => new DefaultState(_context, PlacementStateType.Default)
        };
    }
}

