
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
        //calculating total price
        public int Payment(Pizza item)
        {
            int total = 0;
            total = total + item.price;
            return total;
        }

        private async Task MessageReceivedAsync(IDialogContext context,IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            if (message.Value != null)
            {
                dynamic value = message.Value;
                string submitType = value.Type.ToString();
                //calling the function
                   //Payment(MenuItem);
            }
        }

        public enum Choice
        {
            Veg,
            NonVeg
        }

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(PizzaOptions);
            return Task.CompletedTask;
        }

        public async Task PizzaOptions(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            var message = await activity;
            PromptDialog.Choice(
             context: context,
             resume: ChoiceReceivedAsync,
             options: (IEnumerable<Choice>)Enum.GetValues(typeof(Choice)),
             prompt: "Please Select",
             retry: "There was an error . Please try again.",
             promptStyle: PromptStyle.Auto,
             attempts: 1

             );

        }


        public async Task ChoiceReceivedAsync(IDialogContext context, IAwaitable<Choice> activity)
        {
            Choice response = await activity;
            context.Call<object>(new CarouselDialog(response.ToString()), this.DisplayAddonPrompt);

        }

        private async Task DisplayAddonPrompt(IDialogContext context, IAwaitable<object> result)
        {
            var response = await result;
            await context.PostAsync("Enter yes to add toppings");
            context.Call<object>(new AddonDialog(response.ToString()), this.ResumeAfterAddOn);

        }

        private async Task ResumeAfterAddOn(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("resume after add on");
        }
    }
}

