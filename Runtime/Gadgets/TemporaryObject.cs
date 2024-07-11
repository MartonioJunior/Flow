using UnityEngine;
using MartonioJunior.Core;

namespace MartonioJunior.Flow
{
    [DisallowMultipleComponent]
    public class TemporaryObject: MonoBehaviour
    {
        #region Variables
        [SerializeField, Min(0f)] float timeToLive;
        Timer timer;
        #endregion
        #region MonoBehaviour Lifecycle
        void Awake()
        {
            timer = Timer.Once(timeToLive, false, onComplete: Destroy);
            timer.Resume();
        }

        void Update() => timer.Tick();
        #endregion
        #region Methods
        private void Destroy(Timer timer) => gameObject.DestroyAndUnparent();
        #endregion
    }
}