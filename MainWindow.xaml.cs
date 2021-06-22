using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double num1 = 0, num2 = 0;
        string operation = String.Empty;
        double inputValueForExpressionOnResult = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumButton_Click(object sender, RoutedEventArgs e)
        {
            // If some operation already done before then add that result in expression
            if (inputValueForExpressionOnResult != 0)
            {
                expression.Text = inputValueForExpressionOnResult.ToString();
                inputValueForExpressionOnResult = 0;
            }
            string enteredInput = (String)((Button)e.OriginalSource).Content;
            // To remove starting zero
            if (input.Text == "0")
                input.Text = "";
            input.AppendText(enteredInput);
            expression.AppendText(enteredInput);
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            // If some operation already done before then add that result in expression
            if (inputValueForExpressionOnResult != 0)
            {
                expression.Text = inputValueForExpressionOnResult.ToString();
                inputValueForExpressionOnResult = 0;
            }
            string opr = (String)((Button)e.OriginalSource).Content;
            expression.AppendText(opr);
            // To handle negative num1
            if (input.Text == "0")
            {
                input.Text = "";
                input.AppendText(opr);
            }
            // To handle double operator error
            else if (input.Text == "+" || input.Text == "-" || input.Text == "*" || input.Text == "/")
            {
                MessageBox.Show("Not Allowed", "Warning");
                ResetExpressionAndInput();
            }
            else
            {
                // To get num1
                try
                {
                    num1 = Convert.ToDouble(input.Text);
                }
                catch (SystemException)
                {
                    MessageBox.Show("Not Allowed", "Warning");
                    ResetExpressionAndInput();
                }
                input.Text = "0";
                operation = opr;
            }
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            // To get num2
            try
            {
                num2 = Convert.ToDouble(input.Text);
            }
            catch (SystemException)
            {
                MessageBox.Show("Not Allowed", "Warning");
                ResetExpressionAndInput();
            }

            expression.AppendText("=");
            double answer = 0;
            if (operation == "+")
                answer = num1 + num2;
            else if (operation == "-")
                answer = num1 - num2;
            else if (operation == "*")
                answer = num1 * num2;
            else if (operation == "/")
            {
                if (num2 == 0)
                    MessageBox.Show("Division by Zero is not Allowed", "Warning");
                else
                    answer = num1 / num2;
            }
            input.Text = answer.ToString();
            inputValueForExpressionOnResult = answer;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetExpressionAndInput();
            inputValueForExpressionOnResult = 0;
        }

        private void ResetExpressionAndInput()
        {
            input.Text = "0";
            expression.Text = "";
            inputValueForExpressionOnResult = 0;
        }
    }
}
