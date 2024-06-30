using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using RestSharp;
using Newtonsoft.Json.Linq;
using TelegramFile = Telegram.Bot.Types.File;

namespace CaptchaSolver
{
    public partial class Form1 : Form
    {
        private TelegramBotClient botClient;
        private string botToken;
        private string clientKey = "542ea77985ad594eab3d3a23eeb989a7"; // 2Captcha Client Key

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            botToken = txtBotToken.Text;
            botClient = new TelegramBotClient(botToken);

            var cancellationToken = new System.Threading.CancellationTokenSource().Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // Receive all update types
            };

            botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken);
            MessageBox.Show("Bot started!");
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, System.Threading.CancellationToken cancellationToken)
        {
            if (update.Message?.Type == MessageType.Photo)
            {
                var file = await botClient.GetFileAsync(update.Message.Photo[^1].FileId);
                var filePath = $"https://api.telegram.org/file/bot{botToken}/{file.FilePath}";

                string localFilePath = await DownloadFileAsync(filePath);

                if (!string.IsNullOrEmpty(localFilePath))
                {
                    string captchaResult = await SolveCaptcha(localFilePath);
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"Captcha solved: {captchaResult}");
                }
            }
            else if (update.Message?.Type == MessageType.Text)
            {
                string messageText = update.Message.Text.ToLower();

                if (messageText.Contains("check balance"))
                {
                    int dollarCount = CountDollarsInText(update.Message.Text);
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"Total $ in message: {dollarCount}");
                }
            }
        }

        private int CountDollarsInText(string text)
        {
            int count = 0;
            foreach (char c in text)
            {
                if (c == '$')
                {
                    count++;
                }
            }
            return count;
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, System.Threading.CancellationToken cancellationToken)
        {
            // Handle error here
            MessageBox.Show($"Error occurred: {exception.Message}");
            return Task.CompletedTask;
        }

        private async Task<string> DownloadFileAsync(string url)
        {
            try
            {
                using (var client = new RestClient(url))
                {
                    var request = new RestRequest("", Method.Get);
                    byte[] response = await client.DownloadDataAsync(request);
                    string filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.jpg");

                    System.IO.File.WriteAllBytes(filePath, response); // Use fully qualified name
                    return filePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading file: {ex.Message}");
                return null;
            }
        }

        private async Task<string> SolveCaptcha(string filePath)
        {
            var client = new RestClient("http://2captcha.com/in.php");
            var request = new RestRequest("", Method.Post);
            request.AddParameter("key", clientKey);
            request.AddParameter("method", "post");
            request.AddFile("file", filePath);

            RestResponse response = await client.ExecuteAsync(request);
            var jsonResponse = JObject.Parse(response.Content);
            string requestId = jsonResponse["request"].ToString();

            string result = await GetCaptchaResult(requestId);
            return result;
        }

        private async Task<string> GetCaptchaResult(string requestId)
        {
            var client = new RestClient("http://2captcha.com/res.php");
            var request = new RestRequest("", Method.Get);
            request.AddParameter("key", clientKey);
            request.AddParameter("action", "get");
            request.AddParameter("id", requestId);

            while (true)
            {
                RestResponse response = await client.ExecuteAsync(request);
                var jsonResponse = JObject.Parse(response.Content);

                if (jsonResponse["status"].ToString() == "1")
                {
                    return jsonResponse["request"].ToString();
                }

                await Task.Delay(5000); // Wait for 5 seconds before retrying
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
