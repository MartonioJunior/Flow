using System;
using UnityEngine;

namespace MartonioJunior.Flow
{
    public static class TimeUtility
    {
        #region Static Methods
        public static Func<float> SourceDeltaTime(bool affectedByTimescale = true)
        {
            return affectedByTimescale ? () => Time.deltaTime : () => Time.unscaledDeltaTime;
        }

        public static Func<float> SourceFixedDeltaTime(bool affectedByTimescale = true)
        {
            return affectedByTimescale ? () => Time.fixedDeltaTime : () => Time.fixedUnscaledDeltaTime;
        }

        public static Func<float> SourceGlobalTime(bool affectedByTimescale = true, bool useSystemTime = false)
        {
            if (useSystemTime) {
                return () => Time.realtimeSinceStartup;
            } else if (affectedByTimescale) {
                return () => Time.time;
            } else {
                return () => Time.unscaledTime;
            }
        }

        public static Func<double> SourceGlobalTimeDouble(bool affectedByTimescale = true, bool useSystemTime = false)
        {
            if (useSystemTime) {
                return () => Time.realtimeSinceStartupAsDouble;
            } else if (affectedByTimescale) {
                return () => Time.timeAsDouble;
            } else {
                return () => Time.unscaledTimeAsDouble;
            }
        }

        public static Func<float> SourceTimeSinceLevelLoad()
        {
            return () => Time.timeSinceLevelLoad;
        }
        #endregion
    }
}