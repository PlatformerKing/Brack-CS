using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Brack.Interpretation
{
    /// <summary>
    /// A utility class for serializing and deserializing raw Brack.
    /// </summary>
    public static class BrackSerializer
    {
        /// <summary>
        /// Serialize the given raw Brack to a byte[].
        /// </summary>
        /// <param name="raw">The given raw Brack.</param>
        /// <returns>The serialized byte[].</returns>
        public static byte[] SerializeBrack(object[][] raw)
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
        /// <param name="param">The given byte[].</param>
        /// <returns>The deserialized Brack.</returns>
        public static object[][] DeserializeBrack(byte[] param)
        {
            using (MemoryStream ms = new MemoryStream(param))
            {
                IFormatter br = new BinaryFormatter();
                return (object[][])br.Deserialize(ms);
            }
        }
    }
}
