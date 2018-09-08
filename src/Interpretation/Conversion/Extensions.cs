using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Brack.Interpretation
{

    /// <summary>
    /// Extensions for object[][] (Brack).
    /// </summary>
    public static class ObjectArrayArrayExtension
    {
        /// <summary>
        /// Serialize the given raw Brack to a byte[].
        /// </summary>
        /// <param name="raw">The given raw Brack.</param>
        /// <returns>The serialized byte[].</returns>
        public static byte[] SerializeBrack(this object[][] raw)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, raw);
                return stream.ToArray();
            }
        }
        /// <summary>
        /// Serialize the given raw Brack to a byte[].
        /// </summary>
        /// <param name="raw">The given raw Brack.</param>
        /// <returns>The serialized byte[].</returns>
        public static byte[] SerializeBrack(this object[] raw)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, raw);
                return stream.ToArray();
            }
        }
        /// <summary>
        /// Deserialize the given byte[] to raw Brack.
        /// </summary>
        /// <param name="raw">The given byte[].</param>
        /// <returns>The deserialized Brack.</returns>
        public static object[][] DeserializeBrack(this byte[] raw)
        {
            using (MemoryStream ms = new MemoryStream(raw))
            {
                IFormatter br = new BinaryFormatter();
                return (object[][])br.Deserialize(ms);
            }
        }
        /// <summary>
        /// Deserialize the given byte[] to raw Brack.
        /// </summary>
        /// <param name="raw">The given byte[].</param>
        /// <returns>The deserialized Brack.</returns>
        public static object[] DeserializeBrackSingle(this byte[] raw)
        {
            using (MemoryStream ms = new MemoryStream(raw))
            {
                return (object[])(new BinaryFormatter()).Deserialize(ms);
            }
        }
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
                        ret.Append(curstr);
                    }

                    if (i2 < b.Length - 1)
                    {
                        ret.Append(" ");
                    }
                }
                ret.Append("]");
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
