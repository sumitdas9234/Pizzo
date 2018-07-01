using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Linq;

namespace Pizzo
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            Dialogs.RootDialog rd = new Dialogs.RootDialog();

            if (activity.GetActivityType() == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            string messageType = message.GetActivityType();
            if (messageType == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (messageType == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels

                //create the static context for card
                string title = "Welcome to Gusto Pizza";
                string subtitle = "Address, Gusto Pizza, Bangalore";
                string text = "Thanks for choosing Gusto Pizza. Help yourself with some tasty pizza!";
                string imageUrl = "https://image.freepik.com/free-vector/flat-design-pizza-background_23-2147640743.jpg";
                CardAction button = new CardAction(ActionTypes.OpenUrl, "Locate US", value: "http://restaurants.pizzahut.co.in/");

                IConversationUpdateActivity update = message;
                var client = new ConnectorClient(new Uri(message.ServiceUrl));
                if (update.MembersAdded != null && update.MembersAdded.Any())
                {
                    foreach (var newMember in update.MembersAdded)
                    {
                        if (newMember.Id != message.Recipient.Id)
                        {
                            var reply = message.CreateReply();
                            var attachment = Cards.DynamicCardTemplates.getHeroCard(title, subtitle, text, imageUrl, button);
                            reply.Attachments.Add(attachment);
                            reply.Text = "Welcome pizza lover!" +
                                " In mood for a tasty pizza? Type \"Hi\" to continue";


                            client.Conversations.ReplyToActivityAsync(reply);
                        }
                    }
                }
            }
            else if (messageType == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (messageType == ActivityTypes.Typing)
            {
                // Handle knowing that the user is typing
            }
            else if (messageType == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}