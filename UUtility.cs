using System;
using System.Collections.Generic;
using System.Text;

namespace FileArranger
{
    public static class UUtility
    {
        public static float UnClamp(float value, float InMinimum, float InMaximum, float OutMinimum, float OutMaximum)
        {
            UnClamp(ref value, InMinimum, InMaximum, OutMinimum, OutMaximum);

            return value;
        }

        public static void UnClamp(ref float value, float InMinimum, float InMaximum, float OutMinimum, float OutMaximum)
        {
            float InRange = InMaximum - InMinimum;
            float OutRange = OutMaximum - OutMinimum;
            value = ((value - InMinimum) * OutRange / InRange) + OutMinimum;
        }
    }
}
