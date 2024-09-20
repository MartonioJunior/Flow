namespace MartonioJunior.Flow
{
    public partial class Cronometer
    {
        // MARK: Variables
        ITicker cronometerTicker;
        float duration;
        float tickScale = 1;
        
        // MARK: Properties
        public float TimeScale => tickScale;

        // MARK: Delegates
        public delegate void Event(Cronometer cronometer);
       
        // MARK: Events
        public event Event OnChangeState, OnUpdate;
        
        // MARK: Initializers
        public Cronometer(bool isRealTime)
        {
            cronometerTicker = Ticker.New(isRealTime);
            cronometerTicker.UpdateTimeMarkers();
        }

        public Cronometer(ITicker ticker)
        {
            cronometerTicker = ticker;
            cronometerTicker.UpdateTimeMarkers();
        }
    }

    #region ICronometer Implementation
    public partial class Cronometer: ICronometer
    {
        public float Elapsed => duration;
        public bool Paused {get; private set;}
        public bool Zeroed => duration < float.Epsilon;

        public void Pause()
        {
            if (Paused) return;

            Paused = true;

            OnChangeState?.Invoke(this);
        }

        public void Resume()
        {
            if (!Paused) return;

            cronometerTicker.UpdateTimeMarkers();

            Paused = false;

            OnChangeState?.Invoke(this);
        }

        public void SetTimeScale(float timeScale)
        {
            tickScale = timeScale;
        }

        public void Stop()
        {
            duration = 0;
            Paused = true;

            OnChangeState?.Invoke(this);
        }

        public void Tick()
        {
            if (Paused) return;

            cronometerTicker.Tick();

            duration += cronometerTicker.DeltaTime * tickScale;

            OnUpdate?.Invoke(this);
        }
    }
    #endregion

    #region Factory
    public partial class Cronometer
    {
        public static Cronometer New(bool isRealTime, Event onUpdate = null, Event onChangeState = null, bool autoplay = true)
        {
            var cronometer = new Cronometer(Ticker.New(isRealTime));

            if (onUpdate is not null) cronometer.OnUpdate += onUpdate;
            if (onChangeState is not null) cronometer.OnChangeState += onChangeState;
            if (autoplay) cronometer.Resume();

            return cronometer;
        }
    }
    #endregion
}