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
    [Designer("Metro.Designer.MetroRoundButtonDesigner")]
    [DefaultEvent("Click")]
    public class MetroRoundButton : UserControl, IMetroControl
    {
        #region Proprietà
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
                return VisualManager.MetroRoundButtonCircleNormal;
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
                return VisualManager.MetroRoundButtonCircleHover;
            }
            set { _metroBorderHover = value; Refresh(); }
        }

        private Color _metroBorderPressed = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroBorderPressed
        {
            get
            {
                if (_metroBorderPressed != Color.Empty)
                    return _metroBorderPressed;
                return VisualManager.MetroRoundButtonCirclePressed;
            }
            set { _metroBorderPressed = value; Refresh(); }
        }
        
        private Color _metroBorderDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroBorderDisabled
        {
            get
            {
                if (_metroBorderDisabled != Color.Empty)
                    return _metroBorderDisabled;
                return VisualManager.MetroRoundButtonCircleDisabled;
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
                return VisualManager.MetroRoundButtonBackNormal;
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
                return VisualManager.MetroRoundButtonBackHover;
            }
            set { _metroBackgroundHover = value; Refresh(); }
        }

        private Color _metroBackgroundPressed = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroBackgroundPressed
        {
            get
            {
                if (_metroBackgroundPressed != Color.Empty)
                    return _metroBackgroundPressed;
                return VisualManager.MetroRoundButtonBackPressed;
            }
            set { _metroBackgroundPressed = value; Refresh(); }
        }

        private Color _metroBackgroundDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroBackgroundDisabled
        {
            get
            {
                if (_metroBackgroundDisabled != Color.Empty)
                    return _metroBackgroundDisabled;
                return VisualManager.MetroRoundButtonBackDisabled;
            }
            set { _metroBackgroundDisabled = value; Refresh(); }
        }

        private Color _metroImage = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroImage
        {
            get
            {
                if (_metroImage != Color.Empty)
                    return _metroImage;
                return VisualManager.MetroRoundButtonImageNormal;
            }
            set { _metroImage = value; Refresh(); }
        }

        private Color _metroImageHover = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroImageHover
        {
            get
            {
                if (_metroImageHover != Color.Empty)
                    return _metroImageHover;
                return VisualManager.MetroRoundButtonImageHover;
            }
            set { _metroImageHover = value; Refresh(); }
        }

        private Color _metroImagePressed = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroImagePressed
        {
            get
            {
                if (_metroImagePressed != Color.Empty)
                    return _metroImagePressed;
                return VisualManager.MetroRoundButtonImagePressed;
            }
            set { _metroImagePressed = value; Refresh(); }
        }

        private Color _metroImageDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MetroImageDisabled
        {
            get
            {
                if (_metroImageDisabled != Color.Empty)
                    return _metroImageDisabled;
                return VisualManager.MetroRoundButtonImageDisabled;
            }
            set { _metroImageDisabled = value; Refresh(); }
        }

        private int _borderSize = 3;
        [Category(EtichetteDesigner.Stile)]
        public int BorderSize
        {
            get { return _borderSize; }
            set { _borderSize = value; Refresh(); }
        }

        private TMetroRoundButtonType _tipoPulsante = TMetroRoundButtonType.Nessuno;
        [Category(EtichetteDesigner.Stile)]
        public TMetroRoundButtonType TipoPulsante
        {
            get { return _tipoPulsante; }
            set { _tipoPulsante = value; Refresh(); }
        }

        private TMetroRoundButtonSize _dimesionePulsante = TMetroRoundButtonSize.Medio;
        [Category(EtichetteDesigner.Stile)]
        public TMetroRoundButtonSize DimensionePulsante
        {
            get { return _dimesionePulsante; }
            set 
            { 
                _dimesionePulsante = value;

                switch (value)
                {
                    case TMetroRoundButtonSize.Piccolo: Size = new Size(31, 31); break;
                    case TMetroRoundButtonSize.Medio: Size = new Size(40, 40); break;
                    case TMetroRoundButtonSize.Grande: Size = new Size(50, 50); break;
                }

                Refresh(); 
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        #endregion

        #region Enums

        public enum TMetroRoundButtonType { Nessuno, Impostazioni, Pin, Unpin, Aggiungi, Rimuovi, Avanti, Indietro, Cestino }
        public enum TMetroRoundButtonSize { Grande, Medio, Piccolo }

        #endregion

        private bool _isHover = false;
        private bool _isPressed = false;

        public MetroRoundButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            DimensionePulsante = TMetroRoundButtonSize.Medio;
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

            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            g.Clear(Parent.BackColor);

            DrawBackground(g);
            DrawBorder(g);
            DrawImage(g);
        }

        protected virtual void DrawBackground(Graphics g)
        {
            Rectangle rc = new Rectangle(2, 2, ClientRectangle.Width - 4, ClientRectangle.Height - 4);

            if (!Enabled)
                g.FillEllipse(new SolidBrush(MetroBackgroundDisabled), rc);
            else
            {
                if (_isPressed)
                    g.FillEllipse(new SolidBrush(MetroBackgroundPressed), rc);
                else if (_isHover)
                    g.FillEllipse(new SolidBrush(MetroBackgroundHover), rc);
                else
                    g.FillEllipse(new SolidBrush(MetroBackground), rc);
            }
        }

        protected virtual void DrawBorder(Graphics g)
        {
            Rectangle rc = new Rectangle(2, 2, ClientRectangle.Width - 4, ClientRectangle.Height - 4);

            if (!Enabled)
                g.DrawEllipse(new Pen(MetroBorderDisabled, BorderSize), rc);
            else
            {
                if (_isPressed)
                    g.DrawEllipse(new Pen(MetroBorderPressed, BorderSize), rc);
                else if (_isHover)
                    g.DrawEllipse(new Pen(MetroBorderHover, BorderSize), rc);
                else
                    g.DrawEllipse(new Pen(MetroBorder, BorderSize), rc);
            }
        }

        protected virtual void DrawImage(Graphics g)
        {
            RectangleF rc = new RectangleF();
            int fontSize = 15;
            string immagine = "";

            if (TipoPulsante == TMetroRoundButtonType.Aggiungi)
                immagine = "\uE109";
            if (TipoPulsante == TMetroRoundButtonType.Avanti)
                immagine = "\uE111";
            if (TipoPulsante == TMetroRoundButtonType.Cestino)
                immagine = "\uE107";
            if (TipoPulsante == TMetroRoundButtonType.Impostazioni)
                immagine = "\uE115";
            if (TipoPulsante == TMetroRoundButtonType.Indietro)
                immagine = "\uE112";
            if (TipoPulsante == TMetroRoundButtonType.Pin)
                immagine = "\uE141";
            if (TipoPulsante == TMetroRoundButtonType.Rimuovi)
                immagine = "\uE10A";
            if (TipoPulsante == TMetroRoundButtonType.Unpin)
                immagine = "\uE196";


            switch (DimensionePulsante)
            {
                case TMetroRoundButtonSize.Piccolo:
                    fontSize = 12;
                    rc = new RectangleF((float)3.8, (float)5.0, (float)ClientRectangle.Width - (float)7.6, (float)ClientRectangle.Height - (float)10.0);
                    break;
                case TMetroRoundButtonSize.Medio:
                    fontSize = 15;
                    rc = new RectangleF((float)5.1, (float)6.0, (float)ClientRectangle.Width - (float)10.2, (float)ClientRectangle.Height - (float)12.0);
                    break;
                case TMetroRoundButtonSize.Grande:
                    fontSize = 19;
                    rc = new RectangleF((float)5.5, (float)7.5, (float)ClientRectangle.Width - (float)11.0, (float)ClientRectangle.Height - (float)15.0);
                    break;
            }

            Font font = new Font(VisualManager.MetroSymbolFont.FontFamily, fontSize);

            StringFormat stringFormat = new StringFormat();
            
            if (!Enabled)
                g.DrawString(immagine, font, new SolidBrush(MetroImageDisabled), rc, stringFormat);
            else
            {
                if (_isPressed)
                    g.DrawString(immagine, font, new SolidBrush(MetroImagePressed), rc, stringFormat);
                else if (_isHover)
                    g.DrawString(immagine, font, new SolidBrush(MetroImageHover), rc, stringFormat);
                else
                    g.DrawString(immagine, font, new SolidBrush(MetroImage), rc, stringFormat);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
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
        }
    }
}
