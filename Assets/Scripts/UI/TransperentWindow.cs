using System;
using UnityEngine;
using MARGINS = TWLogic.MARGINS;
using w32 = TWLogic.NativeMethods;
using wPos = TWLogic.WINDOW_POS;
using wStyle = TWLogic.WINDOW_STYLE;

public class TransparentWindow : MonoBehaviour
{
    private void Start()
    {
#if !UNITY_EDITOR
        //   w32.MessageBox(new IntPtr(0), "Hello,world", "hello dialog", 0);

        IntPtr hWnd = w32.GetActiveWindow();

        w32.SetWindowLong(hWnd, wStyle.GWL_STYLE, wStyle.WS_CAPTION);

        w32.SetWindowPos(hWnd, wPos.HWND_TOPMOST, 0, 0, 0, 0, 0);

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };

        w32.DwmExtendFrameIntoClientArea(hWnd, ref margins);

#endif
        Application.runInBackground = true;
    }
}
