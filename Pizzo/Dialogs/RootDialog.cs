
using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Pizzo.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(PlaceOrder);
            return Task.CompletedTask;
        }

        public async Task PlaceOrder(IDialogContext context, IAwaitable<object> result)
        {
         
        }
    }
}

