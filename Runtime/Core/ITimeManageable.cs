using UnityEngine;

namespace MartonioJunior.Flow
{
    public interface ITimeManageable: ITime
    {
        #region Methods
        void Pause();
        void Resume();
        void Stop();
        #endregion
    }
}