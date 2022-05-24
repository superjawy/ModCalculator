using System;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
namespace ModCalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //static int Parse(string expression)
        //{
        //    return CSharpScript.EvaluateAsync<int>(expression).Result;
        //}

        public static double Evaluate(string expression)
        {

            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }

        int modulo(int a, int m) //остаток от деления
        {
            int r = a % m;
            return r < 0 ? r + m : r;
        }

        static int modInverse(int a, int m)
        {
            for (int x = 1; x < m; x++)
                if (((a % m) * (x % m)) % m == 1)
                    return x;
            return 999999;
        }
        // 9 + 3 - 10 * 13 + 7
        // 2 + 3 * 5 / 2 - 10 / 3, p=23
        private void button1_Click(object sender, EventArgs e)
        {
            int equasion = Convert.ToInt32(Evaluate(textBox1.Text));
            int mod = Convert.ToInt32(textBox2.Text);


            string subjectString = textBox1.Text;

            if (IsPrime(mod) == true)
            {
                Regex Inverse = new Regex("(?<=1/)[0-9]+", RegexOptions.IgnoreCase);
                Match matchInverse = Inverse.Match(subjectString);
                if (matchInverse.Success)
                {
                    Regex Expression = new Regex("(1/)[0-9]+", RegexOptions.IgnoreCase);

                    string non = Convert.ToString(modInverse(Convert.ToInt32(matchInverse.Groups[0].Value), mod));
                    string buffer = Expression.Replace(subjectString, Convert.ToString(modInverse(Convert.ToInt32(matchInverse.Groups[0].Value), mod)));
                    int intbuffer = Convert.ToInt32(Evaluate(buffer));

                    if (non == "999999")
                        textBox3.Text = "Обратного нет";
                    else
                        textBox3.Text = Convert.ToString(modulo(intbuffer, mod));
                }

                else
                {
                    textBox3.Text = Convert.ToString(modulo(equasion, mod));
                }
            }

            else
            {
                MessageBox.Show("Введите корректный модуль");
                textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
                textBox2.Focus();
                textBox2.SelectionStart = textBox2.Text.Length;
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9/+*-()]"))
            {
                MessageBox.Show("Введите корректное выражение");
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
                textBox1.Focus();
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "[^0-9]"))
            {
                MessageBox.Show("Введите корректный модуль");
                textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
                textBox2.Focus();
                textBox2.SelectionStart = textBox2.Text.Length;
            }

        }

        public static bool IsPrime(int num)
        {
            if (num == 0) return false;
            for (int i = 2; i < num; i++)
                if (num % i == 0)
                    return false;
            return true;
        }
    }

}
