public static class ChatLogic
{
    public static void MakeStep(GameView view, GameData data)
    {
        var chatView = view.chat;
        var chatData = data.chat;

        if (chatData.lastAnswerLenghts.Count == 0)
            return;
        if (chatData.lettersCount == chatData.lastAnswerLenghts[0])
        {
            chatData.lastAnswerLenghts.Clear();
            return;
        }

        chatData.lastAnswerLenghts.ForEach(_ =>
        {
            ChatLogicGUI.UpdateScrollLength(chatView, chatData);
        });
    }
}
