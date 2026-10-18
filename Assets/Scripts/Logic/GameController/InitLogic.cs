using DebugLogic;
using UnityEngine.Events;

namespace GameControllerLogic
{
    public static class InitLogic
    {
        public static void InitGame(GameController game)
        {
            var view = game.view;
            var data = game.data;
            data.listeners = InitListeners(data, view);
            InitListeners(data, view);

            view.windowSettings.Execute();
            DEBUG.Subscribe(data.debugData);
            DEBUG.InitDebugData(data, view);

            InitChat(data, view);
        }

        public static void InitChat(GameData data, GameView view)
        {
            view.chat.AddInputListener(data.listeners.ChatSendButtonForScroll);
            view.chat.AddSendButtonListener(data.listeners.ChatSendButton);

            if (view.chat.answerScroll == null)
                ChatLogicGUI.CreateAnswerScroll(view.chat);
        }

        public static Listeners InitListeners(GameData data, GameView view)
        {
            return new Listeners
            {
                ChatSendButtonForScroll = text =>
                    ChatLogicGUI.ListenToAnswer(text, view.chat, data.chat),
                ChatSendButton = () => ChatLogicServer.SendMessageToServer(data, view),
            };
        }
    }

    public struct Listeners
    {
        public UnityAction<string> ChatSendButtonForScroll;
        public UnityAction ChatSendButton;
    }
}
