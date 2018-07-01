using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Connector;

namespace Pizzo.Cards
{
    public class DynamicCardTemplates
    {
            /// <summary>
            /// Boiler Plate for the HeroCard in the bot
            /// </summary>
            /// <returns>Attachment</returns>
            public static Attachment getHeroCard(string title, string subtitle, string text, string imageUrl, CardAction button)
            {
                //create a herocard
                var heroCard = new HeroCard
                {
                    //create dynamic fields for the card
                    Title = title,
                    Subtitle = subtitle,
                    Text = text,
                    Images = new List<CardImage> { new CardImage(imageUrl) },
                    Buttons = new List<CardAction> { button }
                };

                //return as an Attachment
                return heroCard.ToAttachment();
            }
        }
    }
