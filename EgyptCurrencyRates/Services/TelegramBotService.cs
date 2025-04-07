namespace EgyptCurrencyRates.Services
{
    using Telegram.Bot;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;

    public class TelegramBotService
    {
        private readonly ITelegramBotClient _botClient;

        public TelegramBotService(IConfiguration configuration)
        {
            var botToken = configuration["TelegramBotToken"];
            _botClient = new TelegramBotClient(botToken);
        }

        // Method to send a message to a group
        public async Task SendMessageToGroupAsync(long chatId, string message)
        {
            //_botClient.SendMessage(chatId, message, ParseMode.Markdown);
            await _botClient.SendTextMessageAsync(chatId, message, parseMode: ParseMode.Markdown);
        }

        // Method to send a message to a channel
        public async Task SendMessageToChannelAsync(string channelUsername, string message)
        {
            await _botClient.SendTextMessageAsync(channelUsername, message, parseMode: ParseMode.Markdown);
        }
    }
}
