using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;
namespace Case2.Editor
{
    public class ErdemEditor : MonoBehaviour
    {
        public class StateCreator : EditorWindow
        {
            [MenuItem("Erdem/State Creator")]
            static void OpenWindow()
            {
                StateCreator window = (StateCreator) EditorWindow.GetWindow(typeof(StateCreator));
                window.Show();
            }
            public string stateName;

            private void OnGUI()
            {
                stateName = EditorGUILayout.TextField("State Name: ", stateName);

                if (GUILayout.Button("Create State"))
                {
                    CreateState();
                }
            }

            private IEnumerator PingAfterCompiling()
            {
                yield return new WaitUntil(() => !EditorApplication.isCompiling);
                var obj = AssetDatabase.LoadAssetAtPath($"Assets/Scripts/Case2/Runtime/States/{stateName}.cs", typeof(Object));
                Selection.activeObject = obj;
                EditorGUIUtility.PingObject(Selection.activeObject);
            }
            private void CreateState()
            {
                if (System.IO.File.Exists($"{Application.dataPath}/Scripts/Case2/Runtime/States/{stateName}.cs"))
                {
                    EditorUtility.DisplayDialog("Error!", $"{stateName}.cs Already Exist!", "OK");
                    TMP_EditorCoroutine.StartCoroutine(PingAfterCompiling());
                    return;
                }
                if (string.IsNullOrEmpty(stateName))
                {
                    EditorUtility.DisplayDialog("Error!", $"State name cannot be empty!", "OK");
                    return;
                }
                FileUtil.CopyFileOrDirectory($"{Application.dataPath}/Scripts/Case2/Runtime/States/EmptyState.cs", $"{Application.dataPath}/Scripts/Case2/Runtime/States/{stateName}.cs");

                StreamReader sr = new StreamReader($"{Application.dataPath}/Scripts/Case2/Runtime/States/{stateName}.cs");
                List<string> rows = StreamReaderLineByLine(sr);
                sr.Close();

                StreamWriter sw = new StreamWriter($"{Application.dataPath}/Scripts/Case2/Runtime/States/{stateName}.cs");
                for (int i = 0; i < rows.Count; i++)
                {
                    if (rows[i].Contains("EmptyState"))
                    {
                        rows[i] = rows[i].Replace("EmptyState", stateName);
                    }
                    sw.WriteLine(rows[i]);
                }
                sw.Close();

                StreamReader imReader = new StreamReader($"{Application.dataPath}/Scripts/Case2/Managers/InputManager.cs");
                List<string> imRows = StreamReaderLineByLine(imReader);
                imReader.Close();

                StreamWriter imWriter = new StreamWriter($"{Application.dataPath}/Scripts/Case2/Managers/InputManager.cs");
                for (int i = 0; i < imRows.Count; i++)
                {
                    if (imRows[i].Contains($"[NEW_STATE]"))
                    {
                        imRows[i] = $"\t\tallStates.Add(\"{stateName}\", new {stateName}());\n\t\t//[NEW_STATE]";
                    }
                    imWriter.WriteLine(imRows[i]);
                }
                imWriter.Close();

                AssetDatabase.Refresh();
                TMP_EditorCoroutine.StartCoroutine(PingAfterCompiling());
            }
            private List<string> StreamReaderLineByLine(StreamReader sr)
            {
                var result = new List<string>();
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        result.Add(sr.ReadLine());
                    }
                }
                return result;
            }
        }
    }
}
