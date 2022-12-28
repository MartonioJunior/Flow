using System;
using UnityEngine;

namespace MartonioJunior.Flow
{
    public class Cronometer: ICronometer
    {
        #region Variables
        ITicker cronometerTicker;
        float duration = 0;
        float tickScale = 1;
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
        public bool Zeroed => duration == 0;

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
        public static Cronometer New(bool isRealTime, Cronometer.Event onUpdate = null, Cronometer.Event onChangeState = null, bool autoplay = true)
        {
            var cronometer = new Cronometer(isRealTime);

            if (onUpdate != null) cronometer.OnUpdate += onUpdate;
            if (onChangeState != null) cronometer.OnChangeState += onChangeState;
            if (autoplay) cronometer.Resume();

            return cronometer;
        }
        #endregion
    }
}