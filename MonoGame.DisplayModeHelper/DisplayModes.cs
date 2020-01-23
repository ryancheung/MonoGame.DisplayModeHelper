using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MonoGame.DisplayModeHelper
{
    public class DisplayModes
    {
        public struct Size
        {
            public int Width;
            public int Height;
        }

        public static Dictionary<string, DEVMODE1> SupportedDisplayModes = new Dictionary<string, DEVMODE1>();

        static DisplayModes()
        {
            InitializeSupportedDisplayModes();
        }

        public static Size CurrentResolution
        {
            get
            {
                return new Size
                {
                    Width = NativeMethods.GetSystemMetrics(NativeMethods.SM_CXSCREEN),
                    Height = NativeMethods.GetSystemMetrics(NativeMethods.SM_CYSCREEN)
                };
            }
        }

        private static void InitializeSupportedDisplayModes()
        {
            var stop = false;
            var index = 0;

            while (!stop)
            {
                DEVMODE1 dm = new DEVMODE1();
                dm.dmDeviceName = new String (new char[32]);
                dm.dmFormName = new String (new char[32]);
                dm.dmSize = (short)Marshal.SizeOf (dm);

                var ret = NativeMethods.EnumDisplaySettings(null, index, ref dm);
                index++;

                if (ret == 0)
                    stop = true;
                else
                {
                    if (dm.dmDisplayFrequency >= 60 && dm.dmBitsPerPel >= 30)
                    {

                        if (!SupportedDisplayModes.ContainsKey(dm.ToString()))
                            SupportedDisplayModes.Add(dm.ToString(), dm);
                    }
                }
            }
        }
    }
}
