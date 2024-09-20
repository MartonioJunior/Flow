namespace MartonioJunior.Flow
{
    public interface ITicker: ITime
    {
        // MARK: Properties
        float DeltaTime {get;}
        
        // MARK: Methods
        void Reset();
        void UpdateTimeMarkers();
    }
}