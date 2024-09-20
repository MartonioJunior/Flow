namespace MartonioJunior.Flow
{
    /**
    <summary>A special timer used to represent cooldown timers.</summary>
    */
    public partial class Cooldown
    {
        // MARK: Variables
        Timer cooldownTimer;
        
        // MARK: Properties
        public bool HasEnded => cooldownTimer.Done;
        public float Remaining => cooldownTimer.Remaining;
        public float Time => cooldownTimer.Target;
        
        // MARK: Delegates
        public delegate void Event(Cooldown cooldown);
        
        // MARK: Events
        public event Event OnTrigger, OnAvailable;
        
        // MARK: Initializers
        public Cooldown(float cooldown, bool isRealTime = false)
        {
            cooldownTimer = Timer.Once(cooldown, isRealTime, onComplete: OnCompleteTimer);
        }

        public Cooldown(float cooldown, ITicker ticker)
        {
            cooldownTimer = new Timer(ticker, cooldown);
            cooldownTimer.OnComplete += OnCompleteTimer;
        }
        
        // MARK: Methods
        private void OnCompleteTimer(Timer timer)
        {
            OnAvailable?.Invoke(this);
        }

        public void Reload()
        {
            if (cooldownTimer.Done) return;

            cooldownTimer.MarkAsDone();
        }

        public void Trigger()
        {
            if (!cooldownTimer.Zeroed) return;

            ForceTrigger();
        }

        public void ForceTrigger()
        {
            cooldownTimer.Restart();
            OnTrigger?.Invoke(this);
        }
    }

    #region ITimeManageable Implementation
    public partial class Cooldown: ITimeManageable
    {
        public void Pause() => cooldownTimer.Pause();
        public void Resume() => cooldownTimer.Resume();
        public void Stop() => cooldownTimer.Stop();
        public void SetTimeScale(float timeScale) => cooldownTimer.SetTimeScale(timeScale);
        public void Tick() => cooldownTimer.Tick();
    }
    #endregion

    #region Factory
    public partial class Cooldown
    {
        public static Cooldown New(float cooldownTime, bool isRealTime, Cooldown.Event onTrigger = null, Cooldown.Event onAvailable = null, bool autoplay = true)
        {
            var cooldown = new Cooldown(cooldownTime, isRealTime);

            if (onTrigger is not null) cooldown.OnTrigger += onTrigger;
            if (onAvailable is not null) cooldown.OnAvailable += onAvailable;
            if (autoplay) cooldown.Resume();

            return cooldown;
        }
    }
    #endregion
}