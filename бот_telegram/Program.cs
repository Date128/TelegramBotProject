using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Threading.Tasks;
using System;
using System.Threading.Tasks;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Telegram.Bot.Types.ReplyMarkups;

namespace бот_telegram
{
    internal class Program
    {



        static async Task Main(string[] args)
        {

            Console.WriteLine("What the hell");

            var botClient = new TelegramBotClient("6840245251:AAENO9CVEO_nMla8ooAqpHvjWLxdgtJqqIs");
            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(

                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token


                );

            var me = await botClient.GetMeAsync();


            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();




            cts.Cancel();

        }

        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            if (update.Message is not { } message)
                return;

            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;
            Console.WriteLine($"Recived a '{messageText}' message in chat {chatId}");

            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "You said:\n" + message.Text,
                cancellationToken: cancellationToken
                );
        

           

                if (message.Text == "Проверка")
                {
                    await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Проверка: ОК!",
                    cancellationToken: cancellationToken);
                }



                if (message.Text == "Привет")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Приветствую тебя, Путешественник!",
                        cancellationToken: cancellationToken);
                }



                if (message.Text == "Изображение")
                {
                    await botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: InputFile.FromUri("https://www.meme-arsenal.com/memes/a38614de07a2e23e7cf4e84d0c83deff.jpg"),
                        cancellationToken: cancellationToken);
                }

                if (message.Text == "Видео")
                {
                    var random = new Random();
                    var videoUrls = new List<string> {
                        "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                        "https://www.youtube.com/watch?v=3tmd-ClpJxA",
                        "https://www.youtube.com/watch?v=6Dh-RL__uN4",
                        "https://www.youtube.com/watch?v=QH2-TGUlwu4",
                        "https://www.youtube.com/watch?v=ZyhrYis509A",
                        "https://www.youtube.com/watch?v=U06jlgpMtQs",
                        "https://www.youtube.com/watch?v=2Vv-BfVoq4g",
                        "https://www.youtube.com/watch?v=JGwWNGJdvx8",
                        "https://www.youtube.com/watch?v=5-sfG8BV8wU",
                        "https://www.youtube.com/watch?v=1Bix44C1EzY"
                    };
                    var randomIndex = random.Next(videoUrls.Count);
                    var videoUrl = videoUrls[randomIndex];
                    await botClient.SendVideoAsync(
                        chatId: chatId,
                        video: InputFile.FromUri(videoUrl),
                        cancellationToken: cancellationToken);
                }

                if (message.Text == "Варианты")
                {
                    var buttons = new List<InlineKeyboardButton>();
                    buttons.Add(InlineKeyboardButton.WithCallbackData("Видео"));
                    buttons.Add(InlineKeyboardButton.WithCallbackData("Фото"));
                    buttons.Add(InlineKeyboardButton.WithCallbackData("Текст"));

                    var keyboard = new InlineKeyboardMarkup(buttons);

                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Выберите вариант:",
                        replyMarkup: keyboard,
                        cancellationToken: cancellationToken);
                }
        }


        static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram Api error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

       


    }
}


