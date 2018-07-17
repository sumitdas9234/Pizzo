using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using AdaptiveCards;
using Pizzo.Utilities;

namespace Pizzo.Dialogs
{
    [Serializable]
    public class UserDetailsDialog : IDialog<object>
    {
        private Attachment CreateAdaptiveCardwithEntry()
        {
            var card = new AdaptiveCard()
            {
                Body = new List<CardElement>()
                {

                    new TextBlock() { Text = "Please Share your details with us:" },
                    //enter your name
                    new TextInput()
                    {
                        Id = "name",
                        Placeholder = "Please Enter Your Name",
                        Style = TextInputStyle.Text
                    },

                   //enter address
                    new TextBlock() { Text = "Your Address" },
                    new TextInput()
                    {
                        Id = "address",
                        Placeholder = "Please Enter Your Address",
                    },

                    //enter email id
                    new TextBlock() { Text = "Email" },
                    new TextInput()
                    {
                        Id = "email",
                        Placeholder = "Please Enter Your email",
                        Style = TextInputStyle.Text
                    },

                    //enter contact no
                    new TextBlock() { Text = "Phone" },
                    new TextInput()
                    {
                        Id = "phone",
                        Placeholder = "Please Enter Your Phone",
                        Style = TextInputStyle.Text
                    },
                },
                Actions = new List<ActionBase>()
                {
                    new SubmitAction()
                    {
                        Title = "Submit"
                    }
                }
            };
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };
            return attachment;
        }

        public async Task StartAsync(IDialogContext context)
        {
            Attachment card = CreateAdaptiveCardwithEntry();
            var message = context.MakeMessage();
            message.Attachments.Add(card);
            await context.PostAsync(message);
            context.Wait(GetUserDetails);
        }

        private async Task GetUserDetails(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            dynamic reply = await result;
            var data = reply.Value;    
            await context.PostAsync($"Thank You {data["name"]} for ordering with us!");
            context.EndConversation($"Your Order has been successfully placed!");
        }
    }
}