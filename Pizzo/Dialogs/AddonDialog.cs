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
        public async Task StartAsync(IDialogContext context)
        {
            
                var message = context.MakeMessage();
                JObject data = Utilities.Utilities.LoadJSON("C:\\Users\\HP\\Desktop\\sonali\\BotFramework\\PizzoNew\\Pizzo\\Pizzo\\Resources\\addons.json");
                AddOnMenuItem menu = Utilities.Utilities.AddOnMapToObject(data);
                List<AddOn> userChoice = menu.veg;
                message.Attachments = AdaptiveCardDialog.AddOnCarouselFromArray(userChoice);
                //posting the adaptive card carousel to the bot 
                await context.PostAsync(message);

                //function call to handle button click to add a pizza
                context.Wait(AddPizza);
           


        }

        private Task AddPizza(IDialogContext context, IAwaitable<object> result)
        {
            throw new NotImplementedException();
        }
    }
}