namespace MartonioJunior.Flow
{
    public partial struct ManualTicker {}

    #region ITicker Implementation
    public partial struct ManualTicker: ITicker
    {
        public float DeltaTime {get; set;}

        public void Reset() {}
        public void Tick() {}
        public void UpdateTimeMarkers() {}
    }
    #endregion
}