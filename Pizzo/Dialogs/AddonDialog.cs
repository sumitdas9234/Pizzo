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
    public class AddonDialog : IDialog<object>

    {

        string choice;
        public AddonDialog(string option)
        {
            choice = option;
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.PostAsync("Ready to add some toppings?");
            
            var message = context.MakeMessage();
            List<AddOn> userChoice;
            JObject data = Utilities.Utilities.LoadJSON("C:\\Users\\HP\\Desktop\\sonali\\BotFramework\\PizzoNew\\Pizzo\\Pizzo\\Resources\\addons.json");
            AddOnMenuItem menu = Utilities.Utilities.AddOnMapToObject(data);
            if (choice.ToLower() == "yes")
            {
                userChoice = menu.veg;

                message.Attachments = AdaptiveCardDialog.AddOnCarouselFromArray(userChoice);
            }
            //posting the adaptive card carousel to the bot 
            await context.PostAsync(message);
            context.Done(this);
        }

        
    }
}
    