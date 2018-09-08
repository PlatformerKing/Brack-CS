using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Brack.Interpretation.Conversion
{
    /// <summary>
    /// A stream for reading files of serialized Brack.
    /// </summary>
    public class BrackByteFileStream
    {
        private BinaryFormatter _BinaryFormatter;
        /// <summary>
        /// The FileStream.
        /// </summary>
        private FileStream _FileStream;
        /// <summary>
        /// The current index of reading.
        /// </summary>
        private int _Index;
        /// <summary>
        /// The next Brack statement.
        /// </summary>
        private object[] _Next;
        /// <summary>
        /// Is there another Brack statement?
        /// </summary>
        public bool HasNext { get => _Next != null; }
        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="mode">The file mode.</param>
        public BrackByteFileStream(string path, FileMode mode)
        {
            _Index = 0;
            _FileStream = new FileStream(path, mode);
            _BinaryFormatter = new BinaryFormatter();
            _Next = GetNext();
        }
        /// <summary>
        /// Can you read another Brack statement from this stream?
        /// </summary>
        public bool CanRead => _Next != null && _FileStream.CanRead;
        /// <summary>
        /// Can you write another Brack statement to this stream?
        /// </summary>
        public bool CanWrite => _FileStream.CanWrite;
        /// <summary>
        /// The current position being read from this stream.
        /// </summary>
        public long Position => _Index;
        /// <summary>
        /// Flush the stream to the file.
        /// </summary>
        public void Flush()
        {
            _FileStream.Flush();
        }
        /// <summary>
        /// Read the next Brack statement.
        /// </summary>
        /// <returns>The next Brack statement.</returns>
        public object[] Next()
        {
            if (!HasNext)
            {
                throw new IOException();
            }
            var ret = _Next;
            _Next = GetNext();
            return ret;
        }
        /// <summary>
        /// Internally read the next Brack statement.
        /// </summary>
        /// <returns>The internal Brack.</returns>
        private object[] GetNext()
        {
            if (!(_FileStream.Position < _FileStream.Length))
            {
                return null;
            }
            return (object[])_BinaryFormatter.Deserialize(_FileStream);
        }
        /// <summary>
        /// Write the next Brack statement.
        /// </summary>
        /// <param name="raw">The Brack statement to write.</param>
        public void Write(object[] raw)
        {
            _BinaryFormatter.Serialize(_FileStream, raw);
        }
    }
}
