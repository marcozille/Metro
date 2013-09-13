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

using Utility;

namespace Metro.Controlli
{
    public class MetroTile : Control, IMetroControl, IContainerControl
    {
        #region Enums

        public enum MetroTileSize { Grande, Media };
        public enum MetroTileClickPosition { Alto, Basso, Destra, Sinistra, Nessuno };

        #endregion

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

        private Color _tileColor = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color TileColor
        {
            get 
            {
                if (_tileColor != Color.Empty)
                    return _tileColor;
                return VisualManager.MetroTileBackColor; 
            }
            set { _tileColor = value; Refresh(); }
        }

        private Control activeControl = null;
        [Browsable(false)]
        public Control ActiveControl
        {
            get { return activeControl; }
            set { activeControl = value; }
        }

        [Category(EtichetteDesigner.Stile)]
        public bool CanAnimate { get; set; }

        private bool _mostraNumeroTile = false;
        [Category(EtichetteDesigner.Stile)]
        public bool MostraNumeroTile 
        { 
            get { return _mostraNumeroTile; }
            set { _mostraNumeroTile = value; Refresh(); }
        }

        private int _numeroTile = 0;
        [Category(EtichetteDesigner.Stile)]
        public int NumeroTile
        {
            get { return _numeroTile; }
            set 
            {
                if (value > 999)
                    throw new ArgumentOutOfRangeException("Il valore massimo del numero per le tile è 999");
                _numeroTile = value; 
                Refresh(); 
            }
        }

        [Browsable(false)]
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

        [Browsable(false)]
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

        private List<MetroTileImage> _immagini = new List<MetroTileImage>();
        [Category(EtichetteDesigner.Stile)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<MetroTileImage> Immagini
        {
            get { return _immagini; }
            set { _immagini = value; }
        }

        private MetroTileSize _dimensione = MetroTileSize.Media;
        [Category(EtichetteDesigner.Stile)]
        public MetroTileSize Dimensione
        {
            get { return _dimensione; }
            set { _dimensione = value; UpdateSize(); }
        }

        [Browsable(false)]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        protected Rectangle TileRectangle
        {
            get
            {
                Rectangle rc = ClientRectangle;
                rc.Inflate(-3, -3);
                return rc;
            }
        }
        
        private Image _icona = null;
        [Category(EtichetteDesigner.Stile)]
        public Image Icona
        {
            get { return _icona; }
            set 
            {
                if (value != null)
                {
                    if (value.Height > 48 || value.Width > 48)
                        throw new Exception("L'immagine può essere al massimo 48x48px");
                }

                _icona = value; 
                Refresh(); 
            }
        }

        private string _descrizione = string.Empty;
        [Category(EtichetteDesigner.Stile)]
        public string Descrizione
        {
            get { return _descrizione; }
            set { _descrizione = value; Refresh(); }
        }
        
        private bool _isMouseHover = false;
        private bool _isMousePressed = false;
        private bool _isSelected = false;

        private Point _mouseClickPos = Point.Empty;

        #endregion

        public MetroTile()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            Dimensione = MetroTileSize.Media;
            BackColor = VisualManager.MetroTileBackColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (BackColor != TileColor)
                BackColor = TileColor;

            e.Graphics.Clear(Parent.BackColor);

            if (_isMousePressed)
                DrawPressedTile(e.Graphics);
            else
                DrawTile(e.Graphics);
        }

        protected virtual void DrawPressedTile(Graphics baseGraphics)
        {
            Bitmap image = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);

            Graphics g = Graphics.FromImage(image);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.FillRectangle(new SolidBrush(BackColor), TileRectangle);

            DrawIcona(g);
            //DrawDescrizione(g);

            if (_isMouseHover)
                DrawHoverBorder(g);

            if (_isSelected)
                DrawSelectionBorder(g);

            #region Prospettiva
            Bitmap pBmp = null;

            MetroTileClickPosition pos = GetPosizioneClick();

            switch (pos)
            {
                case MetroTileClickPosition.Alto:
                    pBmp = Perspective.ImmagineProspettiva.PerspectiveMode2(image, Perspective.PerspectiveModus.VerticalUpMode2, ClientRectangle.Width - 6, true, true, 0, 0);
                    break;
                case MetroTileClickPosition.Destra:
                    pBmp = Perspective.ImmagineProspettiva.PerspectiveMode2(image, Perspective.PerspectiveModus.HorizontalDownMode2, ClientRectangle.Height - 6, true, true, 0, 0);
                    break;
                case MetroTileClickPosition.Basso:
                    pBmp = Perspective.ImmagineProspettiva.PerspectiveMode2(image, Perspective.PerspectiveModus.VerticalDownMode2, ClientRectangle.Width - 6, true, true, 0, 0);
                    break;
                case MetroTileClickPosition.Sinistra:
                    pBmp = Perspective.ImmagineProspettiva.PerspectiveMode2(image, Perspective.PerspectiveModus.HorizontalUpMode2, ClientRectangle.Height - 6, true, true, 0, 0);
                    break;
                default:
                    throw new Exception("Non si dovrebbe mai arrivare qui");
            }
            baseGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            baseGraphics.DrawImage(pBmp, 0, 0);
            DrawDescrizione(baseGraphics);
            #endregion
        }

        protected virtual void DrawTile(Graphics g)
        {
            g.FillRectangle(new SolidBrush(BackColor), TileRectangle);

            DrawIcona(g);
            DrawDescrizione(g);

            if (_isMouseHover)
                DrawHoverBorder(g);

            if (_isSelected)
                DrawSelectionBorder(g);
        }

        protected virtual void DrawDescrizione(Graphics g)
        {
            Rectangle textRc = new Rectangle(10, TileRectangle.Height - 20, TileRectangle.Width - 20, 20);

            TextFormatFlags tff = TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine;

            if (Dimensione == MetroTileSize.Media)
                tff |= TextFormatFlags.HorizontalCenter;

            System.Drawing.Drawing2D.SmoothingMode oldMode = g.SmoothingMode;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            TextRenderer.DrawText(g, Descrizione, VisualManager.MetroTileTextFont, textRc, VisualManager.MetroTileTextColor, tff);
            g.SmoothingMode = oldMode;
        }

        protected virtual void DrawHoverBorder(Graphics g)
        {
            Rectangle top, left, bottom, right;
            top = new Rectangle(0, 0, ClientRectangle.Width, 3);
            bottom = new Rectangle(0, ClientRectangle.Height - 3, ClientRectangle.Width, 3);
            left = new Rectangle(0, 3, 3, ClientRectangle.Height - 6);
            right = new Rectangle(ClientRectangle.Width - 3, 3, 3, ClientRectangle.Height - 6);

            g.FillRectangle(new SolidBrush(VisualManager.MetroTileHoverBorderColor), top);
            g.FillRectangle(new SolidBrush(VisualManager.MetroTileHoverBorderColor), bottom);
            g.FillRectangle(new SolidBrush(VisualManager.MetroTileHoverBorderColor), left);
            g.FillRectangle(new SolidBrush(VisualManager.MetroTileHoverBorderColor), right);
        }

        protected virtual void DrawIcona(Graphics g)
        {
            if (Icona == null)
                return;

            Rectangle rcIcona = new Rectangle(0, 0, Icona.Width, Icona.Height);

            int y = (ClientRectangle.Height - rcIcona.Height) / 2 - 10;
            int x = (ClientRectangle.Width - rcIcona.Width) / 2;

            if (MostraNumeroTile && NumeroTile > 0)
            {
                Rectangle rcCompleto = rcIcona;
                Rectangle rcText = new Rectangle(5, y, 19, rcIcona.Height);

                if (NumeroTile.ToString().Length == 2)
                    rcText.Width += 17;
                if (NumeroTile.ToString().Length == 3)
                    rcText.Width += 32;

                rcCompleto.Width += rcText.Width;

                x = (ClientRectangle.Width - rcCompleto.Width) / 2;
                rcIcona.Location = new Point(x, y);

                rcText.Location = new Point(rcIcona.Right + 5, y);
                TextRenderer.DrawText(g, NumeroTile.ToString(), VisualManager.MetroTileNumberFont, rcText, VisualManager.MetroTileTextColor);
            }
            else
                rcIcona.Location = new Point(x, y);

            g.DrawImage(Icona, rcIcona);
        }

        protected virtual void DrawSelectionBorder(Graphics g)
        {
            Rectangle top, left, bottom, right;
            top = new Rectangle(3, 3, TileRectangle.Width, 3);
            bottom = new Rectangle(3, TileRectangle.Height, TileRectangle.Width, 3);
            left = new Rectangle(3, 6, 3, TileRectangle.Height - 6);
            right = new Rectangle(TileRectangle.Width, 6, 3, TileRectangle.Height - 6);

            Point[] pts = new Point[] { new Point(TileRectangle.Width - 30, 3), new Point(TileRectangle.Width, 3), new Point(TileRectangle.Width, 33) };

            g.FillRectangle(new SolidBrush(VisualManager.MetroTileSelectedBorderColor), top);
            g.FillRectangle(new SolidBrush(VisualManager.MetroTileSelectedBorderColor), bottom);
            g.FillRectangle(new SolidBrush(VisualManager.MetroTileSelectedBorderColor), left);
            g.FillRectangle(new SolidBrush(VisualManager.MetroTileSelectedBorderColor), right);
            g.FillPolygon(new SolidBrush(VisualManager.MetroTileSelectedBorderColor), pts);

            Point pt = new Point(TileRectangle.Width - 22, 3);
            Font font = new Font(MetroGlobals.FontCollection.Families[1], 10f);
            TextRenderer.DrawText(g, "\uE081", font, pt, VisualManager.MetroTileTextColor);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _isMouseHover = true;
            Refresh();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isMouseHover = false;
            Refresh();

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _isMousePressed = true;
            _mouseClickPos = e.Location;
            Invalidate();

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isMousePressed = false;
            _mouseClickPos = Point.Empty;

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                _isSelected = !_isSelected;

            Invalidate();

            base.OnMouseUp(e);
        }

        public bool ActivateControl(Control ctrl)
        {
            if (Controls.Contains(ctrl))
            {
                ctrl.Select();
                activeControl = ctrl;
                return true;
            }

            return false;
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

            BackColor = VisualManager.MetroTileBackColor;
        }

        protected virtual MetroTileClickPosition GetPosizioneClick()
        {
            Point centerPoint = new Point(ClientRectangle.Width / 2, ClientRectangle.Height / 2);

            Point[] trSu = new Point[] { new Point(ClientRectangle.Left, ClientRectangle.Top), new Point(ClientRectangle.Right, ClientRectangle.Top), centerPoint };
            Point[] trDx = new Point[] { new Point(ClientRectangle.Right, ClientRectangle.Top), new Point(ClientRectangle.Right, ClientRectangle.Bottom), centerPoint };
            Point[] trGiu = new Point[] { new Point(ClientRectangle.Right, ClientRectangle.Bottom), new Point(ClientRectangle.Left, ClientRectangle.Bottom), centerPoint };
            Point[] trSx = new Point[] { new Point(ClientRectangle.Left, ClientRectangle.Bottom), new Point(ClientRectangle.Left, ClientRectangle.Top), centerPoint };

            if (ControllaPunto.ContenutoInTriangolo(_mouseClickPos, trSu))
                return MetroTileClickPosition.Alto;
            if (ControllaPunto.ContenutoInTriangolo(_mouseClickPos, trDx))
                return MetroTileClickPosition.Destra;
            if (ControllaPunto.ContenutoInTriangolo(_mouseClickPos, trGiu))
                return MetroTileClickPosition.Basso;
            if (ControllaPunto.ContenutoInTriangolo(_mouseClickPos, trSx))
                return MetroTileClickPosition.Sinistra;

            return MetroTileClickPosition.Nessuno;
        }

        protected virtual void UpdateSize()
        {
            switch (Dimensione)
            {
                case MetroTileSize.Media:
                    Size = new Size(120, 120);
                    break;
                case MetroTileSize.Grande:
                    Size = new Size(240, 120);
                    break;
            }
        }
    }

    public class MetroTileImage
    {
        public string Descrizione { get; set; }
        public Image Immagine { get; set; }

        public MetroTileImage()
        {
            Descrizione = string.Empty;
            Immagine = null;
        }
    }
}
