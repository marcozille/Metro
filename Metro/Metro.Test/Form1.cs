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
        
        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (StileMetro == Componenti.StileMetro.Chiaro)
                StileMetro = Componenti.StileMetro.Scuro;
            else
                StileMetro = Componenti.StileMetro.Chiaro;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            //MetroMessageBox.BackgroundColor = Color.Green;
            switch (MetroMessageBox.Show("Vediamo un pò se funziona meglio ora", "Secondo test!!! :D", MetroMessageBox.MBX_Button.OKANNULLA))
            {
                case MetroMessageBox.MBS_Risultato.SI: MetroMessageBox.Show("premuto SI"); break;
                case MetroMessageBox.MBS_Risultato.NO: MetroMessageBox.Show("premuto NO"); break;
                case MetroMessageBox.MBS_Risultato.ANNULLA: MetroMessageBox.Show("premuto ANNULLA"); break;
                case MetroMessageBox.MBS_Risultato.OK: MetroMessageBox.Show("premuto OK"); break;
            }
        }
    }
}
