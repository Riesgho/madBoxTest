public class InMemoryPlayer: IPlayerRepository
{
    public InMemoryPlayer(float startSpeed)
    {
        Speed = startSpeed;
    }

    public float Speed { get; }
}