using UnityEngine;

namespace MartonioJunior.Flow
{
    public interface ICronometer: ITimeControllable
    {
        #region Variables
        float Elapsed {get;}
        bool Paused {get;}
        bool Zeroed {get;}
        #endregion
    }
}