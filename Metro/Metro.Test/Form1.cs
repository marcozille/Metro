using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Metro.Forms;

namespace Metro.Test
{
    public partial class Form1 : MetroWindow
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (StileMetro == Componenti.StileMetro.Chiaro)
                StileMetro = Componenti.StileMetro.Scuro;
            else
                StileMetro = Componenti.StileMetro.Chiaro;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show("Prima prova della nuova messagebox!", "Test :)");
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            metroButton1.Enabled = !metroButton1.Enabled;
        }
    }
}
