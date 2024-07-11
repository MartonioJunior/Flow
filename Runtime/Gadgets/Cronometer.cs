namespace MartonioJunior.Flow
{
    public class Cronometer: ICronometer
    {
        #region Variables
        ITicker cronometerTicker;
        float duration;
        float tickScale = 1;
        #endregion
        #region Properties
        public float TimeScale => tickScale;
        #endregion
        #region Delegates
        public delegate void Event(Cronometer cronometer);
        #endregion
        #region Events
        public event Event OnChangeState, OnUpdate;
        #endregion
        #region Constructors
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
        #endregion
        #region ICronometer Implementation
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
        #endregion
        #region Static Methods
        public static Cronometer New(bool isRealTime, Event onUpdate = null, Event onChangeState = null, bool autoplay = true)
        {
            var cronometer = new Cronometer(isRealTime);

            if (onUpdate is not null) cronometer.OnUpdate += onUpdate;
            if (onChangeState is not null) cronometer.OnChangeState += onChangeState;
            if (autoplay) cronometer.Resume();

            return cronometer;
        }
        #endregion
    }
}