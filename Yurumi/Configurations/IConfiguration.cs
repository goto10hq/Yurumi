using System.Collections.Specialized;

namespace Yurumi.Configurations
{
    /// <summary>
    /// Configuration interface.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets the header values.
        /// </summary>
        /// <value>The header values.</value>
        NameValueCollection HeaderValues { get; }
    }
}
