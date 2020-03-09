using UnityEditor;
using UnityEngine;
using ToolSuite.AssetTools.ReferencesTools.Operations;
using UnityObject = UnityEngine.Object;

namespace ToolSuite.AssetTools.ReferencesTools
{
    public class ReplaceReferencesWindow : EditorWindow
    {
        public const string MENU_NAME = "ReplaceReferences"; 

        private Object originalAsset = null;
        private Object replaceAsset = null;
        private UnityObject[] references = null;
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

            UnityObject oldOriginalAsset = originalAsset;
            originalAsset = EditorGUILayout.ObjectField(new GUIContent("Original Asset: "), originalAsset, typeof(Object), false);
            if (oldOriginalAsset != originalAsset)
            {
                references = null;
            }

            UnityObject oldReplaceAsset = replaceAsset;
            replaceAsset = EditorGUILayout.ObjectField(new GUIContent("Replace references with: "), replaceAsset, typeof(Object), false);
            if (oldReplaceAsset != replaceAsset)
            {
                references = null;
            }

            if (GUILayout.Button("Replace references"))
            {
                references = null;
                ReferencesOperation operation = new ReplaceReferencesOperation();
                UnityObject[] parameters = new UnityObject[]
                {
                    originalAsset,
                    replaceAsset
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