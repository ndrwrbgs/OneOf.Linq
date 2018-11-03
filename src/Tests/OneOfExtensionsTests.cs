namespace Tests
{
    using System;
    using NUnit.Framework;
    using OneOf;
    using OneOf.Linq;

    [TestFixture]
    internal static class OneOfExtensionsTests
    {
        private static class IsSuccessTests
        {
            [Test]
            public static void IsSuccess2()
            {
                var value = OneOf<int, NoElements>.FromT0(1);

                Assert.IsTrue(value.IsSuccess());
            }

            [Test]
            public static void IsSuccess3()
            {
                var value = OneOf<int, NoElements, NoElements>.FromT0(1);

                Assert.IsTrue(value.IsSuccess());
            }

            [Test]
            public static void DoesNotHaveValue2()
            {
                var value = OneOf<int, NoElements>.FromT1(default(NoElements));

                Assert.IsFalse(value.IsSuccess());
            }

            [Test]
            public static void DoesNotHaveValue3()
            {
                var value = OneOf<int, NoElements, NoElements>.FromT1(default(NoElements));

                Assert.IsFalse(value.IsSuccess());
            }

            //[Test] Fails presently, pending https://github.com/mcintyre321/OneOf/issues/29
            public static void IsDefault2()
            {
                var value = default(OneOf<int, NoElements>);
                Assert.IsFalse(value.IsT0);
                Assert.IsFalse(value.IsT1);
                Assert.AreNotEqual(0, value.Value);
                Assert.IsFalse(value.IsSuccess());
            }

            //[Test] Fails presently, pending https://github.com/mcintyre321/OneOf/issues/29
            public static void IsDefault3()
            {
                var value = default(OneOf<int, NoElements, NoElements>);
                Assert.IsFalse(value.IsT0);
                Assert.IsFalse(value.IsT1);
                Assert.AreNotEqual(0, value.Value);
                Assert.IsFalse(value.IsSuccess());
            }
        }

        private static class ValueTests
        {
            [Test]
            public static void IsSuccess2()
            {
                var value = OneOf<int, NoElements>.FromT0(1);

                int dotValue = value.Value();
                // Versus the built in
                object regularValue = value.Value;

                Assert.AreEqual(1, dotValue);
                Assert.AreEqual(regularValue, dotValue);
            }

            [Test]
            public static void IsSuccess3()
            {
                var value = OneOf<int, NoElements, NoElements>.FromT0(1);

                int dotValue = value.Value();
                // Versus the built in
                object regularValue = value.Value;

                Assert.AreEqual(1, dotValue);
                Assert.AreEqual(regularValue, dotValue);
            }

            [Test]
            public static void DoesNotHaveValue2()
            {
                var value = OneOf<int, NoElements>.FromT1(default(NoElements));

                Assert.Throws<InvalidOperationException>(
                    () => value.Value());
                //Fails presently, pending https://github.com/mcintyre321/OneOf/issues/29
                //Assert.Throws<InvalidOperationException>(
                //    () =>
                //    {
                //        var _ = value.Value;
                //    }, "because parity");
            }

            [Test]
            public static void DoesNotHaveValue3()
            {
                var value = OneOf<int, NoElements, NoElements>.FromT1(default(NoElements));

                Assert.Throws<InvalidOperationException>(
                    () => value.Value());
                //Fails presently, pending https://github.com/mcintyre321/OneOf/issues/29
                //Assert.Throws<InvalidOperationException>(
                //    () =>
                //    {
                //        var _ = value.Value;
                //    }, "because parity");
            }
        }

        private static class WhyNotTests
        {
            [Test]
            public static void IsSuccess2()
            {
                var value = OneOf<int, NoElements>.FromT0(1);

                Assert.Throws<InvalidOperationException>(
                    () => value.WhyNot());
                Assert.Throws<InvalidOperationException>(
                    () =>
                    {
                        var _ = value.AsT1;
                    }, "because parity");
            }

            [Test]
            public static void IsSuccess3()
            {
                var value = OneOf<int, NoElements, NoElements>.FromT0(1);

                Assert.Throws<InvalidOperationException>(
                    () => value.WhyNot());
                Assert.Throws<InvalidOperationException>(
                    () =>
                    {
                        var _ = value.AsT1;
                    }, "because parity");
            }

            [Test]
            public static void IsFirstWhyNot2()
            {
                var value = OneOf<int, NoElements>.FromT1(default(NoElements));

                var whyNot = value.WhyNot();

                Assert.IsTrue(whyNot is NoElements);
            }

            [Test]
            public static void IsFirstWhyNot3()
            {
                var value = OneOf<int, NoElements, MoreThanOneElement>.FromT1(default(NoElements));

                var whyNot = value.WhyNot();

                Assert.IsTrue(whyNot is NoElements);
            }

            [Test]
            public static void IsSecondWhyNot3()
            {
                var value = OneOf<int, NoElements, MoreThanOneElement>.FromT2(default(MoreThanOneElement));

                var whyNot = value.WhyNot();

                Assert.IsTrue(whyNot is MoreThanOneElement);
            }
        }
    }
}