using UnityEditor;
using UnityEngine;

namespace ToolSuite.AssetTools.ReferencesTool
{
    public class ReferencesToolWindow : EditorWindow
    {
        public const string MENU_NAME = "ReferencesTool"; 

        [MenuItem(ToolSuiteMenuName.MENU_NAME + "/" + AssetToolsMenuName.MENU_NAME + "/" + MENU_NAME)]
        private static void ShowWindow()
        {
            var window = GetWindow<ReferencesToolWindow>();
            window.titleContent = new GUIContent(MENU_NAME);
            window.Show();
        }
    }
}