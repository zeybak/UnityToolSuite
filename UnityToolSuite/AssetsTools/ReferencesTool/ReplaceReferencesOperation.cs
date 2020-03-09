using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace ToolSuite.AssetTools.ReferencesTools.Operations
{
    public class ReplaceReferencesOperation : ReferencesOperation
    {
        public override UnityObject[] Execute(UnityObject[] args = null)
        {
            if (args.Length < 2)
            {
                Debug.LogError("Expecting at least 2 arguments at ReplaceReferencesOperation");
                return null;
            }

            UnityObject originalAsset = args[0] as UnityObject;
            UnityObject replaceAsset = args[1] as UnityObject;
            UnityObject[] references = null;

            if (originalAsset == null)
            {
                Debug.LogError("Invalid original asset at ReplaceReferencesOperation, expected type: UnityObject");
                return null;
            }
            if (replaceAsset == null)
            {
                Debug.LogError("Invalid replace asset at ReplaceReferencesOperation, expected type: UnityObject");
                return null;
            }

            UpdateAssetsDatabase();
            references = SearchReferencesFromAsset(originalAsset);

            if (references != null)
            {
                //@TODO: replace references asset guid with new guid
                Debug.Log($"Replace references of {originalAsset.name} with {replaceAsset.name}");
            }

            return references;
        }
    }
}