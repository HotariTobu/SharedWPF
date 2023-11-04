using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace SharedWPF
{
    public static class WindowExtension
    {
        [DllImport("user32.dll")]
        private static extern uint GetWindowLong(IntPtr hWnd, int index);

        [DllImport("user32.dll")]
        private static extern uint SetWindowLong(IntPtr hWnd, int index, uint newLong);

        /// <summary>
        /// Showを呼ぶ前に呼ぶことでWindowがマウスイベントを透過するようにします。
        /// </summary>
        /// <param name="window"></param>
        public static void Isolate(this Window window)
        {
            window.SourceInitialized += ((sender, e) =>
            {
                IntPtr handle = new System.Windows.Interop.WindowInteropHelper(window).Handle;
                SetWindowLong(handle, -20, GetWindowLong(handle, -20) | 0x0020);
            });
        }
    }
}
