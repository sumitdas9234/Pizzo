
using System;
using System.Collections.Generic;
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
            context.Wait(PizzaOptions);
            return Task.CompletedTask;
        }

        public async Task PizzaOptions(IDialogContext context, IAwaitable<object> result)
        {
            var message = context.MakeMessage();

            //setting the layout of the attachments to carousel type
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            //creating a list of message attachments which will be shown in carousel layout
            message.Attachments = new List<Attachment>();

            //creating hero cards showing pizza options
            var attachment1 = Cards.DynamicCardTemplates.getHeroCard("Cheese Margherita", "", "", "https://www.google.co.in/url?sa=i&rct=j&q=&esrc=s&source=images&cd=&ved=2ahUKEwiU1sPwovzbAhUKWX0KHTG2DgMQjRx6BAgBEAU&url=https%3A%2F%2Fwww.dominos.co.in%2Fmenu%2Fveg-pizzas%2Fdouble-cheese-margherita&psig=AOvVaw1ajDTwwvfZVlDt_Dy6fP5E&ust=1530478499680860", new CardAction(ActionTypes.ImBack, "Add", value: "Adding Cheese Margherita"));
            var attachment2 = Cards.DynamicCardTemplates.getHeroCard("Farmhouse", "", "", "https://www.google.co.in/imgres?imgurl=https%3A%2F%2Fwww.dominos.co.in%2F%2Ffiles%2Fitems%2FFarmhouse.jpg&imgrefurl=https%3A%2F%2Fwww.dominos.co.in%2Fmenu%2Fveg-pizzas%2Ffarm-house&docid=ehA3DSH7XRYw8M&tbnid=6jjqSvEv1VMlDM%3A&vet=10ahUKEwiix7TTo_zbAhVGfH0KHV19DecQMwh6KAAwAA..i&w=267&h=265&bih=635&biw=1366&q=farmhouse%20pizza&ved=0ahUKEwiix7TTo_zbAhVGfH0KHV19DecQMwh6KAAwAA&iact=mrc&uact=8", new CardAction(ActionTypes.ImBack, "Add", value: "Adding Farmhouse"));

            //adding the created hero cards to the list of message attachments 
            message.Attachments.Add(attachment1);
            message.Attachments.Add(attachment2);
            //posting the hero cards carousel to the bot 
            await context.PostAsync(message);

            //function call to handle button click to add a pizza
            context.Wait(AddPizza);

        }

        public async Task AddPizza(IDialogContext context, IAwaitable<object> result)
        {
            throw new NotImplementedException();
        }
    }
}

