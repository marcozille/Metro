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
            MetroMessageBox.Show("Vediamo un pò se funziona meglio ora", "Secondo test!!! :D", MetroMessageBox.MBX_Button.OKANNULLA);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            //MetroMessageBox.Show(metroPasswordBox1.Size.ToString());
        }

        private void OnAutentica(object sender, Controlli.MetroPasswordBoxAutenticaEventArgs e)
        {
            MetroMessageBox.Show(e.Password, "Password inserita");
        }

        private void metroSpecialTextBox1_PulsanteSpecialeClick(object sender, EventArgs e)
        {
            MetroMessageBox.Show("cliccato pulsante di ricerca");
        }

        private void metroButton2_Click_1(object sender, EventArgs e)
        {
            metroTile1.NumeroTile++;
        }

        private void metroButton3_Click_1(object sender, EventArgs e)
        {
            metroTile2.NumeroTile++;
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            metroTile1.NumeroTile = Convert.ToInt32(metroTextBox2.Text);
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            metroTile2.NumeroTile = Convert.ToInt32(metroTextBox2.Text);
        }
    }
}
