using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Flow;
using static Tests.Suite;

namespace Tests.MartonioJunior.Flow
{
    public class TimeUtility_Tests: TestModel
    {
        #region TestModel Implementation
        public override void CreateTestContext() {}

        public override void DestroyTestContext() {}
        #endregion
        #region Method Tests
        public static IEnumerable SourceDeltaTime_UseCases()
        {
            yield return new object[2]{true, Time.deltaTime};
            yield return new object[2]{false, Time.unscaledDeltaTime};
        }
        [TestCaseSource(nameof(SourceDeltaTime_UseCases))]
        public void SourceDeltaTime_ReturnsExpectedDeltaTime(bool affectedByTimescale, float output)
        {
            var result = TimeUtility.SourceDeltaTime(affectedByTimescale);

            Assert.AreEqual(output, result.Invoke());
        }

        public static IEnumerable SourceFixedDeltaTime_UseCases()
        {
            yield return new object[2]{true, Time.fixedDeltaTime};
            yield return new object[2]{false, Time.unscaledDeltaTime};
        }
        [TestCaseSource(nameof(SourceFixedDeltaTime_UseCases))]
        public void SourceFixedDeltaTime_ReturnsExpectedFixedDeltaTime(bool affectedByTimescale, float output)
        {
            var result = TimeUtility.SourceFixedDeltaTime(affectedByTimescale);

            Assert.AreEqual(output, result.Invoke());
        }

        public static IEnumerable SourceGlobalTime_UseCases()
        {
            yield return new object[3]{true, true, Time.realtimeSinceStartup};
            yield return new object[3]{true, false, Time.realtimeSinceStartup};
            yield return new object[3]{false, true, Time.time};
            yield return new object[3]{false, false, Time.unscaledTime};
        }
        [TestCaseSource(nameof(SourceGlobalTime_UseCases))]
        public void SourceGlobalTime_ReturnsExpectedGlobalTime(bool affectedByTimescale, bool useSystemTime, double output)
        {
            var result = TimeUtility.SourceGlobalTime(affectedByTimescale, useSystemTime);

            Assert.AreEqual(output, result.Invoke());
        }

        public static IEnumerable SourceGlobalTimeDouble_UseCases()
        {
            yield return new object[3]{true, true, Time.realtimeSinceStartupAsDouble};
            yield return new object[3]{true, false, Time.realtimeSinceStartupAsDouble};
            yield return new object[3]{false, true, Time.timeAsDouble};
            yield return new object[3]{false, false, Time.unscaledTimeAsDouble};
        }
        [TestCaseSource(nameof(SourceGlobalTimeDouble_UseCases))]
        public void SourceGlobalTimeDouble_ReturnsExpectedGlobalTime(bool affectedByTimescale, bool useSystemTime, double output)
        {
            var result = TimeUtility.SourceGlobalTimeDouble(affectedByTimescale, useSystemTime);

            Assert.AreEqual(output, result.Invoke());
        }

        public static IEnumerable SourceTimeSinceLevelLoad_UseCases()
        {
            yield return new object[1]{Time.timeSinceLevelLoad};
        }
        [TestCaseSource(nameof(SourceTimeSinceLevelLoad_UseCases))]
        public void SourceTimeSinceLevelLoad_ReturnsTimeSinceLastLoad(float output)
        {
            var result = TimeUtility.SourceTimeSinceLevelLoad();

            Assert.AreEqual(output, result.Invoke());
        }
        #endregion
    }
}