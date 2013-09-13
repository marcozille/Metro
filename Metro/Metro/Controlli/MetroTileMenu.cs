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
    class MetroTileMenu : Control, IMetroControl, IContainerControl
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

        private Control activeControl = null;
        [Browsable(false)]
        public Control ActiveControl
        {
            get { return activeControl; }
            set { activeControl = value; }
        }

        #endregion

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
    }
}
