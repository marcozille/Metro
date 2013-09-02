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
    public class MetroButton : Button, IMetroControl
    {
        #region proprietà
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category(EtichetteDesigner.Stile)]
        public StileMetro StileMetro
        {
            get { return VisualManager.StileMetro; }
            set { VisualManager.StileMetro = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category(EtichetteDesigner.Stile)]
        public CombinazionaColori CombinazioneColori
        {
            get { return VisualManager.CombinazioneColori; }
            set { VisualManager.CombinazioneColori = value; }
        }

        private MetroVisualManager _visualManager = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public MetroVisualManager VisualManager
        {
            get { return _visualManager; }
            set { _visualManager = value; Refresh(); }
        }

        private Color _metroBorder = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroBorder
        {
            get
            {
                if (_metroBorder != Color.Empty)
                    return _metroBorder;
                return VisualManager.MetroButtonBorder;
            }
            set { _metroBorder = value; Refresh(); }
        }

        private Color _metroBorderDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroBorderDisabled
        {
            get
            {
                if (_metroBorderDisabled != Color.Empty)
                    return _metroBorderDisabled;
                return VisualManager.MetroButtonBorderDisabled;
            }
            set { _metroBorderDisabled = value; Refresh(); }
        }

        private Color _metroBackground = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroBackground
        {
            get
            {
                if (_metroBackground != Color.Empty)
                    return _metroBackground;
                return VisualManager.MetroButtonBackgroundNormal;
            }
            set { _metroBackground = value; Refresh(); }
        }

        private Color _metroBackgroundHover = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroBackgroundHover
        {
            get
            {
                if (_metroBackgroundHover != Color.Empty)
                    return _metroBackgroundHover;
                return VisualManager.MetroButtonBackgroundHover;
            }
            set { _metroBackgroundHover = value; Refresh(); }
        }

        private Color _metroBackgroundPressed = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroBackgroundPressed
        {
            get
            {
                if (_metroBackgroundPressed != Color.Empty)
                    return _metroBackgroundPressed;
                return VisualManager.MetroButtonBackgroundPressed;
            }
            set { _metroBackgroundPressed = value; Refresh(); }
        }

        private Color _metroBackgroundDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroBackgroundDisabled
        {
            get
            {
                if (_metroBackgroundDisabled != Color.Empty)
                    return _metroBackgroundDisabled;
                return VisualManager.MetroButtonBackgroundDisabled;
            }
            set { _metroBackgroundDisabled = value; Refresh(); }
        }

        private Color _metroText = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroText
        {
            get
            {
                if (_metroText != Color.Empty)
                    return _metroText;
                return VisualManager.MetroButtonTextNormal;
            }
            set { _metroText = value; Refresh(); }
        }

        private Color _metroTextHover = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroTextHover
        {
            get
            {
                if (_metroTextHover != Color.Empty)
                    return _metroTextHover;
                return VisualManager.MetroButtonTextHover;
            }
            set { _metroTextHover = value; Refresh(); }
        }

        private Color _metroTextPressed = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroTextPressed
        {
            get
            {
                if (_metroTextPressed != Color.Empty)
                    return _metroTextPressed;
                return VisualManager.MetroButtonTextPressed;
            }
            set { _metroTextPressed = value; Refresh(); }
        }

        private Color _metroTextDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroTextDisabled
        {
            get
            {
                if (_metroTextDisabled != Color.Empty)
                    return _metroTextDisabled;
                return VisualManager.MetroButtonTextDisabled;
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
                return VisualManager.MetroButtonFont;
            }
            set { _metroFont = value; Refresh(); }
        }
        #endregion

        private bool _isHover = false;
        private bool _isPressed = false;

        public MetroButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _isHover = true;
            Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isPressed = true;
                Invalidate();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isPressed = false;
            Invalidate();

            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isHover = false;
            Invalidate();

            base.OnMouseLeave(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;

            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis | TextFormatFlags.SingleLine;

            if (TextAlign == ContentAlignment.BottomCenter || TextAlign == ContentAlignment.MiddleCenter || TextAlign == ContentAlignment.TopCenter)
                flags |= TextFormatFlags.HorizontalCenter;
            else if (TextAlign == ContentAlignment.BottomLeft || TextAlign == ContentAlignment.MiddleLeft || TextAlign == ContentAlignment.TopLeft)
                flags |= TextFormatFlags.Left;
            else if (TextAlign == ContentAlignment.BottomRight || TextAlign == ContentAlignment.MiddleRight || TextAlign == ContentAlignment.TopRight)
                flags |= TextFormatFlags.Right;

            if (RightToLeft == System.Windows.Forms.RightToLeft.Yes)
                flags |= TextFormatFlags.RightToLeft;

            if (!Enabled)
            {
                g.FillRectangle(new SolidBrush(MetroBackgroundDisabled), ClientRectangle);
                ControlPaint.DrawBorder(g, ClientRectangle,MetroBorderDisabled, ButtonBorderStyle.Solid);
                TextRenderer.DrawText(g, Text, MetroFont, ClientRectangle, MetroTextDisabled, flags);
            }
            else
            {
                if (_isPressed)
                {
                    g.FillRectangle(new SolidBrush(MetroBackgroundPressed), ClientRectangle);
                    ControlPaint.DrawBorder(g, ClientRectangle, MetroBorder, ButtonBorderStyle.Solid);
                    TextRenderer.DrawText(g, Text, MetroFont, ClientRectangle, MetroTextPressed, flags);
                }
                else if (_isHover)
                {
                    g.FillRectangle(new SolidBrush(MetroBackgroundHover), ClientRectangle);
                    ControlPaint.DrawBorder(g, ClientRectangle, MetroBorder, ButtonBorderStyle.Solid);
                    TextRenderer.DrawText(g, Text, MetroFont, ClientRectangle, MetroTextHover, flags);
                }
                else
                {
                    g.FillRectangle(new SolidBrush(MetroBackground), ClientRectangle);
                    ControlPaint.DrawBorder(g, ClientRectangle, MetroBorder, ButtonBorderStyle.Solid);
                    TextRenderer.DrawText(g, Text, MetroFont, ClientRectangle, MetroText, flags);
                }
            }
        }
    }
}
