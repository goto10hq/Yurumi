namespace Yurumi.Converters
{
    /// <summary>
    /// Converter interface.
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// Converts the specified input.
        /// </summary>
        /// <returns>The converted input.</returns>
        /// <param name="input">Input.</param>
        string Convert(string input);
    }
}
