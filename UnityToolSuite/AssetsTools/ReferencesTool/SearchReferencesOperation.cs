using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace ToolSuite.AssetTools.ReferencesTools.Operations
{
    public class SearchReferencesOperation : ReferencesOperation
    {
        public override UnityObject[] Execute(UnityObject[] args = null)
        {
            if (args.Length < 1)
            {
                Debug.LogError("Expecting at least 1 argument at SearchReferencesOperation");
                return null;
            }

            UnityObject asset = args[0] as UnityObject;
            if (asset == null)
            {
                Debug.LogError("Invalid asset at SearchReferencesOperation, expected type: UnityObject");
                return null;
            }

            UpdateAssetsDatabase();
            return SearchReferencesFromAsset(asset);
        }
    }
}