using UnityEngine;

namespace MartonioJunior.Flow
{
    /**
    <summary>Interface that describes a temporal object that can operate on a different time scale.</summary>
    */
    public interface ITimeScalable: ITime
    {
        #region Methods
        /**
        <summary>Defines a time scale for the temporal object.</summary>
        <param name="timeScale">The time scale to be set.</param>
        */
        void SetTimeScale(float timeScale);
        #endregion
    }
    /**
    <summary>Defines extension for the ITimeScalable class.</summary>
    */
    public static partial class ITimeScalableExtensions
    {
        /**
        <summary>Sets the time scale to a positive value.</summary>
        <param name="self">The temporal object.</param>
        <param name="multiplier">The multiplier to be applied. Negative values of multiplier will set the time scale to zero.</param>
        <remarks>Setting the value above 1 speeds up the object. Setting the value below 1 creates a slow-down effect.</remarks>
        */
        public static void FastForward(this ITimeScalable self, float multiplier)
        {
            self.SetTimeScale(Mathf.Max(0,multiplier));
        }
        /**
        <summary>Sets the time scale below the base value.</summary>
        <param name="self">The temporal object.</param>
        <param name="multiplier">The multiplier to be applied.</param>
        <remarks>The time scale will not be set above zero.</remarks>
        */
        public static void Rewind(this ITimeScalable self, float multiplier)
        {
            self.SetTimeScale(-Mathf.Abs(multiplier));
        }
    }
}