using UnityEngine;
using MartonioJunior.Core;

namespace MartonioJunior.Flow
{
    public partial class TemporaryObject
    {
        // MARK: Variables
        [SerializeField, Min(0f)] float timeToLive;
        Timer timer;

        // MARK: Methods
        private void Destroy(Timer timer) => gameObject.DestroyAndUnparent();
    }

    #region MonoBehaviour Implementation
    [DisallowMultipleComponent]
    public partial class TemporaryObject: MonoBehaviour
    {
        void Awake()
        {
            timer = Timer.Once(timeToLive, false, onComplete: Destroy);
            timer.Resume();
        }

        void Update() => timer.Tick();
    }
    #endregion
}