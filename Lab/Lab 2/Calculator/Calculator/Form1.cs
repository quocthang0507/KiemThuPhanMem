using CalcLib;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Calculator
{
	public partial class Form1 : Form
	{
		private double num1 = 0;
		private string opr = "";
		private double num2 = 0;

		public Form1()
		{
			InitializeComponent();
			AddClickEvents();
		}

		private void AddClickEvents()
		{
			foreach (var control in this.Controls)
			{
				if (control is Button)
				{
					var ctrl = control as Button;
					string text = ctrl.Text;
					if (text.All(char.IsDigit))
						ctrl.Click += Number_Click;
					else if (text != "=" && text != "C" && text != ".")
						ctrl.Click += Operator_Click;
				}
			}
		}

		private void Number_Click(object sender, EventArgs e)
		{
			tbxResult.Text += (sender as Control).Text;
		}

		private void Operator_Click(object sender, EventArgs e)
		{
			if (!double.TryParse(tbxResult.Text, out num1))
				num1 = 0;
			opr = (sender as Control).Text;
			tbxResult.Text = "";
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			tbxResult.Text = "";
			opr = "";
			num1 = 0;
			num2 = 0;
		}

		private void btnDecimal_Click(object sender, EventArgs e)
		{
			if (!tbxResult.Text.Contains('.'))
				tbxResult.Text += ".";
		}

		private void btnEqual_Click(object sender, EventArgs e)
		{
			string result = "";
			if (!double.TryParse(tbxResult.Text, out num2))
				num2 = 0;
			switch (opr)
			{
				case "+":
					result = Operators.Plus(num1, num2).ToString();
					break;
				case "-":
					result = Operators.Sub(num1, num2).ToString();
					break;
				case "*":
					result = Operators.Mul(num1, num2).ToString();
					break;
				case "/":
					result = Operators.Div(num1, num2).ToString();
					break;
				default:
					break;
			}
			tbxResult.Text = result;
		}
	}
}
