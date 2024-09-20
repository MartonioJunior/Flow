using System;
using UnityEngine;

namespace MartonioJunior.Flow
{
    public partial struct AdvancedTicker
    {
        // MARK: Variables
        float zeroMarker;
        float lastMarker;
        Func<float> timestampSource;
        float deltaTime;
        
        // MARK: Properties
        public float GlobalTime => timestampSource() - zeroMarker;

        // MARK: Initializers
        public AdvancedTicker(Func<float> timestampSource)
        {
            this.timestampSource = timestampSource ?? throw new System.ArgumentNullException();
            lastMarker = zeroMarker = timestampSource();
            deltaTime = 0;
        }
    }

    #region ITicker Implementation
    public partial struct AdvancedTicker: ITicker
    {
        public float DeltaTime => deltaTime;

        public void Tick()
        {
            var currentMarker = timestampSource();
            deltaTime = currentMarker - lastMarker;
            lastMarker = currentMarker;
        }

        public void UpdateTimeMarkers()
        {
            lastMarker = timestampSource();
        }

        public void Reset()
        {
            zeroMarker = lastMarker = timestampSource();
        }
    }
    #endregion

    #region Factory
    public partial struct AdvancedTicker
    {
        public static AdvancedTicker New(bool isRealTime) => isRealTime ? new AdvancedTicker(Ticker.Unscaled) : new AdvancedTicker(Ticker.Scaled);
    }
    #endregion
}