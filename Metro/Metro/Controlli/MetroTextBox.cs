using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

using Metro.Interfaccie;
using Metro.Componenti;
using Metro.Designer;

namespace Metro.Controlli
{
    public class MetroTextBox : TextBox
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
            set { _metroFont = value; Font = value; Refresh(); }
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new char PasswordChar
        {
            get { return base.PasswordChar; }
            set { base.PasswordChar = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new bool UseSystemPasswordChar
        {
            get { return base.UseSystemPasswordChar; }
            set { base.UseSystemPasswordChar = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }

        private string _cueBanner = string.Empty;
        [Category(EtichetteDesigner.Stile)]
        public string CueBanner
        {
            get { return _cueBanner; }
            set
            {
                if (value != string.Empty && value.Trim() != "")
                    WinApi.SendMessage(Handle, (int)WinApi.Messages.EM_SETCUEBANNER, true, value);
                else
                    WinApi.SendMessage(Handle, (int)WinApi.Messages.EM_SETCUEBANNER, false, "");

                _cueBanner = value;

                WinApi.SendMessage(this.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0); 
            }
        }

        private bool _isHover = false;
        private bool _isSelected = false;

        #endregion

        public MetroTextBox()
            : base()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            BackColor = MetroBackground;
            ForeColor = MetroText;
            Font = MetroFont;

            PasswordChar = (char)0;
            UseSystemPasswordChar = false;
        }
        
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (!Focused && Enabled == true)
            {
                _isHover = true;
                BackColor = MetroBackgroundHover;
                ForeColor = MetroTextHover;
                WinApi.SendMessage(this.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0); 
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (!Focused && Enabled == true)
            {
                _isHover = false;
                BackColor = MetroBackground;
                ForeColor = MetroText;
                WinApi.SendMessage(this.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0); 
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            if (!Enabled)
            {
                _isSelected = false;
                _isHover = false;
                BackColor = MetroBackgroundDisabled;
                ForeColor = MetroTextDisabled;
                WinApi.SendMessage(this.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0); 
            }
            else
            {
                BackColor = MetroBackground;
                ForeColor = MetroText;
                WinApi.SendMessage(this.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0); 
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (Enabled)
            {
                _isSelected = true;
                BackColor = MetroBackgroundSelected;
                ForeColor = MetroTextSelected;
                WinApi.SendMessage(this.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0); 
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            if (Enabled)
            {
                _isSelected = false;
                _isHover = false;
                BackColor = MetroBackground;
                ForeColor = MetroText;
                WinApi.SendMessage(this.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0); 
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (uint)WinApi.Messages.WM_NCPAINT)
            {
                IntPtr hdc = WinApi.GetWindowDC(m.HWnd);
                Graphics g = Graphics.FromHdc(hdc);
                OnNCPaint(g);
                WinApi.ReleaseDC(m.HWnd, hdc);
                return;
            }

            base.WndProc(ref m);
        }

        protected virtual void UpdateColors()
        {
            if (Enabled)
            {
                if (_isSelected)
                {
                    if (BackColor != MetroBackgroundSelected)
                        BackColor = MetroBackgroundSelected;
                    if (ForeColor != MetroTextSelected)
                        ForeColor = MetroTextSelected;
                }
                else if (_isHover)
                {
                    if (BackColor != MetroBackgroundHover)
                        BackColor = MetroBackgroundHover;
                    if (ForeColor != MetroTextHover)
                        ForeColor = MetroTextHover;
                }
                else
                {
                    if (BackColor != MetroBackground)
                        BackColor = MetroBackground;
                    if (ForeColor != MetroText)
                        ForeColor = MetroText;
                }
            }
            else
            {
                if (BackColor != MetroBackgroundDisabled)
                    BackColor = MetroBackgroundDisabled;
                if (ForeColor != MetroTextDisabled)
                    ForeColor = MetroTextDisabled;
            }
        }

        protected virtual void OnNCPaint(Graphics g)
        {
            UpdateColors();

            if (BorderStyle != System.Windows.Forms.BorderStyle.None && Parent != null)
            {
                if (Enabled)
                {
                    if (_isSelected)
                    {
                        Rectangle rc1 = new Rectangle(0, 0, Width, Height);
                        ControlPaint.DrawBorder(g, rc1, MetroBorderSelected, ButtonBorderStyle.Solid);
                        Rectangle rc2 = new Rectangle(1, 1, Width - 2, Height - 2);
                        ControlPaint.DrawBorder(g, rc2, MetroBackgroundSelected, ButtonBorderStyle.Solid);
                    }
                    else if (_isHover)
                    {
                        Rectangle rc1 = new Rectangle(0, 0, Width, Height);
                        ControlPaint.DrawBorder(g, rc1, MetroBorderHover, ButtonBorderStyle.Solid);
                        Rectangle rc2 = new Rectangle(1, 1, Width - 2, Height - 2);
                        ControlPaint.DrawBorder(g, rc2, MetroBackgroundHover, ButtonBorderStyle.Solid);
                    }
                    else
                    {
                        Rectangle rc1 = new Rectangle(0, 0, Width, Height);
                        ControlPaint.DrawBorder(g, rc1, MetroBorder, ButtonBorderStyle.Solid);
                        Rectangle rc2 = new Rectangle(1, 1, Width - 2, Height - 2);
                        ControlPaint.DrawBorder(g, rc2, MetroBackground, ButtonBorderStyle.Solid);
                    }
                }
                else
                {
                    Rectangle rc1 = new Rectangle(0, 0, Width, Height);
                    ControlPaint.DrawBorder(g, rc1, MetroBorderDisabled, ButtonBorderStyle.Solid);
                    Rectangle rc2 = new Rectangle(1, 1, Width - 2, Height - 2);
                    ControlPaint.DrawBorder(g, rc2, MetroBackgroundDisabled, ButtonBorderStyle.Solid);
                }
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (Parent != null)
            {
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

                WinApi.SendMessage(this.Handle, (int)WinApi.Messages.WM_NCPAINT, (int)(IntPtr)1, (int)(IntPtr)0);
            }
        }
    }
}
