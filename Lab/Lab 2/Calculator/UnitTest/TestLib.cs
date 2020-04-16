using System;
using System.Collections.Generic;
using CalcLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class TestLib
	{
		private List<TestClass> plusData = new List<TestClass>()
		{
			new TestClass(1, 1, 2), new TestClass(-1, 1, 0), new TestClass(-1.11, -2.22, -3.33),
			new TestClass(-9999, 1000, -8999), new TestClass(2020, -10.0000, 2010), new TestClass(3.1415926, 2, 5.1415926),
		};

		private List<TestClass> subData = new List<TestClass>()
		{
			new TestClass(1, 1, 0), new TestClass(-1, 1, -2), new TestClass(-1.11, -2.22, 1.11),
			new TestClass(-9999, 1000, -10999), new TestClass(2020, -10.0000, 2030), new TestClass(3.1415926, 2, 1.1415926),
		};

		private List<TestClass> mulData = new List<TestClass>()
		{
			new TestClass(1, 1, 1), new TestClass(-1, 1, -1), new TestClass(-1.11, -2.22, 2.4642),
			new TestClass(-9999, 1000, -9999000), new TestClass(2020, -10.0000, -20200), new TestClass(3.1415926, 2, 6.2831852),
		};

		private List<TestClass> divData = new List<TestClass>()
		{
			new TestClass(1, 1, 1), new TestClass(-1, 1, -1), new TestClass(-1.11, -2.22, 0.5),
			new TestClass(-9999, 1000, -9.999), new TestClass(2020, -10.0000, -202), new TestClass(3.1415926, 2, 1.5707963),
		};

		[TestMethod]
		public void TestPlus()
		{
			foreach (var item in plusData)
			{
				double r = Operators.Plus(item.a, item.b);
				Assert.AreEqual(item.result, r);
			}
		}

		[TestMethod]
		public void TestSub()
		{
			foreach (var item in subData)
			{
				double r = Operators.Sub(item.a, item.b);
				Assert.AreEqual(item.result, r);
			}
		}

		[TestMethod]
		public void TestMul()
		{
			foreach (var item in mulData)
			{
				double r = Operators.Mul(item.a, item.b);
				Assert.AreEqual(item.result, r);
			}
		}

		[TestMethod]
		public void TestDiv()
		{
			foreach (var item in divData)
			{
				double r = Operators.Div(item.a, item.b);
				Assert.AreEqual(item.result, r);
			}
		}

	}
}
