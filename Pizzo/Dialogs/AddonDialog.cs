using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Pizzo.Dialogs
{
    [Serializable]
    public class AddonDialog : IDialog<object>
    {

        public Task StartAsync(IDialogContext context)
        {
            PromptDialog.Choice(context, this.AfterMenuSelection, new List<string>() { "Yes", "No" }, "Would you like to get Addons?");
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
                case "Yes":
                    //[TODO 1]context.Call to the show addonCarousel()
                    await context.PostAsync("Yes");
                    break;
                    //[TODO 2]forward to add more items
                case "No":
                    await context.PostAsync("No");
                    break;
            }

            //remove when TODO 1 and TODO 2 are done

            context.Wait(MessageRecievedAsync);
        }
    }



}