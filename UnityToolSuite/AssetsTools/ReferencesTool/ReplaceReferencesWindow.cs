using UnityEditor;
using UnityEngine;

namespace ToolSuite.AssetTools.ReferencesTools
{
    public class ReplaceReferencesWindow : EditorWindow
    {
        public const string MENU_NAME = "ReplaceReferences"; 

        private Object originalAsset = null;
        private Object replaceAsset = null;
        private Vector2 scrollPosition = Vector2.zero;

        [MenuItem(ToolSuiteMenuName.MENU_NAME + "/" + AssetToolsMenuName.MENU_NAME + "/" + MENU_NAME)]
        private static void ShowWindow()
        {
            var window = GetWindow<ReplaceReferencesWindow>();
            window.titleContent = new GUIContent(MENU_NAME);
            window.Show();
        }

        private void OnGUI()
        {
            if (EditorSettings.serializationMode != SerializationMode.ForceText)
            {
                return;
            }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            originalAsset = EditorGUILayout.ObjectField(new GUIContent("Original Asset: "), originalAsset, typeof(Object), false);

            replaceAsset = EditorGUILayout.ObjectField(new GUIContent("Replace references with: "), replaceAsset, typeof(Object), false);

            GUILayout.Button("Replace references");

            EditorGUILayout.EndScrollView();
        }
    }
}