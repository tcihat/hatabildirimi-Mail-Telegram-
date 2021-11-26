using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using DenemeHataBildirimi.Model;
using DenemeHataBildirimi.Model.Helper;
using Microsoft.AspNetCore.Authorization;

namespace DenemeHataBildirimi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        MailHelper mailHelper = new MailHelper();

        [HttpGet("notify")]
        [Authorize]
        public IActionResult errorNotification(string projeAdi, string hataAdi, string hataAciklamasi)
        {
            mailHelper.SendMailForError(projeAdi, hataAdi + " => " + hataAciklamasi);
            var response = TelegramMessagesHelper.SendTelegramMesaage(projeAdi, hataAdi, hataAciklamasi);
            if (response.isSuccess)
            {
                return Ok(response.Message);
            }
            else
            {
                mailHelper.SendMailForError("Telegram Hatası --> ", "Telegrama mesaj gönderilemedi");
                return Ok(response.Message);
            }
        }


    }

}

