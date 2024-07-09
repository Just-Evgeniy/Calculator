using System;
using System.Data;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Calculator : Form
    {
        private bool newMathExample = false;

        public Calculator()
        {
            InitializeComponent();
            RegisterClick();
        }

        private void btNumber_Click(object sender, EventArgs e)
        {
            if (newMathExample)
            {
                textBox1.Text = "0";
            }

            if (sender is Button btNumber)
            {
                if (textBox1.Text == "0")
                {
                    textBox1.Text = btNumber.Text;
                }
                else
                {
                    textBox1.Text += btNumber.Text;
                }
                newMathExample = false;
            }
        }

        private void RegisterClick()
        {
            foreach (var control in Controls)
            {
                if (control is Button button && button.Name.StartsWith("btNumber"))
                {
                    button.Click += btNumber_Click;
                }
            }

            btDot.Click += btNumber_Click;
        }

        private void btComputationOperations_Click(object sender, EventArgs e)
        {
            if (sender is Button btComputationOperations)
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    if (btComputationOperations.Text == "√")
                    {
                        textBox1.Text += btComputationOperations.Text;
                    }
                    return;
                }

                char lastChar = textBox1.Text[textBox1.Text.Length - 1];
                if (lastChar == '+' || lastChar == '-' || lastChar == '*' || lastChar == '/')
                {
                    return;
                }

                textBox1.Text += btComputationOperations.Text;
            }
        }

        private void btErase_Click(object sender, EventArgs e)
        {
            if (sender is Button btClear)
            {
                if (btClear.Text == "AC")
                {
                    textBox1.Text = "0";
                }
                else if (btClear.Text == "C" && !string.IsNullOrEmpty(textBox1.Text))
                {
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                    if (string.IsNullOrEmpty(textBox1.Text))
                    {
                        textBox1.Text = "0";
                    }
                }
            }
        }

        private void btEquals_Click(object sender, EventArgs e)
        {
            try
            {
                object result = new DataTable().Compute(textBox1.Text, null);
                textBox1.Text = result.ToString();
                newMathExample = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}