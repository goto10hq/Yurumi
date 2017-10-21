using System.Text;

namespace Yurumi.Encoders
{
    /// <summary>
    /// Encoder interface.
    /// </summary>
    public interface IEncoder
    {
        /// <summary>
        /// Encodes the specified input.
        /// </summary>
        /// <returns>The encoded input.</returns>
        /// <param name="input">Input.</param>
        string Encode(string input);

        /// <summary>
        /// Gets the charset.
        /// </summary>
        /// <value>The charset.</value>
        string Charset { get; }

        /// <summary>
        /// Gets the encoding.
        /// </summary>
        /// <value>The encoding.</value>
        Encoding Encoding { get; }
    }
}
