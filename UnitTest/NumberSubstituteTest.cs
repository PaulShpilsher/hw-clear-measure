using System;
using System.Collections.Generic;
using System.Linq;
using Homework;
using NUnit.Framework;

namespace UnitTest {
    [TestFixture]
    public class NumberSubstituteTest {
        private Numbers _classUnderTest;

        [SetUp]
        public void Init() {
            _classUnderTest = new Numbers();
        }

        [TestCase(-300)]
        [TestCase(-1)]
        [TestCase(0)]
        public void TestInvalidMaxNumber(int invalidMaxNumber) {
            Assert.Throws<ArgumentOutOfRangeException>(() => _classUnderTest.SubstitutedNumbers(invalidMaxNumber, null));
        }

        [Test]
        public void TestNoSubstitutionsNullDictionary() {
            const int maxNumber = 1000;
            var actual = _classUnderTest.SubstitutedNumbers(maxNumber, null);
            var expected = Enumerable.Range(1, maxNumber).Select(x => x.ToString());
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestNoSubstitutionsEmptyDictionary() {
            const int maxNumber = 1000;
            var actual = _classUnderTest.SubstitutedNumbers(maxNumber, new Dictionary<int, string>());
            var expected = Enumerable.Range(1, maxNumber).Select(x => x.ToString());
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestNoSubstitutionsOutsideOfNumberSequenceRange() {
            const int maxNumber = 1000;
            var numberSubstitutions = new Dictionary<int, string>() {
                {2000, "subst2000"},
                {3000, "subst3000"},
                {4000, "subst4000"},
                {5000, "subst5000"},
                {6000, "subst6000"},
                {-2, "subst-2" }
            };

            var actual = _classUnderTest.SubstitutedNumbers(maxNumber, numberSubstitutions);
            var expected = Enumerable.Range(1, maxNumber).Select(x => x.ToString());
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestSubstitutionsGeneralCase() {
            const int maxNumber = 100;
            var numberSubstitutions = new Dictionary<int, string>() {
                {7, "cheese"},
                {3, "fizz"},
                {5, "buzz"},
                {300, "never" },
                {-1, "never" }
            };

            var actual = _classUnderTest.SubstitutedNumbers(maxNumber, numberSubstitutions)
                .ToArray();

            Assert.IsFalse(actual.Any(x => x == "never"));

            for(int i = 0; i < maxNumber; ++i) {
                int num = i + 1;

                if(num % 3 == 0) {
                    if(num % 5 == 0) {
                        Assert.AreEqual("fizzbuzz", actual[i]);
                    } else if(num % 7 == 0) {
                        Assert.AreEqual("cheesefizz", actual[i]);
                    } else {
                        Assert.AreEqual("fizz", actual[i]);
                    }
                }

                if(num % 5 == 0) {
                    if(num % 3 == 0) {
                        Assert.AreEqual("fizzbuzz", actual[i]);
                    } else if(num % 7 == 0) {
                        Assert.AreEqual("cheesebuzz", actual[i]);
                    } else {
                        Assert.AreEqual("buzz", actual[i]);
                    }
                }

                if((num % 7 == 0) && (num % 3 != 0) && (num % 5 != 0)) {
                    Assert.AreEqual("cheese", actual[i]);
                }

                if((num % 7 != 0) && (num % 3 != 0) && (num % 5 != 0)) {
                    Assert.AreEqual(num.ToString(), actual[i]);
                }
            }
        }
    }
}
