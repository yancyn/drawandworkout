using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public enum NumberingFormat
    {
        /// <summary>
        /// 1, 2, 3, 4...
        /// </summary>
        Digit,
        /// <summary>
        /// I, II, III, IV, ...
        /// </summary>
        Roman,
        /// <summary>
        /// A, B, C, D, ...
        /// </summary>
        Alphabet,
        /// <summary>
        /// 甲, 乙, 丙, 丁, ...
        /// </summary>
        Chinese,
    }
}