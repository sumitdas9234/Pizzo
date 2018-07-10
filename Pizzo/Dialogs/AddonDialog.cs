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
                    context.Call(new AddonCarouselDialog(), ResumeAfterOptionDialog);
                    break;
                    //[TODO 2]forward to add more items
                case "No":
                    context.Wait(ResumeAfterOptionDialog);
                    break;
            }
        }

        private Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            //context.PostAsync("Do you want to add more items to your cart?");
            //context.Wait(this.ResumeAfterOptionDialog);

            PromptDialog.Choice(context, this.AddOtherOrder, new List<string>() { "Yes", "No" }, "Do you want to add more items to your cart?");
            return Task.CompletedTask;
        }

        //After users select option, Bot call other dialogs
        private async Task AddOtherOrder(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;
            switch (optionSelected)
            {
                case "Yes":
                    //[TODO 1]context.Call to the show addonCarousel()
                    context.Call(new NewOrderDialog(), ResumeAfterOptionDialog);
                    break;
                //[TODO 2]forward to add more items
                case "No":
                    context.Wait(ResumeAfterOptionDialog);
                    break;
            }
        }

    }



}