using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Bot_Application1
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        /// 
        MongoClient client;
        public MessagesController()
        {

        }


        int i = 0;

        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {

                Activity reply = activity.CreateReply();//activity 為從simulator收到的訊息
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));//connector為bot和simulator中間的橋樑

                if (i == 0)
                {
                    reply.Text = "Hello 你好啊!";
                    i++;
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (i == 1)
                {
                    reply.Text = "你最近過的怎麼樣壓";
                    i++;
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (i == 2)
                {
                    reply.Text = "有機會再一起出來玩";
                    i++;
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }

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
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }

}