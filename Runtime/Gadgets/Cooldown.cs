using UnityEngine;

namespace MartonioJunior.Flow
{
    /**
    <summary>A special timer used to represent cooldown timers.</summary>
    */
    public class Cooldown: ITimeManageable
    {
        #region Variables
        Timer cooldownTimer;
        #endregion
        #region Properties
        public bool HasEnded => cooldownTimer.Done;
        public float Remaining => cooldownTimer.Remaining;
        public float Time => cooldownTimer.Target;
        #endregion
        #region Delegates
        public delegate void Event(Cooldown cooldown);
        #endregion
        #region Events
        public event Event OnTrigger, OnAvailable;
        #endregion
        #region Constructors
        public Cooldown(float cooldown, bool isRealTime = false)
        {
            cooldownTimer = Timer.Once(cooldown, isRealTime, onComplete: OnCompleteTimer);
        }

        public Cooldown(float cooldown, ITicker ticker)
        {
            cooldownTimer = new Timer(ticker, cooldown);
            cooldownTimer.OnComplete += OnCompleteTimer;
        }
        #endregion
        #region ITimeManageable Implementation
        public void Pause()
        {
            cooldownTimer.Pause();
        }

        public void Resume()
        {
            cooldownTimer.Resume();
        }

        public void Stop()
        {
            cooldownTimer.Stop();
        }

        public void SetTimeScale(float timeScale)
        {
            cooldownTimer.SetTimeScale(timeScale);
        }

        public void Tick()
        {
            cooldownTimer.Tick();
        }
        #endregion
        #region Methods
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
        #endregion
        #region Operators
        #endregion
        #region Static Methods
        public static Cooldown New(float cooldownTime, bool isRealTime, Cooldown.Event onTrigger = null, Cooldown.Event onAvailable = null, bool autoplay = true)
        {
            var cooldown = new Cooldown(cooldownTime, isRealTime);

            if (onTrigger != null) cooldown.OnTrigger += onTrigger;
            if (onAvailable != null) cooldown.OnAvailable += onAvailable;
            if (autoplay) cooldown.Resume();

            return cooldown;
        }
        #endregion
    }
}