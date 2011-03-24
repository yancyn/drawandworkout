using System;
using System.ComponentModel;

namespace HLGranite.Drawing
{
    /// <summary>
    /// Language option for application.
    /// </summary>
    /// <see>http://msdn.microsoft.com/en-us/library/system.globalization.cultureinfo%28VS.80%29.aspx</see>
    public enum Language
    {
        /// <summary>
        /// English for United States stored as en-US.
        /// </summary>
        [Description("en-US")]
        English,
        /// <summary>
        /// Simplified Chinese stored as zh-CN.
        /// </summary>
        [Description("zh-CN")]
        Chinese,
    }
}