using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Pizzo.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(ShowWelcomeBanner);
            return Task.CompletedTask;
        }

        private async Task ShowWelcomeBanner(IDialogContext context, IAwaitable<object> result)
        {

            //create the static context for card
            string title = "Welcome to Gusto Pizza";
            string subtitle = "Address, Gusto Pizza, Bangalore";
            string text = "Thanks for choosing Gusto Pizza. Help yourself with some tasty pizza!";
            string imageUrl = "https://image.freepik.com/free-vector/flat-design-pizza-background_23-2147640743.jpg";
            CardAction button = new CardAction(ActionTypes.OpenUrl, "Locate US", value: "http://restaurants.pizzahut.co.in/");


            //show the default welcome card
            var message = context.MakeMessage();
            //call the static getHeroCard() method
            var attachment = Card.getHeroCard(title, subtitle, text, imageUrl, button);
            //attach the card
            message.Attachments.Add(attachment);
            //wait for the message
            await context.PostAsync(message);
            //reply back the Banner
            context.Wait(ShowWelcomeBanner);
        }
    }
}