namespace MartonioJunior.Flow
{
    public interface ICronometer: ITimeControllable
    {
        // MARK: Variables
        float Elapsed {get;}
        bool Paused {get;}
        bool Zeroed {get;}
    }
}