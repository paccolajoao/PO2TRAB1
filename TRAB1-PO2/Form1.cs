using System.Windows.Forms;
using info.lundin.math;
using System;

namespace TRAB1_PO2
{
    public partial class Form1 : Form
    {

        string func;
        double a, b, epsilon;

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (!VerificaEntrada())
            {
                MessageBox.Show("Algum espaço está vazio ou algum dado está inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            func = textBox1.Text;
            a = Convert.ToDouble(textBox4.Text);
            b = Convert.ToDouble(textBox3.Text);
            epsilon = Convert.ToDouble(textBox2.Text);

            if (radioButton1.Checked)
                MetodoFibonacci();
        }

        public Form1()
        {
            InitializeComponent();

            
        }

        public bool VerificaEntrada()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")

            {
                return false;
            }

            else return true;
        }

        public static int Fibonacci(int n)
        {
            int a = 0, b = 1, c = 0;

            for (int i = 0; i < n; i++)
            {
                c = a + b;
                a = b;
                b = c;
            }

            return c;
        }

        public void MetodoFibonacci()
        {
            int kmax, n=0,k=0, i;
            double fn, u, y, fu, fy, div;

            ExpressionParser parser = new ExpressionParser(); //Interpretador
            if (func.Contains("e"))
            {
                parser.Values.Add("e", Math.E);
            }

            fn = (b - a) / epsilon;
            
            for (i = 0; (Fibonacci(i)) < fn; i++)
            {
                n = i;
            }

            kmax = n;
            n++;
            div = Fibonacci(n - k);

            u = a + (((Fibonacci(n - k - 2)) / div) * (b - a));
            y = a + (((Fibonacci(n - k - 1)) / div) * (b - a));

            parser.Values.Add("x", u);
            fu = parser.Parse(func);

            parser.Values["x"].SetValue(y);
            fy = parser.Parse(func);

            while ( ((b-a) > epsilon) && (k < kmax))

            {
                if (fu > fy)
                {
                    a = u;
                    u = y;
                    fu = fy;
                    div = Fibonacci(n - k);
                    y = a + (((Fibonacci(n - k - 1)) / div) * (b - a));
                    parser.Values["x"].SetValue(y);
                    fy = parser.Parse(func);
                }
                else
                {
                    b = y;
                    y = u;
                    fy = fu;
                    div = Fibonacci(n - k);
                    u = a + (((Fibonacci(n - k - 2)) / div) * (b - a));
                    parser.Values["x"].SetValue(u);
                    fu = parser.Parse(func);
                }

              k++;
            }

    /*
            MessageBox.Show(a.ToString());
            MessageBox.Show(b.ToString());
            MessageBox.Show(u.ToString());
            MessageBox.Show(y.ToString());
            MessageBox.Show(fu.ToString());
            MessageBox.Show(fy.ToString());
            */

             
        }


    }
}
