using System;

namespace MonoGame.DisplayModeHelper
{
    public static class ResolutionHelper
    {
        public static bool ChangeResolution(int width, int height)
        {
            DEVMODE1 supportedMode;
            var key = string.Format("{0}x{1}", width, height);

            if (!DisplayModes.SupportedDisplayModes.TryGetValue(key, out supportedMode))
                return false;

            int iRet = NativeMethods.ChangeDisplaySettings(ref supportedMode, NativeMethods.CDS_TEST);
            if (iRet == NativeMethods.DISP_CHANGE_FAILED)
                return false;

            iRet = NativeMethods.ChangeDisplaySettings(ref supportedMode, NativeMethods.CDS_NONE);

            switch (iRet)
            {
                case NativeMethods.DISP_CHANGE_SUCCESSFUL:
                    break;
                case NativeMethods.DISP_CHANGE_RESTART:
                    //windows 9x series you have to restart
                default:
                    return false;
            }

            return true;
        }
    }
}
