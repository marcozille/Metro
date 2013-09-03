using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Diagnostics;

using Metro.Componenti;
using Metro.Controlli;
using Metro.Designer;
using Metro.Interfaccie;

namespace Metro.Forms
{
    public class MetroMessageBox : Form
    {
        public enum MBX_Button { OK, OKANNULLA, SINO, SINOANNULLA }
        public enum MBS_Risultato { OK, ANNULLA, SI, NO }

        private readonly PrivateFontCollection fontCollection = new PrivateFontCollection();

        private string _contenuto;
        private string Contenuto { set { _contenuto = value; Invalidate(); } get { return _contenuto; } }
        private string _titolo;
        private string Titolo { set { _titolo = value; Invalidate(); } get { return _titolo; } }
        private MBX_Button Pulsanti;
        private MetroButton btn_OK;
        private MetroButton btn_Annulla;
        private MetroButton btn_Si;
        private MetroButton btn_No;
        private MBS_Risultato Risultato;
        private DarkenScreen _darkenScreen;
        private Font _fontTitolo;
        private Font _fontCorpo;
        private bool ShowOnScreen;

        public static Color HideFormColor = Color.FromArgb(255,255,255);
        public static Color BackgroundColor = Color.FromArgb(0,0,0);
        public static Color TitleColor = Color.FromArgb(0,122,204);
        public static Color ContentColor = Color.FromArgb(255, 255, 255);
        public static Color ButtonBackNormal  = Color.FromArgb(0,122,204);
        public static Color ButtonBackHover = Color.FromArgb(0,170,240);
        public static Color ButtonBackPressed = Color.FromArgb(0,70,160);
        public static Color ButtonBorder = Color.FromArgb(255,255,255);
        public static Color ButtonTextNormal = Color.FromArgb(255,255,255);
        public static Color ButtonTextHover = Color.FromArgb(255,255,255);
        public static Color ButtonTextPressed = Color.FromArgb(255,255,255);

        private MetroMessageBox(String contenuto, string titolo, MBX_Button pulsanti, bool showOnScreen)
        {
            InitializeComponent();

            LoadFont();

            Contenuto = contenuto;
            Pulsanti = pulsanti;
            Titolo = titolo;
            Risultato = MBS_Risultato.OK;
            ShowOnScreen = showOnScreen;

            _darkenScreen = new DarkenScreen();
            _darkenScreen.ColoreOscurazione = HideFormColor;
            _darkenScreen.ShowOnScreen = ShowOnScreen;
            
            InitDimensioni();
            InitPulsanti();
        }
        
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        private void LoadFont()
        {
            byte[] fontArray = Properties.Resources.segoeui;
            int fontArrayLen = Properties.Resources.segoeui.Length;

            IntPtr fontData = Marshal.AllocCoTaskMem(fontArrayLen);
            Marshal.Copy(fontArray, 0, fontData, fontArrayLen);

            uint cFonts = 0;
            AddFontMemResourceEx(fontData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            fontCollection.AddMemoryFont(fontData, fontArrayLen);
            Marshal.FreeCoTaskMem(fontData);

            _fontTitolo = new Font(fontCollection.Families[0], 18f);
            _fontCorpo = new Font(fontCollection.Families[0], 11f);
        }

        private void BeginDarken()
        {
            _darkenScreen.Show();
        }

        private void EndDarken()
        {
            _darkenScreen.Close();
        }

        public static MBS_Risultato Show(String contenuto, string titolo = "", MBX_Button pulsanti = MBX_Button.OK, bool showOnScreen = false)
        {
            MetroMessageBox msgbx = new MetroMessageBox(contenuto, titolo, pulsanti, showOnScreen);

            msgbx.BeginDarken();
            msgbx.ShowDialog();
            msgbx.EndDarken();

            return msgbx.Risultato;
        }

        private void InitPulsanti()
        {
            int posXOk, posXAnnulla, posXSi, posXNo;
            int posY = ClientRectangle.Height - 50;
            int larghezza = ClientRectangle.Width - (int)(ClientRectangle.Width * 0.3);
            int padding = (ClientRectangle.Width - larghezza) / 2;

            switch(Pulsanti)
            {
                case MBX_Button.OK:
                    btn_Annulla.Visible = false;
                    btn_No.Visible = false;
                    btn_Si.Visible = false;
                    btn_OK.Visible = true;

                    posXOk = ClientRectangle.Width - padding - 100;
                    btn_OK.Location = new Point(posXOk, posY);
                    break;
                case MBX_Button.OKANNULLA:
                    btn_Annulla.Visible = true;
                    btn_No.Visible = false;
                    btn_Si.Visible = false;
                    btn_OK.Visible = true;

                    posXAnnulla = ClientRectangle.Width - padding - 100;
                    posXOk = posXAnnulla - 6 - 100;

                    btn_Annulla.Location = new Point(posXAnnulla, posY);
                    btn_OK.Location = new Point(posXOk, posY);

                    break;
                case MBX_Button.SINO:
                    btn_Annulla.Visible = false;
                    btn_No.Visible = true;
                    btn_Si.Visible = true;
                    btn_OK.Visible = false;

                    posXNo = ClientRectangle.Width - padding - 100;
                    posXSi = posXNo - 6 - 100;

                    btn_No.Location = new Point(posXNo, posY);
                    btn_Si.Location = new Point(posXSi, posY);

                    break;
                case MBX_Button.SINOANNULLA:
                    btn_Annulla.Visible = true;
                    btn_No.Visible = true;
                    btn_Si.Visible = true;
                    btn_OK.Visible = false;

                    posXAnnulla = ClientRectangle.Width - padding - 100;
                    posXNo = posXAnnulla - 6 - 100;
                    posXSi = posXNo - 6 - 100;

                    btn_Annulla.Location = new Point(posXAnnulla, posY);
                    btn_No.Location = new Point(posXNo, posY);
                    btn_Si.Location = new Point(posXSi, posY);

                    break;
                default: 
                    break;
            }
        }

        private void InitDimensioni()
        {
            if (ShowOnScreen)
            {
                Rectangle schermo = Screen.PrimaryScreen.Bounds;
                Bounds = new Rectangle(0, (schermo.Height - 280) / 2, schermo.Width, 280);
            }
            else
            {
                Form mainFrm = Application.OpenForms[0];
                Rectangle formRect = mainFrm.Bounds;
                Bounds = new Rectangle(formRect.X, formRect.Y + (formRect.Height - 280) / 2, formRect.Width, 280);
            }
        }

        private void InitializeComponent()
        {
            this.btn_OK = new MetroButton();
            this.btn_Annulla = new MetroButton();
            this.btn_Si = new MetroButton();
            this.btn_No = new MetroButton();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_OK.Location = new System.Drawing.Point(2, 213);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(100, 33);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            this.btn_OK.MetroBorder = ButtonBorder;
            this.btn_OK.MetroBackground = ButtonBackNormal;
            this.btn_OK.MetroBackgroundHover = ButtonBackHover;
            this.btn_OK.MetroBackgroundPressed = ButtonBackPressed;
            this.btn_OK.MetroText = ButtonTextNormal;
            this.btn_OK.MetroTextHover = ButtonTextHover;
            this.btn_OK.MetroTextPressed = ButtonTextPressed;
            // 
            // btn_Annulla
            // 
            this.btn_Annulla.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Annulla.Location = new System.Drawing.Point(108, 213);
            this.btn_Annulla.Name = "btn_Annulla";
            this.btn_Annulla.Size = new System.Drawing.Size(100, 33);
            this.btn_Annulla.TabIndex = 1;
            this.btn_Annulla.Text = "Annulla";
            this.btn_Annulla.UseVisualStyleBackColor = true;
            this.btn_Annulla.Click += new System.EventHandler(this.btn_Annulla_Click);
            this.btn_Annulla.MetroBorder = ButtonBorder;
            this.btn_Annulla.MetroBackground = ButtonBackNormal;
            this.btn_Annulla.MetroBackgroundHover = ButtonBackHover;
            this.btn_Annulla.MetroBackgroundPressed = ButtonBackPressed;
            this.btn_Annulla.MetroText = ButtonTextNormal;
            this.btn_Annulla.MetroTextHover = ButtonTextHover;
            this.btn_Annulla.MetroTextPressed = ButtonTextPressed;
            // 
            // btn_Si
            // 
            this.btn_Si.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Si.Location = new System.Drawing.Point(214, 213);
            this.btn_Si.Name = "btn_Si";
            this.btn_Si.Size = new System.Drawing.Size(100, 33);
            this.btn_Si.TabIndex = 2;
            this.btn_Si.Text = "Si";
            this.btn_Si.UseVisualStyleBackColor = true;
            this.btn_Si.Click += new System.EventHandler(this.btn_Si_Click);
            this.btn_Si.MetroBorder = ButtonBorder;
            this.btn_Si.MetroBackground = ButtonBackNormal;
            this.btn_Si.MetroBackgroundHover = ButtonBackHover;
            this.btn_Si.MetroBackgroundPressed = ButtonBackPressed;
            this.btn_Si.MetroText = ButtonTextNormal;
            this.btn_Si.MetroTextHover = ButtonTextHover;
            this.btn_Si.MetroTextPressed = ButtonTextPressed;
            // 
            // btn_No
            // 
            this.btn_No.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_No.Location = new System.Drawing.Point(320, 213);
            this.btn_No.Name = "btn_No";
            this.btn_No.Size = new System.Drawing.Size(100, 33);
            this.btn_No.TabIndex = 3;
            this.btn_No.Text = "No";
            this.btn_No.UseVisualStyleBackColor = true;
            this.btn_No.Click += new System.EventHandler(this.btn_No_Click);
            this.btn_No.MetroBorder = ButtonBorder;
            this.btn_No.MetroBackground = ButtonBackNormal;
            this.btn_No.MetroBackgroundHover = ButtonBackHover;
            this.btn_No.MetroBackgroundPressed = ButtonBackPressed;
            this.btn_No.MetroText = ButtonTextNormal;
            this.btn_No.MetroTextHover = ButtonTextHover;
            this.btn_No.MetroTextPressed = ButtonTextPressed;
            // 
            // MetroMessageBox
            // 
            this.Controls.Add(this.btn_No);
            this.Controls.Add(this.btn_Si);
            this.Controls.Add(this.btn_Annulla);
            this.Controls.Add(this.btn_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ResumeLayout(false);
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            Risultato = MBS_Risultato.OK;
            Close();
        }

        private void btn_Annulla_Click(object sender, EventArgs e)
        {
            Risultato = MBS_Risultato.ANNULLA;
            Close();
        }

        private void btn_Si_Click(object sender, EventArgs e)
        {
            Risultato = MBS_Risultato.SI;
            Close();
        }

        private void btn_No_Click(object sender, EventArgs e)
        {
            Risultato = MBS_Risultato.NO;
            Close();
        }

        private bool ControllaDimensioni()
        {
            Rectangle bounds;
            
            if (ShowOnScreen)
            {
                Rectangle schermo = Screen.PrimaryScreen.Bounds;
                bounds = new Rectangle(0, (schermo.Height - 280) / 2, schermo.Width, 280);
            }
            else
            {
                Form mainFrm = Application.OpenForms[0];
                Rectangle formRect = mainFrm.Bounds;
                bounds = new Rectangle(0, (formRect.Height - 280) / 2, formRect.Width, 280);
            }
            
            if (Bounds != bounds)
                return false;
            return true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!ControllaDimensioni())
            {
                InitDimensioni();
                InitPulsanti();
            }

            e.Graphics.FillRectangle(new SolidBrush(BackgroundColor), ClientRectangle);

            int larghezza = ClientRectangle.Width - (int)(ClientRectangle.Width * 0.3);
            int padding = (ClientRectangle.Width - larghezza) / 2;

            Rectangle titleRc = new Rectangle(new Point(padding, 20), new Size(larghezza, 35));
            Rectangle textRc = new Rectangle(new Point(padding + 3, titleRc.Location.Y + titleRc.Height + 10), new Size(larghezza, ClientRectangle.Height - 60));

            TextRenderer.DrawText(e.Graphics, Titolo, _fontTitolo, titleRc, TitleColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
            TextRenderer.DrawText(e.Graphics, Contenuto, _fontCorpo, textRc, ContentColor, TextFormatFlags.Left | TextFormatFlags.WordBreak);
        }
    }

    class DarkenScreen : Form
    {
        public Color ColoreOscurazione = Color.Black;
        public bool ShowOnScreen;

        public DarkenScreen()
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Opacity = 0.5D;
        }
        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(ColoreOscurazione), ClientRectangle);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            int largezza = 0;
            int altezza = 0;

            if (ShowOnScreen)
            {
                foreach (Screen scr in Screen.AllScreens)
                {
                    if (scr.Bounds.Height > altezza) altezza = scr.Bounds.Height;
                    largezza += scr.Bounds.Width;
                }
                Bounds = new Rectangle(0, 0, largezza, altezza);
            }
            else
            {
                Bounds = Form.ActiveForm.Bounds;
            }
        }
    }
}
