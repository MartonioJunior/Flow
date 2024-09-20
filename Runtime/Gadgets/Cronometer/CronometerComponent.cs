using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Flow
{
    public partial class CronometerComponent
    {
        // MARK: Variables
        [SerializeField] bool isRealTime = false;
        [SerializeField] bool playOnAwake = true;
        Cronometer cronometer;

        // MARK: Events
        [SerializeField] UnityEvent<Cronometer> OnUpdate = new();
        [SerializeField] UnityEvent<Cronometer> OnChangeState = new();
    }

    #region MonoBehaviour Implementation
    [AddComponentMenu("Flow/Cronometer")]
    public partial class CronometerComponent: MonoBehaviour
    {
        void Awake()
        {
            cronometer = new(isRealTime);
            cronometer.OnUpdate += OnUpdate.Invoke;
            cronometer.OnChangeState += OnChangeState.Invoke;
            if (playOnAwake) cronometer.Resume();
        }

        void OnDestroy()
        {
            cronometer.OnUpdate -= OnUpdate.Invoke;
            cronometer.OnChangeState -= OnChangeState.Invoke;
        }

        void Update()
        {
            Tick();
        }
    }
    #endregion

    #region ICronometer Implementation
    public partial class CronometerComponent: ICronometer
    {
        public float Elapsed => cronometer.Elapsed;
        public bool Paused => cronometer.Paused;
        public bool Zeroed => cronometer.Zeroed;
        public void Pause() => cronometer.Pause();
        public void Resume() => cronometer.Resume();
        public void SetTimeScale(float timeScale) => cronometer.SetTimeScale(timeScale);
        public void Stop() => cronometer.Stop();
        public void Tick() => cronometer.Tick();
    }
    #endregion
}