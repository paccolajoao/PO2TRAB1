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

            if ( a > b)
            {
                MessageBox.Show("O valor de a não pode ser maior que o de b!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ( epsilon < 0)
            {
                MessageBox.Show("O valor de epsilon não pode ser negativo!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (radioButton1.Checked)
                MetodoFibonacci();
            else if (radioButton4.Checked)
                Bissessao();


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


            div = (a + b) / 2;

            textBox5.Text = div.ToString();
            parser.Values["x"].SetValue(div);
            div = parser.Parse(func);
            textBox6.Text = div.ToString();

        }

        // Calcula derivada de x
        public double Derivada (double x)
        {
            double d = 0;
            double h = 1000 * epsilon;
            bool achou = false;
            double p = 0, q;
            int it = 0;
            double erro = 1000000;
            int maxIt = 100;
   

            ExpressionParser parser = new ExpressionParser(); //Interpretador
            if (func.Contains("e"))
            {
                parser.Values.Add("e", Math.E);
            }

            parser.Values.Add("x", x + h);
            double fMaisH = parser.Parse(func);
            parser.Values["x"].SetValue(x - h);
            double fMenosH = parser.Parse(func);


            p = (fMaisH - fMenosH) / (2 * h);

            for(it = 0; it < maxIt && !achou; it++)
            {
                q = p;
                h = h / 2;
                parser.Values["x"].SetValue(x + h);
                fMaisH = parser.Parse(func);
                parser.Values["x"].SetValue(x - h);
                fMenosH = parser.Parse(func);

                if (Math.Abs(p - q) < erro)
                    erro = Math.Abs(p - q);
                else
                {
                    d = q;
                    achou = true;
                }
            }
            return d;
        }

        public void Bissessao ()
        {
            int n = 0, i;
            double xk = 0.5, derivadaxk, xfinal = epsilon / (b - a), log;
            bool found = false;

            ExpressionParser parser = new ExpressionParser(); //Interpretador
            if (func.Contains("e"))
            {
                parser.Values.Add("e", Math.E);
            }

            log = Math.Log(xfinal)/Math.Log(xk);

            for (; log > n; n++) ;

            xk = (a + b) / 2;
            derivadaxk = Derivada(xk);

            for (i=1; (i<=n) && (derivadaxk != 0);i++)
            {
                if (derivadaxk == 0)
                {
                    xfinal = xk;
                    found = true;
                } 
                else if (derivadaxk > 0)
                    b = xk;
                else if (derivadaxk < 0)
                    a = xk;

                xk = (a + b) / 2;
                derivadaxk = Derivada(xk);

            }

            if (!found) xfinal = (a + b) / 2;

            parser.Values.Add("x", xfinal);

            log = parser.Parse(func); // qualquer variavel, apenas para armazenar o valor da fx final

            textBox5.Text = xfinal.ToString();
            textBox6.Text = log.ToString();

        }


    }
}
