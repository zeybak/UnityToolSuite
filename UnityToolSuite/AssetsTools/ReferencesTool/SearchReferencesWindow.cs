using UnityEditor;
using UnityEngine;
using ToolSuite.AssetTools.ReferencesTools.Operations;
using UnityObject = UnityEngine.Object;

namespace ToolSuite.AssetTools.ReferencesTools
{
    public class SearchReferencesWindow : EditorWindow
    {
        public const string MENU_NAME = "SearchReferences"; 

        private UnityObject assetToSearchReferencesFor = null;
        private UnityObject[] references = null;
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

            UnityObject oldAsset = assetToSearchReferencesFor;
            assetToSearchReferencesFor = EditorGUILayout.ObjectField(new GUIContent("Asset: "), assetToSearchReferencesFor, typeof(UnityObject), false);

            if (oldAsset != assetToSearchReferencesFor)
            {
                references = null;
            }

            if (GUILayout.Button("Search references"))
            {
                references = null;
                ReferencesOperation operation = new SearchReferencesOperation();
                UnityObject[] parameters = new UnityObject[]
                {
                    assetToSearchReferencesFor
                };
                references = operation.Execute(parameters) as UnityObject[];
            }

            if (references != null)
            {
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
                foreach(UnityObject reference in references)
                {
                    EditorGUILayout.ObjectField(reference, typeof(UnityObject), false);
                };
                EditorGUILayout.EndScrollView();
            }
        }
    }
}