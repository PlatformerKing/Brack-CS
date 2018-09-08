using System;
using System.IO;

namespace Brack.Interpretation.Conversion
{
    /// <summary>
    /// A stream for reading files of Brack.
    /// </summary>
    public class BrackFileStream
    {
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
        public BrackFileStream(string path, FileMode mode)
        {
            _Index = 0;
            _FileStream = new FileStream(path, mode);
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
            int level = 0;
            bool inComment = false, inString = false;
            string tok = "";
            char? cur = null;
            while(true)
            {
                char nxt = (char)_FileStream.ReadByte();
                char bef = (cur == null) ? ' ' : (char)cur;
                cur = nxt;
                if (cur == '#' && bef != '\\')
                {
                    inComment = !inComment;
                }
                else if (!inComment)
                {
                    if (level == 0 && cur == '\"' && bef != '\\')
                    {
                        inString = !inString;
                    }
                    else
                    {
                        tok += cur;
                        if (cur == '\"' && bef != '\\')
                        {
                            inString = !inString;
                        }
                        else if (!inString)
                        {
                            if (cur == '[' && bef != '\\')
                            {
                                level++;
                            }
                            else if (cur == ']' && bef != '\\')
                            {
                                level--;
                                if (level == 0)
                                {
                                    return BrackParser.ParseString(tok)[0];
                                }
                            }
                        }
                    }
                }
                if (!(_FileStream.Position < _FileStream.Length))
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// Write the next Brack statement.
        /// </summary>
        /// <param name="raw">The Brack statement to write.</param>
        public void Write(object[] raw)
        {
            string fin = raw.ToBrackString();

            foreach(var c in fin)
            {
                _FileStream.WriteByte(Convert.ToByte(c));
            }
        }
    }
}
