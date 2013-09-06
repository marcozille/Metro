using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

using Metro.Interfaccie;

namespace Metro.Componenti
{
    public sealed class MetroVisualManager : Component, ICloneable, ISupportInitialize
    {
        #region Campi

        private readonly IContainer parentContainer;

        private ContainerControl owner;
        public ContainerControl Owner
        {
            get { return owner; }
            set
            {
                if (owner != null)
                {
                    owner.ControlAdded -= ControlAdded;
                }

                owner = value;

                if (value != null)
                {
                    owner.ControlAdded += ControlAdded;

                    if (!isInitializing)
                    {
                        UpdateControl(value);
                    }
                }
            }
        }

        private void ControlAdded(object sender, ControlEventArgs e)
        {
            if (!isInitializing)
            {
                UpdateControl(e.Control);
            }
        }

        public void Update()
        {
            if (owner != null)
            {
                UpdateControl(owner);
            }

            if (parentContainer == null || parentContainer.Components == null)
            {
                return;
            }

            foreach (Object obj in parentContainer.Components)
            {
                if (obj is IMetroComponent)
                {
                    ApplyTheme((IMetroComponent)obj);
                }
            }
        }

        private void UpdateControl(Control ctrl)
        {
            if (ctrl == null)
            {
                return;
            }

            IMetroControl metroControl = ctrl as IMetroControl;
            if (metroControl != null)
            {
                ApplyTheme(metroControl);
            }

            IMetroComponent metroComponent = ctrl as IMetroComponent;
            if (metroComponent != null)
            {
                ApplyTheme(metroComponent);
            }

            TabControl tabControl = ctrl as TabControl;
            if (tabControl != null)
            {
                foreach (TabPage tp in ((TabControl)ctrl).TabPages)
                {
                    UpdateControl(tp);
                }
            }

            if (ctrl.Controls != null)
            {
                foreach (Control child in ctrl.Controls)
                {
                    UpdateControl(child);
                }
            }

            if (ctrl.ContextMenuStrip != null)
            {
                UpdateControl(ctrl.ContextMenuStrip);
            }

            ctrl.Refresh();
        }

        private void ApplyTheme(IMetroControl control)
        {
            control.VisualManager = this;
        }

        private void ApplyTheme(IMetroComponent component)
        {
            component.VisualManager = this;
        }

        public object Clone()
        {
            MetroVisualManager newStyleManager = new MetroVisualManager();
            newStyleManager.CombinazioneColori = CombinazioneColori;
            newStyleManager.StileMetro = StileMetro;
            return newStyleManager;
        }

        private bool isInitializing;

        void ISupportInitialize.BeginInit()
        {
            isInitializing = true;
        }

        void ISupportInitialize.EndInit()
        {
            isInitializing = false;
            Update();
        }

        public object Clone(ContainerControl owner)
        {
            MetroVisualManager clonedManager = Clone() as MetroVisualManager;

            if (owner is IMetroWindow)
            {
                clonedManager.Owner = owner;
            }

            return clonedManager;
        }

        #endregion

        private readonly PrivateFontCollection fontCollection = new PrivateFontCollection();

        public MetroVisualManager()
        {
            CombinazioneColori = CombinazionaColori.Blu;
            StileMetro = Componenti.StileMetro.Scuro;
            
            LoadMemoryFont();
            LoadFonts();
        }

        public MetroVisualManager(IContainer parentContainer)
            : this()
        {
            if (parentContainer != null)
            {
                this.parentContainer = parentContainer;
                this.parentContainer.Add(this);
            }

            CombinazioneColori = CombinazionaColori.Blu;
            StileMetro = Componenti.StileMetro.Scuro;

            LoadMemoryFont();
            LoadFonts();
        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        private void LoadMemoryFont()
        {
            byte[] fontArray = Properties.Resources.segoeui;
            int fontArrayLen = Properties.Resources.segoeui.Length;

            IntPtr fontData = Marshal.AllocCoTaskMem(fontArrayLen);
            Marshal.Copy(fontArray, 0, fontData, fontArrayLen);

            uint cFonts = 0;
            AddFontMemResourceEx(fontData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            fontCollection.AddMemoryFont(fontData, fontArrayLen);
            Marshal.FreeCoTaskMem(fontData);
        }

        private void LoadFonts()
        {
            MetroWindowSysButtonFont = new Font("Webdings", 10f);
            MetroToolTipFont = new Font(fontCollection.Families[0], 10f);
            MetroButtonFont = new Font(fontCollection.Families[0], 10f);
            MetroTextBoxFont = new Font(fontCollection.Families[0], 10f);
            MetroWindowTitleFont = new Font(fontCollection.Families[0], 12.5f);
        }

        private bool _bColorsUpdated = false;

        private CombinazionaColori _combinazioneColori;
        public CombinazionaColori CombinazioneColori 
        { 
            get 
            { 
                return _combinazioneColori; 
            } 

            set 
            { 
                _combinazioneColori = value; 
                UpdateColors(); 
            } 
        }

        private StileMetro _stileMentro;
        public StileMetro StileMetro { get { return _stileMentro; } set { _stileMentro = value; UpdateColors(); } }

        #region Colori
        #region MetroWindow
        #region Window
        public Color MetroWindowBackgroundColor { get; set; }
        public Color MetroWindowBorderColorNormal { get; set; }
        public Color MetroWindowBorderColorDisabled { get; set; }
        public Color MetroWindowTitleColor { get; set; }
        public Color MetroWindowTitleBackgroundColor { get; set; }
        public Color MetroWindowResizeGripColor { get; set; }
        #endregion
        #region SysButtons
        public Color MetroWindowSysButtonBackNormal { get; set; }
        public Color MetroWindowSysButtonBackHover { get; set; }
        public Color MetroWindowSysButtonBackPressed { get; set; }
        public Color MetroWindowSysButtonTextNormal { get; set; }
        public Color MetroWindowSysButtonTextHover { get; set; }
        public Color MetroWindowSysButtonTextPressed { get; set; }
        #endregion
        #region ToolTip
        public Color MetroToolTipBorderColor { get; set; }
        public Color MetroToolTipBackgroundColor { get; set; }
        public Color MetroToolTipTextColor { get; set; }
        #endregion
        #region Pulsanti
        public Color MetroButtonBorder { get; set; }
        public Color MetroButtonBorderDisabled { get; set; }
        public Color MetroButtonBackgroundNormal { get; set; }
        public Color MetroButtonBackgroundHover { get; set; }
        public Color MetroButtonBackgroundPressed { get; set; }
        public Color MetroButtonBackgroundDisabled { get; set; }
        public Color MetroButtonTextNormal { get; set; }
        public Color MetroButtonTextHover { get; set; }
        public Color MetroButtonTextPressed { get; set; }
        public Color MetroButtonTextDisabled { get; set; }
        #endregion
        #region TextBox
        public Color MetroTextBoxBackColorNormal { get; set; }
        public Color MetroTextBoxBackColorHover { get; set; }
        public Color MetroTextBoxBackColorSelected { get; set; }
        public Color MetroTextBoxBackColorDisabled { get; set; }

        public Color MetroTextBoxBorderColorNormal { get; set; }
        public Color MetroTextBoxBorderColorSelected { get; set; }
        public Color MetroTextBoxBorderColorDisabled { get; set; }

        public Color MetroTextBoxTextColorNormal { get; set; }
        public Color MetroTextBoxTextColorHover { get; set; }
        public Color MetroTextBoxTextColorSelected { get; set; }
        public Color MetroTextBoxTextColorDisabled { get; set; }
        #endregion
        #endregion
        #endregion

        #region Fonts
        public Font MetroWindowTitleFont { get; set; }
        public Font MetroWindowSysButtonFont { get; set; }
        public Font MetroToolTipFont { get; set; }
        public Font MetroButtonFont { get; set; }
        public Font MetroTextBoxFont { get; set; }
        #endregion

        private void UpdateColors()
        {
            if (StileMetro == Componenti.StileMetro.Chiaro)
            {
                #region MetroWindow
                #region Window
                MetroWindowBackgroundColor = Color.White;
                MetroWindowBorderColorDisabled = Color.FromArgb(160, 160, 160);
                MetroWindowTitleBackgroundColor = MetroWindowBackgroundColor;
                MetroWindowTitleColor = Color.FromArgb(150, 150, 150);
                MetroWindowResizeGripColor = MetroWindowTitleColor;
                #endregion
                #region SysButtons
                MetroWindowSysButtonBackNormal = MetroWindowBackgroundColor;
                MetroWindowSysButtonBackHover = Color.FromArgb(200, 200, 200);
                MetroWindowSysButtonTextNormal = Color.Black;
                #endregion
                #region ToolTip
                MetroToolTipBackgroundColor = Color.FromArgb(230, 230, 230);
                MetroToolTipBorderColor = Color.FromArgb(100, 100, 100);
                MetroToolTipTextColor = Color.FromArgb(120,120,120);
                #endregion
                #region Pulsanti
                MetroButtonBackgroundNormal = MetroWindowBackgroundColor;
                MetroButtonBackgroundHover = Color.FromArgb(240, 240, 240);
                MetroButtonBackgroundDisabled = Color.FromArgb(220, 220, 220);
                MetroButtonTextNormal = Color.FromArgb(100, 100, 100);
                MetroButtonTextHover = Color.FromArgb(100, 100, 100);
                MetroButtonTextDisabled = Color.FromArgb(170, 170, 170);
                MetroButtonBorder = Color.FromArgb(240, 240, 240);
                MetroButtonBorderDisabled = Color.FromArgb(200, 200, 200);
                #endregion
                #region TextBox
                MetroTextBoxBackColorNormal = Color.FromArgb(240, 240, 240);
                MetroTextBoxBackColorHover = Color.FromArgb(255, 255, 255);
                MetroTextBoxBackColorSelected = Color.FromArgb(255, 255, 255);
                MetroTextBoxBackColorDisabled = Color.FromArgb(220, 220, 220);
                MetroTextBoxBorderColorNormal = Color.FromArgb(180, 180, 180);
                MetroTextBoxBorderColorDisabled = Color.FromArgb(180, 180, 180);
                MetroTextBoxTextColorNormal = Color.FromArgb(100, 100, 100);
                MetroTextBoxTextColorHover = Color.FromArgb(100, 100, 100);
                MetroTextBoxTextColorSelected = Color.FromArgb(100, 100, 100);
                MetroTextBoxTextColorDisabled = Color.FromArgb(125, 125, 125);               
                #endregion
                #endregion
                //WindowBackColor = Color.FromArgb(240, 240, 240);
                //WindowForeColor = Color.FromArgb(255, 255, 255);
                //WindowTextColor = Color.FromArgb(50, 50, 50);

                //// Pulsante normale
                //WindowButtonColor = Color.FromArgb(240, 240, 240);
                //WindowButtonTextColor = Color.FromArgb(50, 50, 50); 
                //// Pulsante hover
                //WindowButtonHoverColor = Color.FromArgb(255, 255, 255);
                //// Pulsante clicked
                //WindowButtonClickedTextColor = Color.FromArgb(50, 50, 50);

                //WindowBorderColorDisabled = Color.FromArgb(160, 160, 160);
                //WindowResizeButtonColor = Color.FromArgb(110, 110, 110);

                //MDIWindowTextNormal = WindowTextColor;

                //MetroDropDownPanelBorder = Color.FromArgb(150,150,150);
                //MetroDropDownPanelBackGround = Color.FromArgb(210,210,210);
                //MetroDropDownPanelItemHover = Color.FromArgb(240,240,240);
                //MetroDropDownPanelSeparator = Color.FromArgb(45,45,48);
                //MetroDropDownPanelText = Color.FromArgb(20,20,20);
                //MetroDropDownPanelTextHover = Color.FromArgb(20, 20, 20);

                //MDIDropDownFilesButtonNormal = WindowBackColor;
                //MDIDropDownFilesButtonHover = WindowForeColor;
                //MDIDropDownFilesButtonTextNormal = Color.FromArgb(198,198,198);

                //MetroButtonBackColorDisabled = Color.FromArgb(240,240,240);
                //MetroButtonBackColorNormal = Color.FromArgb(240,240,240);
                //MetroButtonBackColorHover = Color.FromArgb(255,255,255);

                //MetroButtonBorderColorDisabled = Color.FromArgb(255,255,255);
                //MetroButtonBorderColorNormal = Color.FromArgb(255, 255, 255);
                //MetroButtonBorderColorHover = Color.FromArgb(255, 255, 255);
                //MetroButtonBorderColorPressed = Color.FromArgb(255, 255, 255);
                //MetroButtonTextColorDisabled = Color.FromArgb(50,50,50);
                //MetroButtonTextColorNormal = Color.FromArgb(50, 50, 50);
                //MetroButtonTextColorHover = Color.FromArgb(50, 50, 50);

                //MDIMetroButtonBackColorDisabled = Color.FromArgb(255, 255, 255);
                //MDIMetroButtonBackColorNormal = Color.FromArgb(255, 255, 255);
                //MDIMetroButtonBackColorHover = Color.FromArgb(240, 240, 240);

                //MDIMetroButtonBorderColorDisabled = Color.FromArgb(240, 240, 240);
                //MDIMetroButtonBorderColorNormal = Color.FromArgb(240, 240, 240);
                //MDIMetroButtonBorderColorHover = Color.FromArgb(240, 240, 240);
                //MDIMetroButtonBorderColorPressed = Color.FromArgb(240, 240, 240);
                //MDIMetroButtonTextColorDisabled = Color.FromArgb(50, 50, 50);
                //MDIMetroButtonTextColorNormal = Color.FromArgb(50, 50, 50);
                //MDIMetroButtonTextColorHover = Color.FromArgb(50, 50, 50);
                
                //MetroMenuBackColor = WindowBackColor;
                //MetroMenuItemColorNormal = WindowBackColor;
                //MetroMenuItemColorHover = WindowForeColor;
                //MetroMenuItemColorDisabled = WindowBackColor;
                //MetroMenuItemTextColorNormal = WindowTextColor;
                //MetroMenuItemTextColorHover = WindowTextColor;
                //MetroMenuItemTextColorDisabled = Color.FromArgb(100, 100, 100);

                //MetroLabelTextColor = WindowTextColor;
                //MetroLabelBackColor = WindowBackColor;
            }
            else if (StileMetro == Componenti.StileMetro.Scuro)
            {
                #region MetroWindow
                #region Window
                MetroWindowBackgroundColor = Color.FromArgb(45, 45, 48);
                MetroWindowBorderColorDisabled = Color.FromArgb(160, 160, 160);
                MetroWindowTitleColor = Color.FromArgb(120, 120, 120);
                MetroWindowTitleBackgroundColor = MetroWindowBackgroundColor;
                MetroWindowResizeGripColor = MetroWindowTitleColor;
                #endregion
                #region SysButtons
                MetroWindowSysButtonBackNormal = MetroWindowBackgroundColor;
                MetroWindowSysButtonBackHover = Color.FromArgb(80, 80, 80);
                MetroWindowSysButtonTextNormal = Color.White;
                #endregion
                #region ToolTip
                MetroToolTipBackgroundColor = Color.FromArgb(80, 80, 80);
                MetroToolTipBorderColor = Color.FromArgb(160, 160, 160);
                MetroToolTipTextColor = Color.FromArgb(180, 180, 180);
                #endregion
                #region Pulsanti
                MetroButtonBackgroundNormal = MetroWindowBackgroundColor;
                MetroButtonBackgroundHover = Color.FromArgb(90, 90, 90);
                MetroButtonBackgroundDisabled = Color.FromArgb(70, 70, 70);
                MetroButtonTextNormal = Color.FromArgb(180, 180, 180);
                MetroButtonTextHover = Color.FromArgb(180, 180, 180);
                MetroButtonTextDisabled = Color.FromArgb(150, 150, 150);
                MetroButtonBorder = Color.FromArgb(80, 80, 80);
                MetroButtonBorderDisabled = Color.FromArgb(55, 55, 55);
                #endregion
                #region TextBox
                MetroTextBoxBackColorNormal = Color.FromArgb(60,60,60);
                MetroTextBoxBackColorHover = Color.FromArgb(90,90,90);
                MetroTextBoxBackColorSelected = Color.FromArgb(90, 90, 90);
                MetroTextBoxBackColorDisabled = Color.FromArgb(80,80,80);
                MetroTextBoxBorderColorNormal = Color.FromArgb(100,100,100);
                MetroTextBoxBorderColorDisabled = Color.FromArgb(120,120,120);
                MetroTextBoxTextColorNormal = Color.FromArgb(180,180,180);
                MetroTextBoxTextColorHover = Color.FromArgb(180, 180, 180);
                MetroTextBoxTextColorSelected = Color.FromArgb(180, 180, 180);
                MetroTextBoxTextColorDisabled = Color.FromArgb(180, 180, 180);
                #endregion
                #endregion
                //WindowBackColor = Color.FromArgb(45, 45, 48);
                //WindowForeColor = Color.FromArgb(30, 30, 30);
                //WindowTextColor = Color.FromArgb(220, 220, 220);

                //// Pulsante normale
                //WindowButtonColor = Color.FromArgb(45, 45, 48);
                //WindowButtonTextColor = Color.FromArgb(220, 220, 220);
                //// Pulsante hover
                //WindowButtonHoverColor = Color.FromArgb(70, 70, 70);
                //// Pulsante clicked
                //WindowButtonClickedTextColor = Color.FromArgb(220, 220, 220);

                //WindowBorderColorDisabled = Color.FromArgb(160, 160, 160);
                //WindowResizeButtonColor = Color.FromArgb(200, 200, 200);

                //MDIWindowTextNormal = WindowTextColor;

                //MetroDropDownPanelBorder = Color.FromArgb(50, 50, 50);
                //MetroDropDownPanelBackGround = Color.FromArgb(20, 20, 20);
                //MetroDropDownPanelItemHover = Color.FromArgb(45, 45, 48);
                //MetroDropDownPanelSeparator = Color.FromArgb(220, 220, 220);
                //MetroDropDownPanelText = Color.FromArgb(220, 220, 220);
                //MetroDropDownPanelTextHover = Color.FromArgb(220, 220, 220);

                //MDIDropDownFilesButtonNormal = WindowBackColor;
                //MDIDropDownFilesButtonHover = WindowForeColor;
                //MDIDropDownFilesButtonTextNormal = Color.FromArgb(198, 198, 198);

                //MetroButtonBackColorDisabled = Color.FromArgb(45, 45, 48);
                //MetroButtonBackColorNormal = Color.FromArgb(45, 45, 48);
                //MetroButtonBackColorHover = Color.FromArgb(30,30,30);

                //MetroButtonBorderColorDisabled = Color.FromArgb(30,30,30);
                //MetroButtonBorderColorNormal = Color.FromArgb(30, 30, 30);
                //MetroButtonBorderColorHover = Color.FromArgb(30, 30, 30);
                //MetroButtonBorderColorPressed = Color.FromArgb(30, 30, 30);
                //MetroButtonTextColorDisabled = Color.FromArgb(220,220,220);
                //MetroButtonTextColorNormal = Color.FromArgb(220, 220, 220);
                //MetroButtonTextColorHover = Color.FromArgb(220,220,220);

                //MDIMetroButtonBackColorDisabled = Color.FromArgb(30, 30, 30);
                //MDIMetroButtonBackColorNormal = Color.FromArgb(30, 30, 30);
                //MDIMetroButtonBackColorHover = Color.FromArgb(45, 45, 48);

                //MDIMetroButtonBorderColorDisabled = Color.FromArgb(45, 45, 48);
                //MDIMetroButtonBorderColorNormal = Color.FromArgb(45, 45, 48);
                //MDIMetroButtonBorderColorHover = Color.FromArgb(45, 45, 48);
                //MDIMetroButtonBorderColorPressed = Color.FromArgb(45, 45, 48);
                //MDIMetroButtonTextColorDisabled = Color.FromArgb(220, 220, 220);
                //MDIMetroButtonTextColorNormal = Color.FromArgb(220, 220, 220);
                //MDIMetroButtonTextColorHover = Color.FromArgb(220, 220, 220);
                
                //MetroMenuBackColor = WindowBackColor;
                //MetroMenuItemColorNormal = WindowBackColor;
                //MetroMenuItemColorHover = WindowForeColor;
                //MetroMenuItemColorDisabled = WindowBackColor;
                //MetroMenuItemTextColorNormal = WindowTextColor;
                //MetroMenuItemTextColorHover = WindowTextColor;
                //MetroMenuItemTextColorDisabled = Color.FromArgb(160, 160, 160);

                //MetroLabelTextColor = WindowTextColor;
                //MetroLabelBackColor = WindowBackColor;
            }

            switch (CombinazioneColori)
            {
                case CombinazionaColori.Blu:
                    #region MetroWindow
                    #region Window
                    MetroWindowBorderColorNormal = Color.FromArgb(0, 122, 204);
                    #endregion
                    #region SysButtons
                    MetroWindowSysButtonBackPressed = Color.FromArgb(0, 122, 204);
                    MetroWindowSysButtonTextHover = Color.FromArgb(0, 122, 204);
                    MetroWindowSysButtonTextPressed = Color.White;
                    #endregion
                    #region Buttons
                    MetroButtonBackgroundPressed = Color.FromArgb(0, 122, 204);
                    MetroButtonTextPressed = Color.FromArgb(255,255,255);
                    #endregion
                    #region TextBox
                    MetroTextBoxBorderColorSelected = Color.FromArgb(0, 122, 204);
                    #endregion
                    #endregion
                    //WindowBorderColor           = Color.FromArgb(0, 122, 204);
                    //WindowButtonHoverTextColor  = Color.FromArgb(0, 122, 204);
                    //WindowButtonClickedColor    = Color.FromArgb(0, 122, 204);
                    //MetroStatusBarColor         = Color.FromArgb(0, 122, 204);
                    //MetroStatusBarTextColor     = Color.FromArgb(230, 230, 230);

                    //MDIWindowTabHover       = Color.FromArgb(51, 153, 255);
                    //MDIWindowTabNormal      = WindowBackColor;
                    //MDIWindowTabSelected    = Color.FromArgb(0, 122, 204);
                    //MDIWindowTextHover      = Color.White;
                    //MDIWindowTextSelected   = Color.White;
                    //MDIWindowTabButtonNormal = MDIWindowTabSelected;
                    //MDIWindowTabButtonHover = Color.FromArgb(132, 193, 255);
                    //MDIWindowTabButtonPressed = Color.FromArgb(0, 71, 145);

                    //MDIDropDownFilesButtonPressed = WindowButtonClickedColor;
                    //MDIDropDownFilesButtonTextHover = Color.FromArgb(51, 153, 255);
                    //MDIDropDownFilesButtonTextPressed = Color.FromArgb(255,255,255);

                    //MetroStatusBarButtonDisableBorderColor = Color.FromArgb(0, 102, 204);
                    //MetroStatusBarButtonColor = Color.FromArgb(0, 102, 204);
                    //MetroStatusBarButtonHoverColor = Color.FromArgb(7, 90, 173);
                    //MetroStatusBarButtonPressedColor = Color.FromArgb(53, 143, 232);
                    //MetroStatusBarButtonBorderColor= Color.FromArgb(0, 102, 204);
                    //MetroStatusBarButtonBorderHoverColor = Color.FromArgb(7, 90, 173);
                    //MetroStatusBarButtonBorderPressedColor = Color.FromArgb(53, 143, 232);
                    
                    //MetroStatusBarMenuItemTextColor = Color.FromArgb(230, 230, 230);
                    //MetroStatusBarMenuItemColor = Color.FromArgb(53, 143, 232);
                    //MetroStatusBarMenuItemHoverColor = Color.FromArgb(7, 90, 173);
                    //MetroStatusBarMenuItemDisabledColor = Color.FromArgb(53, 143, 232);
                    //MetroStatusBarMenuBackColor = Color.FromArgb(53, 143, 232);
                    //MetroStatusBarMenuBorderColor = Color.FromArgb(7, 90, 173);

                    //MetroLinearProgressColor = Color.FromArgb(0, 122, 204);
                    //MetroCircularProgressColor = Color.FromArgb(0, 122, 204);

                    //MetroButtonTextColorPressed = Color.FromArgb(255,255,255);
                    //MetroButtonBackColorPressed = Color.FromArgb(0,122,204);
                    //MDIMetroButtonTextColorPressed = Color.FromArgb(255,255,255);
                    //MDIMetroButtonBackColorPressed = Color.FromArgb(0,122,204);

                    break;
                case CombinazionaColori.Rosso:
                    //WindowBorderColor = Color.FromArgb(255, 0, 0);
                    //WindowButtonHoverTextColor = Color.FromArgb(255, 0, 0);
                    //WindowButtonClickedColor = Color.FromArgb(255, 0, 0);
                    //MetroStatusBarColor = Color.FromArgb(255, 0, 0);
                    //MetroStatusBarTextColor = Color.FromArgb(230, 230, 230);

                    //MetroStatusBarButtonDisableBorderColor = Color.FromArgb(255, 0, 0);
                    //MetroStatusBarButtonColor = Color.FromArgb(255, 0, 0);
                    //MetroStatusBarButtonHoverColor = Color.FromArgb(209, 10, 10);
                    //MetroStatusBarButtonPressedColor = Color.FromArgb(255, 69, 69);
                    //MetroStatusBarButtonDisableBorderColor = Color.FromArgb(255, 0, 0);
                    //MetroStatusBarButtonBorderColor = Color.FromArgb(255, 0, 0);
                    //MetroStatusBarButtonBorderHoverColor = Color.FromArgb(209, 10, 10);
                    //MetroStatusBarButtonBorderPressedColor = Color.FromArgb(255, 69, 69);
                    
                    //MetroStatusBarMenuItemTextColor = Color.FromArgb(230, 230, 230);
                    //MetroStatusBarMenuItemColor = Color.FromArgb(255, 69, 69);
                    //MetroStatusBarMenuItemHoverColor = Color.FromArgb(209, 10, 10);
                    //MetroStatusBarMenuItemDisabledColor = Color.FromArgb(255, 69, 69);
                    //MetroStatusBarMenuBackColor = Color.FromArgb(255, 69, 69);
                    //MetroStatusBarMenuBorderColor = Color.FromArgb(209, 10, 10);

                    //MetroLinearProgressColor = Color.FromArgb(255, 0, 0);
                    //MetroCircularProgressColor = Color.FromArgb(255, 0, 0);

                    //MetroButtonTextColorPressed = Color.FromArgb(255, 255, 255);
                    //MetroButtonBackColorPressed = Color.FromArgb(255,0,0);
                    //MDIMetroButtonTextColorPressed = Color.FromArgb(255, 255, 255);
                    //MDIMetroButtonBackColorPressed = Color.FromArgb(255,0,0);
                    
                    break;
                case CombinazionaColori.Verde:
                    //WindowBorderColor = Color.FromArgb(31, 186, 17);
                    //WindowButtonHoverTextColor = Color.FromArgb(31, 186, 17);
                    //WindowButtonClickedColor = Color.FromArgb(31, 186, 17);
                    //MetroStatusBarColor = Color.FromArgb(31, 186, 17);
                    //MetroStatusBarTextColor = Color.FromArgb(230, 230, 230);

                    //MetroStatusBarButtonDisableBorderColor = Color.FromArgb(31, 186, 17);
                    //MetroStatusBarButtonColor = Color.FromArgb(31, 186, 17);
                    //MetroStatusBarButtonHoverColor = Color.FromArgb(24, 161, 11);
                    //MetroStatusBarButtonPressedColor = Color.FromArgb(61, 222, 47);
                    //MetroStatusBarButtonDisableBorderColor = Color.FromArgb(31, 186, 17);
                    //MetroStatusBarButtonBorderColor = Color.FromArgb(31, 186, 17);
                    //MetroStatusBarButtonBorderHoverColor = Color.FromArgb(24, 161, 11);
                    //MetroStatusBarButtonBorderPressedColor = Color.FromArgb(61, 222, 47);
                    
                    //MetroStatusBarMenuItemTextColor = Color.FromArgb(230, 230, 230);
                    //MetroStatusBarMenuItemColor = Color.FromArgb(61, 222, 47);
                    //MetroStatusBarMenuItemHoverColor = Color.FromArgb(24, 161, 11);
                    //MetroStatusBarMenuItemDisabledColor = Color.FromArgb(61, 222, 47);
                    //MetroStatusBarMenuBackColor = Color.FromArgb(61, 222, 47);
                    //MetroStatusBarMenuBorderColor = Color.FromArgb(24, 161, 11);

                    //MetroLinearProgressColor = Color.FromArgb(31, 186, 17);
                    //MetroCircularProgressColor = Color.FromArgb(31, 186, 17);

                    //MetroButtonTextColorPressed = Color.FromArgb(255, 255, 255);
                    //MetroButtonBackColorPressed = Color.FromArgb(31, 186, 17);
                    //MDIMetroButtonTextColorPressed = Color.FromArgb(255, 255, 255);
                    //MDIMetroButtonBackColorPressed = Color.FromArgb(31, 186, 17);

                    break;
                case CombinazionaColori.Viola:
                    //WindowBorderColor = Color.FromArgb(166, 17, 186);
                    //WindowButtonHoverTextColor = Color.FromArgb(166, 17, 186);
                    //WindowButtonClickedColor = Color.FromArgb(166, 17, 186);
                    //MetroStatusBarColor = Color.FromArgb(166, 17, 186);
                    //MetroStatusBarTextColor = Color.FromArgb(230, 230, 230);

                    //MetroStatusBarButtonDisableBorderColor = Color.FromArgb(166, 17, 186);
                    //MetroStatusBarButtonColor = Color.FromArgb(166, 17, 186);
                    //MetroStatusBarButtonHoverColor = Color.FromArgb(129, 7, 145);
                    //MetroStatusBarButtonPressedColor = Color.FromArgb(208, 46, 230);
                    //MetroStatusBarButtonDisableBorderColor = Color.FromArgb(166, 17, 186);
                    //MetroStatusBarButtonBorderColor = Color.FromArgb(166, 17, 186);
                    //MetroStatusBarButtonBorderHoverColor = Color.FromArgb(129, 7, 145);
                    //MetroStatusBarButtonBorderPressedColor = Color.FromArgb(208, 46, 230);
                    
                    //MetroStatusBarMenuItemTextColor = Color.FromArgb(230, 230, 230);
                    //MetroStatusBarMenuItemColor = Color.FromArgb(208, 46, 230);
                    //MetroStatusBarMenuItemHoverColor = Color.FromArgb(129, 7, 145);
                    //MetroStatusBarMenuItemDisabledColor = Color.FromArgb(208, 46, 230);
                    //MetroStatusBarMenuBackColor = Color.FromArgb(208, 46, 230);
                    //MetroStatusBarMenuBorderColor = Color.FromArgb(129, 7, 145);

                    //MetroLinearProgressColor = Color.FromArgb(166, 17, 186);
                    //MetroCircularProgressColor = Color.FromArgb(166, 17, 186);

                    //MetroButtonTextColorPressed = Color.FromArgb(255, 255, 255);
                    //MetroButtonBackColorPressed = Color.FromArgb(166, 17, 186);
                    //MDIMetroButtonTextColorPressed = Color.FromArgb(255, 255, 255);
                    //MDIMetroButtonBackColorPressed = Color.FromArgb(166, 17, 186);

                    break;
                case CombinazionaColori.Arancione:
                    //WindowBorderColor = Color.FromArgb(219, 132, 9);
                    //WindowButtonHoverTextColor = Color.FromArgb(219, 132, 9);
                    //WindowButtonClickedColor = Color.FromArgb(219, 132, 9);
                    //MetroStatusBarColor = Color.FromArgb(219, 132, 9);
                    //MetroStatusBarTextColor = Color.FromArgb(230, 230, 230);

                    //MetroStatusBarButtonDisableBorderColor = Color.FromArgb(219, 132, 9);
                    //MetroStatusBarButtonColor = Color.FromArgb(219, 132, 9);
                    //MetroStatusBarButtonHoverColor = Color.FromArgb(189, 111, 0);
                    //MetroStatusBarButtonPressedColor = Color.FromArgb(242, 157, 36);
                    //MetroStatusBarButtonDisableBorderColor = Color.FromArgb(219, 132, 9);
                    //MetroStatusBarButtonBorderColor = Color.FromArgb(219, 132, 9);
                    //MetroStatusBarButtonBorderHoverColor = Color.FromArgb(189, 111, 0);
                    //MetroStatusBarButtonBorderPressedColor = Color.FromArgb(242, 157, 36);
                    
                    //MetroStatusBarMenuItemTextColor = Color.FromArgb(230, 230, 230);
                    //MetroStatusBarMenuItemColor = Color.FromArgb(242, 157, 36);
                    //MetroStatusBarMenuItemHoverColor = Color.FromArgb(189, 111, 0);
                    //MetroStatusBarMenuItemDisabledColor = Color.FromArgb(242, 157, 36);
                    //MetroStatusBarMenuBackColor = Color.FromArgb(242, 157, 36);
                    //MetroStatusBarMenuBorderColor = Color.FromArgb(189, 111, 0);

                    //MetroLinearProgressColor = Color.FromArgb(219, 132, 9);
                    //MetroCircularProgressColor = Color.FromArgb(219, 132, 9);

                    //MetroButtonTextColorPressed = Color.FromArgb(255, 255, 255);
                    //MetroButtonBackColorPressed = Color.FromArgb(219, 132, 9);
                    //MDIMetroButtonTextColorPressed = Color.FromArgb(255, 255, 255);
                    //MDIMetroButtonBackColorPressed = Color.FromArgb(219, 132, 9);

                    break;
            }
        }
    }
}
