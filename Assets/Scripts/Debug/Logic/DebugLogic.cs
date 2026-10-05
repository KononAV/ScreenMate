using System.Collections.Generic;
using System.Linq;
using DebugList;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DebugLogic
{
    static class DEBUG
    {
        public static void InitDebugData(GameData data, GameView view)
        {
            DebugCommands.InitDebugList(data, view);
        }

        public static void Update(DebugData data)
        {
            if (data.showConsole && Keyboard.current.enterKey.wasPressedThisFrame)
            {
                OnReturn(data);
            }
            if (
                // Keyboard.current.altKey.wasPressedThisFrame
                // && Keyboard.current.shiftKey.wasPressedThisFrame
                // && Keyboard.current.cKey.wasPressedThisFrame
                Keyboard.current.zKey.wasPressedThisFrame
                && Keyboard.current.xKey.wasPressedThisFrame
            )
            {
                data.showConsole = !data.showConsole;
            }
        }

        public static void OnGUI(DebugData data)
        {
            if (!data.showConsole)
            {
                return;
            }
            float y = 100f;
            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            data.input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), data.input);
        }

        public static void HandleLog(
            string logString,
            string stackTrace,
            LogType type,
            DebugData data
        )
        {
            data.history += "\n" + logString;
        }

        public static void Subscribe(DebugData data)
        {
            Application.logMessageReceived += (logString, stackTrace, type) =>
                HandleLog(logString, stackTrace, type, data);
        }

        public static void UnSubscribe(DebugData data)
        {
            Application.logMessageReceived -= (logString, stackTrace, type) =>
                HandleLog(logString, stackTrace, type, data);
        }

        public static void HandleInput(DebugData data)
        {
            List<object> commands = DebugCommands.commandList;
            for (int i = 0; i < commands?.Count; i++)
            {
                DebugCommandBase commandBase = commands[i] as DebugCommandBase;
                if (data.input.Contains(commandBase.commandId))
                {
                    DebugCommand command = commands[i] as DebugCommand;
                    if (command != null)
                    {
                        command.Invoke();
                    }
                }
            }
        }

        public static void OnReturn(DebugData data)
        {
            HandleInput(data);

            data.history += "\n" + data.input;
            data.input = "";
        }

        public static void UpdateHistory(DebugData data)
        {
            if (!data.showConsole || data.history == null)
                return;

            string[] lines = data.history.Split('\n');

            const int lineHeight = 20;
            const int scrollHeight = 90;

            int contentHeight = lines.Length * lineHeight;

            Rect viewport = new Rect(0, 0, Screen.width - 30, contentHeight);

            // История изменилась — прокручиваем вниз
            if (lines.Length != data.lastLineCount)
            {
                data.lastLineCount = lines.Length;

                if (contentHeight > scrollHeight)
                {
                    data.scroll.y = contentHeight - scrollHeight;
                }
            }

            data.scroll = GUI.BeginScrollView(
                new Rect(0, 5f, Screen.width, scrollHeight),
                data.scroll,
                viewport
            );

            for (int i = 0; i < lines.Length; i++)
            {
                Rect labelRect = new Rect(5, lineHeight * i, viewport.width - 10, lineHeight);

                GUI.Label(labelRect, lines[i]);
            }

            GUI.EndScrollView();
        }
    }
}
