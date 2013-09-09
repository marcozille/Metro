using System.Collections;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace Metro.Designer
{
    public class MetroRoundButtonDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules
        {
            get
            {
                return SelectionRules.Moveable;
            }
        }
    }
}
