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
    public class MetroRoundButtonText : UserControl, IMetroControl
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

        [Category(EtichetteDesigner.Stile)]
        public Color ButtonNormalColor
        {
            get { return _button.MetroBorder; }
            set
            {
                _button.MetroBorder = value;
                _button.MetroImage = value;
            }
        }

        [Category(EtichetteDesigner.Stile)]
        public Color ButtonHoverColor
        {
            get { return _button.MetroBorderHover; }
            set
            {
                _button.MetroBorderHover = value;
                _button.MetroImageHover = value;
            }
        }

        [Category(EtichetteDesigner.Stile)]
        public Color ButtonPressedColor
        {
            get { return _button.MetroBorderPressed; }
            set
            {
                _button.MetroBorderPressed = value;
                _button.MetroImagePressed = value;
            }
        }

        [Category(EtichetteDesigner.Stile)]
        public Color ButtonDisabledColor
        {
            get { return _button.MetroBorderDisabled; }
            set
            {
                _button.MetroBorderDisabled = value;
                _button.MetroImageDisabled = value;
            }
        }

        private Color _textColor = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color TextColor
        {
            get 
            {
                if (_textColor != Color.Empty)
                    return _textColor;
                return VisualManager.MetroRoundButtonTextNormal; 
            }
            set { _textColor = value; Refresh(); }
        }

        private Color _textColorHover = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color TextColorHover
        {
            get
            {
                if (_textColorHover != Color.Empty)
                    return _textColorHover;
                return VisualManager.MetroRoundButtonTextHover;
            }
            set { _textColorHover = value; Refresh(); }
        }

        private Color _textColorDisabled = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color TextColorDisabled
        {
            get
            {
                if (_textColorDisabled != Color.Empty)
                    return _textColorDisabled;
                return VisualManager.MetroRoundButtonTextDisabled;
            }
            set { _textColorDisabled = value; Refresh(); }
        }

        [Category(EtichetteDesigner.Stile)]
        public MetroRoundButton.TMetroRoundButtonSize DimensionePulsante
        {
            get { return _button.DimensionePulsante; }
            set { _button.DimensionePulsante = value; PosizionaPulsante(); }
        }

        private MetroRoundButton.TMetroRoundButtonType _tipoPulsante = MetroRoundButton.TMetroRoundButtonType.Nessuno;
        [Category(EtichetteDesigner.Stile)]
        public MetroRoundButton.TMetroRoundButtonType TipoPulsante
        {
            get { return _button.TipoPulsante; }
            set { _button.TipoPulsante = value; Refresh(); }
        }

        private string _text = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true), Category(EtichetteDesigner.Stile)]
        new public string Text
        {
            get { return _text; }
            set { _text = value; Refresh(); }
        }

        private MetroRoundButton _button;
        
        #endregion

        public MetroRoundButtonText()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            _button = new MetroRoundButton();

            Controls.Add(_button);

            _button.DimensionePulsante = MetroRoundButton.TMetroRoundButtonSize.Grande;
            Size = new Size(_button.Size.Width, _button.Size.Height + 20);

            Text = "Button";
        }

        protected virtual void PosizionaPulsante()
        {
            int x = (ClientRectangle.Width - _button.Size.Width) / 2;
            int y = 0;

            _button.Location = new Point(x, y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


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

            _button.Parent = null;
            _button.Parent = this;
        }
    }
}
