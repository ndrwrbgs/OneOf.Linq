# OneOf.Linq

Extensions for https://github.com/mcintyre321/OneOf that expose `XOrResonWhyNot` extension methods for LINQ.

# Demonstration

Without library:
```C#
var myList1 = new object[] { 1, null };
var myList2 = new object[] { };

object last1 = myList1.LastOrDefault();
object last2 = myList2.LastOrDefault();

// Oh no! This fails because both are null!
// Even worse when you have value types (like int) and
// can't tell the difference between 0 and 'no-values'
Assert.AreNotEqual(last1, last2, "because one had a last and one did not");
```

With library:
```C#
var myList1 = new object[] { 1, null };
var myList2 = new object[] { };

OneOf<object, NoElements> last1 = myList1.LastOrReasonWhyNot();
OneOf<object, NoElements> last2 = myList2.LastOrReasonWhyNot();

// Yay! it works!
Assert.AreNotEqual(last1, last2, "because one had a last and one did not");

Assert.IsTrue(last1.HasValue());
Assert.IsFalse(last2.HasValue());

Assert.AreEqual(null, last1.Value());
Assert.AreEqual(default(NoElements), last2.WhyNot());
```
