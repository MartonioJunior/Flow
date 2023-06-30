using System;
using UnityEngine;

namespace MartonioJunior.Flow
{
    public struct AdvancedTicker: ITicker
    {
        #region Variables
        float zeroMarker;
        float lastMarker;
        Func<float> timestampSource;
        #endregion
        #region Properties
        public float GlobalTime => timestampSource() - zeroMarker;
        #endregion
        #region Constructors
        public AdvancedTicker(Func<float> timestampSource)
        {
            this.timestampSource = timestampSource ?? throw new System.ArgumentNullException();
            lastMarker = zeroMarker = timestampSource();
            DeltaTime = 0;
        }
        #endregion
        #region ITicker Implementation
        public float DeltaTime {get; private set;}

        public void Tick()
        {
            var currentMarker = timestampSource();
            DeltaTime = currentMarker - lastMarker;
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
        #endregion
        #region Static Methods
        public static AdvancedTicker New(bool isRealTime) => isRealTime ? new AdvancedTicker(Ticker.Unscaled) : new AdvancedTicker(Ticker.Scaled);
        #endregion
    }
}