using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using Pizzo.Utilities;
using Pizzo.Cards;

namespace Pizzo.Dialogs
{
    [Serializable]
    public class CarouselDialog:IDialog<object>
    {
        string choice;
        public CarouselDialog(string option)
        {
            choice = option;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var message = context.MakeMessage();

            //setting the layout of the attachments to carousel type
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            //getting the current menu
            JObject data = Utilities.Utilities.LoadJSON("C:\\Users\\Sumit Das\\source\\repos\\Pizzo\\Pizzo\\Resources\\menu.json");
            MenuItem menu = Utilities.Utilities.MapToPizzaObject(data);

            //getting the users choice
            List<Pizza> userChoice = (choice.ToLower() == "veg") ? menu.veg : menu.nonveg;


            //creating a list of message attachments which will be shown in carousel layout
            message.Attachments = AdaptiveCardDialog.CarouselFromPizzaArray(userChoice);
            //posting the adaptive card carousel to the bot 
            await context.PostAsync(message);

            //function call to handle button click to add a pizza
            context.Wait(AddPizza);
        }
        public async Task AddPizza(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            // Store the value that AddToCart Menu returned. 
            // (At this point, new order dialog has finished and returned some value to use within the root dialog.)
            var resultFromNewOrder = await argument;
            context.PrivateConversationData.SetValue("PizzaItem",resultFromNewOrder.Text);

            await context.PostAsync($"Adding {resultFromNewOrder.Text} to your cart.");

            // Again, wait for the next message from the user.
            context.Done(this);
        }


    }

}