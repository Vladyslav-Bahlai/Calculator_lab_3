using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicInterface
{
    public partial class Form1 : Form
    {
        long buffer = 0;
        private AnalyzerClass.AnalyzerClass analyzer = new AnalyzerClass.AnalyzerClass();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        public Form1() {
            InitializeComponent();

            AllocConsole();
            string expression = "";
            string[] argum = Environment.GetCommandLineArgs();

            if (argum.Length < 2) return;

            for (int i = 1; i < argum.Length; i++) {
                expression += argum[i];
            }
            Console.WriteLine(expression);

            try {
                string res = analyzer.Estimate(expression);
                Console.WriteLine(res);
            } catch (Exception exception) {
                string errorMessage = analyzer.lastErrorMsg == "" ? exception.Message : analyzer.lastErrorMsg;
                Console.WriteLine(errorMessage);
            }
        }

        private void Evaluate() {
            try {
                resultTextbox.Text = analyzer.Estimate(expressionTextbox.Text);

            } catch (Exception ex) {
                resultTextbox.Text = ex.Message;
            }

        }

        private void symbolBtn_click(object sender, EventArgs e) {
            expressionTextbox.Text += ((Button)sender).Text;
        }

        private void backspaceBtn_click(object sender, EventArgs e) {
            if (expressionTextbox.Text.Length > 3) {
                if (expressionTextbox.Text.Substring(expressionTextbox.Text.Length - 3, 3) == "mod") {
                    expressionTextbox.Text = expressionTextbox.Text.Remove(expressionTextbox.Text.Length - 3);
                    return;
                }
            }
            if (expressionTextbox.Text.Length > 0)
                expressionTextbox.Text = expressionTextbox.Text.Remove(expressionTextbox.Text.Length - 1);
        }

        private void clearBtn_click(object sender, EventArgs e) {
            expressionTextbox.Text = "";
            resultTextbox.Text = "";
        }

        private void plusMinusBtn_click(object sender, EventArgs e) {
            if (expressionTextbox.Text.Length == 0) {
                expressionTextbox.Text += 'm';
                return;
            }

            switch (expressionTextbox.Text.Last()) {
                case 'p':
                    expressionTextbox.Text = expressionTextbox.Text.Remove(expressionTextbox.Text.Length - 1) + 'm';
                    break;
                case 'm':
                    expressionTextbox.Text = expressionTextbox.Text.Remove(expressionTextbox.Text.Length - 1) + 'p';
                    break;
                default:
                    expressionTextbox.Text += 'm';
                    break;
            }
        }

        private void equalBtn_click(object sender, EventArgs e) {
            Evaluate();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
            switch ((int)e.KeyChar) {
                case 0x1B:
                    Close();
                    break;
                case 0x0D:
                    Evaluate();
                    break;
            }
        }

        private void mrBtn_click(object sender, EventArgs e) {
            expressionTextbox.Text += buffer.ToString();
        }

        private void mPlusBtn_click(object sender, EventArgs e) {
            long tmp = 0;
            if (long.TryParse(resultTextbox.Text, out tmp))
                buffer += tmp;
            else
                resultTextbox.Text = "Cannot do this :(";
        }

        private void mcBtn_click(object sender, EventArgs e) {
            buffer = 0;
        }

    }
}
