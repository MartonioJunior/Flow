using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Flow
{
    public partial class TimerComponent
    {
        // MARK: Variables
        [SerializeField, Min(0)] float targetTime = 1f;
        [SerializeField, Min(-1)] int numberOfLoops = 1;
        [SerializeField] bool isRealTime = false;
        [SerializeField] bool playOnAwake = true;
        Timer timer;

        // MARK: Events
        [SerializeField] UnityEvent<Timer> OnUpdate = new();
        [SerializeField] UnityEvent<Timer> OnChangeState = new();
        [SerializeField] UnityEvent<Timer> OnComplete = new();
    }

    #region MonoBehaviour Implementation
    [AddComponentMenu("Flow/Timer")]
    public partial class TimerComponent: MonoBehaviour
    {
        void Awake()
        {
            timer = new(targetTime, isRealTime, numberOfLoops);
            timer.OnUpdate += OnUpdate.Invoke;
            timer.OnChangeState += OnChangeState.Invoke;
            timer.OnComplete += OnComplete.Invoke;
            if (playOnAwake) timer.Resume();
        }

        void OnDestroy()
        {
            timer.OnUpdate -= OnUpdate.Invoke;
            timer.OnChangeState -= OnChangeState.Invoke;
            timer.OnComplete -= OnComplete.Invoke;
        }

        void Update()
        {
            Tick();
        }
    }
    #endregion

    #region ITimer Implementation
    public partial class TimerComponent: ITimer
    {
        public bool Done => timer.Done;
        public float ElapsedNormal => timer.ElapsedNormal;
        public float Remaining => timer.Remaining;
        public float RemainingNormal => timer.RemainingNormal;
        public float Target => timer.Target;
        public float Elapsed => timer.Elapsed;
        public bool Paused => timer.Paused;
        public bool Zeroed => timer.Zeroed;
        public void Pause() => timer.Pause();
        public void Resume() => timer.Resume();
        public void SetTimeScale(float timeScale) => timer.SetTimeScale(timeScale);
        public void Stop() => timer.Stop();
        public void Tick() => timer.Tick();
    }
    #endregion
}