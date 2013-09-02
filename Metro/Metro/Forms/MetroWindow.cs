using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security;

using Metro.Componenti;
using Metro.Designer;
using Metro.Interfaccie;

namespace Metro.Forms
{
    public class MetroWindow : Form, IMetroWindow
    {
        #region enums

        public enum MetroWindowBorderStyle { Nessuno, Fisso, Ridimensionabile }
        public enum MetroWindowTitlePosition { Centro, Destra, Sinistra }

        #endregion

        #region Proprietà

        private MetroWindowBorderStyle _stileBordo = MetroWindowBorderStyle.Ridimensionabile;
        [Category(EtichetteDesigner.Stile)]
        public MetroWindowBorderStyle StileBordo
        {
            get { return _stileBordo; }
            set { _stileBordo = value; Refresh(); }
        }

        [Category(EtichetteDesigner.Stile)]
        public StileMetro StileMetro
        {
            get { return VisualManager.StileMetro; }
            set { VisualManager.StileMetro = value; Refresh(); }
        }

        [Category(EtichetteDesigner.Stile)]
        public CombinazionaColori CombinazioneColori
        {
            get { return VisualManager.CombinazioneColori; }
            set { VisualManager.CombinazioneColori = value; Refresh(); }
        }

        private MetroVisualManager _visualManager = new MetroVisualManager();
        [Browsable(false)]
        public MetroVisualManager VisualManager
        {
            get { return _visualManager; }
            set { _visualManager = value; Refresh(); }
        }

        private Color _metroBackgroundColor = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroBackgroundColor
        {
            get
            {
                if (_metroBackgroundColor != Color.Empty)
                    return _metroBackgroundColor;
                return VisualManager.MetroWindowBackgroundColor;
            }
            set { _metroBackgroundColor = value; Refresh(); }
        }

        private Color _metroBorderColor = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroBorderColor
        {
            get
            {
                if (!IsActive && !DesignMode)
                    return VisualManager.MetroWindowBorderColorDisabled;

                if (_metroBorderColor != Color.Empty)
                    return _metroBorderColor;
                return VisualManager.MetroWindowBorderColorNormal;
            }
            set { _metroBorderColor = value; Refresh(); }
        }

        private Color _metroTitleTextColor = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroTitleTextColor
        {
            get
            {
                if (_metroTitleTextColor != Color.Empty)
                    return _metroTitleTextColor;
                return VisualManager.MetroWindowTitleColor;
            }
            set { _metroTitleTextColor = value; Refresh(); }
        }

        private Font _metroTitleFont = null;
        [Category(EtichetteDesigner.Stile)]
        public Font MetroTitleFont
        {
            get
            {
                if (_metroTitleFont != null)
                    return _metroTitleFont;
                return VisualManager.MetroWindowTitleFont;
            }
            set { _metroTitleFont = value; Refresh(); }
        }

        private Color _metroTitleTextBackgroundColor = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroTitleBackgroundColor
        {
            get
            {
                if (_metroTitleTextBackgroundColor != Color.Empty)
                    return _metroTitleTextBackgroundColor;
                return VisualManager.MetroWindowTitleBackgroundColor;
            }
            set { _metroTitleTextBackgroundColor = value; Refresh(); }
        }

        private Color _metroSysButtonNormal = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroSysButtonNormal
        {
            get
            {
                if (_metroSysButtonNormal != Color.Empty)
                    return _metroSysButtonNormal;
                return VisualManager.MetroWindowSysButtonBackNormal;
            }
            set { _metroSysButtonNormal = value; Refresh(); }
        }

        private Color _metroSysButtonHover = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroSysButtonHover
        {
            get
            {
                if (_metroSysButtonHover != Color.Empty)
                    return _metroSysButtonHover;
                return VisualManager.MetroWindowSysButtonBackHover;
            }
            set { _metroSysButtonHover = value; Refresh(); }
        }

        private Color _metroSysButtonPressed = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroSysButtonPressed
        {
            get
            {
                if (_metroSysButtonPressed != Color.Empty)
                    return _metroSysButtonPressed;
                return VisualManager.MetroWindowSysButtonBackPressed;
            }
            set { _metroSysButtonPressed = value; Refresh(); }
        }

        private Color _metroSysButtonTextNormal = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroSysButtonTextNormal
        {
            get
            {
                if (_metroSysButtonTextNormal != Color.Empty)
                    return _metroSysButtonTextNormal;
                return VisualManager.MetroWindowSysButtonTextNormal;
            }
            set { _metroSysButtonTextNormal = value; Refresh(); }
        }

        private Color _metroSysButtonTextHover = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroSysButtonTextHover
        {
            get
            {
                if (_metroSysButtonTextHover != Color.Empty)
                    return _metroSysButtonTextHover;
                return VisualManager.MetroWindowSysButtonTextHover;
            }
            set { _metroSysButtonTextHover = value; Refresh(); }
        }

        private Color _metroSysButtonTextPressed = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroSysButtonTextPressed
        {
            get
            {
                if (_metroSysButtonTextPressed != Color.Empty)
                    return _metroSysButtonTextPressed;
                return VisualManager.MetroWindowSysButtonTextPressed;
            }
            set { _metroSysButtonTextPressed = value; Refresh(); }
        }

        private Color _metroResizeGripColor = Color.Empty;
        [Category(EtichetteDesigner.Stile)]
        public Color MetroResizeGripColor
        {
            get
            {
                if (_metroResizeGripColor != Color.Empty)
                    return _metroResizeGripColor;
                return VisualManager.MetroWindowResizeGripColor;
            }
            set { _metroResizeGripColor = value; Refresh(); }
        }

        private bool _mostraTitolo = true;
        [Category(EtichetteDesigner.Stile)]
        public bool MostraTitolo
        {
            get { return _mostraTitolo; }
            set { _mostraTitolo = value; Refresh(); }
        }

        private int _altezzaTitolo = 30;
        [Category(EtichetteDesigner.Stile)]
        public int AltezzaTitolo
        {
            get 
            {
                if (ShowIcon && Icon != null)
                    return Math.Max(Icon.Height + 6, _altezzaTitolo);
                return _altezzaTitolo; 
            }
            set { _altezzaTitolo = value; Refresh(); }
        }

        private MetroWindowTitlePosition _posizioneTitolo = MetroWindowTitlePosition.Sinistra;
        [Category(EtichetteDesigner.Stile)]
        public MetroWindowTitlePosition PosizioneTitolo
        {
            get { return _posizioneTitolo; }
            set { _posizioneTitolo = value; Refresh(); }
        }

        public new Padding Padding
        {
            get { return base.Padding; }
            set
            {
                value.Top = Math.Max(value.Top, MostraTitolo ? AltezzaTitolo : 10);
                value.Left = 10;
                value.Right = 10;
                value.Bottom = 10;

                base.Padding = value;
            }
        }

        protected override Padding DefaultPadding
        {
            get { return new Padding(10, MostraTitolo ? AltezzaTitolo : 10, 10, 10); }
        }

        private Dictionary<MetroWindowSysButton.MetroWindowSysButtonType, MetroWindowSysButton> MetroWindowButtons = new Dictionary<MetroWindowSysButton.MetroWindowSysButtonType,MetroWindowSysButton>();

        private int LarghezzaPulsanti = 30;
        private int AltezzaPulsanti = 24;

        private bool _closeBox = true;
        [Category(EtichetteDesigner.Stile)]
        public bool CloseBox
        {
            get { return _closeBox; }
            set { _closeBox = value; UpdateMetroWindowButtons(); }
        }

        [Category(EtichetteDesigner.Stile)]
        new public bool MinimizeBox
        {
            get { return base.MinimizeBox; }
            set { base.MinimizeBox = value; UpdateMetroWindowButtons(); }
        }

        [Category(EtichetteDesigner.Stile)]
        new public bool MaximizeBox
        {
            get { return base.MaximizeBox; }
            set { base.MaximizeBox = value; UpdateMetroWindowButtons(); }
        }

        [Category(EtichetteDesigner.Stile)]
        public Color MetroToolTipBackgroundColor
        {
            get { return VisualManager.MetroToolTipBackgroundColor; }
            set { VisualManager.MetroToolTipBackgroundColor = value; }
        }

        [Category(EtichetteDesigner.Stile)]
        public Color MetroToolTipBorderColor
        {
            get { return VisualManager.MetroToolTipBorderColor; }
            set { VisualManager.MetroToolTipBorderColor = value; }
        }

        [Category(EtichetteDesigner.Stile)]
        public Color MetroToolTipTextColor
        {
            get { return VisualManager.MetroToolTipTextColor; }
            set { VisualManager.MetroToolTipTextColor = value; }
        }

        [Category(EtichetteDesigner.Stile)]
        public Font MetroToolTipFont
        {
            get { return VisualManager.MetroToolTipFont; }
            set { VisualManager.MetroToolTipFont = value; }
        }

        public new Form MdiParent
        {
            get { return base.MdiParent; }
            set
            {
                if (value != null)
                    RemoveShadow();

                base.MdiParent = value;
            }
        }

        protected bool IsActive = false;

        private Controlli.MetroToolTip _windowToolTip = new Controlli.MetroToolTip();

        #endregion

        public MetroWindow()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            UpdateMetroWindowButtons();

            ControlAdded += MetroWindow_ControlAdded;
        }

        void MetroWindow_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control is IMetroControl)
            {
                ((IMetroControl)e.Control).StileMetro = StileMetro;
                ((IMetroControl)e.Control).CombinazioneColori = CombinazioneColori;
                ((IMetroControl)e.Control).VisualManager = VisualManager;
            }
            else if (e.Control is IMetroComponent)
            {
                ((IMetroComponent)e.Control).StileMetro = StileMetro;
                ((IMetroComponent)e.Control).CombinazioneColori = CombinazioneColori;
                ((IMetroComponent)e.Control).VisualManager = VisualManager;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(new SolidBrush(MetroBackgroundColor), ClientRectangle);

            if (MostraTitolo)
            {
                Rectangle titleRect = new Rectangle(0, 0, ClientRectangle.Width, AltezzaTitolo);
                Rectangle titleTextRect = new Rectangle(0, 0, ClientRectangle.Width, AltezzaTitolo);

                g.FillRectangle(new SolidBrush(MetroTitleBackgroundColor), titleRect);

                if (ShowIcon && Icon != null)
                {
                    Rectangle iconRect = new Rectangle(3, 3, Icon.Width, Icon.Height);

                    g.DrawIcon(Icon, iconRect);

                    titleTextRect.Width -= Icon.Width + 6;
                    titleTextRect.X = Icon.Width + 6;
                }

                TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix;

                switch (PosizioneTitolo)
                {
                    case MetroWindowTitlePosition.Centro: flags |= TextFormatFlags.HorizontalCenter; break;
                    case MetroWindowTitlePosition.Destra: flags |= TextFormatFlags.Right; break;
                    case MetroWindowTitlePosition.Sinistra: flags |= TextFormatFlags.Left; break;
                }

                TextRenderer.DrawText(g, Text, MetroTitleFont, titleTextRect, MetroTitleTextColor, flags);
            }

            if (StileBordo == MetroWindowBorderStyle.Ridimensionabile && WindowState == FormWindowState.Normal)
            {
                using (SolidBrush b = new SolidBrush(MetroResizeGripColor))
                {
                    Size resizeHandleSize = new Size(1, 1);
                    g.FillRectangles(b, new Rectangle[] {
                        new Rectangle(new Point(ClientRectangle.Width-5, ClientRectangle.Height-5), resizeHandleSize),
                        new Rectangle(new Point(ClientRectangle.Width-7, ClientRectangle.Height-5), resizeHandleSize),
                        new Rectangle(new Point(ClientRectangle.Width-9, ClientRectangle.Height-5), resizeHandleSize),
                        new Rectangle(new Point(ClientRectangle.Width-11, ClientRectangle.Height-5), resizeHandleSize),
                        new Rectangle(new Point(ClientRectangle.Width-5, ClientRectangle.Height-7), resizeHandleSize),
                        new Rectangle(new Point(ClientRectangle.Width-7, ClientRectangle.Height-7), resizeHandleSize),
                        new Rectangle(new Point(ClientRectangle.Width-9, ClientRectangle.Height-7), resizeHandleSize),
                        new Rectangle(new Point(ClientRectangle.Width-5, ClientRectangle.Height-9), resizeHandleSize),
                        new Rectangle(new Point(ClientRectangle.Width-7, ClientRectangle.Height-9), resizeHandleSize),
                        new Rectangle(new Point(ClientRectangle.Width-5, ClientRectangle.Height-11), resizeHandleSize),
                    });
                }
            }

            if (WindowState == FormWindowState.Normal && StileBordo != MetroWindowBorderStyle.Nessuno)
                    ControlPaint.DrawBorder(g, ClientRectangle, MetroBorderColor, ButtonBorderStyle.Solid);
        }
        
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            UpdateWindowButtonPosition();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode) return;

            switch (StartPosition)
            {
                case FormStartPosition.CenterParent:
                    CenterToParent();
                    break;
                case FormStartPosition.CenterScreen:
                    if (IsMdiChild)
                        CenterToParent();
                    else
                        CenterToScreen();
                    break;
            }

            RemoveCloseButton();

            CreateShadow();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                RemoveShadow();

            base.Dispose(disposing);
        }

        protected override void OnClosed(EventArgs e)
        {
            RemoveShadow();
            base.OnClosed(e);
        }

        protected override void OnActivated(EventArgs e)
        {
            IsActive = true;
            base.OnActivated(e);
            Refresh();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            IsActive = false;
            base.OnDeactivate(e);
            Refresh();
        }

        protected override void WndProc(ref Message m)
        {
            if (DesignMode)
            {
                base.WndProc(ref m);
                return;
            }

            switch (m.Msg)
            {
                case (int)WinApi.Messages.WM_SYSCOMMAND:
                    int sc = m.WParam.ToInt32() & 0xFFF0;
                    switch (sc)
                    {
                        case (int)WinApi.Messages.SC_MAXIMIZE:
                            RemoveShadow();
                            break;
                        case (int)WinApi.Messages.SC_RESTORE:
                            CreateShadow();
                            break;
                    }
                    break;
                case (int)WinApi.Messages.WM_NCLBUTTONDBLCLK:
                case (int)WinApi.Messages.WM_LBUTTONDBLCLK:
                    if (!MaximizeBox) return;
                    break;

                case (int)WinApi.Messages.WM_NCHITTEST:
                    WinApi.HitTest ht = HitTestNCA(m.HWnd, m.WParam, m.LParam);
                    if (ht != WinApi.HitTest.HTCLIENT)
                    {
                        m.Result = (IntPtr)ht;
                        return;
                    }
                    break;
            }

            base.WndProc(ref m);

            switch (m.Msg)
            {
                case (int)WinApi.Messages.WM_GETMINMAXINFO:
                    OnGetMinMaxInfo(m.HWnd, m.LParam);
                    break;
            }
        }

        [SecuritySafeCritical]
        private unsafe void OnGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            WinApi.MINMAXINFO* pmmi = (WinApi.MINMAXINFO*)lParam;

            Screen s = Screen.FromHandle(hwnd);
            pmmi->ptMaxSize.x = s.WorkingArea.Width;
            pmmi->ptMaxSize.y = s.WorkingArea.Height;
            pmmi->ptMaxPosition.x = Math.Abs(s.WorkingArea.Left - s.Bounds.Left);
            pmmi->ptMaxPosition.y = Math.Abs(s.WorkingArea.Top - s.Bounds.Top);
        }

        private WinApi.HitTest HitTestNCA(IntPtr hwnd, IntPtr wparam, IntPtr lparam)
        {
            Point vPoint = new Point((Int16)lparam, (Int16)((int)lparam >> 16));
            int vPadding = 10;

            if (StileBordo == MetroWindowBorderStyle.Ridimensionabile)
            {
                if (RectangleToScreen(new Rectangle(ClientRectangle.Width - vPadding, ClientRectangle.Height - vPadding, vPadding, vPadding)).Contains(vPoint))
                    return WinApi.HitTest.HTBOTTOMRIGHT;
            }

            if (RectangleToScreen(new Rectangle(0, 0, ClientRectangle.Width, AltezzaTitolo)).Contains(vPoint))
                return WinApi.HitTest.HTCAPTION;

            return WinApi.HitTest.HTCLIENT;
        }

        #region SysButtons

        protected virtual void UpdateMetroWindowButtons()
        {
            if (CloseBox)
                AddWindowButton(MetroWindowSysButton.MetroWindowSysButtonType.Close);
            else
                RemoveWindowButton(MetroWindowSysButton.MetroWindowSysButtonType.Close);

            if (MinimizeBox)
                AddWindowButton(MetroWindowSysButton.MetroWindowSysButtonType.Minimize);
            else
                RemoveWindowButton(MetroWindowSysButton.MetroWindowSysButtonType.Minimize);

            if (MaximizeBox)
                AddWindowButton(MetroWindowSysButton.MetroWindowSysButtonType.Maximize);
            else
                RemoveWindowButton(MetroWindowSysButton.MetroWindowSysButtonType.Maximize);

            UpdateWindowButtonPosition();
        }

        protected virtual void AddWindowButton(MetroWindowSysButton.MetroWindowSysButtonType tipo)
        {
            try
            {
                if (MetroWindowButtons == null)
                    MetroWindowButtons = new Dictionary<MetroWindowSysButton.MetroWindowSysButtonType, MetroWindowSysButton>();

                if (MetroWindowButtons.ContainsKey(tipo))
                    return;

                MetroWindowSysButton button = new MetroWindowSysButton();
                button.Parent = this;
                button.MetroWindowButtonType = tipo;
                button.Size = new Size(LarghezzaPulsanti, AltezzaPulsanti);
                button.StileMetro = StileMetro;
                button.CombinazioneColori = CombinazioneColori;
                button.VisualManager = VisualManager;
                button.Click += SystemButtonClick;

                MetroWindowButtons.Add(tipo, button);
                Controls.Add(button);
            }
            catch (Exception ex)
            {
                MessageBox.Show("1" + ex.Message);
            }
        }

        void SystemButtonClick(object sender, EventArgs e)
        {
            MetroWindowSysButton button = sender as MetroWindowSysButton;

            switch (button.MetroWindowButtonType)
            {
                case MetroWindowSysButton.MetroWindowSysButtonType.Close: 
                    Close(); 
                    break;
                case MetroWindowSysButton.MetroWindowSysButtonType.Maximize:
                    if (WindowState == FormWindowState.Normal)
                    {
                        WindowState = FormWindowState.Maximized;
                        //RemoveShadow();
                    }
                    else if (WindowState == FormWindowState.Maximized)
                    {
                        WindowState = FormWindowState.Normal;
                        //CreateShadow();
                    }

                    MetroWindowButtons[MetroWindowSysButton.MetroWindowSysButtonType.Maximize].MetroWindowButtonType = MetroWindowSysButton.MetroWindowSysButtonType.Maximize;
                    break;
                case MetroWindowSysButton.MetroWindowSysButtonType.Minimize:
                    WindowState = FormWindowState.Minimized; 
                    break;
            }
        }

        protected virtual void RemoveWindowButton(MetroWindowSysButton.MetroWindowSysButtonType tipo)
        {
            try
            {
                if (MetroWindowButtons == null)
                    return;

                MetroWindowSysButton button = null;

                if (MetroWindowButtons.TryGetValue(tipo, out button))
                {
                    Controls.Remove(button);
                    MetroWindowButtons.Remove(tipo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected virtual void UpdateWindowButtonPosition()
        {
            try
            {
                if (MetroWindowButtons == null)
                    return;

                int yOffset = (WindowState == FormWindowState.Maximized || (StileBordo == MetroWindowBorderStyle.Nessuno)) ? 0 : 1;
                int xOffset = (WindowState == FormWindowState.Maximized || (StileBordo == MetroWindowBorderStyle.Nessuno)) ? 0 : 1;

                Dictionary<int, MetroWindowSysButton.MetroWindowSysButtonType> priorityOrder = new Dictionary<int, MetroWindowSysButton.MetroWindowSysButtonType>(4) 
                { 
                    { 0, MetroWindowSysButton.MetroWindowSysButtonType.Close }, 
                    { 1, MetroWindowSysButton.MetroWindowSysButtonType.Maximize },
                    { 2, MetroWindowSysButton.MetroWindowSysButtonType.Minimize }
                };

                int index = 1;

                if (MetroWindowButtons.Count == 1)
                {
                    foreach (KeyValuePair<MetroWindowSysButton.MetroWindowSysButtonType, MetroWindowSysButton> button in MetroWindowButtons)
                    {
                        button.Value.Location = new Point(ClientRectangle.Width - (LarghezzaPulsanti * index) - xOffset, yOffset);
                        button.Value.Size = new Size(LarghezzaPulsanti, AltezzaPulsanti - yOffset);
                    }
                }
                else
                {
                    foreach (KeyValuePair<int, MetroWindowSysButton.MetroWindowSysButtonType> button in priorityOrder)
                    {
                        bool buttonExists = MetroWindowButtons.ContainsKey(button.Value);

                        if (buttonExists)
                        {
                            MetroWindowButtons[button.Value].Location = new Point(ClientRectangle.Width - (LarghezzaPulsanti * index) - xOffset, yOffset);
                            MetroWindowButtons[button.Value].Size = new Size(LarghezzaPulsanti, AltezzaPulsanti - yOffset);
                            index++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        #endregion

        #region Ombre

        private const int CS_DROPSHADOW = 0x20000;
        const int WS_MINIMIZEBOX = 0x20000;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_MINIMIZEBOX;

                return cp;
            }
        }

        protected MetroShadowForm shadowForm;

        private void CreateShadow()
        {
            shadowForm = new MetroShadowForm(this, 6, MetroShadowForm.WS_EX_LAYERED | MetroShadowForm.WS_EX_TRANSPARENT | MetroShadowForm.WS_EX_NOACTIVATE);
            Activate();
        }

        private void RemoveShadow()
        {
            if (shadowForm == null || shadowForm.IsDisposed) return;

            shadowForm.Visible = false;
            Owner = shadowForm.Owner;
            shadowForm.Owner = null;
            shadowForm.Dispose();
            shadowForm = null;
        }

        protected class MetroShadowForm : Form
        {
            class MetroShadowStruct
            {
                public Color combinazioneColoriCorrente;
                public Bitmap top;
                public Bitmap left;
                public Bitmap bottom;
                public Bitmap right;
                public Bitmap topRight;
                public Bitmap topLeft;
                public Bitmap bottomRight;
                public Bitmap bottomLeft;
            }

            protected MetroWindow TargetForm { get; private set; }

            private MetroShadowStruct _shadow;

            private readonly int shadowSize;
            private readonly int wsExStyle;

            public MetroShadowForm(MetroWindow targetForm, int shadowSize, int wsExStyle)
            {
                TargetForm = targetForm;
                this.shadowSize = shadowSize;
                this.wsExStyle = wsExStyle;

                TargetForm.Activated += OnTargetFormActivated;
                targetForm.Deactivate += OnTargetForm_Deactivate;
                TargetForm.ResizeBegin += OnTargetFormResizeBegin;
                TargetForm.ResizeEnd += OnTargetFormResizeEnd;
                TargetForm.VisibleChanged += OnTargetFormVisibleChanged;
                TargetForm.SizeChanged += OnTargetFormSizeChanged;

                TargetForm.Move += OnTargetFormMove;

                if (TargetForm.Owner != null)
                    Owner = TargetForm.Owner;

                TargetForm.Owner = this;

                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                ShowIcon = false;
                FormBorderStyle = FormBorderStyle.None;

                Bounds = GetShadowBounds();

                _shadow = new MetroShadowStruct(); 
               CreateShadows(TargetForm.MetroBorderColor, out _shadow.top, out _shadow.bottom, out _shadow.left, out _shadow.right, out _shadow.topLeft, out _shadow.topRight, out _shadow.bottomLeft, out _shadow.bottomRight);
                _shadow.combinazioneColoriCorrente = targetForm.MetroBorderColor;
            }
            
            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= wsExStyle;
                    return cp;
                }
            }

            private Rectangle GetShadowBounds()
            {
                Rectangle r = TargetForm.Bounds;
                r.Inflate(shadowSize, shadowSize);
                return r;
            }


            #region Event Handlers

            private bool isBringingToFront;

            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                PaintShadow();
            }

            protected override void OnDeactivate(EventArgs e)
            {
                isBringingToFront = true;
                base.OnDeactivate(e);
            }

            void OnTargetForm_Deactivate(object sender, EventArgs e)
            {
                if (Visible) PaintShadow();
            }

            private void OnTargetFormActivated(object sender, EventArgs e)
            {
                if (Visible) PaintShadow();
                if (isBringingToFront)
                {
                    Visible = true;
                    isBringingToFront = false;
                    return;
                }
                //BringToFront();
            }

            private void OnTargetFormVisibleChanged(object sender, EventArgs e)
            {
                Visible = TargetForm.Visible && TargetForm.WindowState != FormWindowState.Minimized;
                Update();
            }

            private long lastResizedOn;

            private bool IsResizing { get { return lastResizedOn > 0; } }

            private void OnTargetFormResizeBegin(object sender, EventArgs e)
            {
                lastResizedOn = DateTime.Now.Ticks;
            }

            private void OnTargetFormMove(object sender, EventArgs e)
            {
                if (!TargetForm.Visible || TargetForm.WindowState != FormWindowState.Normal)
                {
                    Visible = false;
                }
                else
                {
                    Bounds = GetShadowBounds();
                }
            }
            
            private void OnTargetFormSizeChanged(object sender, EventArgs e)
            {
                Bounds = GetShadowBounds();

                //if (IsResizing)
                //{
                //    return;
                //}

                PaintShadowIfVisible();
            }

            private void OnTargetFormResizeEnd(object sender, EventArgs e)
            {
                lastResizedOn = 0;
                PaintShadowIfVisible();
            }

            private void PaintShadowIfVisible()
            {
                if (TargetForm.Visible && TargetForm.WindowState == FormWindowState.Normal)
                    PaintShadow();
            }

            private void PaintShadow()
            {
                if (_shadow.combinazioneColoriCorrente != TargetForm.MetroBorderColor)
                {
                    CreateShadows(TargetForm.MetroBorderColor, out _shadow.top, out _shadow.bottom, out _shadow.left, out _shadow.right, out _shadow.topLeft, out _shadow.topRight, out _shadow.bottomLeft, out _shadow.bottomRight);
                    _shadow.combinazioneColoriCorrente = TargetForm.MetroBorderColor;
                }

                Bitmap shadowImage = CreateShadowImage(_shadow, Bounds);

                IntPtr screenDc = WinApi.GetDC(IntPtr.Zero);
                IntPtr memDc = WinApi.CreateCompatibleDC(screenDc);
                IntPtr hBitmap = IntPtr.Zero;
                IntPtr oldBitmap = IntPtr.Zero;

                try
                {
                    hBitmap = shadowImage.GetHbitmap(Color.FromArgb(0));
                    oldBitmap = WinApi.SelectObject(memDc, hBitmap);

                    WinApi.SIZE size = new WinApi.SIZE(shadowImage.Width, shadowImage.Height);
                    WinApi.POINT pointSource = new WinApi.POINT(0, 0);
                    WinApi.POINT topPos = new WinApi.POINT(Bounds.Left, Bounds.Top);
                    WinApi.BLENDFUNCTION blend = new WinApi.BLENDFUNCTION
                    {
                        BlendOp = WinApi.AC_SRC_OVER,
                        BlendFlags = 0,
                        SourceConstantAlpha = 255,
                        AlphaFormat = WinApi.AC_SRC_ALPHA
                    };

                    WinApi.UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, WinApi.ULW_ALPHA);
                }
                finally
                {
                    WinApi.ReleaseDC(IntPtr.Zero, screenDc);
                    if (hBitmap != IntPtr.Zero)
                    {
                        WinApi.SelectObject(memDc, oldBitmap);
                        WinApi.DeleteObject(hBitmap);
                    }
                    WinApi.DeleteDC(memDc);
                }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Visible = true;
                PaintShadow();
            }

            public void CreateShadows(Color shadowColor, out Bitmap top, out Bitmap bottom, out Bitmap left, out Bitmap right, out Bitmap topLeft, out Bitmap topRight, out Bitmap bottomLeft, out Bitmap bottomRight)
            {
                Image sourceBmp = DrawOutsetShadow(10, 10, 10, 10, shadowColor, new Rectangle(new Point(0, 0), new Size(20, 20)));

                top = new Bitmap(10, 10, PixelFormat.Format32bppArgb);
                bottom = new Bitmap(10, 10, PixelFormat.Format32bppArgb);
                right = new Bitmap(10, 10, PixelFormat.Format32bppArgb);
                left = new Bitmap(10, 10, PixelFormat.Format32bppArgb);
                topLeft = new Bitmap(10, 10, PixelFormat.Format32bppArgb);
                topRight = new Bitmap(10, 10, PixelFormat.Format32bppArgb);
                bottomLeft = new Bitmap(10, 10, PixelFormat.Format32bppArgb);
                bottomRight = new Bitmap(10, 10, PixelFormat.Format32bppArgb);

                Graphics g;

                #region Top Image
                g = Graphics.FromImage(top);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(sourceBmp, 0, 0, new Rectangle(new Point(10, 0), new Size(10, 10)), GraphicsUnit.Pixel);
                g.Flush();
                g.Dispose();
                #endregion
                #region Bottom Image
                g = Graphics.FromImage(bottom);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(sourceBmp, 0, 0, new Rectangle(new Point(10, sourceBmp.Height - 10), new Size(10, 10)), GraphicsUnit.Pixel);
                g.Flush();
                g.Dispose();
                #endregion
                #region Left Image
                g = Graphics.FromImage(left);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(sourceBmp, 0, 0, new Rectangle(new Point(0, 10), new Size(10, 10)), GraphicsUnit.Pixel);
                g.Flush();
                g.Dispose();
                #endregion
                #region Right Image
                g = Graphics.FromImage(right);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(sourceBmp, 0, 0, new Rectangle(new Point(sourceBmp.Width - 10, 10), new Size(10, 10)), GraphicsUnit.Pixel);
                g.Flush();
                g.Dispose();
                #endregion

                #region TopLeft Image
                g = Graphics.FromImage(topLeft);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(sourceBmp, 0, 0, new Rectangle(new Point(0, 0), new Size(10, 10)), GraphicsUnit.Pixel);
                g.Flush();
                g.Dispose();
                #endregion
                #region TopRight Image
                g = Graphics.FromImage(topRight);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(sourceBmp, 0, 0, new Rectangle(new Point(sourceBmp.Width - 10, 0), new Size(10, 10)), GraphicsUnit.Pixel);
                g.Flush();
                g.Dispose();
                #endregion
                #region BottomLeft Image
                g = Graphics.FromImage(bottomLeft);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(sourceBmp, 0, 0, new Rectangle(new Point(0, sourceBmp.Height - 10), new Size(10, 10)), GraphicsUnit.Pixel);
                g.Flush();
                g.Dispose();
                #endregion
                #region BottomRight Image
                g = Graphics.FromImage(bottomRight);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(sourceBmp, 0, 0, new Rectangle(new Point(sourceBmp.Width - 10, sourceBmp.Height - 10), new Size(10, 10)), GraphicsUnit.Pixel);
                g.Flush();
                g.Dispose();
                #endregion

                sourceBmp.Dispose();
            }

            private Image DrawOutsetShadow(int hShadow, int vShadow, int blur, int spread, Color color, Rectangle shadowCanvasArea)
            {
                Rectangle rOuter = shadowCanvasArea;
                Rectangle rInner = shadowCanvasArea;
                rInner.Offset(hShadow, vShadow);
                rInner.Inflate(-blur, -blur);
                rOuter.Inflate(spread, spread);
                rOuter.Offset(hShadow, vShadow);

                Rectangle originalOuter = rOuter;

                Bitmap img = new Bitmap(originalOuter.Width, originalOuter.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(img);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                var currentBlur = 0;
                do
                {
                    var transparency = (rOuter.Height - rInner.Height) / (double)(blur * 2 + spread * 2);
                    var shadowColor = Color.FromArgb(((int)(60 * (transparency))), color);
                    var rOutput = rInner;
                    rOutput.Offset(-originalOuter.Left, -originalOuter.Top);

                    DrawRoundedRectangle(g, rOutput, currentBlur, Pens.Transparent, shadowColor);
                    rInner.Inflate(1, 1);
                    currentBlur = (int)((double)blur * (1 - (transparency * transparency)));

                } while (rOuter.Contains(rInner));

                g.Flush();
                g.Dispose();

                return img;
            }

            private void DrawRoundedRectangle(Graphics g, Rectangle bounds, int cornerRadius, Pen drawPen, Color fillColor)
            {
                int strokeOffset = Convert.ToInt32(Math.Ceiling(drawPen.Width));
                bounds = Rectangle.Inflate(bounds, -strokeOffset, -strokeOffset);

                var gfxPath = new GraphicsPath();

                if (cornerRadius > 0)
                {
                    gfxPath.AddArc(bounds.X, bounds.Y, cornerRadius, cornerRadius, 180, 90);
                    gfxPath.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y, cornerRadius, cornerRadius, 270, 90);
                    gfxPath.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y + bounds.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                    gfxPath.AddArc(bounds.X, bounds.Y + bounds.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                }
                else
                {
                    gfxPath.AddRectangle(bounds);
                }

                gfxPath.CloseAllFigures();

                if (cornerRadius > 5)
                {
                    using (SolidBrush b = new SolidBrush(fillColor))
                    {
                        g.FillPath(b, gfxPath);
                    }
                }
                if (drawPen != Pens.Transparent)
                {
                    using (Pen p = new Pen(drawPen.Color))
                    {
                        p.EndCap = p.StartCap = LineCap.Round;
                        g.DrawPath(p, gfxPath);
                    }
                }
            }

            private Bitmap CreateShadowImage(MetroShadowStruct shadow, Rectangle borderRect)
            {
                Bitmap resultBitmap = new Bitmap(borderRect.Width, borderRect.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(resultBitmap);

                #region top
                for (int padding = 6; padding < borderRect.Width - 6; padding += 10)
                {
                    Size size = new Size(10, 6);

                    while (padding + size.Width > borderRect.Width - 6)
                        size.Width = size.Width - 1;

                    g.DrawImage(shadow.top, new Rectangle(new Point(padding, 0), size), 0, 0, shadow.top.Width, shadow.top.Height, GraphicsUnit.Pixel);//, attributes);
                }
                #endregion
                #region bottom
                for (int padding = 6; padding < borderRect.Width - 6; padding += 10)
                {
                    Size size = new Size(10, 6);

                    while (padding + size.Width > borderRect.Width - 6)
                        size.Width = size.Width - 1;

                    g.DrawImage(shadow.bottom, new Rectangle(new Point(padding, borderRect.Height - 6), size), 0, 0, shadow.bottom.Width, shadow.bottom.Height, GraphicsUnit.Pixel);//, attributes);
                }
                #endregion
                #region left
                for (int padding = 6; padding < borderRect.Height - 6; padding += 10)
                {
                    Size size = new Size(6, 10);

                    while (padding + size.Height > borderRect.Height - 6)
                        size.Height = size.Height - 1;

                    g.DrawImage(shadow.left, new Rectangle(new Point(0, padding), size), 0, 0, shadow.left.Width, shadow.left.Height, GraphicsUnit.Pixel);//, attributes);
                }
                #endregion
                #region right
                for (int padding = 6; padding < borderRect.Height - 6; padding += 10)
                {
                    Size size = new Size(6, 10);

                    while (padding + size.Height > borderRect.Height - 6)
                        size.Height = size.Height - 1;

                    g.DrawImage(shadow.right, new Rectangle(new Point(borderRect.Width - 6, padding), size), 0, 0, shadow.right.Width, shadow.right.Height, GraphicsUnit.Pixel);//, attributes);
                }
                #endregion

                #region topleft
                g.DrawImage(shadow.topLeft, new Rectangle(new Point(0, 0), new Size(6, 6)), 0, 0, shadow.topLeft.Width, shadow.topLeft.Height, GraphicsUnit.Pixel);
                #endregion
                #region topright
                g.DrawImage(shadow.topRight, new Rectangle(new Point(borderRect.Width - 6, 0), new Size(6, 6)), 0, 0, shadow.topRight.Width, shadow.topRight.Height, GraphicsUnit.Pixel);
                #endregion
                #region bottomleft
                g.DrawImage(shadow.bottomLeft, new Rectangle(new Point(0, borderRect.Height - 6), new Size(6, 6)), 0, 0, shadow.bottomLeft.Width, shadow.bottomLeft.Height, GraphicsUnit.Pixel);
                #endregion
                #region bottomright
                g.DrawImage(shadow.bottomRight, new Rectangle(new Point(borderRect.Width - 6, borderRect.Height - 6), new Size(6, 6)), 0, 0, shadow.bottomRight.Width, shadow.bottomRight.Height, GraphicsUnit.Pixel);
                #endregion

                g.Flush();
                g.Dispose();

                return resultBitmap;
            }

            #endregion

            #region Constants

            public const int WS_EX_TRANSPARENT = 0x20;
            public const int WS_EX_LAYERED = 0x80000;
            public const int WS_EX_NOACTIVATE = 0x8000000;

            private const int TICKS_PER_MS = 10000;
            private const long RESIZE_REDRAW_INTERVAL = 1000 * TICKS_PER_MS;

            #endregion
        }

        #endregion

        #region Altro
        
        [SecuritySafeCritical]
        public void RemoveCloseButton()
        {
            IntPtr hMenu = WinApi.GetSystemMenu(Handle, false);
            if (hMenu == IntPtr.Zero) return;

            int n = WinApi.GetMenuItemCount(hMenu);
            if (n <= 0) return;

            WinApi.RemoveMenu(hMenu, (uint)(n - 1), WinApi.MfByposition | WinApi.MfRemove);
            WinApi.RemoveMenu(hMenu, (uint)(n - 2), WinApi.MfByposition | WinApi.MfRemove);
            WinApi.DrawMenuBar(Handle);
        }
        
        #endregion
    }

    public class MetroWindowSysButton : Button, IMetroControl
    {
        [Category(EtichetteDesigner.Stile)]
        public StileMetro StileMetro
        {
            get { return VisualManager.StileMetro; }
            set { VisualManager.StileMetro = value; Refresh(); }
        }

        [Category(EtichetteDesigner.Stile)]
        public CombinazionaColori CombinazioneColori
        {
            get { return VisualManager.CombinazioneColori; }
            set { VisualManager.CombinazioneColori = value; Refresh(); }
        }

        private MetroVisualManager _visualManager = new MetroVisualManager();
        [Browsable(false)]
        public MetroVisualManager VisualManager
        {
            get { return _visualManager; }
            set { _visualManager = value; Refresh(); }
        }

        [Browsable(false)]
        public enum MetroWindowSysButtonType { Close, Maximize, Minimize }

        private bool _isHover = false;
        private bool _isPressed = false;

        private Controlli.MetroToolTip _tooltip;

        private MetroWindowSysButtonType _metroWindowSysButtonType;
        [Category(EtichetteDesigner.Stile)]
        public MetroWindowSysButtonType MetroWindowButtonType
        {
            get { return _metroWindowSysButtonType; }
            set
            {
                _metroWindowSysButtonType = value;

                switch (_metroWindowSysButtonType)
                {
                    case MetroWindowSysButtonType.Close: 
                        Text = "r";
                        _tooltip.SetToolTip(this, "Chiudi");
                        break;
                    case MetroWindowSysButtonType.Maximize:
                        MetroWindow window = Parent as MetroWindow;

                        if (window != null)
                        {
                            if (window.WindowState == FormWindowState.Maximized)
                            {
                                Text = "2";
                                _tooltip.SetToolTip(this, "Ripristina dimensioni originali");
                            }
                            else
                            {
                                Text = "1";
                                _tooltip.SetToolTip(this, "Ingrandisci");
                            }
                        }

                        break;
                    case MetroWindowSysButtonType.Minimize: 
                        Text = "0"; 
                        _tooltip.SetToolTip(this, "Riduci a icona");
                        break;
                }
            }
        }
        
        public MetroWindowSysButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            _tooltip = new Controlli.MetroToolTip();
            _tooltip.StileMetro = StileMetro;
            _tooltip.CombinazioneColori = CombinazioneColori;
            _tooltip.VisualManager = VisualManager;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            MetroWindow wndParent = Parent as MetroWindow;

            if (wndParent == null)
                throw new InvalidCastException("Parent must be a MetroWindow");

            Graphics g = pevent.Graphics;

            TextFormatFlags flag = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;

            if (_isPressed)
            {
                g.FillRectangle(new SolidBrush(wndParent.MetroSysButtonPressed), ClientRectangle);
                TextRenderer.DrawText(g, Text, VisualManager.MetroWindowSysButtonFont, ClientRectangle, wndParent.MetroSysButtonTextPressed, flag);
            }
            else if (_isHover)
            {
                g.FillRectangle(new SolidBrush(wndParent.MetroSysButtonHover), ClientRectangle);
                TextRenderer.DrawText(g, Text, VisualManager.MetroWindowSysButtonFont, ClientRectangle, wndParent.MetroSysButtonTextHover, flag);
            }
            else
            {
                g.FillRectangle(new SolidBrush(wndParent.MetroSysButtonNormal), ClientRectangle);
                TextRenderer.DrawText(g, Text, VisualManager.MetroWindowSysButtonFont, ClientRectangle, wndParent.MetroSysButtonTextNormal, flag);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _isHover = true;
            Refresh();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isPressed = true;
                Refresh();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isPressed = false;
            Refresh();

            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isHover = false;
            Refresh();

            base.OnMouseLeave(e);
        }
    }
}
