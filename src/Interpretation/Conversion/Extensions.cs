using System.Text;

namespace Brack.Interpretation
{
    /// <summary>
    /// Extensions for object[][] (Brack).
    /// </summary>
    public static class ObjectArrayArrayExtension
    {
        /// <summary>
        /// Convert this brack to string format.
        /// </summary>
        /// <param name="raw">This raw Brack.</param>
        /// <returns>This Brack in string format.</returns>
        public static string ToBrackString(this object[][] raw)
        {
            StringBuilder ret = new StringBuilder("");
            for (var i = 0; i < raw.Length; i ++)
            {
                var b = raw[i];
                ret.Append("[");
                for(var i2 = 0; i2 < b.Length; i2 ++)
                {
                    var c = b[i2];
                    if (c is object[])
                    {
                        ret.Append((new object[][] { (object[])c }).ToBrackString());
                    }
                    else
                    {
                        var curstr = c.ToString();
                        curstr = curstr.Replace("\"", "\\\"").Replace("\n", "\\n").Replace("#", "\\#").Replace("\t", "\\t").Replace("\r", "\\r");
                        if (curstr.Contains(" ") || curstr.Contains("\n") || curstr.Contains("\t") || curstr.Contains("\r"))
                        {
                            curstr = "\"" + curstr + "\"";
                        }
                    }

                    if (i2 < b.Length - 1)
                    {
                        ret.Append(" ");
                    }
                }
            }
            return ret.ToString();
        }
    }
    /// <summary>
    /// Extensions for object[] (a single Brack statement).
    /// </summary>
    public static class ObjectArrayExtension
    {
        /// <summary>
        /// Convert this brack to string format.
        /// </summary>
        /// <param name="raw">This raw Brack statement.</param>
        /// <returns>This Brack in string format.</returns>
        public static string ToBrackString(this object[] raw)
        {
            return (new object[][] { raw }).ToBrackString();
        }
    }
}
