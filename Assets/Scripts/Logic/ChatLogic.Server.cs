using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class ChatLogicServer
{
    public static async void SendMessageToServer(GameData data, GameView view)
    {
        ChatView chatView = view.chat;

        if (!chatView.send.interactable || string.IsNullOrWhiteSpace(chatView.input.text))
            return;

        chatView.send.interactable = false;
        chatView.input.interactable = false;

        chatView.text.text = "waiting..";

        try
        {
            string reply = await GetReplyAsync(chatView.input.text, data.Client);
            if (chatView.text != null)
                chatView.text.text = reply;
        }
        catch (Exception exception)
        {
            if (chatView.text != null)
                chatView.text.text = "Не удалось получить ответ. Подробности в Console.";
            Debug.LogException(exception);
        }
        finally
        {
            if (chatView.send != null)
                chatView.send.interactable = true;
            chatView.input.text = "";
            chatView.input.interactable = true;
        }
    }

    public static async Task<string> GetReplyAsync(string message, HttpClient Client)
    {
        var data = new ChatRequest { message = message };
        string json = JsonUtility.ToJson(data);

        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var response = await Client.PostAsync("http://localhost:5251/chat", content);
        string responseJson = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Сервер /chat вернул {(int)response.StatusCode}: {responseJson}"
            );
        }

        var result = JsonUtility.FromJson<ChatResponse>(responseJson);

        if (result == null || result.reply == null)
        {
            throw new InvalidOperationException("No reply field.");
        }

        return result.reply;
    }

    [Serializable]
    private class ChatRequest
    {
        public string message;
    }

    [Serializable]
    private class ChatResponse
    {
        public string reply;
    }
}
