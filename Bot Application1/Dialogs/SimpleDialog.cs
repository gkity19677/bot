using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class SimpleDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            //result 是從simulator傳過來的訊息
            var activity = await result as Activity;
            //context為連接simulator和這個bot的管道 PostAsync則是傳回去的函式
            if (activity.Text.StartsWith("yoman"))
            {
                await context.PostAsync($"yowoman");
            }
            else if (activity.Text.StartsWith("今天空氣好差"))
            {
                await context.PostAsync($"對阿臭空汙");
            }
            else 
                await context.PostAsync($"hihi 早安太陽早安雲朵早安全世界");
            //將管道接到Step2函式下一次則變促發Step2而不是MessageReceivedAsync
            context.Wait(Step2);
        }
        private async Task Step2(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            await context.PostAsync(activity.Text+$" 你好啊 今天天氣挺好的齁");

            context.Wait(Step3);
        }
        private async Task Step3(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            await context.PostAsync("有任何不需要跟我說 我絕對不會理你");

            context.Wait(MessageReceivedAsync);
        }
    }
}