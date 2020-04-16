using CalcLib;
using NUnit.Framework;

namespace NUnitTest
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		[TestCase(1, 1, 2)]
		[TestCase(-1, 1, 0)]
		[TestCase(-1.11, -2.22, -3.33)]
		[TestCase(-9999, 1000, -8999)]
		[TestCase(2020, -10.00, 2010)]
		[TestCase(3.1415926, 2, 5.1415926)]
		public void TestPlus(double a, double b, double expected)
		{
			double r = Operators.Plus(a, b);
			Assert.True(expected == r);
		}

		[Test]
		[TestCase(1, 1, 0)]
		[TestCase(-1, 1, -2)]
		[TestCase(-1.11, -2.22, 1.11)]
		[TestCase(-9999, 1000, -10999)]
		[TestCase(2020, -10.00, 2030)]
		[TestCase(3.1415926, 2, 1.1415926)]
		public void TestSub(double a, double b, double expected)
		{
			double r = Operators.Sub(a, b);
			Assert.True(expected == r);
		}


		[Test]
		[TestCase(1, 1, 1)]
		[TestCase(-1, 1, -1)]
		[TestCase(-1.11, -2.22, 2.4642)]
		[TestCase(-9999, 1000, -9999000)]
		[TestCase(2020, -10.00, -20200)]
		[TestCase(3.1415926, 2, 6.2831852)]
		public void TestMul(double a, double b, double expected)
		{
			double r = Operators.Mul(a, b);
			Assert.True(expected == r);
		}

		[Test]
		[TestCase(1, 1, 1)]
		[TestCase(-1, 1, -1)]
		[TestCase(-1.11, -2.22, 0.5)]
		[TestCase(-9999, 1000, -9.999000)]
		[TestCase(2020, -10.00, -202.00)]
		[TestCase(3.1415926, 2, 1.5707963)]
		public void TestDiv(double a, double b, double expected)
		{
			double r = Operators.Div(a, b);
			Assert.True(expected == r);
		}

	}
}