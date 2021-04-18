using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;

namespace Isshi777
{
    public class Editor_CopyAssetInfoToClipboard
    {
        /// <summary>
        /// �R�s�[���鍀��
        /// </summary>
        private enum ECopyKind
        {
            FullPath,   // ��΃p�X
            Path,       // �p�X�iAssets/�`�`�`�j
            GUID,       // GUID
        }

        /// <summary>
        /// �I�����ꂽ�A�Z�b�g�̐�΃p�X�̃R�s�[
        /// </summary>
        [MenuItem("Assets/CopyAssetInfoToClipboard/CopyAssetFullPath", false)]
        private static void CopyAssetFullPath()
        {
            CopyClipBoard(ECopyKind.FullPath);
        }

        /// <summary>
        /// �I�����ꂽ�A�Z�b�g�̃p�X�iAssets/�`�`�`�j�̃R�s�[
        /// </summary>
        [MenuItem("Assets/CopyAssetInfoToClipboard/CopyAssetPath", false)]
        private static void CopyAssetRelativePath()
        {
            CopyClipBoard(ECopyKind.Path);
        }

        /// <summary>
        /// �I�����ꂽ�A�Z�b�g��GUID�̃R�s�[
        /// </summary>
        [MenuItem("Assets/CopyAssetInfoToClipboard/CopyAssetGUID", false)]
        private static void CopyAssetGUID()
        {
            CopyClipBoard(ECopyKind.GUID);
        }

        /// <summary>
        /// �R�s�[����
        /// </summary>
        /// <param name="list">�R�s�[������e�̃��X�g</param>
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
