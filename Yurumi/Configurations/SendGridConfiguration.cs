using System.Collections.Specialized;
using Newtonsoft.Json;
using Yurumi.Helpers;

namespace Yurumi.Configurations
{
    /// <summary>
    /// SendGrid configuration.
    /// </summary>
    /// <remarks>https://sendgrid.com/</remarks>
    public class SendGridConfiguration: IConfiguration
    {
        const string _header = "X-SMTPAPI";

        class Filters
        {
            [JsonProperty("clicktrack")]
            public ClickTrack ClickTrack { get; set; }

            public Filters(ClickTrack clickTrack)
            {
                ClickTrack = clickTrack;
            }
        }

        class Settings
        {
            [JsonProperty("enable")]
            public bool Enable { get; set; }

            public Settings(bool enable)
            {
                Enable = enable;
            }
        }

        class ClickTrack
        {
            [JsonProperty("settings")]
            public Settings Settings { get; set; }

            public ClickTrack(Settings settings)
            {
                Settings = settings;
            }
        }

        class Token
        {
            [JsonProperty("category")]
            public string Category { get; set; }

            [JsonProperty("filters")]
            public Filters Filters { get; set; }

            public Token(string category, Filters filters)
            {
                Category = category;
                Filters = filters;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Yurumi.Configurations.SendGridConfiguration"/> class.
        /// </summary>
        /// <param name="category">Category.</param>
        /// <param name="enableClickTracking">If set to <c>true</c> enable click tracking.</param>
        public SendGridConfiguration(string category, bool enableClickTracking)
        {
            var token = new Token(category, new Filters(new ClickTrack(new Settings(enableClickTracking))));
            var output = JsonConvert.SerializeObject(token, new BoolConverter());
            HeaderValues.Add(_header, output);
        }

        /// <summary>
        /// Gets the header values.
        /// </summary>
        /// <value>The header values.</value>
        public NameValueCollection HeaderValues { get; } = new NameValueCollection();
    }
}
