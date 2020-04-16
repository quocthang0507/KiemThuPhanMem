using CalcLib;
using Xunit;

namespace XUnitTest
{
	public class TestLib
	{
		[Theory]
		[InlineData(1, 1, 2)]
		[InlineData(-1, 1, 0)]
		[InlineData(-1.11, -2.22, -3.33)]
		[InlineData(-9999, 1000, -8999)]
		[InlineData(2020, -10.00, 2010)]
		[InlineData(3.1415926, 2, 5.1415926)]
		public void TestPlus(double a, double b, double expected)
		{
			double r = Operators.Plus(a, b);
			Assert.True(expected == r);
		}

		[Theory]
		[InlineData(1, 1, 0)]
		[InlineData(-1, 1, -2)]
		[InlineData(-1.11, -2.22, 1.11)]
		[InlineData(-9999, 1000, -10999)]
		[InlineData(2020, -10.00, 2030)]
		[InlineData(3.1415926, 2, 1.1415926)]
		public void TestSub(double a, double b, double expected)
		{
			double r = Operators.Sub(a, b);
			Assert.True(expected == r);
		}


		[Theory]
		[InlineData(1, 1, 1)]
		[InlineData(-1, 1, -1)]
		[InlineData(-1.11, -2.22, 2.4642)]
		[InlineData(-9999, 1000, -9999000)]
		[InlineData(2020, -10.00, -20200)]
		[InlineData(3.1415926, 2, 6.2831852)]
		public void TestMul(double a, double b, double expected)
		{
			double r = Operators.Mul(a, b);
			Assert.True(expected == r);
		}

		[Theory]
		[InlineData(1, 1, 1)]
		[InlineData(-1, 1, -1)]
		[InlineData(-1.11, -2.22, 0.5)]
		[InlineData(-9999, 1000, -9.999000)]
		[InlineData(2020, -10.00, -202.00)]
		[InlineData(3.1415926, 2, 1.5707963)]
		public void TestDiv(double a, double b, double expected)
		{
			double r = Operators.Div(a, b);
			Assert.True(expected == r);
		}
	}
}
