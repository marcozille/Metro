using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Metro.Componenti;

namespace Metro.Interfaccie
{
    public interface IMetroWindow
    {
        CombinazionaColori CombinazioneColori { get; set; }
        StileMetro StileMetro { get; set; }
        MetroVisualManager VisualManager { get; set; }
    }

    public interface IMetroComponent
    {
        CombinazionaColori CombinazioneColori { get; set; }
        StileMetro StileMetro { get; set; }
        MetroVisualManager VisualManager { get; set; }
    }

    public interface IMetroControl
    {
        CombinazionaColori CombinazioneColori { get; set; }
        StileMetro StileMetro { get; set; }
        MetroVisualManager VisualManager { get; set; }
    }
}
