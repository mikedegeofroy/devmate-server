using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace DevMate.Infrastructure.Integration.Telegram.UpdateHandlers;

public class InvoiceHandler : IUpdateHandler
{
    private IUpdateHandler _updateHandler;

    public async void Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (
            update.Type == UpdateType.Message
            && update.Message!.Text != null
            && update.Message.Text.StartsWith("/start")
        )
        {
            Message? message = update.Message;
            if (message.Text.StartsWith("/invoice"))
            {
                await SendInvoiceAsync(botClient, message.Chat.Id, cancellationToken);
            }

            return;
        }

        _updateHandler?.Handle(botClient, update, cancellationToken);
    }


    public IUpdateHandler SetNext(IUpdateHandler handler)
    {
        _updateHandler = handler;
        return _updateHandler;
    }

    private async Task SendInvoiceAsync(ITelegramBotClient client, long chatId, CancellationToken cancellationToken)
    {
        // Example product information
        const string title = "Example Product";
        const string description = "This is an example product description.";
        const string payload = "Unique_Payload_Code"; // Unique payload to identify the transaction
        const string providerToken = "381764678:TEST:80074"; // Token from your payment provider
        const string startParameter = "start_parameter_example"; // Used in deep-linking
        const string currency = "RUB"; // ISO 4217 currency codes
        const int priceAmount = 50000; // Price in smallest units of the currency (e.g., cents for USD)

        // Price breakdown
        LabeledPrice[] prices = { new("Product", priceAmount) };

        await client.SendInvoiceAsync(
            chatId: chatId,
            title: title,
            description: description,
            payload: payload,
            providerToken: providerToken,
            startParameter: startParameter,
            currency: currency,
            prices: prices,
            cancellationToken: cancellationToken
        );
    }
}