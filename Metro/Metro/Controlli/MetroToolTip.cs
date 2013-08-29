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
    public class MetroToolTip : ToolTip, IMetroComponent
    {
        [Category(EtichetteDesigner.Stile)]
        public StileMetro StileMetro
        {
            get { return VisualManager.StileMetro; }
            set { VisualManager.StileMetro = value; }
        }

        [Category(EtichetteDesigner.Stile)]
        public CombinazionaColori CombinazioneColori
        {
            get { return VisualManager.CombinazioneColori; }
            set { VisualManager.CombinazioneColori = value; }
        }

        private MetroVisualManager _visualManager = new MetroVisualManager();
        [Browsable(false)]
        public MetroVisualManager VisualManager
        {
            get { return _visualManager; }
            set { _visualManager = value; }
        }

        private Color _metroBackgroundColor = Color.Transparent;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroBackgroundColor
        {
            get
            {
                if (_metroBackgroundColor != Color.Transparent)
                    return _metroBackgroundColor;
                return VisualManager.MetroToolTipBackgroundColor;
            }
            set { _metroBackgroundColor = value; }
        }
        
        private Color _metroBorderColor = Color.Transparent;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroBorderColor
        {
            get
            {
                if (_metroBorderColor != Color.Transparent)
                    return _metroBorderColor;
                return VisualManager.MetroToolTipBorderColor;
            }
            set { _metroBorderColor = value; }
        }

        private Color _metroTextColor = Color.Transparent;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroTextColor
        {
            get
            {
                if (_metroTextColor != Color.Transparent)
                    return _metroTextColor;
                return VisualManager.MetroToolTipTextColor;
            }
            set { _metroTextColor = value; }
        }

        private Font _metroFont = null;
        [Category(EtichetteDesigner.Stile)]
        public Font MetroFont
        {
            get
            {
                if (_metroFont != null)
                    return _metroFont;
                return VisualManager.MetroToolTipFont;
            }
            set { _metroFont = value; }
        }

        #region Campi

        [DefaultValue(true)]
        [Browsable(false)]
        public new bool ShowAlways
        {
            get { return base.ShowAlways; }
            set { base.ShowAlways = true; }
        }

        [DefaultValue(true)]
        [Browsable(false)]
        public new bool OwnerDraw
        {
            get { return base.OwnerDraw; }
            set { base.OwnerDraw = true; }
        }

        [Browsable(false)]
        public new bool IsBalloon
        {
            get { return base.IsBalloon; }
            set { base.IsBalloon = false; }
        }

        [Browsable(false)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [Browsable(false)]
        public new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [Browsable(false)]
        public new string ToolTipTitle
        {
            get { return base.ToolTipTitle; }
            set { base.ToolTipTitle = ""; }
        }

        [Browsable(false)]
        public new ToolTipIcon ToolTipIcon
        {
            get { return base.ToolTipIcon; }
            set { base.ToolTipIcon = ToolTipIcon.None; }
        }

        #endregion

        public MetroToolTip()
        {
            OwnerDraw = true;
            ShowAlways = true;

            Draw += new DrawToolTipEventHandler(MetroToolTip_Draw);
            Popup += new PopupEventHandler(MetroToolTip_Popup);
        }

        public new void SetToolTip(Control control, string caption)
        {
            base.SetToolTip(control, caption);

            if (control is IMetroControl)
            {
                foreach (Control c in control.Controls)
                {
                    SetToolTip(c, caption);
                }
            }
        }

        private void MetroToolTip_Popup(object sender, PopupEventArgs e)
        {
            if (e.AssociatedWindow is IMetroWindow)
            {
                StileMetro = ((IMetroWindow)e.AssociatedWindow).StileMetro;
                CombinazioneColori = ((IMetroWindow)e.AssociatedWindow).CombinazioneColori;
                VisualManager = ((IMetroWindow)e.AssociatedWindow).VisualManager;
            }
            else if (e.AssociatedControl is IMetroControl)
            {
                StileMetro = ((IMetroControl)e.AssociatedWindow).StileMetro;
                CombinazioneColori = ((IMetroControl)e.AssociatedWindow).CombinazioneColori;
                VisualManager = ((IMetroControl)e.AssociatedWindow).VisualManager;
            }

            e.ToolTipSize = new Size(e.ToolTipSize.Width + 24, e.ToolTipSize.Height + 9);
        }

        private void MetroToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(new SolidBrush(MetroBackgroundColor), e.Bounds);
            TextRenderer.DrawText(e.Graphics, e.ToolTipText, MetroFont, e.Bounds, MetroTextColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            ControlPaint.DrawBorder(g, e.Bounds, MetroBorderColor, ButtonBorderStyle.Solid);
        }
    }
}
