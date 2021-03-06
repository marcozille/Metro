﻿using System;
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
    [Designer("Metro.Designer.MetroPasswordBoxDisegner")]
    [DefaultEvent("Autentica")]
    public class MetroPasswordBox : UserControl, IMetroControl
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

        private Color _pulsanteVaiBack = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteVaiBack
        {
            get
            {
                if (_pulsanteVaiBack != Color.Empty)
                    return _pulsanteVaiBack;
                return VisualManager.MetroPasswordBoxVaiButtonBackNormal;
            }
            set { _pulsanteVaiBack = value; Refresh(); }
        }

        private Color _pulsanteVaiBackHover = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteVaiBackHover
        {
            get
            {
                if (_pulsanteVaiBackHover != Color.Empty)
                    return _pulsanteVaiBackHover;
                return VisualManager.MetroPasswordBoxVaiButtonBackHover;
            }
            set { _pulsanteVaiBackHover = value; Refresh(); }
        }

        private Color _pulsanteVaiBackPressed = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteVaiBackPressed
        {
            get
            {
                if (_pulsanteVaiBackPressed != Color.Empty)
                    return _pulsanteVaiBackPressed;
                return VisualManager.MetroPasswordBoxVaiButtonBackPressed;
            }
            set { _pulsanteVaiBackPressed = value; Refresh(); }
        }

        private Color _pulsanteVaiBackDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteVaiBackDisabled
        {
            get
            {
                if (_pulsanteVaiBackDisabled != Color.Empty)
                    return _pulsanteVaiBackDisabled;
                return VisualManager.MetroPasswordBoxVaiButtonBackDisabled;
            }
            set { _pulsanteVaiBackDisabled = value; Refresh(); }
        }

        private Color _pulsanteVaiText = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteVaiText
        {
            get
            {
                if (_pulsanteVaiText != Color.Empty)
                    return _pulsanteVaiText;
                return VisualManager.MetroPasswordBoxVaiButtonTextNormal;
            }
            set { _pulsanteVaiText = value; Refresh(); }
        }

        private Color _pulsanteVaiTextHover = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteVaiTextHover
        {
            get
            {
                if (_pulsanteVaiTextHover != Color.Empty)
                    return _pulsanteVaiTextHover;
                return VisualManager.MetroPasswordBoxVaiButtonTextHover;
            }
            set { _pulsanteVaiTextHover = value; Refresh(); }
        }

        private Color _pulsanteVaiTextPressed = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteVaiTextPressed
        {
            get
            {
                if (_pulsanteVaiTextPressed != Color.Empty)
                    return _pulsanteVaiTextPressed;
                return VisualManager.MetroPasswordBoxVaiButtonTextPressed;
            }
            set { _pulsanteVaiTextPressed = value; Refresh(); }
        }

        private Color _pulsanteVaiTextDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteVaiTextDisabled
        {
            get
            {
                if (_pulsanteVaiTextDisabled != Color.Empty)
                    return _pulsanteVaiTextDisabled;
                return VisualManager.MetroPasswordBoxVaiButtonTextDisabled;
            }
            set { _pulsanteVaiTextDisabled = value; Refresh(); }
        }

        private Color _pulsanteMostraPasswordBack = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteMostraPasswordBack
        {
            get
            {
                if (_pulsanteMostraPasswordBack != Color.Empty)
                    return _pulsanteMostraPasswordBack;
                return VisualManager.MetroTextBoxSpecialeButtonBackNormal;
            }
            set { _pulsanteMostraPasswordBack = value; Refresh(); }
        }

        private Color _pulsanteMostraPasswordBackHover = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteMostraPasswordBackHover
        {
            get
            {
                if (_pulsanteMostraPasswordBackHover != Color.Empty)
                    return _pulsanteMostraPasswordBackHover;
                return VisualManager.MetroTextBoxSpecialeButtonBackHover;
            }
            set { _pulsanteMostraPasswordBackHover = value; Refresh(); }
        }

        private Color _pulsanteMostraPasswordBackPressed = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteMostraPasswordBackPressed
        {
            get
            {
                if (_pulsanteMostraPasswordBackPressed != Color.Empty)
                    return _pulsanteMostraPasswordBackPressed;
                return VisualManager.MetroTextBoxSpecialeButtonBackPressed;
            }
            set { _pulsanteMostraPasswordBackPressed = value; Refresh(); }
        }

        private Color _pulsanteMostraPasswordBackDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteMostraPasswordBackDisabled
        {
            get
            {
                if (_pulsanteMostraPasswordBackDisabled != Color.Empty)
                    return _pulsanteMostraPasswordBackDisabled;
                return VisualManager.MetroTextBoxSpecialeButtonBackDisabled;
            }
            set { _pulsanteMostraPasswordBackDisabled = value; Refresh(); }
        }

        private Color _pulsanteMostraPasswordText = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteMostraPasswordText
        {
            get
            {
                if (_pulsanteMostraPasswordText != Color.Empty)
                    return _pulsanteMostraPasswordText;
                return VisualManager.MetroTextBoxSpecialeButtonTextNormal;
            }
            set { _pulsanteMostraPasswordText = value; Refresh(); }
        }

        private Color _pulsanteMostraPasswordTextHover = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteMostraPasswordTextHover
        {
            get
            {
                if (_pulsanteMostraPasswordTextHover != Color.Empty)
                    return _pulsanteMostraPasswordTextHover;
                return VisualManager.MetroTextBoxSpecialeButtonTextHover;
            }
            set { _pulsanteMostraPasswordTextHover = value; Refresh(); }
        }

        private Color _pulsanteMostraPasswordTextPressed = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteMostraPasswordTextPressed
        {
            get
            {
                if (_pulsanteMostraPasswordTextPressed != Color.Empty)
                    return _pulsanteMostraPasswordTextPressed;
                return VisualManager.MetroTextBoxSpecialeButtonTextPressed;
            }
            set { _pulsanteMostraPasswordTextPressed = value; Refresh(); }
        }

        private Color _pulsanteMostraPasswordTextDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PulsanteMostraPasswordTextDisabled
        {
            get
            {
                if (_pulsanteMostraPasswordTextDisabled != Color.Empty)
                    return _pulsanteMostraPasswordTextDisabled;
                return VisualManager.MetroTextBoxSpecialeButtonTextDisabled;
            }
            set { _pulsanteMostraPasswordTextDisabled = value; Refresh(); }
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

        private int _pulsanteVaiSize = 10;
        [Category(EtichetteDesigner.Stile)]
        public int PulsanteVaiSize
        {
            get { return _pulsanteVaiSize; }
            set { _pulsanteVaiSize = value; Refresh(); }
        }

        private int _pulsanteMostraPasswordSize = 10;
        [Category(EtichetteDesigner.Stile)]
        public int PulsanteMostraPasswordSize
        {
            get { return _pulsanteMostraPasswordSize; }
            set { _pulsanteMostraPasswordSize = value; Refresh(); }
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
        
        private bool _hoverPulsanteVai = false;
        [Browsable(false)]
        private bool HoverPulsanteVai
        {
            get { return _hoverPulsanteVai; }
            set { _hoverPulsanteVai = value; Refresh(); }
        }

        private bool _pressedPulsanteVai = false;
        [Browsable(false)]
        private bool PressedPulsanteVai
        {
            get { return _pressedPulsanteVai; }
            set { _pressedPulsanteVai = value; Refresh(); }
        }

        private bool _hoverPulsanteMostraPassword = false;
        [Browsable(false)]
        private bool HoverPulsanteMostraPassword
        {
            get { return _hoverPulsanteMostraPassword; }
            set { _hoverPulsanteMostraPassword = value; Refresh(); }
        }

        private bool _pressedPulsanteMostraPassword = false;
        [Browsable(false)]
        private bool PressedPulsanteMostraPassword
        {
            get { return _pressedPulsanteMostraPassword; }
            set { _pressedPulsanteMostraPassword = value; Refresh(); }
        }

        private bool _isHover = false;
        private bool _isSelected = false;

        private Rectangle _pulsanteMostraPasswordRect;
        private Rectangle _pulsanteVaiRect;
        
        #endregion

        #region Eventi

        public delegate void OnAutenticaEventHandler(object sender, MetroPasswordBoxAutenticaEventArgs e);
        public event OnAutenticaEventHandler Autentica = null;

        #endregion

        private MetroTextBox _textBox = null;

        public MetroPasswordBox()
            : base()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            _textBox = new MetroTextBox();
            _textBox.UseSystemPasswordChar = true;
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
            _textBox.Size = new Size(Width - 10 - ((Height - 4) * 2), Height - 4);

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
            HoverPulsanteMostraPassword = false;
            HoverPulsanteVai = false;            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Color clrBorder = Color.Empty;
            Color clrPulsanteVai = Color.Empty;
            Color clrTextPulsanteVai = Color.Empty;
            Color clrPulsanteMostraPassword = Color.Empty;
            Color clrTextPulsanteMostraPassword = Color.Empty;

            if (!Enabled)
            {
                if (BackColor != MetroBackgroundDisabled)
                    BackColor = MetroBackgroundDisabled;
                if (ForeColor != MetroTextDisabled)
                    ForeColor = MetroTextDisabled;

                WinApi.SendMessage(_textBox.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0); 

                clrBorder = MetroBorderDisabled;
                clrPulsanteVai = PulsanteVaiBackDisabled;
                clrTextPulsanteVai = PulsanteVaiTextDisabled;
                clrPulsanteMostraPassword = PulsanteMostraPasswordBackDisabled;
                clrTextPulsanteMostraPassword = PulsanteMostraPasswordTextDisabled;
            }
            else if (_isSelected)
            {
                if (BackColor != MetroBackgroundSelected)
                    BackColor = MetroBackgroundSelected;
                if (ForeColor != MetroTextSelected)
                    ForeColor = MetroTextSelected;

                WinApi.SendMessage(_textBox.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0);

                clrBorder = MetroBorderSelected;

                if (PressedPulsanteVai)
                {
                    clrPulsanteVai = PulsanteVaiBackPressed;
                    clrTextPulsanteVai = PulsanteVaiTextPressed;
                }
                else if (HoverPulsanteVai)
                {
                    clrPulsanteVai = PulsanteVaiBackHover;
                    clrTextPulsanteVai = PulsanteVaiTextHover;
                }
                else
                {
                    clrPulsanteVai = PulsanteVaiBack;
                    clrTextPulsanteVai = PulsanteVaiText;
                }

                if (PressedPulsanteMostraPassword)
                {
                    clrPulsanteMostraPassword = PulsanteMostraPasswordBackPressed;
                    clrTextPulsanteMostraPassword = PulsanteMostraPasswordTextPressed;
                }
                else if (HoverPulsanteMostraPassword)
                {
                    clrPulsanteMostraPassword = PulsanteMostraPasswordBackHover;
                    clrTextPulsanteMostraPassword = PulsanteMostraPasswordTextHover;
                }
                else
                {
                    clrPulsanteMostraPassword = PulsanteMostraPasswordBack;
                    clrTextPulsanteMostraPassword = PulsanteMostraPasswordText;
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

                if (PressedPulsanteVai)
                {
                    clrPulsanteVai = PulsanteVaiBackPressed;
                    clrTextPulsanteVai = PulsanteVaiTextPressed;
                }
                else if (HoverPulsanteVai)
                {
                    clrPulsanteVai = PulsanteVaiBackHover;
                    clrTextPulsanteVai = PulsanteVaiTextHover;
                }
                else
                {
                    clrPulsanteVai = PulsanteVaiBack;
                    clrTextPulsanteVai = PulsanteVaiText;
                }

                if (PressedPulsanteMostraPassword)
                {
                    clrPulsanteMostraPassword = PulsanteMostraPasswordBackPressed;
                    clrTextPulsanteMostraPassword = PulsanteMostraPasswordTextPressed;
                }
                else if (HoverPulsanteMostraPassword)
                {
                    clrPulsanteMostraPassword = PulsanteMostraPasswordBackHover;
                    clrTextPulsanteMostraPassword = PulsanteMostraPasswordTextHover;
                }
                else
                {
                    clrPulsanteMostraPassword = PulsanteMostraPasswordBack;
                    clrTextPulsanteMostraPassword = PulsanteMostraPasswordText;
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

                if (PressedPulsanteVai)
                {
                    clrPulsanteVai = PulsanteVaiBackPressed;
                    clrTextPulsanteVai = PulsanteVaiTextPressed;
                }
                else if (HoverPulsanteVai)
                {
                    clrPulsanteVai = PulsanteVaiBackHover;
                    clrTextPulsanteVai = PulsanteVaiTextHover;
                }
                else
                {
                    clrPulsanteVai = PulsanteVaiBack;
                    clrTextPulsanteVai = PulsanteVaiText;
                }

                if (PressedPulsanteMostraPassword)
                {
                    clrPulsanteMostraPassword = PulsanteMostraPasswordBackPressed;
                    clrTextPulsanteMostraPassword = PulsanteMostraPasswordTextPressed;
                }
                else if (HoverPulsanteMostraPassword)
                {
                    clrPulsanteMostraPassword = PulsanteMostraPasswordBackHover;
                    clrTextPulsanteMostraPassword = PulsanteMostraPasswordTextHover;
                }
                else
                {
                    clrPulsanteMostraPassword = PulsanteMostraPasswordBack;
                    clrTextPulsanteMostraPassword = PulsanteMostraPasswordText;
                }
            }

            g.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
            
            g.FillRectangle(new SolidBrush(clrPulsanteVai), _pulsanteVaiRect);
            g.FillRectangle(new SolidBrush(clrPulsanteMostraPassword), _pulsanteMostraPasswordRect);

            Font fontVai = new Font(VisualManager.MetroSymbolFont.FontFamily, PulsanteVaiSize);
            Font fontMostra = new Font(VisualManager.MetroSymbolFont.FontFamily, PulsanteMostraPasswordSize);

            TextRenderer.DrawText(g, "\uE111", fontVai, new Rectangle(_pulsanteVaiRect.X + 2, _pulsanteVaiRect.Y, _pulsanteVaiRect.Width - 2, _pulsanteVaiRect.Height), clrTextPulsanteVai, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine);
            TextRenderer.DrawText(g, "\uE052", fontMostra, new Rectangle(_pulsanteMostraPasswordRect.X + 2, _pulsanteMostraPasswordRect.Y, _pulsanteMostraPasswordRect.Width - 2, _pulsanteMostraPasswordRect.Height), clrTextPulsanteMostraPassword, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine);

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

            HoverPulsanteVai = _pulsanteVaiRect.Contains(e.Location);
            HoverPulsanteMostraPassword = _pulsanteMostraPasswordRect.Contains(e.Location);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            PressedPulsanteVai = (_pulsanteVaiRect.Contains(e.Location) && (e.Button == MouseButtons.Left));
            PressedPulsanteMostraPassword = (_pulsanteMostraPasswordRect.Contains(e.Location) && (e.Button == MouseButtons.Left));
            
            if (_pulsanteMostraPasswordRect.Contains(e.Location) && (e.Button == MouseButtons.Left))
                _textBox.UseSystemPasswordChar = false;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            PressedPulsanteVai = !(e.Button == MouseButtons.Left);
            PressedPulsanteMostraPassword = !(e.Button == MouseButtons.Left);
            
            if (e.Button == MouseButtons.Left)
                _textBox.UseSystemPasswordChar = true;

            if (_pulsanteVaiRect.Contains(e.Location) && (e.Button == MouseButtons.Left) && (Autentica != null))
                Autentica(this, new MetroPasswordBoxAutenticaEventArgs(Text));
        }

        protected void CalcolaPulsanti()
        {
            _pulsanteVaiRect = new Rectangle(Width - Height + 4, 4, Height - 8, Height - 8);
            _pulsanteMostraPasswordRect = new Rectangle(Width - (Height * 2) + 10, 4, Height - 8, Height - 8);
        }
    }

    public class MetroPasswordBoxAutenticaEventArgs : EventArgs
    {
        public string Password { get; set; }

        public MetroPasswordBoxAutenticaEventArgs(string password)
        {
            Password = password;
        }
    }
}
