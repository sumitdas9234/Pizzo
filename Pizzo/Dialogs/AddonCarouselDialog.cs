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
    public class AddonCarouselDialog : IDialog<object>
    {

        public async Task StartAsync(IDialogContext context)
        {
            var message = context.MakeMessage();

            //setting the layout of the attachments to carousel type
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            //getting the current menu
            JObject data = Utilities.Utilities.LoadJSON("C:\\Users\\Sumit Das\\source\\repos\\Pizzo\\Pizzo\\Resources\\addons.json");
            AddonItems menu = Utilities.Utilities.MapToAddonObject(data);
            //creating a list of message attachments which will be shown in carousel layout
            message.Attachments = AdaptiveCardDialog.CarouselFromAddonArray(menu.addons);
            //posting the adaptive card carousel to the bot 
            await context.PostAsync(message);

            //function call to handle button click to add a pizza
            context.Wait(AddAddOns);
        }
        public async Task AddAddOns(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            // Store the value that AddToCart Menu returned. 
            // (At this point, new order dialog has finished and returned some value to use within the root dialog.)
            var resultFromNewOrder = await argument;
            context.PrivateConversationData.SetValue("AddOnItem", resultFromNewOrder.Text);

            await context.PostAsync($"Adding {resultFromNewOrder.Text} to your pizza.");

            // Again, wait for the next message from the user.
            context.Done(this);
        }


    }

}