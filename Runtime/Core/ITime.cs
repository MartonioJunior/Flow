using UnityEngine;

namespace MartonioJunior.Flow
{
    /**
    <summary>Interface that describes an object that can have it's state updated over time.</summary>
    */
    public interface ITime
    {
        #region Methods
        /**
        <summary>Notifies that the state of the game has updated.</summary>
        */
        void Tick();
        #endregion
    }
}