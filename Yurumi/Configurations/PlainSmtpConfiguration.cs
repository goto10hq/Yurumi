using System.Collections.Specialized;

namespace Yurumi.Configurations
{
    /// <summary>
    /// Plain smtp configuration.
    /// </summary>
    public class PlainSmtpConfiguration : IConfiguration
    {
        /// <summary>
        /// Get header values.
        /// </summary>
        public NameValueCollection HeaderValues { get; } = new NameValueCollection();
    }
}
