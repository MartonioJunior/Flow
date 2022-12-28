using UnityEngine;

namespace MartonioJunior.Flow
{
    public interface ITimeScalable: ITime
    {
        #region Methods
        void SetTimeScale(float timeScale);
        #endregion
    }

    public static partial class ITimeScalableExtensions
    {
        public static void FastForward(this ITimeScalable self, float multiplier)
        {
            self.SetTimeScale(Mathf.Max(0,multiplier));
        }

        public static void Rewind(this ITimeScalable self, float multiplier)
        {
            self.SetTimeScale(Mathf.Abs(multiplier));
        }
    }
}