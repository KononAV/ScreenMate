using System.Diagnostics;
using UnityEditor;
using a = UnityEngine;

namespace KononAV.FileByTemplate.Editor
{
    [InitializeOnLoad]
    public static class AutoStart
    {
        static AutoStart()
        {
            a.Debug.Log("PACKAGE START");
            RoslynAnalizer.Main();
        }
    }
}
