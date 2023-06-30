using UnityEngine;

namespace MartonioJunior.Flow
{
    /**
    <summary>Interface that describes a temporal object that can be controlled.</summary>
    */
    public interface ITimeManageable: ITime
    {
        #region Methods
        /**
        <summary>Pauses updates in certain states of the object.</summary>
        */
        void Pause();
        /**
        <summary>Resumes updates in certain states of the object.</summary>
        */
        void Resume();
        /**
        <summary>Resets certain states of the object.</summary>
        */
        void Stop();
        #endregion
    }
}