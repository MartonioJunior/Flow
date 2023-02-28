using UnityEngine;

namespace MartonioJunior.Flow
{
    public interface ITicker: ITime
    {
        #region Properties
        float DeltaTime {get;}
        #endregion
        #region Methods
        void Reset();
        void UpdateTimeMarkers();
        #endregion
    }
}