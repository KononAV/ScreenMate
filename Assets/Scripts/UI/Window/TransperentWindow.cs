using System;
using UnityEngine;
using MARGINS = TWLogic.MARGINS;
using w32 = TWLogic.NativeMethods;
using wPos = TWLogic.WINDOW_POS;
using wStyle = TWLogic.WINDOW_STYLE;

public struct TransparentWindow
{
    public void Execute()
    {
#if !UNITY_EDITOR

        IntPtr hWnd = w32.GetActiveWindow();

        w32.setMouseTrackerByPxls(hWnd);
        w32.SetWindowPos(hWnd, wPos.HWND_TOPMOST, 0, 0, 0, 0, 0);

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };

        w32.DwmExtendFrameIntoClientArea(hWnd, ref margins);

#endif
        Application.runInBackground = true;
    }

    public void UpdateForMouseTrackerByPhscs()
    {
        // bool clickThrouch =
    }
}
