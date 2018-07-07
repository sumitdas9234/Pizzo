using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdaptiveCards;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Pizzo.Utilities;

namespace Pizzo.Cards
{
    public class AdaptiveCardDialog
    {
        public static Attachment CreateAdaptiveCard(string id, string name, string desc, int price, string photo)
        {
            //creating the layout using single column
            var card = new AdaptiveCard();
            //adding Image to the body
                card.Body.Add(new Image()
                  {
                      Url = photo,
                      Size = ImageSize.Auto,
                      Style = ImageStyle.Normal,
                      AltText = name
                  });
            //adding title to the body
            card.Body.Add(new TextBlock()
            {
                Text = name,
                Weight = TextWeight.Bolder,
                Size = TextSize.Medium,
                Color = TextColor.Dark
            });

            //Adding the ingredients
            card.Body.Add(new TextBlock()
            {
                Text = desc,
                Weight = TextWeight.Normal,
                Color = TextColor.Accent,
                Wrap = true
            });

            //Adding the Price
            card.Body.Add(new TextBlock()
            {
                Text = "Price: "+price,
                Size = TextSize.Small,
                Weight = TextWeight.Bolder,
                Color = TextColor.Dark
            });

            //Adding the Add to cart button
            card.Actions.Add(new SubmitAction()
            {
                Title = "Add to Cart",
                Data = id
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


        //Utility function to create a carousel from an Array of MenuItem.veg or MenuItem.nonveg objects
        public static List<Attachment> CarouselFromArray(List<Pizza> array)
        {
            List<Attachment> Carousel = new List<Attachment>();
            foreach (var item in array) {
                Attachment card = CreateAdaptiveCard(item.id, item.name, item.desc, item.price, item.image);
                Carousel.Add(card);
            }

            return Carousel;

        }

        public static List<Attachment> AddOnCarouselFromArray(List<AddOn> array)
        {
            List<Attachment> Carousel = new List<Attachment>();
            foreach (var item in array)
            {
                Attachment card = CreateAdaptiveCard(item.id, item.name, item.desc, item.price, item.image);
                Carousel.Add(card);
            }

            return Carousel;

        }

    }
}