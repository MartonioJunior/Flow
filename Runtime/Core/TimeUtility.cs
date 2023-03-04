using UnityEngine;

namespace MartonioJunior.Flow
{
    public static class TimeUtility
    {
        #region Static Methods
        public static float GetDeltaTime(bool affectedByTimescale = true)
        {
            return affectedByTimescale ? Time.deltaTime : Time.unscaledDeltaTime;
        }

        public static float GetFixedDeltaTime(bool affectedByTimescale = true)
        {
            return affectedByTimescale ? Time.fixedDeltaTime : Time.fixedUnscaledDeltaTime;
        }

        public static float GetGlobalTime(bool affectedByTimescale = true, bool useSystemTime = false)
        {
            if (useSystemTime) {
                return Time.realtimeSinceStartup;
            } else if (affectedByTimescale) {
                return Time.time;
            } else {
                return Time.unscaledTime;
            }
        }

        public static double GetGlobalTimeDouble(bool affectedByTimescale = true, bool useSystemTime = false)
        {
            if (useSystemTime) {
                return Time.realtimeSinceStartupAsDouble;
            } else if (affectedByTimescale) {
                return Time.timeAsDouble;
            } else {
                return Time.unscaledTimeAsDouble;
            }
        }

        public static float GetTimeSinceLevelLoad()
        {
            return Time.timeSinceLevelLoad;
        }
        #endregion
    }
}