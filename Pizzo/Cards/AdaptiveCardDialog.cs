using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdaptiveCards;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Pizzo.Cards
{
    public class AdaptiveCardDialog
    {
        public static Attachment CreateAdaptiveCard()
        {
            //creating the layout using single column
            var card = new AdaptiveCard();
            //adding Image to the body
                card.Body.Add(new Image()
                  {
                      Url = "https://image.freepik.com/free-vector/flat-design-pizza-background_23-2147640743.jpg",
                      Size = ImageSize.Auto,
                      Style = ImageStyle.Normal,
                      AltText = " Pizza Name"
                  });
            //adding title to the body
            card.Body.Add(new TextBlock()
            {
                Text = "Pizza Name",
                Weight = TextWeight.Bolder,
                Size = TextSize.Medium,
                Color = TextColor.Dark
            });

            //Adding the ingredients
            card.Body.Add(new TextBlock()
            {
                Text = "with some awesome flavours and tasty spices sprinkled on the top.",
                Weight = TextWeight.Normal,
                Color = TextColor.Accent,
                Wrap = true
            });

            //Adding the Price
            card.Body.Add(new TextBlock()
            {
                Text = "Price: $29",
                Size = TextSize.Small,
                Weight = TextWeight.Bolder,
                Color = TextColor.Dark
            });

            //Adding the Add to cart button
            card.Actions.Add(new SubmitAction()
            {
                Title = "Add to Cart",
                Data = "Pizza Name"
            });

            //Converting the adaptive card into an attachment
            Attachment attachment = new Attachment()
            {
                //set the content to AdaptiveCard
                ContentType = AdaptiveCard.ContentType,
                // Set the content to card
                Content = card
            };
            return attachment;
        }

    }
}