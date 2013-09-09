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

namespace Metro
{
    public static class MetroGlobals
    {
        public static PrivateFontCollection FontCollection { get; set; }

        static MetroGlobals()
        {
            LoadMemoryFont();
        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        private static void LoadMemoryFont()
        {
            FontCollection = new PrivateFontCollection();

            byte[] fontArray = Properties.Resources.segoeui;
            int fontArrayLen = Properties.Resources.segoeui.Length;

            IntPtr fontData = Marshal.AllocCoTaskMem(fontArrayLen);
            Marshal.Copy(fontArray, 0, fontData, fontArrayLen);

            uint cFonts = 0;
            AddFontMemResourceEx(fontData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            FontCollection.AddMemoryFont(fontData, fontArrayLen);
            Marshal.FreeCoTaskMem(fontData);

            fontArray = Properties.Resources.seguisym;
            fontArrayLen = Properties.Resources.seguisym.Length;

            fontData = Marshal.AllocCoTaskMem(fontArrayLen);
            Marshal.Copy(fontArray, 0, fontData, fontArrayLen);

            cFonts = 0;
            AddFontMemResourceEx(fontData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            FontCollection.AddMemoryFont(fontData, fontArrayLen);
            Marshal.FreeCoTaskMem(fontData);
        }
    }
}
