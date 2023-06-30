using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Flow;
using static MartonioJunior.Test.Suite;
using NSubstitute;
using MartonioJunior.Test;

namespace Tests.MartonioJunior.Flow
{
    public class ITimeScalable_Tests: SubstituteModel<ITimeScalable>
    {
        #region Method Tests
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

            modelReference.Received(1).SetTimeScale(expectedResult);
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

            modelReference.Received(1).SetTimeScale(expectedResult);
        }
        #endregion
    }
}