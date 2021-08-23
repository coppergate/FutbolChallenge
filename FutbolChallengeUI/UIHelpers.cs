using System;

namespace FutbolChallengeUI
{
	static public class UIHelpers
	{

		public static void SetWindowSize(IntPtr hwnd, int width, int height)
		{
			// Win32 uses pixels and WinUI 3 uses effective pixels, so you should apply the DPI scale factor
			var dpi = PInvoke.User32.GetDpiForWindow(hwnd);
			float scalingFactor = (float)dpi / 96;
			width = (int)(width * scalingFactor);
			height = (int)(height * scalingFactor);

			PInvoke.User32.SetWindowPos(hwnd, PInvoke.User32.SpecialWindowHandles.HWND_TOP,
										0, 0, width, height,
										PInvoke.User32.SetWindowPosFlags.SWP_NOMOVE);
		}
	}
}
