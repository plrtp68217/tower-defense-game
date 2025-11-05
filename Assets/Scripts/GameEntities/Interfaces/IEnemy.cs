public interface IEnemy : IPlacable, IDamagable 
{
    Team Team { get; set; }
}

