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
using Metro.Controlli;

namespace Metro.Controlli
{
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
            set { _metroFont = value; Refresh(); }
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
        
        private bool _isHover = false;
        private bool _isSelected = false;

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
        }

        void _textBox_Leave(object sender, EventArgs e)
        {
            if (Enabled)
            {
                _isSelected = false;
                _isHover = false;
                BackColor = MetroBackground;
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
            }
        }

        void _textBox_MouseLeave(object sender, EventArgs e)
        {
            if (!Focused && !_textBox.Focused && Enabled == true)
            {
                _isHover = true;
                BackColor = MetroBackground;
                ForeColor = MetroText;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            _textBox.Location = new Point(3, 3);
            _textBox.Size = new Size(Width - 10 - ((Height - 4) * 2), Height - 4);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rc = new Rectangle(1, 1, Width - 2, Height - 2);

            g.FillRectangle(new SolidBrush(Parent.BackColor), ClientRectangle);
            g.FillRectangle(new SolidBrush(BackColor), rc);

            Color clrBorder = Color.Empty;

            if (!Enabled)
                clrBorder = MetroBorderDisabled;
            else if (_isSelected)
                clrBorder = MetroBorderSelected;
            else
                clrBorder = MetroBorder;

            ControlPaint.DrawBorder(g, rc, clrBorder, ButtonBorderStyle.Solid);
        }
    }
}
