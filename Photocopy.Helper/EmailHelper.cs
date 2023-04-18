using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using Photocopy.Core.Interface.Helper;

namespace Photocopy.Helper
{
    public  class EmailHelper:IEmailHelper
    {
        public  void SendEmail(ContactDto contact)
        {
            MailMessage msg = new MailMessage(); //Mesaj gövdesini tanımlıyoruz...
            msg.Subject = "İletişim Formu - Fotokopi";
            msg.From = new MailAddress(contact.Email, contact.Fullname);
            msg.To.Add(new MailAddress("info@fotokopi.com", "Fotokopi İletişim"));

            //Mesaj içeriğinde HTML karakterler yer alıyor ise aşağıdaki alan TRUE olarak gönderilmeli ki HTML olarak yorumlansın. Yoksa düz yazı olarak gönderilir...
            msg.IsBodyHtml = true;
            msg.Body = contact.Message;

            //Mesaj önceliği (BELİRTMEK ZORUNLU DEĞİL!)
            //msg.Priority = MailPriority.High;

            //SMTP/Gönderici bilgilerinin yer aldığı erişim/doğrulama bilgileri
            SmtpClient smtp = new SmtpClient("smtp.siteadi.com", 587); //Bu alanda gönderim yapacak hizmetin smtp adresini ve size verilen portu girmelisiniz.
            NetworkCredential AccountInfo = new NetworkCredential("info@fotokopi.com", "");
            smtp.UseDefaultCredentials = false; //Standart doğrulama kullanılsın mı? -> Yalnızca gönderici özellikle istiyor ise TRUE işaretlenir.
            smtp.Credentials = AccountInfo;
            smtp.EnableSsl = false; //SSL kullanılarak mı gönderilsin...

            try
            {
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                // ?
            }

        }

    }
}
