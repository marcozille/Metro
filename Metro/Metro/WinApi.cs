﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

namespace Metro
{
    [SuppressUnmanagedCodeSecurity]
    internal static class WinApi
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public Int32 x;
            public Int32 y;

            public POINT(Int32 x, Int32 y) { this.x = x; this.y = y; }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE
        {
            public Int32 cx;
            public Int32 cy;

            public SIZE(Int32 cx, Int32 cy) { this.cx = cx; this.cy = cy; }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ARGB
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TCHITTESTINFO
        {
            public System.Drawing.Point pt;
            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public Rectangle Rect
            {
                get { return new Rectangle(Left, Top, Right - Left, Bottom - Top); }
                set { Left = value.Left; Top = value.Top; Right = value.Right; Bottom = value.Bottom; }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            public RECT rect0;
            public RECT rect1;
            public RECT rect2;
            public IntPtr lppos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public uint cbSize;
            public IntPtr hWnd;
            public uint uCallbackMessage;
            public ABE uEdge;
            public RECT rc;
            public int lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowPos
        {
            public int hwnd;
            public int hWndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        #endregion

        #region Enums

        public enum RedrawWindowFlags : uint
        {
            Invalidate = 0x1,
            InternalPaint = 0x2,
            Erase = 0x4,
            Validate = 0x8,
            NoInternalPaint = 0x10,
            NoErase = 0x20,
            NoChildren = 0x40,
            AllChildren = 0x80,
            UpdateNow = 0x100,
            EraseNow = 0x200,
            Frame = 0x400,
            NoFrame = 0x800
        }

        public enum SystemMetric
        {
            SM_CXSCREEN = 0,  // 0x00
            SM_CYSCREEN = 1,  // 0x01
            SM_CXVSCROLL = 2,  // 0x02
            SM_CYHSCROLL = 3,  // 0x03
            SM_CYCAPTION = 4,  // 0x04
            SM_CXBORDER = 5,  // 0x05
            SM_CYBORDER = 6,  // 0x06
            SM_CXDLGFRAME = 7,  // 0x07
            SM_CXFIXEDFRAME = 7,  // 0x07
            SM_CYDLGFRAME = 8,  // 0x08
            SM_CYFIXEDFRAME = 8,  // 0x08
            SM_CYVTHUMB = 9,  // 0x09
            SM_CXHTHUMB = 10, // 0x0A
            SM_CXICON = 11, // 0x0B
            SM_CYICON = 12, // 0x0C
            SM_CXCURSOR = 13, // 0x0D
            SM_CYCURSOR = 14, // 0x0E
            SM_CYMENU = 15, // 0x0F
            SM_CXFULLSCREEN = 16, // 0x10
            SM_CYFULLSCREEN = 17, // 0x11
            SM_CYKANJIWINDOW = 18, // 0x12
            SM_MOUSEPRESENT = 19, // 0x13
            SM_CYVSCROLL = 20, // 0x14
            SM_CXHSCROLL = 21, // 0x15
            SM_DEBUG = 22, // 0x16
            SM_SWAPBUTTON = 23, // 0x17
            SM_CXMIN = 28, // 0x1C
            SM_CYMIN = 29, // 0x1D
            SM_CXSIZE = 30, // 0x1E
            SM_CYSIZE = 31, // 0x1F
            SM_CXSIZEFRAME = 32, // 0x20
            SM_CXFRAME = 32, // 0x20
            SM_CYSIZEFRAME = 33, // 0x21
            SM_CYFRAME = 33, // 0x21
            SM_CXMINTRACK = 34, // 0x22
            SM_CYMINTRACK = 35, // 0x23
            SM_CXDOUBLECLK = 36, // 0x24
            SM_CYDOUBLECLK = 37, // 0x25
            SM_CXICONSPACING = 38, // 0x26
            SM_CYICONSPACING = 39, // 0x27
            SM_MENUDROPALIGNMENT = 40, // 0x28
            SM_PENWINDOWS = 41, // 0x29
            SM_DBCSENABLED = 42, // 0x2A
            SM_CMOUSEBUTTONS = 43, // 0x2B
            SM_SECURE = 44, // 0x2C
            SM_CXEDGE = 45, // 0x2D
            SM_CYEDGE = 46, // 0x2E
            SM_CXMINSPACING = 47, // 0x2F
            SM_CYMINSPACING = 48, // 0x30
            SM_CXSMICON = 49, // 0x31
            SM_CYSMICON = 50, // 0x32
            SM_CYSMCAPTION = 51, // 0x33
            SM_CXSMSIZE = 52, // 0x34
            SM_CYSMSIZE = 53, // 0x35
            SM_CXMENUSIZE = 54, // 0x36
            SM_CYMENUSIZE = 55, // 0x37
            SM_ARRANGE = 56, // 0x38
            SM_CXMINIMIZED = 57, // 0x39
            SM_CYMINIMIZED = 58, // 0x3A
            SM_CXMAXTRACK = 59, // 0x3B
            SM_CYMAXTRACK = 60, // 0x3C
            SM_CXMAXIMIZED = 61, // 0x3D
            SM_CYMAXIMIZED = 62, // 0x3E
            SM_NETWORK = 63, // 0x3F
            SM_CLEANBOOT = 67, // 0x43
            SM_CXDRAG = 68, // 0x44
            SM_CYDRAG = 69, // 0x45
            SM_SHOWSOUNDS = 70, // 0x46
            SM_CXMENUCHECK = 71, // 0x47
            SM_CYMENUCHECK = 72, // 0x48
            SM_SLOWMACHINE = 73, // 0x49
            SM_MIDEASTENABLED = 74, // 0x4A
            SM_MOUSEWHEELPRESENT = 75, // 0x4B
            SM_XVIRTUALSCREEN = 76, // 0x4C
            SM_YVIRTUALSCREEN = 77, // 0x4D
            SM_CXVIRTUALSCREEN = 78, // 0x4E
            SM_CYVIRTUALSCREEN = 79, // 0x4F
            SM_CMONITORS = 80, // 0x50
            SM_SAMEDISPLAYFORMAT = 81, // 0x51
            SM_IMMENABLED = 82, // 0x52
            SM_CXFOCUSBORDER = 83, // 0x53
            SM_CYFOCUSBORDER = 84, // 0x54
            SM_TABLETPC = 86, // 0x56
            SM_MEDIACENTER = 87, // 0x57
            SM_STARTER = 88, // 0x58
            SM_SERVERR2 = 89, // 0x59
            SM_MOUSEHORIZONTALWHEELPRESENT = 91, // 0x5B
            SM_CXPADDEDBORDER = 92, // 0x5C
            SM_DIGITIZER = 94, // 0x5E
            SM_MAXIMUMTOUCHES = 95, // 0x5F

            SM_REMOTESESSION = 0x1000, // 0x1000
            SM_SHUTTINGDOWN = 0x2000, // 0x2000
            SM_REMOTECONTROL = 0x2001, // 0x2001
        }

        public enum ABM : uint
        {
            New = 0x00000000,
            Remove = 0x00000001,
            QueryPos = 0x00000002,
            SetPos = 0x00000003,
            GetState = 0x00000004,
            GetTaskbarPos = 0x00000005,
            Activate = 0x00000006,
            GetAutoHideBar = 0x00000007,
            SetAutoHideBar = 0x00000008,
            WindowPosChanged = 0x00000009,
            SetState = 0x0000000A,
        }

        public enum HOOKS : int
        {
            WH_CALLWNDPROC = 4,
            WH_CALLWNDPROCRET = 12,
            WH_CBT = 5,
            WH_DEBUG = 9,
            WH_FOREGROUNDIDLE = 11,
            WH_GETMESSAGE = 3,
            WH_JOURNALPLAYBACK = 1,
            WH_JOURNALRECORD = 0,
            WH_KEYBOARD = 2,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE = 7,
            WH_MOUSE_LL = 14,
            WH_MSGFILTER = -1,
            WH_SHELL = 10,
            WH_SYSMSGFILTER = 6
        }

        public enum ABE : uint
        {
            Left = 0,
            Top = 1,
            Right = 2,
            Bottom = 3
        }

        public enum ScrollBar
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3,
        }

        public enum HitTest
        {
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTGROWBOX = 4,
            HTSIZE = HTGROWBOX,
            HTMINBUTTON = 8,
            HTMAXBUTTON = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTCLOSE = 20,
            HTREDUCE = HTMINBUTTON,
            HTZOOM = HTMAXBUTTON,
            HTSIZEFIRST = HTLEFT,
            HTSIZELAST = HTBOTTOMRIGHT,
            HTTRANSPARENT = -1
        }

        public enum TabControlHitTest
        {
            TCHT_NOWHERE = 1,
        }

        public enum WindowStyles : uint
        {
            WS_BORDER = 0x00800000,
            WS_EX_CLIENTEDGE = 0x200,
            WS_EX_TRANSPARENT = 0x20,
            WS_EX_LAYERED = 0x80000,
            WS_EX_NOACTIVATE = 0x8000000
        }

        public enum Messages : uint
        {
            WM_NULL = 0x0,
            WM_CREATE = 0x1,
            WM_DESTROY = 0x2,
            WM_MOVE = 0x3,
            WM_SIZE = 0x5,
            WM_ACTIVATE = 0x6,
            WM_SETFOCUS = 0x7,
            WM_KILLFOCUS = 0x8,
            WM_ENABLE = 0xa,
            WM_SETREDRAW = 0xb,
            WM_SETTEXT = 0xc,
            WM_GETTEXT = 0xd,
            WM_GETTEXTLENGTH = 0xe,
            WM_PAINT = 0xf,
            WM_CLOSE = 0x10,
            WM_QUERYENDSESSION = 0x11,
            WM_QUERYOPEN = 0x13,
            WM_ENDSESSION = 0x16,
            WM_QUIT = 0x12,
            WM_ERASEBKGND = 0x14,
            WM_SYSCOLORCHANGE = 0x15,
            WM_SHOWWINDOW = 0x18,
            WM_WININICHANGE = 0x1a,
            WM_SETTINGCHANGE = WM_WININICHANGE,
            WM_DEVMODECHANGE = 0x1b,
            WM_ACTIVATEAPP = 0x1c,
            WM_FONTCHANGE = 0x1d,
            WM_TIMECHANGE = 0x1e,
            WM_CANCELMODE = 0x1f,
            WM_SETCURSOR = 0x20,
            WM_MOUSEACTIVATE = 0x21,
            WM_CHILDACTIVATE = 0x22,
            WM_QUEUESYNC = 0x23,
            WM_GETMINMAXINFO = 0x24,
            WM_PAINTICON = 0x26,
            WM_ICONERASEBKGND = 0x27,
            WM_NEXTDLGCTL = 0x28,
            WM_SPOOLERSTATUS = 0x2a,
            WM_DRAWITEM = 0x2b,
            WM_MEASUREITEM = 0x2c,
            WM_DELETEITEM = 0x2d,
            WM_VKEYTOITEM = 0x2e,
            WM_CHARTOITEM = 0x2f,
            WM_SETFONT = 0x30,
            WM_GETFONT = 0x31,
            WM_SETHOTKEY = 0x32,
            WM_GETHOTKEY = 0x33,
            WM_QUERYDRAGICON = 0x37,
            WM_COMPAREITEM = 0x39,
            WM_GETOBJECT = 0x3d,
            WM_COMPACTING = 0x41,
            WM_COMMNOTIFY = 0x44,
            WM_WINDOWPOSCHANGING = 0x46,
            WM_WINDOWPOSCHANGED = 0x47,
            WM_POWER = 0x48,
            WM_COPYDATA = 0x4a,
            WM_CANCELJOURNAL = 0x4b,
            WM_NOTIFY = 0x4e,
            WM_INPUTLANGCHANGEREQUEST = 0x50,
            WM_INPUTLANGCHANGE = 0x51,
            WM_TCARD = 0x52,
            WM_HELP = 0x53,
            WM_USERCHANGED = 0x54,
            WM_NOTIFYFORMAT = 0x55,
            WM_CONTEXTMENU = 0x7b,
            WM_STYLECHANGING = 0x7c,
            WM_STYLECHANGED = 0x7d,
            WM_DISPLAYCHANGE = 0x7e,
            WM_GETICON = 0x7f,
            WM_SETICON = 0x80,
            WM_NCCREATE = 0x81,
            WM_NCDESTROY = 0x82,
            WM_NCCALCSIZE = 0x83,
            WM_NCHITTEST = 0x84,
            WM_NCPAINT = 0x85,
            WM_NCACTIVATE = 0x86,
            WM_GETDLGCODE = 0x87,
            WM_SYNCPAINT = 0x88,
            WM_NCMOUSEMOVE = 0xa0,
            WM_NCLBUTTONDOWN = 0xa1,
            WM_NCLBUTTONUP = 0xa2,
            WM_NCLBUTTONDBLCLK = 0xa3,
            WM_NCRBUTTONDOWN = 0xa4,
            WM_NCRBUTTONUP = 0xa5,
            WM_NCRBUTTONDBLCLK = 0xa6,
            WM_NCMBUTTONDOWN = 0xa7,
            WM_NCMBUTTONUP = 0xa8,
            WM_NCMBUTTONDBLCLK = 0xa9,
            WM_NCXBUTTONDOWN = 0xab,
            WM_NCXBUTTONUP = 0xac,
            WM_NCXBUTTONDBLCLK = 0xad,
            WM_INPUT = 0xff,
            WM_KEYFIRST = 0x100,
            WM_KEYDOWN = 0x100,
            WM_KEYUP = 0x101,
            WM_CHAR = 0x102,
            WM_DEADCHAR = 0x103,
            WM_SYSKEYDOWN = 0x104,
            WM_SYSKEYUP = 0x105,
            WM_SYSCHAR = 0x106,
            WM_SYSDEADCHAR = 0x107,
            WM_UNICHAR = 0x109,
            WM_KEYLAST = 0x108,
            WM_IME_STARTCOMPOSITION = 0x10d,
            WM_IME_ENDCOMPOSITION = 0x10e,
            WM_IME_COMPOSITION = 0x10f,
            WM_IME_KEYLAST = 0x10f,
            WM_INITDIALOG = 0x110,
            WM_COMMAND = 0x111,
            WM_SYSCOMMAND = 0x112,
            WM_TIMER = 0x113,
            WM_HSCROLL = 0x114,
            WM_VSCROLL = 0x115,
            WM_INITMENU = 0x116,
            WM_INITMENUPOPUP = 0x117,
            WM_MENUSELECT = 0x11f,
            WM_MENUCHAR = 0x120,
            WM_ENTERIDLE = 0x121,
            WM_MENURBUTTONUP = 0x122,
            WM_MENUDRAG = 0x123,
            WM_MENUGETOBJECT = 0x124,
            WM_UNINITMENUPOPUP = 0x125,
            WM_MENUCOMMAND = 0x126,
            WM_CHANGEUISTATE = 0x127,
            WM_UPDATEUISTATE = 0x128,
            WM_QUERYUISTATE = 0x129,
            WM_CTLCOLOR = 0x19,
            WM_CTLCOLORMSGBOX = 0x132,
            WM_CTLCOLOREDIT = 0x133,
            WM_CTLCOLORLISTBOX = 0x134,
            WM_CTLCOLORBTN = 0x135,
            WM_CTLCOLORDLG = 0x136,
            WM_CTLCOLORSCROLLBAR = 0x137,
            WM_CTLCOLORSTATIC = 0x138,
            WM_MOUSEFIRST = 0x200,
            WM_MOUSEMOVE = 0x200,
            WM_LBUTTONDOWN = 0x201,
            WM_LBUTTONUP = 0x202,
            WM_LBUTTONDBLCLK = 0x203,
            WM_RBUTTONDOWN = 0x204,
            WM_RBUTTONUP = 0x205,
            WM_RBUTTONDBLCLK = 0x206,
            WM_MBUTTONDOWN = 0x207,
            WM_MBUTTONUP = 0x208,
            WM_MBUTTONDBLCLK = 0x209,
            WM_MOUSEWHEEL = 0x20a,
            WM_XBUTTONDOWN = 0x20b,
            WM_XBUTTONUP = 0x20c,
            WM_XBUTTONDBLCLK = 0x20d,
            WM_MOUSELAST = 0x20d,
            WM_PARENTNOTIFY = 0x210,
            WM_ENTERMENULOOP = 0x211,
            WM_EXITMENULOOP = 0x212,
            WM_NEXTMENU = 0x213,
            WM_SIZING = 0x214,
            WM_CAPTURECHANGED = 0x215,
            WM_MOVING = 0x216,
            WM_POWERBROADCAST = 0x218,
            WM_DEVICECHANGE = 0x219,
            WM_MDICREATE = 0x220,
            WM_MDIDESTROY = 0x221,
            WM_MDIACTIVATE = 0x222,
            WM_MDIRESTORE = 0x223,
            WM_MDINEXT = 0x224,
            WM_MDIMAXIMIZE = 0x225,
            WM_MDITILE = 0x226,
            WM_MDICASCADE = 0x227,
            WM_MDIICONARRANGE = 0x228,
            WM_MDIGETACTIVE = 0x229,
            WM_MDISETMENU = 0x230,
            WM_ENTERSIZEMOVE = 0x231,
            WM_EXITSIZEMOVE = 0x232,
            WM_DROPFILES = 0x233,
            WM_MDIREFRESHMENU = 0x234,
            WM_IME_SETCONTEXT = 0x281,
            WM_IME_NOTIFY = 0x282,
            WM_IME_CONTROL = 0x283,
            WM_IME_COMPOSITIONFULL = 0x284,
            WM_IME_SELECT = 0x285,
            WM_IME_CHAR = 0x286,
            WM_IME_REQUEST = 0x288,
            WM_IME_KEYDOWN = 0x290,
            WM_IME_KEYUP = 0x291,
            WM_MOUSEHOVER = 0x2a1,
            WM_MOUSELEAVE = 0x2a3,
            WM_NCMOUSELEAVE = 0x2a2,
            WM_WTSSESSION_CHANGE = 0x2b1,
            WM_TABLET_FIRST = 0x2c0,
            WM_TABLET_LAST = 0x2df,
            WM_CUT = 0x300,
            WM_COPY = 0x301,
            WM_PASTE = 0x302,
            WM_CLEAR = 0x303,
            WM_UNDO = 0x304,
            WM_RENDERFORMAT = 0x305,
            WM_RENDERALLFORMATS = 0x306,
            WM_DESTROYCLIPBOARD = 0x307,
            WM_DRAWCLIPBOARD = 0x308,
            WM_PAINTCLIPBOARD = 0x309,
            WM_VSCROLLCLIPBOARD = 0x30a,
            WM_SIZECLIPBOARD = 0x30b,
            WM_ASKCBFORMATNAME = 0x30c,
            WM_CHANGECBCHAIN = 0x30d,
            WM_HSCROLLCLIPBOARD = 0x30e,
            WM_QUERYNEWPALETTE = 0x30f,
            WM_PALETTEISCHANGING = 0x310,
            WM_PALETTECHANGED = 0x311,
            WM_HOTKEY = 0x312,
            WM_PRINT = 0x317,
            WM_PRINTCLIENT = 0x318,
            WM_APPCOMMAND = 0x319,
            WM_THEMECHANGED = 0x31a,
            WM_HANDHELDFIRST = 0x358,
            WM_HANDHELDLAST = 0x35f,
            WM_AFXFIRST = 0x360,
            WM_AFXLAST = 0x37f,
            WM_PENWINFIRST = 0x380,
            WM_PENWINLAST = 0x38f,
            WM_USER = 0x400,
            WM_REFLECT = 0x2000,
            WM_APP = 0x8000,
            WM_DWMCOMPOSITIONCHANGED = 0x031E,

            SC_MOVE = 0xF010,
            SC_MINIMIZE = 0XF020,
            SC_MAXIMIZE = 0xF030,
            SC_RESTORE = 0xF120,

            EM_SETCUEBANNER = 0x1501
        }

        public enum Bool
        {
            False = 0,
            True
        };

        #endregion

        #region Fields

        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOZORDER = 0x0004;
        public const uint SWP_NOREDRAW = 0x0008;
        public const uint SWP_NOACTIVATE = 0x0010;
        public const uint SWP_FRAMECHANGED = 0x0020;
        public const uint SWP_SHOWWINDOW = 0x0040;
        public const uint SWP_HIDEWINDOW = 0x0080;
        public const uint SWP_NOCOPYBITS = 0x0100;
        public const uint SWP_NOOWNERZORDER = 0x0200;
        public const uint SWP_NOSENDCHANGING = 0x0400;

        public const int Autohide = 0x0000001;
        public const int AlwaysOnTop = 0x0000002;

        public const Int32 MfByposition = 0x400;
        public const Int32 MfRemove = 0x1000;

        public const int TCM_HITTEST = 0x1313;

        public const Int32 ULW_COLORKEY = 0x00000001;
        public const Int32 ULW_ALPHA = 0x00000002;
        public const Int32 ULW_OPAQUE = 0x00000004;

        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;

        // GetWindow() constants
        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;
        public const int HC_ACTION = 0;
        public const int WH_CALLWNDPROC = 4;
        public const int GWL_WNDPROC = -4;

        #endregion

        #region API Calls

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(SystemMetric smIndex);


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint RegisterWindowMessage(string lpString);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, String pszSubAppName, String pszSubIdList);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, int pszSubAppName, String pszSubIdList);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, String pszSubAppName, int pszSubIdList);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, int pszSubAppName, int pszSubIdList);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern void DisableProcessWindowsGhosting();

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool SetLayeredWindowAttributes(IntPtr hwnd, Int32 crKey, byte alpha, Int32 dwFlags);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr wnd, int msg, bool param, int lparam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, bool wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(int hWnd, uint Msg, int wParam, int lParam);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern IntPtr SHAppBarMessage(ABM dwMessage, [In] ref APPBARDATA pData);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDCEx(IntPtr hwnd, IntPtr hrgnclip, uint fdwOptions);

        [DllImport("user32.dll")]
        public static extern bool ShowScrollBar(IntPtr hWnd, int bar, int cmd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr handle);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ReleaseDC(IntPtr handle, IntPtr hDC);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hwnd, char[] className, int maxCount);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindow(IntPtr hwnd, int uCmd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetClientRect(IntPtr hwnd, ref RECT lpRect);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetClientRect(IntPtr hwnd, [In, Out] ref Rectangle rect);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool UpdateWindow(IntPtr hwnd);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool InvalidateRect(IntPtr hwnd, ref Rectangle rect, bool bErase);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool ValidateRect(IntPtr hwnd, ref Rectangle rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref Rectangle rect);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, long dwRop);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string moduleName);
    
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int UnhookWindowsHookEx(IntPtr hhook);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, uint wParam, IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);
        public delegate IntPtr HookProc(int nCode, uint wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags);

        #endregion

        #region Helper Methods

        public static int LoWord(int dwValue)
        {
            return dwValue & 0xffff;
        }

        public static int HiWord(int dwValue)
        {
            return (dwValue >> 16) & 0xffff;
        }

        #endregion
    }
}
