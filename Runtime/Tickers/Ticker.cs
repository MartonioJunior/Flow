using System;
using UnityEngine;

namespace MartonioJunior.Flow
{
    public partial struct Ticker
    {
        // MARK: Variables
        float lastMarker;
        Func<float> timestampSource;
        float deltaTime;
        
        // MARK: Initializers
        public Ticker(Func<float> timestampSource)
        {
            this.timestampSource = timestampSource ?? throw new ArgumentNullException();
            lastMarker = timestampSource();
            deltaTime = 0;
        }

        // MARK: Methods
        public static float Scaled()
        {
            return Time.time;
        }

        public static float Unscaled()
        {
            return Time.unscaledTime;
        }
    }

    #region ITicker Implementation
    public partial struct Ticker: ITicker
    {
        public float DeltaTime => deltaTime;

        public void Reset()
        {
            lastMarker = timestampSource();
        }

        public void Tick()
        {
            var currentMarker = timestampSource();
            deltaTime = currentMarker - lastMarker;
            UpdateTimeMarkers();
        }

        public void UpdateTimeMarkers()
        {
            lastMarker = timestampSource();
        }
    }
    #endregion

    #region Factory
    public partial struct Ticker
    {
        public static Ticker New(bool isRealTime)
        {
            if (isRealTime) {
                return new Ticker(Unscaled);
            } else {
                return new Ticker(Scaled);
            }
        }
    }
    #endregion
}