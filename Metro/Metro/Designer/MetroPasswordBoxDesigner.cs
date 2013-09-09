using System.Collections;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace Metro.Designer
{
    public class MetroPasswordBoxDisegner : ControlDesigner
    {
        public override SelectionRules SelectionRules
        {
            get
            {
                return SelectionRules.LeftSizeable | SelectionRules.RightSizeable | SelectionRules.Moveable;
            }
        }
    }
}
