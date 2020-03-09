using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace ToolSuite.AssetTools.ReferencesTools.Operations
{
    public abstract class ReferencesOperation
    {
        protected string[] ExcludeSettings => new string[]
        {
            ".cs",
            ".js",
            ".png",
            ".tga",
            ".psd",
            ".jpg",
            ".fbx",
            ".obj",
            ".wav",
            ".ogg",
            ".mp3",
            ".otf",
            ".ttf",
            ".mp4",
            ".3gp",
            ".usm",
            ".meta"
        };

        public abstract UnityObject[] Execute(UnityObject[] args = null);

        protected UnityObject[] SearchReferencesFromAsset(UnityObject asset)
        {
            List<UnityObject> references = new List<UnityObject>();
            string assetPath = AssetDatabase.GetAssetPath(asset);
            string guid = AssetDatabase.AssetPathToGUID(assetPath);
            string[] dataPaths = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories);
            
            foreach(string dataPath in dataPaths)
            {
                string dataPathFixed = dataPath.Replace(@"\", "/"); // fix OSX / Windows path
                if (!dataPathFixed.Contains("."))
                {
                    // don't process folders
                    continue;
                }
                if (ShouldExcludeFile(dataPathFixed))
                {
                    continue;
                }

                string[] lines = File.ReadAllLines(dataPathFixed);
                foreach(string line in lines)
                {
                    if (!line.Contains("guid:"))
                    {
                        continue;
                    }
                    if (!line.Contains(guid))
                    {
                        continue;
                    }

                    UnityObject referenceAsset = AssetDatabase.LoadAssetAtPath<UnityObject>(ApplicationDataPathToAssetPath(dataPathFixed));
                    if (referenceAsset == null)
                    {
                        continue;
                    }

                    references.Add(referenceAsset);
                }
            }

            return references.ToArray();
        }

        protected bool ShouldExcludeFile(string filePath)
        {
            foreach (string extension in ExcludeSettings)
            {
                if (filePath.IndexOf(extension, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        protected void UpdateAssetsDatabase()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        protected string ApplicationDataPathToAssetPath(string dataPath)
        {
            string assetPath = "Assets" + dataPath.Replace(Application.dataPath, string.Empty);
            assetPath = assetPath.Replace(".meta", string.Empty);
            return assetPath;
        }
    }
}