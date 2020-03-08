using UnityEditor;
using UnityEngine;

namespace ToolSuite.AssetTools.ReferencesTools
{
    public class SearchReferencesWindow : EditorWindow
    {
        public const string MENU_NAME = "SearchReferences"; 

        private Object assetToSearchReferencesFor = null;
        private Vector2 scrollPosition = Vector2.zero;

        [MenuItem(ToolSuiteMenuName.MENU_NAME + "/" + AssetToolsMenuName.MENU_NAME + "/" + MENU_NAME)]
        private static void ShowWindow()
        {
            var window = GetWindow<SearchReferencesWindow>();
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

            assetToSearchReferencesFor = EditorGUILayout.ObjectField(new GUIContent("Asset: "), assetToSearchReferencesFor, typeof(Object), false);

            GUILayout.Button("Search references");

            EditorGUILayout.EndScrollView();
        }
    }
}