using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace DatabaseReservation.Service
{
    /// <summary>
    /// Email confirmation service outter class
    /// </summary>
    public class EmailOuterService
    {
        public EmailInnerService Inner { get; set; }

        // Constructor to establish link between

        // instance of Outer_class to its

        // instance of the Inner_class

        public EmailOuterService()
        {
            this.Inner = new EmailInnerService(this);
        }
        /// <summary>
        /// Email confirmation service inner class
        /// </summary>
        public class EmailInnerService
        {
            private readonly EmailOuterService obj;
            public EmailInnerService(){}

            public EmailInnerService(EmailOuterService outer)
            {
                this.obj = outer;
            }
            /// <summary>
            /// a method for sending email confirmation on register
            /// </summary>
            /// <param name="emailTo"></param>
            /// <param name="Lname"></param>
            /// <param name="userName"></param>
            /// <param name="password"></param>
            /// <returns></returns>
            public bool SendEmailConfirmation(string emailTo, String Lname, string userName, string password)

            {
               
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("reservationsystem164@gmail.com"));
                email.To.Add(MailboxAddress.Parse(emailTo));
                email.Subject = "Registration Confirmation";
                email.Body = new TextPart(TextFormat.Plain) { Text = "Dear" + Lname + ",\n" + "You have successfully register to the Reservation System. \n Your user name: " + userName + " and password: " + password + "\n Kind Regards\n Reservation Service Team" };
                // send email
                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("reservationsystem164@gmail.com", "qrxu ejmq xtne ayuk");

                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
        }
    }
}
