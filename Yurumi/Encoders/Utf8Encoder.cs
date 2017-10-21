using System.Text;

namespace Yurumi.Encoders
{
    /// <summary>
    /// UTF-8 encoder.
    /// </summary>
    public class Utf8Encoder : IEncoder
    {
        /// <summary>
        /// Gets the charset.
        /// </summary>
        /// <value>The charset.</value>
        public string Charset => "utf-8";

        /// <summary>
        /// Gets the encoding.
        /// </summary>
        /// <value>The encoding.</value>
        public Encoding Encoding => Encoding.UTF8;

        /// <summary>
        /// Encodes the specified input.
        /// </summary>
        /// <returns>The encoded input.</returns>
        /// <param name="input">Input.</param>
        public string Encode(string input)
        {
            if (input == null)
                throw new System.ArgumentNullException(nameof(input));

            return input;
        }
    }
}
