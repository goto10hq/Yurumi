using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Sushi2;

namespace Yurumi.Sample
{
    class Program
    {
        static void Main()
        {
            var appSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json")
                .AddUserSecrets("yurumi")
                .AddEnvironmentVariables()
                .Build();

            var connection = new Connections.SmtpConnection
            (
                 appSettings["Server"], 
                 appSettings["Port"].ToInt32().Value, 
                 appSettings["Login"], 
                 appSettings["Password"], 
                 appSettings["EnableSsl"].ToBoolean().Value
            );
            
            var configuration = new Configurations.SendGridConfiguration("Yurumi", true);
            var mailer = new Mailer(connection, configuration);

            var from = appSettings["From"];
            var to = appSettings["To"];

            mailer.SendFromFile
            (
              "Template/index.html",
              new System.Net.Mail.MailAddress(from),
              new System.Net.Mail.MailAddressCollection { to },
              "[TEST] Yurumi | Příliš žluťoučký kůň úpěl ďábelské ódy",
              new Dictionary<string, object>
              {
                { "Salutation", "Hello my lovely robot," },
                { "Yes", "http://www.goto10.cz" },
                { "No", "http://www.github.com" }
              }
            );
        }
    }
}
