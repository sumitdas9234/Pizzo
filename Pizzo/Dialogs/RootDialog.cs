
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;
using Pizzo.Cards;
using Pizzo.Utilities;

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

            //getting the current menu
            JObject data = Utilities.Utilities.LoadJSON("c:\\Users\\Sumit Das\\source\\repos\\Pizzo\\Pizzo\\Resources\\menu.json");
            MenuItem menu = Utilities.Utilities.MapToObject(data);

            //creating a list of message attachments which will be shown in carousel layout
            message.Attachments =  AdaptiveCardDialog.CarouselFromArray(menu.veg);      
            //posting the adaptive card carousel to the bot 
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

