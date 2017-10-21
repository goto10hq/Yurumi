using System.Text;

namespace Yurumi.Encoders
{
    /// <summary>
    /// ISO-8859-2 encoder.
    /// </summary>
    public class Iso88592Encoder : IEncoder
    {
        /// <summary>
        /// Gets the charset.
        /// </summary>
        /// <value>The charset.</value>
        public string Charset => "iso-8859-2";

        /// <summary>
        /// Gets the encoding.
        /// </summary>
        /// <value>The encoding.</value>
        public Encoding Encoding => CodePagesEncodingProvider.Instance.GetEncoding(Charset);

        /// <summary>
        /// Encodes the specified input.
        /// </summary>
        /// <returns>The encoded input.</returns>
        /// <param name="input">Input.</param>
        public string Encode(string input)
        {
            if (input == null)
                throw new System.ArgumentNullException(nameof(input));
            
            var iso = Encoding;
            var utf8 = Encoding.UTF8;
            var utfBytes = utf8.GetBytes(input);
            var isoBytes = Encoding.Convert(utf8, iso, utfBytes);
            var msg = iso.GetString(isoBytes);

            return msg;
        }
    }
}
