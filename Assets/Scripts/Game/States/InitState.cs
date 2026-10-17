using DebugList;
using DebugLogic;

public class InitGameState : BaseGameState
{
    private UnityEngine.Events.UnityAction sendListener;
    private ChatView subscribedChat;

    public override void OnEnter(GameView view, GameData data)
    {
        DebugLogic.DEBUG.InitDebugData(data, view);

        UnsubscribeSendButton();
        subscribedChat = view.chat;
        sendListener = () => ChatLogicServer.SendMessageToServer(data, view);
        subscribedChat.AddSendButtonListener(sendListener);
        if (view.chat.answerScroll == null)
            ChatLogicGUI.CreateAnswerScroll(view.chat);

        // view.debug.windowSettings.Setup(
        //     minWidth: 200,
        //     minHeight: 100,
        //     maxWidth: 1000,
        //     maxHeight: 800
        // );
    }

    public override void OnExit(GameView view, GameData data)
    {
        UnsubscribeSendButton();
    }

    private void UnsubscribeSendButton()
    {
        if (subscribedChat != null && subscribedChat.send != null && sendListener != null)
            subscribedChat.RemoveSendButtonListener(sendListener);
        subscribedChat = null;
        sendListener = null;
    }

    public override void Update(float dt, GameView view, GameData data)
    {
        //throw new System.NotImplementedException();
    }
}
