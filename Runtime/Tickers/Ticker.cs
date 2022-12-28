using System;
using UnityEngine;

namespace MartonioJunior.Flow
{
    public struct Ticker: ITicker
    {
        #region Variables
        float lastMarker;
        Func<float> timestampSource;
        #endregion
        #region Constructors
        public Ticker(Func<float> timestampSource)
        {
            if (timestampSource == null) throw new System.ArgumentNullException();

            this.timestampSource = timestampSource;
            this.lastMarker = timestampSource();
            this.DeltaTime = 0;
        }
        #endregion
        #region ITicker Implementation
        public float DeltaTime {get; private set;}

        public void Tick()
        {
            var currentMarker = timestampSource();
            DeltaTime = currentMarker - lastMarker;
            UpdateTimeMarkers();
        }

        public void UpdateTimeMarkers()
        {
            lastMarker = timestampSource();
        }
        #endregion
        #region Static Methods
        public static Ticker New(bool isRealTime)
        {
            if (isRealTime) {
                return new Ticker(Ticker.Unscaled);
            } else {
                return new Ticker(Ticker.Scaled);
            }
        }

        public static float Scaled()
        {
            return Time.time;
        }

        public static float Unscaled()
        {
            return Time.unscaledTime;
        }
        #endregion
    }
}