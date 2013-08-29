namespace Metro.Test
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.metroButton1 = new Metro.Controlli.MetroButton();
            this.metroButton2 = new Metro.Controlli.MetroButton();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(366, 204);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(359, 261);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // metroButton1
            // 
            this.metroButton1.CombinazioneColori = Metro.Componenti.CombinazionaColori.Blu;
            this.metroButton1.Enabled = false;
            this.metroButton1.Location = new System.Drawing.Point(440, 412);
            this.metroButton1.MetroBackground = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.metroButton1.MetroBackgroundDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.metroButton1.MetroBackgroundHover = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.metroButton1.MetroBackgroundPressed = System.Drawing.Color.Empty;
            this.metroButton1.MetroBorder = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.metroButton1.MetroBorderDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.metroButton1.MetroFont = new System.Drawing.Font("Segoe UI", 10F);
            this.metroButton1.MetroText = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.metroButton1.MetroTextDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.metroButton1.MetroTextHover = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.metroButton1.MetroTextPressed = System.Drawing.Color.Empty;
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(275, 34);
            this.metroButton1.StileMetro = Metro.Componenti.StileMetro.Scuro;
            this.metroButton1.TabIndex = 5;
            this.metroButton1.Text = "metroButton1";
            this.metroButton1.UseVisualStyleBackColor = true;
            // 
            // metroButton2
            // 
            this.metroButton2.CombinazioneColori = Metro.Componenti.CombinazionaColori.Blu;
            this.metroButton2.Location = new System.Drawing.Point(509, 341);
            this.metroButton2.MetroBackground = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.metroButton2.MetroBackgroundDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.metroButton2.MetroBackgroundHover = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.metroButton2.MetroBackgroundPressed = System.Drawing.Color.Empty;
            this.metroButton2.MetroBorder = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.metroButton2.MetroBorderDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.metroButton2.MetroFont = new System.Drawing.Font("Segoe UI", 10F);
            this.metroButton2.MetroText = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.metroButton2.MetroTextDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.metroButton2.MetroTextHover = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.metroButton2.MetroTextPressed = System.Drawing.Color.Empty;
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(149, 34);
            this.metroButton2.StileMetro = Metro.Componenti.StileMetro.Scuro;
            this.metroButton2.TabIndex = 6;
            this.metroButton2.Text = "metroButton2";
            this.metroButton2.UseVisualStyleBackColor = true;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 545);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MetroBackgroundColor = System.Drawing.Color.White;
            this.MetroResizeGripColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.MetroSysButtonHover = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.MetroSysButtonNormal = System.Drawing.Color.White;
            this.MetroSysButtonTextNormal = System.Drawing.Color.Black;
            this.MetroTitleBackgroundColor = System.Drawing.Color.White;
            this.MetroTitleTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Controlli.MetroButton metroButton1;
        private Controlli.MetroButton metroButton2;


    }
}

