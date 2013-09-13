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

            LoadFonts();
        }

        private void LoadFonts()
        {
            MetroWindowSysButtonFont = new Font("Webdings", 10f);
            MetroToolTipFont = new Font(MetroGlobals.FontCollection.Families[0], 10f);
            MetroButtonFont = new Font(MetroGlobals.FontCollection.Families[0], 10f);
            MetroTextBoxFont = new Font(MetroGlobals.FontCollection.Families[0], 10f);
            MetroComboBoxFont = new Font(MetroGlobals.FontCollection.Families[0], 10f);
            MetroWindowTitleFont = new Font(MetroGlobals.FontCollection.Families[0], 12.5f);
            MetroTileTextFont = new Font(MetroGlobals.FontCollection.Families[0], 9f);
            MetroTileNumberFont = new Font(MetroGlobals.FontCollection.Families[0], 20f);
            MetroSymbolFont = new Font(MetroGlobals.FontCollection.Families[1], 10f);
        }

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
        public Color MetroTextBoxBorderColorHover { get; set; }
        public Color MetroTextBoxBorderColorSelected { get; set; }
        public Color MetroTextBoxBorderColorDisabled { get; set; }

        public Color MetroTextBoxTextColorNormal { get; set; }
        public Color MetroTextBoxTextColorHover { get; set; }
        public Color MetroTextBoxTextColorSelected { get; set; }
        public Color MetroTextBoxTextColorDisabled { get; set; }
        #region PasswordBox
        public Color MetroPasswordBoxVaiButtonBackNormal { get; set; }
        public Color MetroPasswordBoxVaiButtonBackHover { get; set; }
        public Color MetroPasswordBoxVaiButtonBackPressed { get; set; }
        public Color MetroPasswordBoxVaiButtonBackDisabled { get; set; }
        public Color MetroPasswordBoxVaiButtonTextNormal { get; set; }
        public Color MetroPasswordBoxVaiButtonTextHover { get; set; }
        public Color MetroPasswordBoxVaiButtonTextPressed { get; set; }
        public Color MetroPasswordBoxVaiButtonTextDisabled { get; set; }
        #endregion
        #region Speciali
        public Color MetroTextBoxSpecialeButtonBackNormal { get; set; }
        public Color MetroTextBoxSpecialeButtonBackHover { get; set; }
        public Color MetroTextBoxSpecialeButtonBackPressed { get; set; }
        public Color MetroTextBoxSpecialeButtonBackDisabled { get; set; }
        public Color MetroTextBoxSpecialeButtonTextNormal { get; set; }
        public Color MetroTextBoxSpecialeButtonTextHover { get; set; }
        public Color MetroTextBoxSpecialeButtonTextPressed { get; set; }
        public Color MetroTextBoxSpecialeButtonTextDisabled { get; set; }
        #endregion
        #endregion
        #region RoundButtons
        public Color MetroRoundButtonBackNormal { get; set; }
        public Color MetroRoundButtonBackHover { get; set; }
        public Color MetroRoundButtonBackPressed { get; set; }
        public Color MetroRoundButtonBackDisabled { get; set; }
        public Color MetroRoundButtonCircleNormal { get; set; }
        public Color MetroRoundButtonCircleHover { get; set; }
        public Color MetroRoundButtonCirclePressed { get; set; }
        public Color MetroRoundButtonCircleDisabled { get; set; }
        public Color MetroRoundButtonImageNormal { get; set; }
        public Color MetroRoundButtonImageHover { get; set; }
        public Color MetroRoundButtonImagePressed { get; set; }
        public Color MetroRoundButtonImageDisabled { get; set; }
        public Color MetroRoundButtonTextNormal { get; set; }
        public Color MetroRoundButtonTextHover { get; set; }
        public Color MetroRoundButtonTextDisabled { get; set; }
        #endregion
        #region ComboBox
        public Color MetroComboBoxBorderNormal { get; set; }
        public Color MetroComboBoxBorderHover { get; set; }
        public Color MetroComboBoxBorderExpanded { get; set; }
        public Color MetroComboBoxBorderDisabled { get; set; }

        public Color MetroComboBoxBackNormal { get; set; }
        public Color MetroComboBoxBackHover { get; set; }
        public Color MetroComboBoxBackExpanded { get; set; }
        public Color MetroComboBoxBackDisabled { get; set; }

        public Color MetroComboBoxButtonNormal { get; set; }
        public Color MetroComboBoxButtonHover { get; set; }
        public Color MetroComboBoxButtonExpanded { get; set; }
        public Color MetroComboBoxButtonDisabled { get; set; }

        public Color MetroComboBoxTextNormal { get; set; }
        public Color MetroComboBoxTextHover { get; set; }
        public Color MetroComboBoxTextExpanded { get; set; }
        public Color MetroComboBoxTextDisabled { get; set; }

        public Color MetroComboBoxFrecciaButtonNormal { get; set; }
        public Color MetroComboBoxFrecciaButtonHover { get; set; }
        public Color MetroComboBoxFrecciaButtonExpanded { get; set; }
        public Color MetroComboBoxFrecciaButtonDisabled { get; set; }

        public Color MetroComboBoxPanel { get; set; }
        public Color MetroComboBoxPanelBorder { get; set; }

        public Color MetroComboBoxItemHover { get; set; }

        public Color MetroComboBoxItemTextNormal { get; set; }
        public Color MetroComboBoxItemTextHover { get; set; }

        public Color MetroComboBoxPromptTextNormal { get; set; }
        public Color MetroComboBoxPromptTextHover { get; set; }
        public Color MetroComboBoxPromptTextExpanded { get; set; }
        public Color MetroComboBoxPromptTextDisabled { get; set; }
        #endregion
        #region Tile
        public Color MetroTileBackColor { get; set; }
        public Color MetroTileTextColor { get; set; }
        public Color MetroTileHoverBorderColor { get; set; }
        public Color MetroTileSelectedBorderColor { get; set; }
        #endregion
        #endregion

        #region Fonts
        public Font MetroWindowTitleFont { get; set; }
        public Font MetroWindowSysButtonFont { get; set; }
        public Font MetroToolTipFont { get; set; }
        public Font MetroButtonFont { get; set; }
        public Font MetroTextBoxFont { get; set; }
        public Font MetroComboBoxFont { get; set; }
        public Font MetroSymbolFont { get; set; }
        public Font MetroTileTextFont { get; set; }
        public Font MetroTileNumberFont { get; set; }
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
                MetroTextBoxBorderColorHover = Color.FromArgb(180, 180, 180);
                MetroTextBoxBorderColorDisabled = Color.FromArgb(180, 180, 180);
                MetroTextBoxTextColorNormal = Color.FromArgb(100, 100, 100);
                MetroTextBoxTextColorHover = Color.FromArgb(100, 100, 100);
                MetroTextBoxTextColorSelected = Color.FromArgb(100, 100, 100);
                MetroTextBoxTextColorDisabled = Color.FromArgb(125, 125, 125);               
                #endregion
                #endregion
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
                MetroButtonBorder = Color.FromArgb(90, 90, 90);
                MetroButtonBorderDisabled = Color.FromArgb(55, 55, 55);
                #endregion
                #region TextBox
                MetroTextBoxBackColorNormal = Color.FromArgb(60,60,60);
                MetroTextBoxBackColorHover = MetroTextBoxBackColorNormal;
                MetroTextBoxBackColorSelected = MetroTextBoxBackColorNormal;
                MetroTextBoxBackColorDisabled = Color.FromArgb(80, 80, 80);
                MetroTextBoxBorderColorNormal = Color.FromArgb(20, 20, 20);
                MetroTextBoxBorderColorHover = Color.FromArgb(140, 140, 140);
                MetroTextBoxBorderColorDisabled = Color.FromArgb(20, 20, 20);
                MetroTextBoxTextColorNormal = Color.FromArgb(180, 180, 180);
                MetroTextBoxTextColorHover = Color.FromArgb(180, 180, 180);
                MetroTextBoxTextColorSelected = Color.FromArgb(180, 180, 180);
                MetroTextBoxTextColorDisabled = Color.FromArgb(180, 180, 180);
                #region Password
                MetroPasswordBoxVaiButtonBackPressed = MetroTextBoxBackColorSelected;
                MetroPasswordBoxVaiButtonTextPressed = Color.FromArgb(180, 180, 180);
                MetroPasswordBoxVaiButtonBackDisabled = MetroTextBoxBackColorDisabled;
                MetroPasswordBoxVaiButtonTextDisabled = Color.FromArgb(255, 255, 255);
                #endregion
                #region Speciali
                MetroTextBoxSpecialeButtonBackNormal = MetroTextBoxBackColorNormal;
                MetroTextBoxSpecialeButtonTextNormal = Color.FromArgb(180, 180, 180);
                MetroTextBoxSpecialeButtonBackHover = Color.FromArgb(75, 75, 75);
                MetroTextBoxSpecialeButtonTextHover = Color.FromArgb(180, 180, 180);
                MetroTextBoxSpecialeButtonBackDisabled = MetroTextBoxBackColorDisabled;
                MetroTextBoxSpecialeButtonTextDisabled = Color.FromArgb(255, 255, 255);
                #endregion
                #endregion
                #region RoundButtons
                MetroRoundButtonBackNormal = Color.Transparent;
                MetroRoundButtonBackHover = Color.Transparent;
                MetroRoundButtonBackDisabled = Color.Transparent;
                MetroRoundButtonCircleNormal = Color.FromArgb(80, 80, 80);
                MetroRoundButtonCircleHover = Color.FromArgb(150, 150, 150);
                MetroRoundButtonCircleDisabled = Color.FromArgb(80, 80, 80);  
                MetroRoundButtonImageNormal = Color.FromArgb(80, 80, 80);
                MetroRoundButtonImageHover = Color.FromArgb(150, 150, 150);
                MetroRoundButtonImageDisabled = Color.FromArgb(80, 80, 80);
                MetroRoundButtonTextNormal = Color.FromArgb(80, 80, 80);
                MetroRoundButtonTextHover = Color.FromArgb(150, 150, 150);
                MetroRoundButtonTextDisabled = Color.FromArgb(80, 80, 80);
                #endregion
                #endregion
                #region ComboBox
                MetroComboBoxBorderNormal = Color.FromArgb(90, 90, 90);
                MetroComboBoxBorderHover = Color.FromArgb(90, 90, 90);
                MetroComboBoxBorderExpanded = Color.FromArgb(90, 90, 90);
                MetroComboBoxBorderDisabled = Color.FromArgb(90, 90, 90);
                MetroComboBoxBackNormal = Color.FromArgb(55, 55, 55);
                MetroComboBoxBackHover = Color.FromArgb(90, 90, 90);
                MetroComboBoxBackExpanded = Color.FromArgb(90, 90, 90);
                MetroComboBoxBackDisabled = Color.FromArgb(55, 55, 55);
                MetroComboBoxButtonNormal = Color.FromArgb(55, 55, 55);
                MetroComboBoxButtonHover = Color.FromArgb(30, 30, 30);
                MetroComboBoxButtonDisabled = Color.FromArgb(55, 55, 55);
                MetroComboBoxFrecciaButtonNormal = Color.FromArgb(140, 140, 140);
                MetroComboBoxFrecciaButtonDisabled = Color.FromArgb(140, 140, 140);
                MetroComboBoxPanel = Color.FromArgb(25, 25, 25);
                MetroComboBoxPanelBorder = Color.FromArgb(90, 90, 90);

                MetroComboBoxTextNormal = Color.FromArgb(180, 180, 180);
                MetroComboBoxTextHover = Color.FromArgb(180, 180, 180);
                MetroComboBoxTextExpanded = Color.FromArgb(180, 180, 180);
                MetroComboBoxTextDisabled = Color.FromArgb(150, 150, 150);

                MetroComboBoxItemTextNormal = Color.FromArgb(180, 180, 180);
                
                MetroComboBoxPromptTextNormal = Color.FromArgb(140, 140, 140);
                MetroComboBoxPromptTextHover = Color.FromArgb(140, 140, 140);
                MetroComboBoxPromptTextExpanded = Color.FromArgb(140, 140, 140);
                MetroComboBoxPromptTextDisabled = Color.FromArgb(100, 100, 100);
                #endregion
                #region Tile
                MetroTileHoverBorderColor = Color.FromArgb(150, 140, 140, 140);
                #endregion
            }

            switch (CombinazioneColori)
            {
                case CombinazionaColori.Blu:
                    #region MetroWindow
                    #region Window
                    MetroWindowBorderColorNormal = Color.FromArgb(78, 166, 234);
                    #endregion
                    #region SysButtons
                    MetroWindowSysButtonBackPressed = Color.FromArgb(78, 166, 234);
                    MetroWindowSysButtonTextHover = Color.FromArgb(78, 166, 234);
                    MetroWindowSysButtonTextPressed = Color.White;
                    #endregion
                    #region Buttons
                    MetroButtonBackgroundPressed = Color.FromArgb(78, 166, 234);
                    MetroButtonTextPressed = Color.FromArgb(255, 255, 255);
                    #endregion
                    #region TextBox
                    MetroTextBoxBorderColorSelected = Color.FromArgb(78, 166, 234);
                    #region Password
                    MetroPasswordBoxVaiButtonBackNormal = Color.FromArgb(78, 166, 234);
                    MetroPasswordBoxVaiButtonBackHover  = Color.FromArgb(115, 186, 238);
                    MetroPasswordBoxVaiButtonTextNormal = Color.FromArgb(255, 255, 255);
                    MetroPasswordBoxVaiButtonTextHover = Color.FromArgb(255, 255, 255);
                    #endregion
                    #region Speciale
                    MetroTextBoxSpecialeButtonBackPressed = Color.FromArgb(78, 166, 234);
                    MetroTextBoxSpecialeButtonTextPressed = Color.FromArgb(255, 255, 255);                    
                    #endregion
                    #endregion
                    #region RoundButtons
                    MetroRoundButtonBackPressed = Color.Transparent;
                    MetroRoundButtonCirclePressed = Color.FromArgb(78, 166, 234);
                    MetroRoundButtonImagePressed = Color.FromArgb(78, 166, 234);
                    #endregion
                    #endregion
                    #region ComboBox
                    MetroComboBoxButtonExpanded = Color.FromArgb(78, 166, 234);
                    MetroComboBoxFrecciaButtonHover = Color.FromArgb(78, 166, 234);
                    MetroComboBoxFrecciaButtonExpanded = Color.FromArgb(255, 255, 255);
                    MetroComboBoxItemHover = Color.FromArgb(78, 166, 234);
                    MetroComboBoxItemTextHover = Color.FromArgb(255, 255, 255);
                    #endregion
                    #region Tile
                    MetroTileBackColor = Color.FromArgb(78, 166, 234);
                    MetroTileSelectedBorderColor = Color.FromArgb(21, 11, 176);
                    MetroTileTextColor = Color.FromArgb(255, 255, 255);
                    #endregion
                    break;
                case CombinazionaColori.Rosso:
                    break;
                case CombinazionaColori.Verde:
                    break;
                case CombinazionaColori.Viola:
                    break;
                case CombinazionaColori.Arancione:
                    break;
            }
        }
    }
}
