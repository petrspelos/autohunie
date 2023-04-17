using System.Diagnostics;
using System.Runtime.InteropServices;

internal static class WindowsApi
{
    internal enum SpecialWindowHandles
    {
        Top = 0,
        Bottom = 1,
        TopMost = -1,
        NoTopMost = -2
    }

    [Flags]
    internal enum SetWindowPosFlags : uint
    {
        AsyncWindowPositioning = 0x4000,
        DeferErase = 0x2000,
        DrawFrame = 0x0020,
        FrameChanged = 0x0020,
        HideWindow = 0x0080,
        NoActivate = 0x0010,
        NoCopyBits = 0x0100,
        NoMove = 0x0002,
        NoOwnerZOrder = 0x0200,
        NoRedraw = 0x0008,
        NoReposition = 0x0200,
        NoSendChanging = 0x0400,
        NoSize = 0x0001,
        NoZOrder = 0x0004,
        ShowWindow = 0x0040
    }

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SetWindowPosFlags uFlags);

    internal static IntPtr GetWindowHandleByTitle(string windowTitle)
    {
        IntPtr hWnd = IntPtr.Zero;
        foreach (Process pList in Process.GetProcesses())
        {
            if (pList.MainWindowTitle.Contains(windowTitle))
            {
                hWnd = pList.MainWindowHandle;
            }
        }
        return hWnd;
    }

    internal static void MoveWindow(string windowTitle, int x, int y, int width = 1270, int height = 745)
    {
        var handle = GetWindowHandleByTitle(windowTitle);

        SetWindowPos(handle, 0, x, y, width, height, SetWindowPosFlags.DrawFrame);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    internal static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const int MOUSEEVENTF_RIGHTUP = 0x10;

    internal static void DoMouseClick()
    {
        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }

    internal static void PressMouseButton()
    {
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
    }

    internal static void ReleaseMouseButton()
    {
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

    internal static void MoveCursorToPoint(int x, int y)
    {
        SetCursorPos(x, y);
    }

    internal static void MoveCursorToPointScreenSpace(int x, int y)
    {
        SetCursorPos((int)(x * 0.8), (int)(y * 0.8));
    }
}
