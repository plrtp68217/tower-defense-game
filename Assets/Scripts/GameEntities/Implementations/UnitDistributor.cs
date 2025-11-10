using System.Collections.Generic;

public class UnitDistributor
{
    private int _index = 0;

    public IList<Tower> Targets { get; }

    public UnitDistributor(IList<Tower> towers)
    {
        Targets = towers;
    }

    public IEnumerable<Tower> NextTarget()
    {
        while (true)
        {
            if (Targets.Count == 0)
            {
                yield return null;
                continue;
            }

            _index = (_index + 1) % Targets.Count;

            yield return Targets[_index];
        }
    }
}