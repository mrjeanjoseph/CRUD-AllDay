using System.Net;
using System.Net.Mail;
using System.Text;

namespace SportsStore.Domain
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@deepvuecrud.com";
        public string MailFromAddress = "orders@deepvuecrud.com";
        public bool UseSsl = true;
        public string Username = "dvc-username";
        public string Password = "dvc-password";
        public string ServerName = "smtp.dvc-server.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\dvcdirols";
    }

    public class EmailOrderProcessor : IOrderProceessor
    {
        private readonly EmailSettings _emailSettings;

        public EmailOrderProcessor(EmailSettings settingParams)
        {
            _emailSettings = settingParams;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = _emailSettings.UseSsl;
                smtpClient.Host = _emailSettings.ServerName;
                smtpClient.Port = _emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(
                    _emailSettings.Username,
                    _emailSettings.Password
                    );

                if (_emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = _emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("------------------------------")
                    .AppendLine("Items: ");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Merchandise.Price * line.Quantity;
                    body.AppendFormat("{0} * {1} (subtotal: {2:c})", line.Quantity,subtotal);
                }

                body.AppendFormat("Total order value: {o:c}", cart.ComputeTotalValue()).AppendLine("------------------------------")
                    .AppendLine("Ship To: ")
                    .AppendLine(shippingInfo.FullLegalName)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.Line3 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.State)
                    .AppendLine(shippingInfo.ZipCode ?? "")
                    .AppendLine("------------------------------")
                    .AppendFormat("Gift Wrap: {0}", shippingInfo.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                        _emailSettings.MailFromAddress,
                        _emailSettings.MailToAddress,
                        "New Order has been submitted",
                        body.ToString());

                if (_emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
