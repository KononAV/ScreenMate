using System;
using System.Runtime.InteropServices;

namespace TWLogic
{
    static class NativeMethods
    {
        const string user32 = "user32.dll";
        const string dwmapi = "Dwmapi.dll";

        [DllImport(dwmapi)]
        public static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

        [DllImport(user32)]
        public static extern int MessageBox(IntPtr hWnd, string txt, string caption, uint type);

        [DllImport(user32)]
        public static extern IntPtr GetActiveWindow();

        [DllImport(user32)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport(user32)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport(user32)]
        static extern int SetLayeredWindowAttributes(
            IntPtr hwnd,
            uint crKey,
            byte bAlpha,
            uint dwFlags
        );

        [DllImport(user32, SetLastError = true)]
        public static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hSndInsertAfter,
            int x,
            int y,
            int cx,
            int cy,
            uint uFlags
        );

        public static void setMouseTrackerByPxls(IntPtr hWnd)
        {
            SetWindowLong(hWnd, WINDOW_STYLE.GWL_STYLE, WINDOW_STYLE.WS_BORDER);

            SetLayeredWindowAttributes(hWnd, 0, 0, WINDOW_POS.LWA_COLORKEY);
        }

        public static void setMouseTrackerByPhscs(IntPtr hWnd)
        {
            SetWindowLong(hWnd, WINDOW_STYLE.GWL_STYLE, WINDOW_STYLE.WS_CAPTION);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cxTopWidth;
        public int cxBottomWidth;
    }

    public struct WINDOW_STYLE
    {
        public const int GWL_STYLE = -20;
        public const int WS_BORDER = 0x0080000;
        public const int WS_DLGFRAME = 0x00000020;
        public const int WS_CAPTION = WS_BORDER | WS_DLGFRAME;
    };

    public struct WINDOW_POS
    {
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly uint LWA_COLORKEY = 0x00000001;
    }
}
