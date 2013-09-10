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
    public class MetroComboBox : ComboBox, IMetroControl
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

        [DefaultValue(DrawMode.OwnerDrawFixed)]
        [Browsable(false)]
        public new DrawMode DrawMode
        {
            get { return DrawMode.OwnerDrawFixed; }
            set { base.DrawMode = DrawMode.OwnerDrawFixed; }
        }

        [DefaultValue(ComboBoxStyle.DropDownList)]
        [Browsable(false)]
        public new ComboBoxStyle DropDownStyle
        {
            get { return ComboBoxStyle.DropDownList; }
            set { base.DropDownStyle = ComboBoxStyle.DropDownList; }
        }

        private string _cueBanner = "";
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue("")]
        [Category(EtichetteDesigner.Stile)]
        public string CueBanner
        {
            get { return _cueBanner; }
            set
            {
                _cueBanner = value.Trim();
                Invalidate();
            }
        }

        private bool _isHover = false;
        private bool _hasFocus = false;

        private bool _drawCue = false;

        private Rectangle _buttonRectangle;
        private Rectangle _textRectangle;
        
        private const int OCM_COMMAND = 0x2111;
        private const int WM_PAINT = 15;

        #endregion

        public MetroComboBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.DropDownStyle = ComboBoxStyle.DropDownList;

            _drawCue = (SelectedIndex == -1);

            PosizionaPulsante();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.ItemHeight = GetPreferredSize(Size.Empty).Height;
            DrawBackGround(e.Graphics);
            DrawButton(e.Graphics);
            
            if (_drawCue)
                DrawCueText(e.Graphics);
            else
                DrawText(e.Graphics);

            DrawBorder(e.Graphics);
        }

        protected virtual void DrawBackGround(Graphics g)
        {
            Color clr = Color.Empty;

            if (Enabled)
            {
                if (DroppedDown)//(_isPressed)
                    clr = VisualManager.MetroComboBoxBackExpanded;
                else if (_isHover || _hasFocus)
                    clr = VisualManager.MetroComboBoxBackHover;
                else
                    clr = VisualManager.MetroComboBoxBackNormal;
            }
            else
                clr = VisualManager.MetroComboBoxBackDisabled;

            g.Clear(clr);
        }
        
        protected virtual void DrawBorder(Graphics g)
        {
            Color clr = Color.Empty;

            if (Enabled)
            {
                if (DroppedDown)//(_isPressed)
                    clr = VisualManager.MetroComboBoxBorderExpanded;
                else if (_isHover || _hasFocus)
                    clr = VisualManager.MetroComboBoxBorderHover;
                else
                    clr = VisualManager.MetroComboBoxBorderNormal;
            }
            else
                clr = VisualManager.MetroComboBoxBorderDisabled;

            ControlPaint.DrawBorder(g, ClientRectangle, clr, ButtonBorderStyle.Solid);
        }

        protected virtual void DrawButton(Graphics g)
        { 
            Color clr = Color.Empty;
            Color clrSym = Color.Empty;

            Point[] puntiSym = { new Point(_buttonRectangle.Left + ((_buttonRectangle.Width / 2) - 3) - 1, (Height / 2) - 2),
                                 new Point(_buttonRectangle.Left + ((_buttonRectangle.Width / 2) + 4) - 1, (Height / 2) - 2),
                                 new Point(_buttonRectangle.Left + (_buttonRectangle.Width / 2) - 1, (Height / 2) + 2) };

            if (Enabled)
            {
                if (DroppedDown)//(_isPressed)
                {
                    clr = VisualManager.MetroComboBoxButtonExpanded;
                    clrSym = VisualManager.MetroComboBoxFrecciaButtonExpanded;
                }
                else if (_isHover || _hasFocus)
                {
                    clr = VisualManager.MetroComboBoxButtonHover;
                    clrSym = VisualManager.MetroComboBoxFrecciaButtonHover;
                }
                else
                {
                    clr = VisualManager.MetroComboBoxButtonNormal;
                    clrSym = VisualManager.MetroComboBoxFrecciaButtonNormal;
                }
            }
            else
            {
                clr = VisualManager.MetroComboBoxButtonDisabled;
                clrSym = VisualManager.MetroComboBoxFrecciaButtonDisabled;
            }

            g.FillRectangle(new SolidBrush(clr), _buttonRectangle);
            g.FillPolygon(new SolidBrush(clrSym), puntiSym);
        }

        protected virtual void DrawCueText()
        {
            using (Graphics graphics = CreateGraphics())
            {
                DrawCueText(graphics);
            }
        }

        protected virtual void DrawCueText(Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Near;
            sf.Trimming = StringTrimming.EllipsisCharacter;

            Color clr = Color.Empty;

            if (Enabled)
            {
                if (DroppedDown)//(_isPressed)
                    clr = VisualManager.MetroComboBoxPromptTextExpanded;
                else if (_isHover || _hasFocus)
                    clr = VisualManager.MetroComboBoxPromptTextHover;
                else
                    clr = VisualManager.MetroComboBoxPromptTextNormal;
            }
            else
                clr = VisualManager.MetroComboBoxPromptTextDisabled;

            g.DrawString(CueBanner, VisualManager.MetroComboBoxFont, new SolidBrush(clr), _textRectangle, sf); 
        }

        protected virtual void DrawText(Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Near;
            sf.Trimming = StringTrimming.EllipsisCharacter;

            Color clr = Color.Empty;

            if (Enabled)
            {
                if (DroppedDown)//(_isPressed)
                    clr = VisualManager.MetroComboBoxTextExpanded;
                else if (_isHover || _hasFocus)
                    clr = VisualManager.MetroComboBoxTextHover;
                else
                    clr = VisualManager.MetroComboBoxTextNormal;
            }
            else
                clr = VisualManager.MetroComboBoxTextDisabled;

            g.DrawString(Text, VisualManager.MetroComboBoxFont, new SolidBrush(clr), _textRectangle, sf);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                Color clr = Color.Empty;
                Color clrText = Color.Empty;

                if (e.State == (DrawItemState.NoAccelerator | DrawItemState.NoFocusRect) || e.State == DrawItemState.None)
                {
                    clr = VisualManager.MetroComboBoxPanel;
                    clrText = VisualManager.MetroComboBoxItemTextNormal;
                }
                else
                {
                    clr = VisualManager.MetroComboBoxItemHover;
                    clrText = VisualManager.MetroComboBoxItemTextHover;
                }

                e.Graphics.FillRectangle(new SolidBrush(clr), e.Bounds);

                Rectangle textRect = new Rectangle(0, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
                TextRenderer.DrawText(e.Graphics, GetItemText(Items[e.Index]), VisualManager.MetroComboBoxFont, textRect, clrText, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
            else
            {
                base.OnDrawItem(e);
            }
        }

        #region Gestione
        protected override void OnDropDownClosed(EventArgs e)
        {
            _isHover = false;
            _hasFocus = false;
            Refresh();

            OnMouseLeave(new EventArgs());

            base.OnDropDownClosed(e);
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            _hasFocus = true;
            Refresh();

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            _hasFocus = false;
            _isHover = false;
            Refresh();

            base.OnLostFocus(e);
        }
        
        protected override void OnEnter(EventArgs e)
        {
            _hasFocus = true;
            Invalidate();

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            _hasFocus = false;
            _isHover = false;
            Refresh();

            base.OnLeave(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                _isHover = true;
                Refresh();
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            _isHover = false;
            Refresh();

            base.OnKeyUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _isHover = true;
            Refresh();

            base.OnMouseEnter(e);
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Refresh();

            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!_isHover)
            {
                _isHover = true;
                Refresh();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isHover = false;
            Refresh();

            base.OnMouseLeave(e);
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

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            _drawCue = (SelectedIndex == -1);
            Refresh();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            PosizionaPulsante();
        }

        protected virtual void PosizionaPulsante()
        {
            int larghezza = 17;
            _buttonRectangle = new Rectangle(Width - larghezza, 1, larghezza, Height - 2);
            _textRectangle = new Rectangle(2, 1, Width - larghezza - 5, Height - 2);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize;
            base.GetPreferredSize(proposedSize);

            using (var g = CreateGraphics())
            {
                string measureText = Text.Length > 0 ? Text : "MeasureText";
                proposedSize = new Size(int.MaxValue, int.MaxValue);
                preferredSize = TextRenderer.MeasureText(g, measureText, VisualManager.MetroComboBoxFont, proposedSize, TextFormatFlags.Left | TextFormatFlags.LeftAndRightPadding | TextFormatFlags.VerticalCenter);
                //preferredSize.Height += 1;
            }

            return preferredSize;
        }
        
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (((m.Msg == WM_PAINT) || (m.Msg == OCM_COMMAND)) && (_drawCue))
            {
                DrawCueText();
            }
        }        
        #endregion
    }
}
