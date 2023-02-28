using UnityEngine;

namespace MartonioJunior.Flow
{
    public struct ManualTicker: ITicker
    {
        #region ITicker Implementation
        public float DeltaTime {get; set;}
        
        public void Reset() {}
        public void Tick() {}
        public void UpdateTimeMarkers() {}
        #endregion
    }
}