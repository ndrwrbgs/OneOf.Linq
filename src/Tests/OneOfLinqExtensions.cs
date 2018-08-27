namespace Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using OneOf.Linq;

    [TestFixture]
    internal static class OneOfLinqExtensions
    {
        private static class ElementAtTests
        {
            [Test]
            public static void IsNull()
            {
                int[] source = null;

                Assert.Throws<ArgumentNullException>(
                    () => source.ElementAtOrReasonWhyNot(1));
            }

            [Test]
            public static void IsValidList()
            {
                var source = new[] {1};

                var result = source.ElementAtOrReasonWhyNot(0);

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(1, result.Value());
            }

            [Test]
            public static void IsValidCollection()
            {
                var source = new HashSet<int> {1};

                var result = source.ElementAtOrReasonWhyNot(0);

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(1, result.Value());
            }

            [Test]
            public static void IsNegativeList()
            {
                var source = new[] {1};

                var result = source.ElementAtOrReasonWhyNot(-1);

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(IndexOutOfBounds), result.WhyNot());
            }

            [Test]
            public static void IsNegativeCollection()
            {
                var source = new HashSet<int> {1};

                var result = source.ElementAtOrReasonWhyNot(-1);

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(IndexOutOfBounds), result.WhyNot());
            }

            [Test]
            public static void IsOutOfBoundsList()
            {
                var source = new[] {1};

                var result = source.ElementAtOrReasonWhyNot(4);

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(IndexOutOfBounds), result.WhyNot());
            }

            [Test]
            public static void IsOutOfBoundsCollection()
            {
                var source = new HashSet<int> {1};

                var result = source.ElementAtOrReasonWhyNot(4);

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(IndexOutOfBounds), result.WhyNot());
            }
        }

        private static class FirstTests
        {
            [Test]
            public static void IsNull()
            {
                int[] source = null;

                Assert.Throws<ArgumentNullException>(
                    () => source.FirstOrReasonWhyNot());
            }

            [Test]
            public static void IsValidList()
            {
                var source = new[] {1};

                var result = source.FirstOrReasonWhyNot();

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(1, result.Value());
            }

            [Test]
            public static void IsValidCollection()
            {
                var source = new HashSet<int> {1};

                var result = source.FirstOrReasonWhyNot();

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(1, result.Value());
            }

            [Test]
            public static void IsEmptyList()
            {
                var source = new int[] { };

                var result = source.FirstOrReasonWhyNot();

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(NoElements), result.WhyNot());
            }

            [Test]
            public static void IsEmptyCollection()
            {
                var source = new HashSet<int>();

                var result = source.FirstOrReasonWhyNot();

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(NoElements), result.WhyNot());
            }

            [Test]
            public static void HasMoreThanOneList()
            {
                var source = new[] {1, 2};

                var result = source.FirstOrReasonWhyNot();

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(1, result.Value());
            }

            [Test]
            public static void HasMoreThanOneCollection()
            {
                var source = new HashSet<int> {1, 2};

                var result = source.FirstOrReasonWhyNot();

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(1, result.Value());
            }
        }

        private static class FirstWithPredicateTests
        {
            [Test]
            public static void IsNull()
            {
                int[] source = null;

                Assert.Throws<ArgumentNullException>(
                    () => source.FirstOrReasonWhyNot(_ => true));
            }

            [Test]
            public static void PredicateIsNull()
            {
                var source = new[] {1};

                Assert.Throws<ArgumentNullException>(
                    () => source.FirstOrReasonWhyNot(null));
            }

            [Test]
            public static void IsValid()
            {
                var source = new[] {1, 2};

                var result = source.FirstOrReasonWhyNot(i => i == 2);

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(2, result.Value());
            }

            [Test]
            public static void IsEmpty()
            {
                var source = new int[] { };

                var result = source.FirstOrReasonWhyNot(_ => true);

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(NoElements), result.WhyNot());
            }

            [Test]
            public static void HasMoreThanOne()
            {
                var source = new[] {1, 2};

                var result = source.FirstOrReasonWhyNot(i => i > 0);

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(1, result.Value());
            }

            [Test]
            public static void HasNone()
            {
                var source = new[] {1, 2};

                var result = source.FirstOrReasonWhyNot(i => i > 9);

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(NoElements), result.WhyNot());
            }
        }

        private static class LastTests
        {
            [Test]
            public static void IsNull()
            {
                int[] source = null;

                Assert.Throws<ArgumentNullException>(
                    () => source.LastOrReasonWhyNot());
            }

            [Test]
            public static void IsValidList()
            {
                var source = new[] {1};

                var result = source.LastOrReasonWhyNot();

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(1, result.Value());
            }

            [Test]
            public static void IsValidCollection()
            {
                var source = new HashSet<int> {1};

                var result = source.LastOrReasonWhyNot();

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(1, result.Value());
            }

            [Test]
            public static void IsEmptyList()
            {
                var source = new int[] { };

                var result = source.LastOrReasonWhyNot();

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(NoElements), result.WhyNot());
            }

            [Test]
            public static void IsEmptyCollection()
            {
                var source = new HashSet<int>();

                var result = source.LastOrReasonWhyNot();

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(NoElements), result.WhyNot());
            }

            [Test]
            public static void HasMoreThanOneList()
            {
                var source = new[] {1, 2};

                var result = source.LastOrReasonWhyNot();

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(2, result.Value());
            }

            [Test]
            public static void HasMoreThanOneCollection()
            {
                var source = new HashSet<int> {1, 2};

                var result = source.LastOrReasonWhyNot();

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(2, result.Value());
            }
        }

        private static class LastWithPredicateTests
        {
            [Test]
            public static void IsNull()
            {
                int[] source = null;

                Assert.Throws<ArgumentNullException>(
                    () => source.LastOrReasonWhyNot(_ => true));
            }

            [Test]
            public static void PredicateIsNull()
            {
                var source = new[] {1};

                Assert.Throws<ArgumentNullException>(
                    () => source.LastOrReasonWhyNot(null));
            }

            [Test]
            public static void IsValid()
            {
                var source = new[] {1, 2};

                var result = source.LastOrReasonWhyNot(i => i == 2);

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(2, result.Value());
            }

            [Test]
            public static void IsEmpty()
            {
                var source = new int[] { };

                var result = source.LastOrReasonWhyNot(_ => true);

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(NoElements), result.WhyNot());
            }

            [Test]
            public static void HasMoreThanOne()
            {
                var source = new[] {1, 2};

                var result = source.LastOrReasonWhyNot(i => i > 0);

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(2, result.Value());
            }

            [Test]
            public static void HasNone()
            {
                var source = new[] {1, 2};

                var result = source.LastOrReasonWhyNot(i => i > 9);

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(NoElements), result.WhyNot());
            }
        }

        private static class SingleTests
        {
            [Test]
            public static void IsNull()
            {
                int[] source = null;

                Assert.Throws<ArgumentNullException>(
                    () => source.SingleOrReasonWhyNot());
            }

            [Test]
            public static void IsValidList()
            {
                var source = new[] {1};

                var result = source.SingleOrReasonWhyNot();

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(1, result.Value());
            }

            [Test]
            public static void IsValidCollection()
            {
                var source = new HashSet<int> {1};

                var result = source.SingleOrReasonWhyNot();

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(1, result.Value());
            }

            [Test]
            public static void IsEmptyList()
            {
                var source = new int[] { };

                var result = source.SingleOrReasonWhyNot();

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(NoElements), result.WhyNot());
            }

            [Test]
            public static void IsEmptyCollection()
            {
                var source = new HashSet<int>();

                var result = source.SingleOrReasonWhyNot();

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(NoElements), result.WhyNot());
            }

            [Test]
            public static void HasMoreThanOneList()
            {
                var source = new[] {1, 2};

                var result = source.SingleOrReasonWhyNot();

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(MoreThanOneElement), result.WhyNot());
            }

            [Test]
            public static void HasMoreThanOneCollection()
            {
                var source = new HashSet<int> {1, 2};

                var result = source.SingleOrReasonWhyNot();

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(MoreThanOneElement), result.WhyNot());
            }
        }

        private static class SingleWithPredicateTests
        {
            [Test]
            public static void IsNull()
            {
                int[] source = null;

                Assert.Throws<ArgumentNullException>(
                    () => source.SingleOrReasonWhyNot(_ => true));
            }

            [Test]
            public static void PredicateIsNull()
            {
                var source = new[] {1};

                Assert.Throws<ArgumentNullException>(
                    () => source.SingleOrReasonWhyNot(null));
            }

            [Test]
            public static void IsValid()
            {
                var source = new[] {1, 2};

                var result = source.SingleOrReasonWhyNot(i => i == 2);

                Assert.IsTrue(result.HasValue());
                Assert.AreEqual(2, result.Value());
            }

            [Test]
            public static void IsEmpty()
            {
                var source = new int[] { };

                var result = source.SingleOrReasonWhyNot(_ => true);

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(NoElements), result.WhyNot());
            }

            [Test]
            public static void HasMoreThanOne()
            {
                var source = new[] {1, 2};

                var result = source.SingleOrReasonWhyNot(i => i > 0);

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(MoreThanOneElement), result.WhyNot());
            }

            [Test]
            public static void HasNone()
            {
                var source = new[] {1, 2};

                var result = source.SingleOrReasonWhyNot(i => i > 9);

                Assert.IsFalse(result.HasValue());
                Assert.AreEqual(default(NoElements), result.WhyNot());
            }
        }
    }
}