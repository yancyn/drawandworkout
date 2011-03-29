using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    /// <summary>
    /// Numbering format.
    /// </summary>
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
    /// <summary>
    /// Theme color for application.
    /// </summary>
    public enum Theme
    {
        /// <summary>
        /// Plain background.
        /// </summary>
        Classic,
        /// <summary>
        /// Black background.
        /// </summary>
        Professional,
    }
}