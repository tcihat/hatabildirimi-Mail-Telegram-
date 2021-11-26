using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using MailKit.Net.Smtp;
using MimeKit;
using DenemeHataBildirimi.Model;

namespace DenemeHataBildirimi.Model.Helper
{
    public class MailHelper
    {

        public  bool SendMailForError(string subject, string description)
        {
            try
            {
                MailMessage mail = new MailMessage(); //yeni bir mail nesnesi Oluşturuldu.
                mail.IsBodyHtml = false; //mail içeriğinde html etiketleri kullanılsın mı?
                mail.To.Add("notification@ludens.com.tr");

                //mail kimden geliyor, hangi ifade görünsün?
                mail.From = new MailAddress("notification@ludens.com.tr");
                mail.Subject = subject;//mailin konusu

                //mailin içeriği.. Bu alan isteğe göre genişletilip daraltılabilir.
                mail.Body = description;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

                //mailin gönderileceği adres ve şifresi
                smtp.Credentials = new NetworkCredential("notification@ludens.com.tr", "Error123*!");
                smtp.Port = 587;
                smtp.Host = "mail.ludens.com.tr";
                smtp.EnableSsl = false;
                smtp.Send(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
