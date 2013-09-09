using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.IO;

using Metro.Interfaccie;
using Metro.Componenti;
using Metro.Designer;
using Metro.Controlli;

namespace Metro.Controlli
{
    [Designer("Metro.Designer.MetroSpecialTextBoxDisegner")]
    [DefaultEvent("PulsanteSpecialeClick")]
    public class MetroSpecialTextBox : UserControl, IMetroControl
    {
        #region proprietà
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category(EtichetteDesigner.Stile)]
        public StileMetro StileMetro
        {
            get { return VisualManager.StileMetro; }
            set { VisualManager.StileMetro = value; Refresh(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category(EtichetteDesigner.Stile)]
        public CombinazionaColori CombinazioneColori
        {
            get { return VisualManager.CombinazioneColori; }
            set { VisualManager.CombinazioneColori = value; Refresh(); }
        }

        private MetroVisualManager _visualManager = new MetroVisualManager();
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MetroVisualManager VisualManager
        {
            get { return _visualManager; }
            set { _visualManager = value; Refresh(); }
        }

        private Color _metroBorder = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroBorder
        {
            get
            {
                if (_metroBorder != Color.Empty)
                    return _metroBorder;
                return VisualManager.MetroTextBoxBorderColorNormal;
            }
            set { _metroBorder = value; Refresh(); }
        }

        private Color _metroBorderHover = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroBorderHover
        {
            get
            {
                if (_metroBorderHover != Color.Empty)
                    return _metroBorderHover;
                return VisualManager.MetroTextBoxBorderColorHover;
            }
            set { _metroBorderHover = value; Refresh(); }
        }

        private Color _metroBorderSelected = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroBorderSelected
        {
            get
            {
                if (_metroBorderSelected != Color.Empty)
                    return _metroBorderSelected;
                return VisualManager.MetroTextBoxBorderColorSelected;
            }
            set { _metroBorderSelected = value; Refresh(); }
        }

        private Color _metroBorderDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroBorderDisabled
        {
            get
            {
                if (_metroBorderDisabled != Color.Empty)
                    return _metroBorderDisabled;
                return VisualManager.MetroTextBoxBorderColorDisabled;
            }
            set { _metroBorderDisabled = value; Refresh(); }
        }

        private Color _metroBackground = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroBackground
        {
            get
            {
                if (_metroBackground != Color.Empty)
                    return _metroBackground;
                return VisualManager.MetroTextBoxBackColorNormal;
            }
            set { _metroBackground = value; Refresh(); }
        }

        private Color _metroBackgroundHover = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroBackgroundHover
        {
            get
            {
                if (_metroBackgroundHover != Color.Empty)
                    return _metroBackgroundHover;
                return VisualManager.MetroTextBoxBackColorHover;
            }
            set { _metroBackgroundHover = value; Refresh(); }
        }

        private Color _metroBackgroundSelected = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroBackgroundSelected
        {
            get
            {
                if (_metroBackgroundSelected != Color.Empty)
                    return _metroBackgroundSelected;
                return VisualManager.MetroTextBoxBackColorSelected;
            }
            set { _metroBackgroundSelected = value; Refresh(); }
        }

        private Color _metroBackgroundDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroBackgroundDisabled
        {
            get
            {
                if (_metroBackgroundDisabled != Color.Empty)
                    return _metroBackgroundDisabled;
                return VisualManager.MetroTextBoxBackColorDisabled;
            }
            set { _metroBackgroundDisabled = value; Refresh(); }
        }

        private Color _metroText = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroText
        {
            get
            {
                if (_metroText != Color.Empty)
                    return _metroText;
                return VisualManager.MetroTextBoxTextColorNormal;
            }
            set { _metroText = value; Refresh(); }
        }

        private Color _metroTextHover = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroTextHover
        {
            get
            {
                if (_metroTextHover != Color.Empty)
                    return _metroTextHover;
                return VisualManager.MetroTextBoxTextColorHover;
            }
            set { _metroTextHover = value; Refresh(); }
        }

        private Color _metroTextSelected = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroTextSelected
        {
            get
            {
                if (_metroTextSelected != Color.Empty)
                    return _metroTextSelected;
                return VisualManager.MetroTextBoxTextColorSelected;
            }
            set { _metroTextSelected = value; Refresh(); }
        }

        private Color _metroTextDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroTextDisabled
        {
            get
            {
                if (_metroTextDisabled != Color.Empty)
                    return _metroTextDisabled;
                return VisualManager.MetroTextBoxTextColorDisabled;
            }
            set { _metroTextDisabled = value; Refresh(); }
        }

        private Color _pulsanteSpecialeBack = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteSpecialeBack
        {
            get
            {
                if (_pulsanteSpecialeBack != Color.Empty)
                    return _pulsanteSpecialeBack;
                return VisualManager.MetroTextBoxSpecialeButtonBackNormal;
            }
            set { _pulsanteSpecialeBack = value; Refresh(); }
        }

        private Color _pulsanteSpecialeBackHover = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteSpecialeBackHover
        {
            get
            {
                if (_pulsanteSpecialeBackHover != Color.Empty)
                    return _pulsanteSpecialeBackHover;
                return VisualManager.MetroTextBoxSpecialeButtonBackHover;
            }
            set { _pulsanteSpecialeBackHover = value; Refresh(); }
        }

        private Color _pulsanteSpecialeBackPressed = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteSpecialeBackPressed
        {
            get
            {
                if (_pulsanteSpecialeBackPressed != Color.Empty)
                    return _pulsanteSpecialeBackPressed;
                return VisualManager.MetroTextBoxSpecialeButtonBackPressed;
            }
            set { _pulsanteSpecialeBackPressed = value; Refresh(); }
        }

        private Color _pulsanteSpecialeBackDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteSpecialeBackDisabled
        {
            get
            {
                if (_pulsanteSpecialeBackDisabled != Color.Empty)
                    return _pulsanteSpecialeBackDisabled;
                return VisualManager.MetroTextBoxSpecialeButtonBackDisabled;
            }
            set { _pulsanteSpecialeBackDisabled = value; Refresh(); }
        }

        private Color _pulsanteSpecialeText = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteSpecialeText
        {
            get
            {
                if (_pulsanteSpecialeText != Color.Empty)
                    return _pulsanteSpecialeText;
                return VisualManager.MetroTextBoxSpecialeButtonTextNormal;
            }
            set { _pulsanteSpecialeText = value; Refresh(); }
        }

        private Color _pulsanteSpecialeTextHover = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteSpecialeTextHover
        {
            get
            {
                if (_pulsanteSpecialeTextHover != Color.Empty)
                    return _pulsanteSpecialeTextHover;
                return VisualManager.MetroTextBoxSpecialeButtonTextHover;
            }
            set { _pulsanteSpecialeTextHover = value; Refresh(); }
        }

        private Color _pulsanteSpecialeTextPressed = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteSpecialeTextPressed
        {
            get
            {
                if (_pulsanteSpecialeTextPressed != Color.Empty)
                    return _pulsanteSpecialeTextPressed;
                return VisualManager.MetroTextBoxSpecialeButtonTextPressed;
            }
            set { _pulsanteSpecialeTextPressed = value; Refresh(); }
        }

        private Color _pulsanteSpecialeTextDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteSpecialeTextDisabled
        {
            get
            {
                if (_pulsanteSpecialeTextDisabled != Color.Empty)
                    return _pulsanteSpecialeTextDisabled;
                return VisualManager.MetroTextBoxSpecialeButtonTextDisabled;
            }
            set { _pulsanteSpecialeTextDisabled = value; Refresh(); }
        }

        private Font _metroFont = null;
        [Category(EtichetteDesigner.Stile)]
        public Font MetroFont
        {
            get
            {
                if (_metroFont != null)
                    return _metroFont;
                return VisualManager.MetroTextBoxFont;
            }
            set { _metroFont = value; _textBox.Font = value; Refresh(); }
        }

        private int _pulsanteSpecialeSize = 10;
        [Category(EtichetteDesigner.Stile)]
        public int PulsanteSpecialeSize
        {
            get { return _pulsanteSpecialeSize; }
            set { _pulsanteSpecialeSize = value; Refresh(); }
        }

        private bool _password = false;
        [Category(EtichetteDesigner.Stile)]
        public bool Password
        {
            get { return _password; }
            set { _textBox.UseSystemPasswordChar = value; _password = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        public new Size Size
        {
            get { return base.Size; }
            set
            {
                Size sz = new Size();
                sz.Width = value.Width;
                sz.Height = _textBox.Height + SystemInformation.BorderSize.Height * 4 + 3;

                base.Size = sz;
            }
        }

        private string _cueBanner = string.Empty;
        [Category(EtichetteDesigner.Stile)]
        public string CueBanner
        {
            get { return _cueBanner; }
            set
            {
                if (_textBox != null)
                {
                    if (value != string.Empty && value.Trim() != "")
                        WinApi.SendMessage(_textBox.Handle, (int)WinApi.Messages.EM_SETCUEBANNER, true, value);
                    else
                        WinApi.SendMessage(_textBox.Handle, (int)WinApi.Messages.EM_SETCUEBANNER, false, "");

                    WinApi.SendMessage(_textBox.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0);
                }

                _cueBanner = value;

            }
        }

        [Browsable(true)]
        [Category(EtichetteDesigner.Stile)]
        public new string Text
        {
            get
            {
                return _textBox.Text;
            }
            set
            {
                _textBox.Text = value;
            }
        }

        private bool _hoverPulsanteSpeciale = false;
        [Browsable(false)]
        private bool HoverPulsanteSpeciale
        {
            get { return _hoverPulsanteSpeciale; }
            set { _hoverPulsanteSpeciale = value; Refresh(); }
        }

        private bool _pressedPulsanteSpeciale = false;
        [Browsable(false)]
        private bool PressedPulsanteSpeciale
        {
            get { return _pressedPulsanteSpeciale; }
            set { _pressedPulsanteSpeciale = value; Refresh(); }
        }

        private bool _isHover = false;
        private bool _isSelected = false;

        private Rectangle _pulsanteSpecialeRect;

        private TPulsanteSpeciale _pulsanteSpeciale = TPulsanteSpeciale.Ricerca;
        [Category(EtichetteDesigner.Stile)]
        public TPulsanteSpeciale PulsanteSpeciale
        {
            get { return _pulsanteSpeciale; }
            set { _pulsanteSpeciale = value; Refresh(); }
        }

        #endregion

        #region Eventi

        public delegate void OnPulsanteSpecialeClickEventHandler(object sender, EventArgs e);
        public event OnPulsanteSpecialeClickEventHandler PulsanteSpecialeClick = null;

        #endregion

        #region Enum

        public enum TPulsanteSpeciale { MostraPassword, Ricerca, Pulisci }

        #endregion

        private MetroTextBox _textBox = null;

        public MetroSpecialTextBox()
            : base()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            _textBox = new MetroTextBox();
            _textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;

            _textBox.MouseLeave += _textBox_MouseLeave;
            _textBox.MouseEnter += _textBox_MouseEnter;
            _textBox.Enter += _textBox_Enter;
            _textBox.Leave += _textBox_Leave;

            Controls.Add(_textBox);

            Size = new Size(100, 25);

            BackColor = MetroBackground;

            CalcolaPulsanti();
        }

        void _textBox_Leave(object sender, EventArgs e)
        {
            if (Enabled)
            {
                _isSelected = false;
                _isHover = false;
                BackColor = MetroBackground;
                Refresh();
            }
        }

        void _textBox_Enter(object sender, EventArgs e)
        {
            if (Enabled)
            {
                _isSelected = true;
                BackColor = MetroBackgroundSelected;
                Refresh();
            }
        }

        void _textBox_MouseEnter(object sender, EventArgs e)
        {
            if (!Focused && Enabled == true)
            {
                _isHover = true;
                BackColor = MetroBackgroundHover;
                ForeColor = MetroTextHover;
                Refresh();
            }
        }

        void _textBox_MouseLeave(object sender, EventArgs e)
        {
            if (!Focused && !_textBox.Focused && Enabled == true)
            {
                _isHover = false;
                BackColor = MetroBackground;
                ForeColor = MetroText;
                Refresh();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            _textBox.Location = new Point(3, 3);
            _textBox.Size = new Size(Width - Height - 6, Height - 4);

            CalcolaPulsanti();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            _isHover = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            _isHover = false;
            HoverPulsanteSpeciale = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Color clrBorder = Color.Empty;
            Color clrPulsanteSpeciale = Color.Empty;
            Color clrTextPulsanteSpeciale = Color.Empty;

            if (!Enabled)
            {
                if (BackColor != MetroBackgroundDisabled)
                    BackColor = MetroBackgroundDisabled;
                if (ForeColor != MetroTextDisabled)
                    ForeColor = MetroTextDisabled;

                WinApi.SendMessage(_textBox.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0);

                clrBorder = MetroBorderDisabled;
                clrPulsanteSpeciale = PulsanteSpecialeBackDisabled;
                clrTextPulsanteSpeciale = PulsanteSpecialeTextDisabled;
            }
            else if (_isSelected)
            {
                if (BackColor != MetroBackgroundSelected)
                    BackColor = MetroBackgroundSelected;
                if (ForeColor != MetroTextSelected)
                    ForeColor = MetroTextSelected;

                WinApi.SendMessage(_textBox.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0);

                clrBorder = MetroBorderSelected;

                if (PressedPulsanteSpeciale)
                {
                    clrPulsanteSpeciale = PulsanteSpecialeBackPressed;
                    clrTextPulsanteSpeciale = PulsanteSpecialeTextPressed;
                }
                else if (HoverPulsanteSpeciale)
                {
                    clrPulsanteSpeciale = PulsanteSpecialeBackHover;
                    clrTextPulsanteSpeciale = PulsanteSpecialeTextHover;
                }
                else
                {
                    clrPulsanteSpeciale = PulsanteSpecialeBack;
                    clrTextPulsanteSpeciale = PulsanteSpecialeText;
                }
            }
            else if (_isHover)
            {
                if (BackColor != MetroBackgroundHover)
                    BackColor = MetroBackgroundHover;
                if (ForeColor != MetroTextHover)
                    ForeColor = MetroTextHover;

                WinApi.SendMessage(_textBox.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0);

                clrBorder = MetroBorderHover;

                if (PressedPulsanteSpeciale)
                {
                    clrPulsanteSpeciale = PulsanteSpecialeBackPressed;
                    clrTextPulsanteSpeciale = PulsanteSpecialeTextPressed;
                }
                else if (HoverPulsanteSpeciale)
                {
                    clrPulsanteSpeciale = PulsanteSpecialeBackHover;
                    clrTextPulsanteSpeciale = PulsanteSpecialeTextHover;
                }
                else
                {
                    clrPulsanteSpeciale = PulsanteSpecialeBack;
                    clrTextPulsanteSpeciale = PulsanteSpecialeText;
                }
            }
            else
            {
                if (BackColor != MetroBackground)
                    BackColor = MetroBackground;
                if (ForeColor != MetroText)
                    ForeColor = MetroText;

                WinApi.SendMessage(_textBox.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0);

                clrBorder = MetroBorder;

                if (PressedPulsanteSpeciale)
                {
                    clrPulsanteSpeciale = PulsanteSpecialeBackPressed;
                    clrTextPulsanteSpeciale = PulsanteSpecialeTextPressed;
                }
                else if (HoverPulsanteSpeciale)
                {
                    clrPulsanteSpeciale = PulsanteSpecialeBackHover;
                    clrTextPulsanteSpeciale = PulsanteSpecialeTextHover;
                }
                else
                {
                    clrPulsanteSpeciale = PulsanteSpecialeBack;
                    clrTextPulsanteSpeciale = PulsanteSpecialeText;
                }
            }
            
            g.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
            
            g.FillRectangle(new SolidBrush(clrPulsanteSpeciale), _pulsanteSpecialeRect);

            string pulsante = "";

            if (PulsanteSpeciale == TPulsanteSpeciale.MostraPassword)
                pulsante = "\uE052";
            else if (PulsanteSpeciale == TPulsanteSpeciale.Ricerca)
                pulsante = "\uE11A";
            else if (PulsanteSpeciale == TPulsanteSpeciale.Pulisci)
                pulsante = "\uE10A";

            Font font = new Font(VisualManager.MetroSymbolFont.FontFamily, PulsanteSpecialeSize);
            TextRenderer.DrawText(g, pulsante, font, new Rectangle(_pulsanteSpecialeRect.X + 2, _pulsanteSpecialeRect.Y, _pulsanteSpecialeRect.Width - 2, _pulsanteSpecialeRect.Height), clrTextPulsanteSpeciale, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine);

            ControlPaint.DrawBorder(g, ClientRectangle, clrBorder, ButtonBorderStyle.Solid);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (Parent is IMetroWindow)
            {
                VisualManager = ((IMetroWindow)Parent).VisualManager;
                StileMetro = ((IMetroWindow)Parent).StileMetro;
                CombinazioneColori = ((IMetroWindow)Parent).CombinazioneColori;
            }
            else if (Parent is IMetroControl)
            {
                VisualManager = ((IMetroControl)Parent).VisualManager;
                StileMetro = ((IMetroControl)Parent).StileMetro;
                CombinazioneColori = ((IMetroControl)Parent).CombinazioneColori;
            }

            _textBox.Parent = null;
            _textBox.Parent = this;

            Refresh();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            HoverPulsanteSpeciale = _pulsanteSpecialeRect.Contains(e.Location);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            PressedPulsanteSpeciale = (_pulsanteSpecialeRect.Contains(e.Location) && (e.Button == MouseButtons.Left));

            if (Password && _pulsanteSpecialeRect.Contains(e.Location) && (e.Button == MouseButtons.Left))
                _textBox.UseSystemPasswordChar = false;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            PressedPulsanteSpeciale = !(e.Button == MouseButtons.Left);

            if (Password && e.Button == MouseButtons.Left)
                _textBox.UseSystemPasswordChar = true;

            if (_pulsanteSpecialeRect.Contains(e.Location) && (e.Button == MouseButtons.Left) && (PulsanteSpecialeClick != null))
                PulsanteSpecialeClick(this, new EventArgs());

            if (_pulsanteSpecialeRect.Contains(e.Location) && (e.Button == MouseButtons.Left))
                SpecialButtonCliecked();
        }

        protected void CalcolaPulsanti()
        {
            _pulsanteSpecialeRect = new Rectangle(Width - Height + 4, 4, Height - 8, Height - 8);
        }

        protected virtual void SpecialButtonCliecked()
        {
            if (PulsanteSpeciale == TPulsanteSpeciale.Pulisci)
                Text = string.Empty;
        }
    }
}
