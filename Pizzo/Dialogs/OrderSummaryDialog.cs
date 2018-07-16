using AdaptiveCards;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Pizzo.Dialogs
{
    [Serializable]
    public class OrderSummaryDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var card = new AdaptiveCard();
            card.Body.Add(new TextBlock()
            {
                Text = "Order Summary",
                Weight = TextWeight.Bolder,
                Size = TextSize.ExtraLarge,
                Color = TextColor.Dark
            });

            card.Body.Add(new TextBlock()
            {
                Text = "Do you want to checkout your order?",
                Weight = TextWeight.Normal,
                Size = TextSize.Normal,
                Color = TextColor.Attention
            });
            card.Actions.Add(new SubmitAction()
            {
                Title = "Checkout",
                Data = "Yes"
            });

            card.Actions.Add(new SubmitAction()
            {
                Title = "Cancel",
                Data = "No"
            });

            //Converting the adaptive card into an attachment
            Attachment attachment = new Attachment()
            {
                //set the content to AdaptiveCard
                ContentType = AdaptiveCard.ContentType,
                // Set the content to card
                Content = card
            };

            var message = context.MakeMessage();
            message.Attachments.Add(attachment);
            await context.PostAsync(message);
            context.Wait(ShowOrderSummary);
        }

        private async Task ShowOrderSummary(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var reply = await result;
            if((reply.Text).ToLower() == "yes")
            {
                context.Call(new UserDetailsDialog(), HandleResponse);
            }
        }

        private Task HandleResponse(IDialogContext context, IAwaitable<object> result)
        {
             context.EndConversation("Your Order has been sucessfully placed!");
            return Task.CompletedTask;
        }
    }
}