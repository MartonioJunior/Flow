using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Flow;
using static MartonioJunior.Test.Suite;
using MartonioJunior.Test;
using NSubstitute;

namespace Tests.MartonioJunior.Flow
{
    public class Cronometer_Tests: Model<Cronometer>
    {
        #region Constants
        ITicker ticker;
        #endregion
        #region Model Implementation
        public override void CreateTestContext()
        {
            modelReference = new Cronometer(Substitute(out ticker));
        }

        public override void DestroyTestContext()
        {
            modelReference = null;
            ticker = null;
        }
        #endregion
        #region Method Tests
        public static IEnumerable Pause_UseCases()
        {
            yield return Case(0, false, 0);
            yield return Case(Range(1,1000,20), true, 1);
        }
        [TestCaseSource(nameof(Pause_UseCases))]
        public void Pause_StopsCronometer(int numberOfCalls, bool endState, int numberOfTriggers)
        {
            int triggerCount = 0;
            modelReference.OnChangeState += _ => triggerCount++;

            for (int i = 0; i < numberOfCalls; i++) modelReference.Pause();

            Assert.AreEqual(endState, modelReference.Paused);
            Assert.AreEqual(numberOfTriggers, triggerCount);
        }

        public static IEnumerable Resume_UseCases()
        {
            yield return Case(false, 0, false, 0);
            yield return Case(false, Range(1,1000,19), false, 0);
            yield return Case(true, Range(1,1000,20), false, 1);
        }
        [TestCaseSource(nameof(Resume_UseCases))]
        public void Resume_SetsCronometerAsActiveAgain(bool pauseBeforehand, int numberOfCalls, bool endState, int numberOfTriggers)
        {
            if (pauseBeforehand) modelReference.Pause();
            int triggerCount = 0;
            modelReference.OnChangeState += _ => triggerCount++;

            for (int i = 0; i < numberOfCalls; i++) modelReference.Resume();

            Assert.AreEqual(endState, modelReference.Paused);
            Assert.AreEqual(numberOfTriggers, triggerCount);
            ticker.Received(numberOfTriggers).UpdateTimeMarkers();
        }

        public static IEnumerable SetTimeScale_UseCases()
        {
            float value = Range(-1000,1000,1);
            yield return Case(value, value);
        }
        [TestCaseSource(nameof(SetTimeScale_UseCases))]
        public void SetTimeScale_DefinesCronometerSpeed(float timeScale, float expectedValue)
        {
            modelReference.SetTimeScale(timeScale);

            Assert.AreEqual(expectedValue, modelReference.TimeScale);
        }

        public static IEnumerable Stop_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Stop_UseCases))]
        public void Stop_ReturnsCronometerBackToZero()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable Tick_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Tick_UseCases))]
        public void Tick_UpdatesCronometer()
        {
            Assert.Ignore(NotImplemented);
        }
        public static IEnumerable New_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(New_UseCases))]
        public void New_CreatesNewCronometer()
        {
            Assert.Ignore(NotImplemented);
        }
        #endregion
    }
}