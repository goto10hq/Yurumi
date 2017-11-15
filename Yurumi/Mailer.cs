using System;
using Yurumi.Configurations;
using Yurumi.Connections;
using Yurumi.Encoders;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Collections.Generic;
using Yurumi.Tokens;
using System.IO;
using System.Text;
using HtmlAgilityPack;
using System.Linq;
using Sushi2;
using System.Text.RegularExpressions;
using System.Net.Mime;
using Yurumi.Helpers;

namespace Yurumi
{
    /// <summary>
    /// Mailer.
    /// </summary>
    public class Mailer : Mailer<SmtpClient>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Yurumi.Mailer"/> class.
        /// </summary>
        /// <param name="connection">Connection.</param>
        /// <param name="configuration">Configuration.</param>
        public Mailer(IConnection<SmtpClient> connection, IConfiguration configuration) : this(connection, configuration, new Utf8Encoder())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Yurumi.Mailer"/> class.
        /// </summary>
        /// <param name="connection">Connection.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="encoder">Encoder.</param>
        public Mailer(IConnection<SmtpClient> connection, IConfiguration configuration, IEncoder encoder) : base(connection, configuration, encoder)
        {
        }
    }

    /// <summary>
    /// Mailer.
    /// </summary>
    public class Mailer<T> where T : class
    {
        readonly IConnection<T> _connection;
        readonly IConfiguration _configuration;
        readonly IEncoder _encoder;
        readonly T _client;

        Lazy<IMemoryCache> _templatesCacheLazy = new Lazy<IMemoryCache>(() => new MemoryCache(new MemoryCacheOptions()));
        IMemoryCache _templatesCache => _templatesCacheLazy.Value;

        static object _lock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Yurumi.Mailer"/> class.
        /// </summary>
        /// <param name="connection">Connection.</param>
        /// <param name="configuration">Configuration.</param>
        public Mailer(IConnection<T> connection, IConfiguration configuration) : this(connection, configuration, new Utf8Encoder())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Yurumi.Mailer"/> class.
        /// </summary>
        /// <param name="connection">Connection.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="encoder">Encoder.</param>
        public Mailer(IConnection<T> connection, IConfiguration configuration, IEncoder encoder)
        {
            _connection = connection;
            _configuration = configuration;
            _encoder = encoder;
            _client = connection.Client;
        }

        /// <summary>
        /// Sends the e-mail from the file.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="fullPath">Full path to template.</param>
        /// <param name="from">From.</param>
        /// <param name="tos">Tos.</param>
        /// <param name="subject">Subject.</param>
        /// <param name="replacements">Replacements.</param>
        /// <param name="replyTos">Reply tos.</param>
        /// <param name="ccs">Ccs.</param>
        /// <param name="attachments">Attachments.</param>
        public void SendFromFile(string fullPath, MailAddress from, MailAddressCollection tos, string subject, Dictionary<string, object> replacements = null, MailAddressCollection replyTos = null,
                                    MailAddressCollection ccs = null, IEnumerable<Attachment> attachments = null)
        {
            if (fullPath == null)
                throw new ArgumentNullException(nameof(fullPath));

            if (@from == null)
                throw new ArgumentNullException(nameof(@from));

            if (tos == null)
                throw new ArgumentNullException(nameof(tos));

            if (subject == null)
                throw new ArgumentNullException(nameof(subject));

            if (!Regex.IsMatch(@from.Address, RegexPatterns.Email))
                throw new Exception($"From address {@from.Address} is not valid e-mail.");

            AsyncTools.RunSync(() => SendFromFileAsync(fullPath, from, tos, subject, replacements, replyTos, ccs, attachments));
        }

        /// <summary>
        /// Sends the e-mail from the file.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="fullPath">Full path to template.</param>
        /// <param name="from">From.</param>
        /// <param name="tos">Tos.</param>
        /// <param name="subject">Subject.</param>
        /// <param name="replacements">Replacements.</param>
        /// <param name="replyTos">Reply tos.</param>
        /// <param name="ccs">Ccs.</param>
        /// <param name="attachments">Attachments.</param>
        public async Task SendFromFileAsync(string fullPath, MailAddress from, MailAddressCollection tos, string subject, Dictionary<string, object> replacements = null, MailAddressCollection replyTos = null,
                                    MailAddressCollection ccs = null, IEnumerable<Attachment> attachments = null)
        {
            if (fullPath == null)
                throw new ArgumentNullException(nameof(fullPath));

            if (@from == null)
                throw new ArgumentNullException(nameof(@from));

            if (tos == null)
                throw new ArgumentNullException(nameof(tos));

            if (subject == null)
                throw new ArgumentNullException(nameof(subject));

            if (!Regex.IsMatch(@from.Address, RegexPatterns.Email))
                throw new Exception($"From address {@from.Address} is not valid e-mail.");

            var template = GetTemplate(fullPath);
            var content = template.Content;

            var mail = new MailMessage
            {
                From = @from,
                Subject = _encoder.Encode(subject),
                SubjectEncoding = _encoder.Encoding
            };

            foreach (var to in tos)
            {
                if (!Regex.IsMatch(to.Address, RegexPatterns.Email))
                    throw new Exception($"To address {to.Address} is not valid e-mail.");

                mail.To.Add(to);
            }

            if (replyTos != null &&
                replyTos.Any())
            {
                foreach (var rto in replyTos)
                {
                    if (!Regex.IsMatch(rto.Address, RegexPatterns.Email))
                        throw new Exception($"Reply to address {rto.Address} is not valid e-mail.");

                    mail.ReplyToList.Add(rto);
                }
            }

            if (ccs != null &&
                ccs.Any())
            {
                foreach (var cc in ccs)
                {
                    if (!Regex.IsMatch(cc.Address, RegexPatterns.Email))
                        throw new Exception($"CC address {cc.Address} is not valid e-mail.");

                    mail.CC.Add(cc);
                }
            }

            if (replacements != null &&
                replacements.Count > 0)
            {
                content = replacements.Aggregate(content, (current, r) => current.Replace("{" + r.Key + "}", r.Value.ToString()));
            }

            var htmlView = AlternateView.CreateAlternateViewFromString(_encoder.Encode(content), _encoder.Encoding, "text/html");
            htmlView.TransferEncoding = TransferEncoding.QuotedPrintable;

            if (template.Images != null &&
                template.Images.Any())
            {
                foreach (var it in template.Images)
                {
                    var lr = new LinkedResource(it.OriginalNameWithFullPath, it.OriginalName.ToMimeType()) { ContentId = it.ImageNameInHtml };
                    htmlView.LinkedResources.Add(lr);
                }
            }

            mail.AlternateViews.Add(htmlView);

            mail.Body = content.ToPlainText();
            mail.IsBodyHtml = false;

            if (attachments != null)
            {
                var attachmentList = attachments as IList<Attachment> ?? attachments.ToList();

                foreach (var attachment in attachmentList)
                {
                    mail.Attachments.Add(attachment);
                }
            }

            if (_configuration != null)
            {
                mail.Headers.Add(_configuration.HeaderValues);
            }

            var smtpClient = _client as SmtpClient;

            if (smtpClient != null)
                await smtpClient.SendMailAsync(mail);
        }

        EmailTemplate GetTemplate(string fullPath)
        {
            var normalizedFullPath = fullPath.ToNormalizedString();

            lock (_lock)
            {
                var template = _templatesCache.Get<EmailTemplate>(normalizedFullPath);

                if (template == null)
                {
                    template = GetTemplateFromFile(fullPath);
                    _templatesCache.Set(normalizedFullPath, template);
                }

                return template;
            }
        }

        EmailTemplate GetTemplateFromFile(string fullPath)
        {
            string template;

            using (var sr = new StreamReader(fullPath, Encoding.UTF8))
            {
                template = sr.ReadToEnd();
            }

            template = template.ToReplacedString("charset=utf-8", "charset=" + _encoder.Charset, StringComparison.OrdinalIgnoreCase);

            var html = new HtmlDocument();
            html.LoadHtml(template);

            var allImages = new List<EmailImage>();

            var images = html.DocumentNode.SelectNodes("//img");

            if (images != null)
            {
                foreach (var i in images)
                {
                    var src = i.Attributes["src"].Value;

                    // don't add duplicates
                    if (allImages.Any(ix => ix.OriginalNameWithPath.Equals(src, StringComparison.OrdinalIgnoreCase)))
                        continue;

                    var c = allImages.Count + 1;

                    var it = new EmailImage(Path.Combine(Path.GetDirectoryName(fullPath), src), src, src.Substring(src.LastIndexOf("/", StringComparison.Ordinal) + 1), $"{c}@YRM");

                    allImages.Add(it);
                }

                template = allImages.Aggregate(template, (current, it) => current.Replace(it.OriginalNameWithPath, $"cid:{it.ImageNameInHtml}"));
            }

            return new EmailTemplate(template, allImages);
        }
    }
}