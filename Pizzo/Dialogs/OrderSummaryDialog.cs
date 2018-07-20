﻿using AdaptiveCards;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Pizzo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Pizzo.Dialogs
{
    [Serializable]
    public class OrderSummaryDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            IList<CardElement> OrderList = new List<CardElement>();
            //Display ordered pizza summary
            foreach (Order pz in RootDialog.orders)
            {
                OrderList.Add(
                    new TextBlock() {
                        Text = pz.name,
                        Weight = TextWeight.Normal,
                        Size = TextSize.Normal,
                        Color = TextColor.Good,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Wrap = true
                    });
            
            }

            IList<CardElement> OrderPrice = new List<CardElement>();
            //Display ordered pizza summary
            foreach (Order pz in RootDialog.orders)
            {
                OrderList.Add(
                    new TextBlock()
                    {
                        Text = "" + pz.price,
                        Weight = TextWeight.Normal,
                        Size = TextSize.Normal,
                        Color = TextColor.Good,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Wrap = true
                    });

            }



            IList<CardElement> OrderQuantity = new List<CardElement>();
            //Display ordered pizza summary
            foreach (Order pz in RootDialog.orders)
            {
                OrderList.Add(
                    new TextBlock()
                    {
                        Text = ""+ pz.quantity,
                        Weight = TextWeight.Normal,
                        Size = TextSize.Normal,
                        Color = TextColor.Good,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Wrap = true
                    });

            }


            //Display ordered add on summary
            string addonsummary = " And your add ons are: ";
            foreach (Order addon in RootDialog.addonorders)
                addonsummary += addon.name + ", ";
            
            var card = new AdaptiveCard();
            card.Body.Add(new TextBlock()
            {
                Text = "Order Summary",
                Weight = TextWeight.Bolder,
                Size = TextSize.ExtraLarge,
                Color = TextColor.Dark
            });

            card.Body.Add(new TextBlock()
            {
                Text = "Do you want to checkout your order?",
                Weight = TextWeight.Normal,
                Size = TextSize.Normal,
                Color = TextColor.Attention
            });


            //card.Body.Add(new TextBlock() {
            //        Text = pizzasummary,
            //        Weight = TextWeight.Normal,
            //        Size = TextSize.Small,
            //        Color = TextColor.Light
            //});

            card.Body.Add(new Container() {
                Items = new List<CardElement>()
                {
                    new ColumnSet()
                    {
                        Columns = new List<Column>()
                        {
                            //Item Coloumn
                            new Column(){
                              Items = new List<CardElement>()
                              {
                                  new TextBlock()
                                  {
                                      Text = "Item",
                                      Weight = TextWeight.Bolder,
                                      Size = TextSize.Normal,
                                      Color = TextColor.Good,
                                      HorizontalAlignment = HorizontalAlignment.Center
                                  }

                              }.Concat(OrderList).ToList()
                            },


                            //Quantity Coloumn
                            new Column(){
                               Items = new List<CardElement>()
                              {
                                  new TextBlock()
                                  {
                                      Text = "Quantity",
                                      Weight = TextWeight.Bolder,
                                      Size = TextSize.Normal,
                                      Color = TextColor.Good,
                                      HorizontalAlignment = HorizontalAlignment.Center
                                  }
                              }.Concat(OrderQuantity).ToList()
                            },

                            //Price Coloumn
                            new Column(){
                             Items = new List<CardElement>()
                              {
                                  new TextBlock()
                                  {
                                      Text = "Price",
                                      Weight = TextWeight.Bolder,
                                      Size = TextSize.Normal,
                                      Color = TextColor.Good,
                                      HorizontalAlignment = HorizontalAlignment.Center
                                  }
                              }.Concat(OrderPrice).ToList()
                            }
                        }
                    }
                },
                Separation = SeparationStyle.Strong,
                Style = ContainerStyle.Emphasis
            });

            card.Body.Add(new TextBlock()
            {
                Text = addonsummary,
                Weight = TextWeight.Normal,
                Size = TextSize.Small,
                Color = TextColor.Light
            });

            card.Actions.Add(new SubmitAction()
            {
                Title = "Checkout",
                Data = "Yes"
            });

            card.Actions.Add(new SubmitAction()
            {
                Title = "Cancel",
                Data = "No"
            });

            //Converting the adaptive card into an attachment
            Attachment attachment = new Attachment()
            {
                //set the content to AdaptiveCard
                ContentType = AdaptiveCard.ContentType,
                // Set the content to card
                Content = card
            };

            var message = context.MakeMessage();
            message.Attachments.Add(attachment);
            await context.PostAsync(message);
            context.Wait(ShowOrderSummary);
        }

        private async Task ShowOrderSummary(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var reply = await result;
            if((reply.Text).ToLower() == "yes")
            {
                context.Call(new UserDetailsDialog(), HandleResponse);
            }
        }

        private Task HandleResponse(IDialogContext context, IAwaitable<object> result)
        {
             
             context.EndConversation("Your Order has been sucessfully placed!");
             return Task.CompletedTask;
        }
    }
}