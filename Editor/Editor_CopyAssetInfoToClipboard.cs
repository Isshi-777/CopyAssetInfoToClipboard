using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;

namespace Isshi777
{
    public class Editor_CopyAssetInfoToClipboard
    {
        /// <summary>
        /// コピーする項目
        /// </summary>
        private enum ECopyKind
        {
            FullPath,   // 絶対パス
            Path,       // パス（Assets/～～～）
            GUID,       // GUID
        }

        /// <summary>
        /// 選択されたアセットの絶対パスのコピー
        /// </summary>
        [MenuItem("Assets/CopyAssetInfoToClipboard/CopyAssetFullPath", false)]
        private static void CopyAssetFullPath()
        {
            CopyClipBoard(ECopyKind.FullPath);
        }

        /// <summary>
        /// 選択されたアセットのパス（Assets/～～～）のコピー
        /// </summary>
        [MenuItem("Assets/CopyAssetInfoToClipboard/CopyAssetPath", false)]
        private static void CopyAssetRelativePath()
        {
            CopyClipBoard(ECopyKind.Path);
        }

        /// <summary>
        /// 選択されたアセットのGUIDのコピー
        /// </summary>
        [MenuItem("Assets/CopyAssetInfoToClipboard/CopyAssetGUID", false)]
        private static void CopyAssetGUID()
        {
            CopyClipBoard(ECopyKind.GUID);
        }

        /// <summary>
        /// コピー処理
        /// </summary>
        /// <param name="list">コピーする内容のリスト</param>
        private static void CopyClipBoard(ECopyKind kind)
        {
            List<string> list = new List<string>();
            foreach (var guid in Selection.assetGUIDs)
            {
                if (kind == ECopyKind.FullPath)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var fullPath = Path.GetFullPath(path);
                    list.Add(fullPath);
                }
                else if (kind == ECopyKind.Path)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    list.Add(path);
                }
                else if (kind == ECopyKind.GUID)
                {
                    list.Add(guid);
                }
            }

            StringBuilder sb = new StringBuilder();
            list.ForEach(x => sb.AppendLine(x));

            EditorGUIUtility.systemCopyBuffer = sb.ToString();
        }
    }
}
