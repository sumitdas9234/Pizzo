using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Pizzo.Utilities;

namespace Pizzo.Dialogs
{
    [Serializable]
    public class NewOrderDialog : IDialog<object>
    {

        public Task StartAsync(IDialogContext context)
        {
            PromptDialog.Choice(context, this.AfterMenuSelection, new List<string>() { "Veg", "NonVeg" }, "Please choose your category?");
            return Task.CompletedTask;
        }


        public virtual async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            context.Wait(MessageRecievedAsync);

        }

        //After users select option, Bot call other dialogs
        private async Task AfterMenuSelection(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;
            switch (optionSelected)
            {
                case "Veg":
                    //[TODO 1]context.Call to the show addonCarousel()
                    context.Call(new CarouselDialog("veg"), ResumeAfterOptionDialog);
                    break;
                //[TODO 2]forward to add more items
                case "NonVeg":
                    context.Call(new CarouselDialog("nonveg"), ResumeAfterOptionDialog);
                    break;
            }
        }

        private Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            Order item = new Order()
            {
                name = context.PrivateConversationData.GetValue<string>("PizzaItem"),
                price = 10,
                quantity = 1
            };
            RootDialog.orders.Add(item);
            context.Call(new AddonDialog(), ResumeAfterOptionDialog);
            return Task.CompletedTask;
        }

    }



}