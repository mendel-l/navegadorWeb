using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace navegadorWeb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Guardar(string nombreArchivo, string texto)
        {
            FileStream stream = new FileStream(nombreArchivo, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(texto);
            writer.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string textoANavegar = comboBox1.Text;

            //Guardar(@"historialNavegacion.txt", comboBox1.Text);

            // es una direccion
            if (textoANavegar.Contains(".com") || textoANavegar.Contains("http") ||
                textoANavegar.Contains("www"))
            {
                if (textoANavegar.StartsWith("https://") == false
                    && textoANavegar.StartsWith("http://") == false)
                {
                    textoANavegar = "https://" + textoANavegar;
                }

                //webBrowser1.Navigate(textoANavegar);

                int Repetida = 0; 
                for (int i = 0; i < comboBox1.Items.Count; i++)
                {
                    if (comboBox1.Items[i].ToString() == textoANavegar)
                    {
                        Repetida++;
                    }
                    if (Repetida == 0)
                    {
                        comboBox1.Items.Add(textoANavegar);
                        Guardar("Historial.txt", textoANavegar);
                    }
                }
                webBrowser1.Navigate(new Uri(textoANavegar));
                comboBox1.Items.Add(textoANavegar);
                Guardar(@"Historial.txt", textoANavegar);
            }

            // es una palabra
            else
            {
                webBrowser1.Navigate("https://www.google.com/search?q=" + textoANavegar);
            }
        }

        private void Leer(string FileName) 
        {
            FileStream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1) 
            {
                comboBox1.Items.Add(reader.ReadLine());
            }
            reader.Close();
        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }

        private void siguienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void anteriorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
