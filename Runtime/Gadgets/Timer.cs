using System;
using MartonioJunior.Core;
using UnityEngine;

namespace MartonioJunior.Flow
{
    public class Timer: ITimer
    {
        #region Variables
        float duration = 0;
        int numberOfLoops;
        float targetDuration;
        float tickScale = 1;
        ITicker ticker;
        #endregion
        #region Delegates
        public delegate void Event(Timer timer);
        #endregion
        #region Events
        public event Event OnChangeState, OnComplete, OnUpdate;
        #endregion
        #region Constructors
        public Timer(float targetDuration, bool isRealTime, int numberOfLoops = 0)
        {
            ticker = AdvancedTicker.New(isRealTime);
            duration = 0;
            this.numberOfLoops = numberOfLoops;
            this.targetDuration = targetDuration;
        }

        public Timer(ITicker ticker, float targetDuration, int numberOfLoops = 1)
        {
            duration = 0;
            this.ticker = ticker;
            this.numberOfLoops = numberOfLoops;
            this.targetDuration = targetDuration;
        }
        #endregion
        #region ITimer Implementation
        public bool Done => duration >= targetDuration;
        public float Elapsed => duration;
        public float ElapsedNormal => Elapsed / targetDuration;
        public float Remaining => targetDuration - duration;
        public float RemainingNormal => Remaining / targetDuration;
        public bool Paused {get; private set;}
        public float Target => targetDuration;
        public bool Zeroed => duration.IsZero();

        public void Pause()
        {
            if (Paused) return;

            Paused = true;

            OnChangeState?.Invoke(this);
        }

        public void Resume()
        {
            if (!Paused) return;

            ticker.UpdateTimeMarkers();

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

            OnChangeState?.Invoke(this);
        }

        public void Tick()
        {
            if (Paused) return;

            ticker.Tick();

            duration += ticker.DeltaTime * tickScale;

            CheckTimer();

            OnUpdate?.Invoke(this);
        }
        #endregion
        #region Methods
        public void ChangeTargetTime(float targetTime)
        {
            targetDuration = targetTime;
        }

        private void CheckTimer()
        {
            if (duration < targetDuration) return;

            if (numberOfLoops == -1) {
                RemapTimer();
            } else if (numberOfLoops > 0) {
                RemapTimer();
                numberOfLoops--;
            } else {
                MarkAsDone();
            }
        }

        public void MarkAsDone(bool fireEvent=true)
        {
            duration = targetDuration;
            Paused = true;

            if (fireEvent) OnComplete?.Invoke(this);
        }

        private void RemapTimer()
        {
            duration -= targetDuration;
            ticker.Reset();
        }

        public void Restart()
        {
            duration = 0;
            Resume();
        }
        #endregion
        #region Static Methods
        public static Timer Every(float time, bool isRealTime, Timer.Event onComplete = null, Timer.Event onUpdate = null, Timer.Event onChangeState = null, bool autoplay = false) => New(time, isRealTime, -1, onComplete, onUpdate, onChangeState, autoplay);
        public static Timer ForXLoops(float time, bool isRealTime, int numberOfLoops, Timer.Event onComplete = null, Timer.Event onUpdate = null, Timer.Event onChangeState = null, bool autoplay = false) => New(time, isRealTime, numberOfLoops, onComplete, onUpdate, onChangeState, autoplay);
        public static Timer Once(float time, bool isRealTime, Timer.Event onComplete = null, Timer.Event onUpdate = null, Timer.Event onChangeState = null, bool autoplay = false) => New(time, isRealTime, 0, onComplete, onUpdate, onChangeState, autoplay);

        public static Timer New(float time, bool isRealTime, int numberOfLoops = 0, Timer.Event onComplete = null, Timer.Event onUpdate = null, Timer.Event onChangeState = null, bool autoplay = false)
        {
            Timer cronometer = new(time, isRealTime) {
                numberOfLoops = numberOfLoops
            };

            if (onComplete is not null) cronometer.OnComplete += onComplete;
            if (onUpdate is not null) cronometer.OnUpdate += onUpdate;
            if (onChangeState is not null) cronometer.OnChangeState += onChangeState;
            if (autoplay) cronometer.Resume();

            return cronometer;
        }
        #endregion
    }
}