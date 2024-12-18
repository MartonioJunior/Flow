using System.Collections;
using NUnit.Framework;
using MartonioJunior.Flow;
using static MartonioJunior.Test.Suite;
using MartonioJunior.Test;

namespace Tests.MartonioJunior.Flow
{
    #region Mock
    public class TimeScalableMock: ITimeScalable
    {
        public float? TimeScale { get; private set; }

        public void SetTimeScale(float timeScale)
        {
            TimeScale = timeScale;
        }

        public void Tick() {}
    }
    #endregion

    #region Model Implementation
    public partial class ITimeScalable_Tests: Model<TimeScalableMock>
    {
        public override void CreateTestContext()
        {
            modelReference = new TimeScalableMock();
        }

        public override void DestroyTestContext()
        {
            modelReference = null;
        }
    }
    #endregion

    #region Method Tests
    public partial class ITimeScalable_Tests
    {
        public static IEnumerable FastForward_UseCases()
        {
            var positiveValue = Range(1,1000,15);

            yield return new TestCaseData(positiveValue, positiveValue);
            yield return new TestCaseData(0, 0);
            yield return new TestCaseData(-positiveValue, 0);
        }
        [TestCaseSource(nameof(FastForward_UseCases))]
        public void FastForward_SetsPositiveTimescaleToObject(int suggestedScale, int expectedResult)
        {
            modelReference.FastForward(suggestedScale);

            Assert.AreEqual(expectedResult, modelReference.TimeScale);
        }

        public static IEnumerable Rewind_UseCases()
        {
            var positiveValue = Range(1,1000,15);

            yield return new TestCaseData(positiveValue, -positiveValue);
            yield return new TestCaseData(0, 0);
            yield return new TestCaseData(-positiveValue, -positiveValue);
        }
        [TestCaseSource(nameof(Rewind_UseCases))]
        public void Rewind_SetsNegativeTimescaleToObject(int suggestedScale, int expectedResult)
        {
            modelReference.Rewind(suggestedScale);

            Assert.AreEqual(expectedResult, modelReference.TimeScale);
        }
    }
    #endregion
}