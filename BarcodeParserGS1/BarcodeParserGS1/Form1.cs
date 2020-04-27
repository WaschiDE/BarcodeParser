using System;
using System.Linq;
using System.Windows.Forms;

namespace BarcodeParserGS1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Button1_click(object sender, EventArgs e)
        {

            //is Numeric schleife 1 (Mitarbeiter und NVE)
            bool m = false;
            int n = 2;
            while (n < textBox1.Text.Length)
            {
                m = char.IsDigit(char.Parse(textBox1.Text.Substring(n, 1)));
                //Console.WriteLine(m);
                if (m is false)
                { break; }
                n++;
            }

            //is Numeric schleife 2 (Lagerplatz)
            bool o = false;
            int p = 7;
            while (p < textBox1.Text.Length)
            {
                o = char.IsDigit(char.Parse(textBox1.Text.Substring(p, 1)));
                //Console.WriteLine(o);
                if (o is false)
                { break; }
                p++;
            }

            // Keine Eingabe aber Knopfgedrückt
            if (textBox1.Text == "")
            {
                textBox2.Text = "Bitten geben Sie einen Code ein!";
                textBox3.Text = "";
                Console.WriteLine("Eingabe leer");
            }

            ///Mitarbeiter
            else if (textBox1.Text.StartsWith("MA") & textBox1.Text.Length > 2 & m is true
                 // ÜBERPRÜFEN AUF ZIFFERN& textBox1.Text.Remove(0,2) is int
                 )
            {
                textBox2.Text = textBox1.Text.Remove(0, 2);
                textBox3.Text = "Mitarbeiter";
                Console.WriteLine("Mitarbeiter");
            }

            ///NVE
            else if (
                textBox1.Text.StartsWith("00") & textBox1.Text.Length is 20 & m is true
                )
            {
                int a;
                int b = Int32.Parse(textBox1.Text.Remove(0, 19));
                a = (textBox1.Text.ElementAt(2) * 3 + textBox1.Text.ElementAt(3) + textBox1.Text.ElementAt(4) * 3 +
                       textBox1.Text.ElementAt(5) + textBox1.Text.ElementAt(6) * 3 + textBox1.Text.ElementAt(7) + textBox1.Text.ElementAt(8) * 3 +
                       textBox1.Text.ElementAt(9) + textBox1.Text.ElementAt(10) * 3 + textBox1.Text.ElementAt(11) + textBox1.Text.ElementAt(12) * 3 +
                       textBox1.Text.ElementAt(13) + textBox1.Text.ElementAt(14) * 3 + textBox1.Text.ElementAt(15) + textBox1.Text.ElementAt(16) * 3 +
                       textBox1.Text.ElementAt(17) + textBox1.Text.ElementAt(18) * 3) % 10 * -1 + 10;

                ///Wenn der Modulo der Summe 0 ist und die letzte ziffer gleich 0 dann
                if (
                   (textBox1.Text.ElementAt(2) * 3 + textBox1.Text.ElementAt(3) + textBox1.Text.ElementAt(4) * 3 + textBox1.Text.ElementAt(5) +
                   textBox1.Text.ElementAt(6) * 3 + textBox1.Text.ElementAt(7) + textBox1.Text.ElementAt(8) * 3 + textBox1.Text.ElementAt(9) +
                   textBox1.Text.ElementAt(10) * 3 + textBox1.Text.ElementAt(11) + textBox1.Text.ElementAt(12) * 3 + textBox1.Text.ElementAt(13) +
                   textBox1.Text.ElementAt(14) * 3 + textBox1.Text.ElementAt(15) + textBox1.Text.ElementAt(16) * 3 + textBox1.Text.ElementAt(17) +
                   textBox1.Text.ElementAt(18) * 3) % 10 == 0 & textBox1.Text.Remove(0, 19) == "0"
                   )
                {
                    textBox3.Text = "NVE";
                    textBox2.Text = textBox1.Text.Remove(0, 2);
                }
                else if (a - b == 0)
                {
                    textBox3.Text = "NVE";
                    textBox2.Text = textBox1.Text.Remove(0, 2);
                }
                else
                {
                    textBox2.Text = "Die eingegebene NVE ist nicht valide!";
                    textBox3.Text = "Überprüfen Sie Ihre Eingabe!";
                };
            }

            ///Lagerplatz
            else if (
                textBox1.Text.StartsWith("#LP#") & textBox1.Text.Length == 22 & o is true
                )
            {
                textBox3.Text = "Lagerplatz";
                textBox2.Text = textBox1.Text.Substring(4, 3) + "/" + textBox1.Text.Substring(7, 3) + "/" +
                    textBox1.Text.Substring(10, 3) + "/" + textBox1.Text.Substring(13, 3) + "/" + textBox1.Text.Substring(16, 3) +
                    "/" + textBox1.Text.Substring(19, 3);

                Console.WriteLine(("Lagerplatz: " + textBox1.Text.Substring(4, 3) + "/" + textBox1.Text.Substring(7, 3) + "/" +
                    textBox1.Text.Substring(10, 3) + "/" + textBox1.Text.Substring(13, 3) + "/" + textBox1.Text.Substring(16, 3) +
                    "/" + textBox1.Text.Substring(19, 3)));
            }

            ///Kommission
            else if (
                textBox1.Text.StartsWith("KO") & textBox1.Text.Contains(",KU")
                )
            {
                textBox3.Text = "Kommission";
                textBox2.Text = textBox1.Text.Remove(0, (textBox1.Text.IndexOf(",") + 3)) + " - " + textBox1.Text.Remove(0, 2).Remove(textBox1.Text.IndexOf(",") - 2, (textBox1.Text.Length) - textBox1.Text.IndexOf(","));

                Console.WriteLine(("Kommission: ") + textBox1.Text.Remove(0, (textBox1.Text.IndexOf(",") + 3)) + " - " + textBox1.Text.Remove(0, 2).Remove(textBox1.Text.IndexOf(",") - 2, (textBox1.Text.Length) - textBox1.Text.IndexOf(",")));
            }

            ///Kein Valider Code
            else
            {
                textBox2.Text = "Der eingegebene Code ist invalide!";
                textBox3.Text = "Überprüfen Sie Ihre Eingabe!";
                Console.WriteLine("Kein valider Code");
            }
        }
    }
}

