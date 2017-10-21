using System.Net;
using System.Net.Mail;

namespace Yurumi.Connections
{
    /// <summary>
    /// Smtp connection.
    /// </summary>
    public class SmtpConnection : IConnection<SmtpClient>
    {
        readonly string Server;
        readonly int Port;
        readonly string Login;
        readonly string Password;
        readonly bool EnableSsl;

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>The client.</value>
        public SmtpClient Client 
        {
            get
            {
                var smtp = new SmtpClient(Server, Port)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                if (!string.IsNullOrEmpty(Login) &&
                    !string.IsNullOrEmpty(Password))
                    smtp.Credentials = new NetworkCredential(Login, Password);

                smtp.EnableSsl = EnableSsl;

                return smtp;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Yurumi.Connections.SmtpConnection"/> class.
        /// </summary>
        /// <param name="server">Server.</param>
        /// <param name="port">Port.</param>
        /// <param name="login">Login.</param>
        /// <param name="password">Password.</param>
        /// <param name="enableSsl">If set to <c>true</c> enable Ssl.</param>
        public SmtpConnection(string server, int port = 25, string login = null, string password = null, bool enableSsl = false)
        {
            Server = server;
            Port = port;
            Login = login;
            Password = password;
            EnableSsl = enableSsl;
        }
    }
}
