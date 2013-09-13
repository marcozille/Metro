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
            set { _numeroTile = value; Refresh(); }
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
                rc.Inflate(-2, -2);
                return rc;
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
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            g.FillRectangle(new SolidBrush(BackColor), TileRectangle);

            DrawDescrizione(g);

            if (_isMouseHover)
                DrawHoverBorder(g);

            #region Prospettiva
            Bitmap pBmp = null;

            MetroTileClickPosition pos = GetPosizioneClick();
            int[] drawingPos = new int[2];
            PointF[] puntiRett = new PointF[4];

            switch (pos)
            {
                case MetroTileClickPosition.Alto:
                    pBmp = Perspective.ImmagineProspettiva.PerspectiveMode2(image, Perspective.PerspectiveModus.HorizontalUpMode2, ClientRectangle.Width - 6, true, true, 0, 0);
                    puntiRett[0] = new PointF(3, 5);
                    puntiRett[1] = new PointF(ClientRectangle.Width - 3, 5);
                    puntiRett[2] = new PointF(ClientRectangle.Width, ClientRectangle.Height);
                    puntiRett[3] = new PointF(0, ClientRectangle.Height);
                    drawingPos[0] = 0;
                    drawingPos[1] = 5;
                    break;
                case MetroTileClickPosition.Destra:
                    pBmp = Perspective.ImmagineProspettiva.PerspectiveMode2(image, Perspective.PerspectiveModus.VerticalDownMode2, ClientRectangle.Height - 6, true, true, 0, 0);
                    puntiRett[0] = new PointF(0, 0);
                    puntiRett[1] = new PointF(ClientRectangle.Width - 5, 3);
                    puntiRett[2] = new PointF(ClientRectangle.Width - 5, ClientRectangle.Height - 3);
                    puntiRett[3] = new PointF(0, ClientRectangle.Height);
                    drawingPos[0] = 0;
                    drawingPos[1] = 0;
                    break;
                case MetroTileClickPosition.Basso:
                    pBmp = Perspective.ImmagineProspettiva.PerspectiveMode2(image, Perspective.PerspectiveModus.HorizontalDownMode2, ClientRectangle.Width - 6, true, true, 0, 0);
                    puntiRett[0] = new PointF(0, 0);
                    puntiRett[1] = new PointF(ClientRectangle.Width, 0);
                    puntiRett[2] = new PointF(ClientRectangle.Width - 3, ClientRectangle.Height - 5);
                    puntiRett[3] = new PointF(3, ClientRectangle.Height - 5);
                    drawingPos[0] = 0;
                    drawingPos[1] = 0;
                    break;
                case MetroTileClickPosition.Sinistra:
                    pBmp = Perspective.ImmagineProspettiva.PerspectiveMode2(image, Perspective.PerspectiveModus.VerticalUpMode2, ClientRectangle.Height - 6, true, true, 0, 0);
                    puntiRett[0] = new PointF(5, 3);
                    puntiRett[1] = new PointF(ClientRectangle.Width, 0);
                    puntiRett[2] = new PointF(ClientRectangle.Width, ClientRectangle.Height);
                    puntiRett[3] = new PointF(5, ClientRectangle.Height - 3);
                    drawingPos[0] = 5;
                    drawingPos[1] = 0;
                    break;
                default:
                    throw new Exception("Non si dovrebbe mai arrivare qui");
            }

            int h = pBmp.Height;
            int w = pBmp.Width;
            baseGraphics.DrawImage(pBmp, 0, 0);

            //YLScsDrawing.Imaging.Filters.FreeTransform tranform = new YLScsDrawing.Imaging.Filters.FreeTransform();
            //tranform.Bitmap = image;
            //tranform.FourCorners = puntiRett;
            //tranform.IsBilinearInterpolation = true;

            //using (Bitmap perspectBmp = tranform.Bitmap)
            //{
            //    baseGraphics.DrawImage(perspectBmp, drawingPos[0], drawingPos[1]);
            //}
            #endregion
        }

        protected virtual void DrawTile(Graphics g)
        {
            g.FillRectangle(new SolidBrush(BackColor), TileRectangle);

            DrawDescrizione(g);

            if (_isMouseHover)
                DrawHoverBorder(g);
        }

        protected virtual void DrawDescrizione(Graphics g)
        {
            Rectangle textRc = new Rectangle(10, TileRectangle.Height - 20, TileRectangle.Width - 20, 20);

            TextFormatFlags tff = TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine;

            if (Dimensione == MetroTileSize.Media)
                tff |= TextFormatFlags.HorizontalCenter;
            
            TextRenderer.DrawText(g, Descrizione, VisualManager.MetroTileTextFont, textRc, VisualManager.MetroTileTextColor, tff);
        }

        protected virtual void DrawHoverBorder(Graphics g)
        {
            Rectangle top, left, bottom, right;
            top = new Rectangle(0, 0, ClientRectangle.Width, 2);
            bottom = new Rectangle(0, ClientRectangle.Height - 2, ClientRectangle.Width, 2);
            left = new Rectangle(0, 2, 2, ClientRectangle.Height - 4);
            right = new Rectangle(ClientRectangle.Width - 2, 2, 2, ClientRectangle.Height - 4);

            g.FillRectangle(new SolidBrush(VisualManager.MetroTileHoverBorderColor), top);
            g.FillRectangle(new SolidBrush(VisualManager.MetroTileHoverBorderColor), bottom);
            g.FillRectangle(new SolidBrush(VisualManager.MetroTileHoverBorderColor), left);
            g.FillRectangle(new SolidBrush(VisualManager.MetroTileHoverBorderColor), right);
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
                    Size = new Size(100, 100);
                    break;
                case MetroTileSize.Grande:
                    Size = new Size(200, 100);
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
