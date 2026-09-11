using UnityEditor;
using a = UnityEngine;

namespace KononAV.FileByTemplate.Editor
{
    [InitializeOnLoad]
    public static class AutoStart
    {
        static AutoStart()
        {
            RoslynAnalizer.Main();
        }
    }
}
