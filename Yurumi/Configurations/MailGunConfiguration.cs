using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Yurumi.Configurations
{
    /// <summary>
    /// MailGun configuration.
    /// </summary>
    /// <remarks>http://www.mailgun.com</remarks>
    public class MailGunConfiguration: IConfiguration
    {
        const string _headerVariables = "X-Mailgun-Variables";
        const string _headerTag = "X-Mailgun-Tag";
        const string _headerTrack = "X-Mailgun-Track";

        /// <summary>
        /// MailGun configuration.
        /// </summary>
        /// <param name="tag">Tag name.</param>
        /// <param name="enableClickTracking">If set to <c>true</c> enable click tracking.</param>
        /// <param name="variables">Object to be serialized as a collection of variables or null.</param>
        public MailGunConfiguration(string tag, bool enableClickTracking, object variables = null)
        {
            HeaderValues.Add(_headerTag, tag);
            HeaderValues.Add(_headerTrack, enableClickTracking ? "yes" : "no");

            if (variables != null)
            {
                var output = JsonConvert.SerializeObject(variables, Formatting.None,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

                HeaderValues.Add(_headerVariables, output);
            }
        }

        /// <summary>
        /// Gets the header values.
        /// </summary>
        /// <value>The header values.</value>
        public NameValueCollection HeaderValues { get; } = new NameValueCollection();
    }
}
