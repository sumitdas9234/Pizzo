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
using AdaptiveCards;



public class details
    {
public Attachment CreateAdaptiveCardwithEntry()
{
    var card = new AdaptiveCard()
    {
        Body = new List<CardElement>()
    {  
                          
            new TextBlock() { Text = "Please Share your details with us:" },
            //enter your name
            new TextInput()
            {
                Id = "Your Name",
                Placeholder = "Please Enter Your Name",
                Style = TextInputStyle.Text
            },

           //enter address
            new TextBlock() { Text = "Your Address" },
            new NumberInput()
            {
                Id = "Your Address",
                Min = 1,
                Max = 200,
            },

            //enter email id
            new TextBlock() { Text = "Email" },
            new TextInput()
            {
                Id = "Email",
                Placeholder = "Please Enter Your email",
                Style = TextInputStyle.Text
            },

            //enter contact no
            new TextBlock() { Text = "Phone" },
            new TextInput()
            {
                Id = "Phone",
                Placeholder = "Please Enter Your Phone",
                Style = TextInputStyle.Text
            },
    },
        Actions = new List<ActionBase>()
    {
        new SubmitAction()
        {
            Title = "Submit",

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
}