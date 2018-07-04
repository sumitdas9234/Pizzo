
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Pizzo.Cards;

namespace Pizzo.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(PizzaOptions);
            return Task.CompletedTask;
        }

        public async Task PizzaOptions(IDialogContext context, IAwaitable<object> result)
        {
            var message = context.MakeMessage();

            //setting the layout of the attachments to carousel type
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            //creating a list of message attachments which will be shown in carousel layout
            message.Attachments = new List<Attachment>();

            //creating hero cards showing pizza options
           
            var attachment1 = AdaptiveCardDialog.CreateAdaptiveCard();
            var attachment2 = AdaptiveCardDialog.CreateAdaptiveCard();

            //adding the created Adaptive cards to the list of message attachments 
            message.Attachments.Add(attachment1);
            message.Attachments.Add(attachment2);
            //posting the hero cards carousel to the bot 
            await context.PostAsync(message);

            //function call to handle button click to add a pizza
            context.Wait(AddPizza);

        }

        public async Task AddPizza(IDialogContext context, IAwaitable<object> result)
        {
            throw new NotImplementedException();
        }
    }
}

