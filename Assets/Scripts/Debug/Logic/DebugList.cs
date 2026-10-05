using System.Collections.Generic;
using UnityEngine;

namespace DebugList
{
    public static class DebugCommands
    {
        public static List<object> commandList;
        public static DebugCommand LOG_HELLO;

        public static void InitDebugList(GameData data, GameView view)
        {
            LOG_HELLO = new(
                "hello",
                "debugs hello",
                "debug",
                () =>
                {
                    Debug.Log("HELLO");
                }
            );

            commandList = new() { LOG_HELLO };
        }
    }
}
