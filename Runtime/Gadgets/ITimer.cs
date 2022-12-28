using UnityEngine;

namespace MartonioJunior.Flow
{
    public interface ITimer: ICronometer
    {
        #region Properties
        bool Done {get;}
        float ElapsedNormal {get;}
        float Remaining {get;}
        float RemainingNormal {get;}
        float Target {get;}
        #endregion
    }
}