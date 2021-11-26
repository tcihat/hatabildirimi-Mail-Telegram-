using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Net.Mail;
using System.Net;
using NotificationProcess.Model;

namespace DenemeHataBildirimi.Model.Helper
{
    
    public class TelegramMessagesHelper
    {
        MailHelper mail1 = new MailHelper();

        private static readonly TelegramBotClient Bot = new TelegramBotClient("1681550639:AAEkFdQg5Nvrg7N23PoS6GhGSL7CksQw_gA");
 
        public  static TlMessageResponse SendTelegramMesaage(string projeAdi, string hataAdi, string hataAciklamasi)
        {
            try
            {
                string message = projeAdi + " - " + hataAdi + " - " + hataAciklamasi;
                sendMessage("-506351473", message);

                return new TlMessageResponse { isSuccess = true, Message = "Başarılı" };
            }
            catch (Exception ex)
            {
                return new TlMessageResponse { isSuccess = false, Message = ex.Message };

            }
        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static void sendMessage(string destID, string text)
        {
           
                var bot = new Telegram.Bot.TelegramBotClient("1681550639:AAEkFdQg5Nvrg7N23PoS6GhGSL7CksQw_gA");
                 bot.SendTextMessageAsync(destID, text);
           
        }
    }
}
