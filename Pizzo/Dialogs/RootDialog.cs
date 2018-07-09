
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
            context.Call(new CarouselDialog(response.ToString()), DisplayAddonPrompt);

        }

        public static Task DisplayAddonPrompt(IDialogContext context, IAwaitable<object> result)
        {
            var response = result.ToString();
            context.Call(new AddonDialog(), OrderCompletedMessage);
            return Task.CompletedTask;
        }

        public static Task OrderCompletedMessage(IDialogContext context, IAwaitable<object> result)
        {
            var response = result.ToString();
            context.PostAsync("Done!");
            return Task.CompletedTask;
        }
    }
}

