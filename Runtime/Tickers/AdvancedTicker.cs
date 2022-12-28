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
            if (timestampSource == null) throw new System.ArgumentNullException();

            this.timestampSource = timestampSource;
            this.lastMarker = this.zeroMarker = timestampSource();
            this.DeltaTime = 0;
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
        #endregion
        #region Methods
        public void Reset()
        {
            zeroMarker = lastMarker = timestampSource();
        }
        #endregion
        #region Static Methods
        public static AdvancedTicker New(bool isRealTime)
        {
            if (isRealTime) {
                return new AdvancedTicker(Ticker.Unscaled);
            } else {
                return new AdvancedTicker(Ticker.Scaled);
            }
        }
        #endregion
    }
}