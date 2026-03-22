public abstract class Powerup
{
    public float lifespan;
    public abstract void Apply(Pawn target);
    public abstract void Remove(Pawn target);
}
