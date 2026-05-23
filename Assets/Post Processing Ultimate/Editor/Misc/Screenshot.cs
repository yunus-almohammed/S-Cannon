using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class Screenshot : EditorWindow {

        private static Camera camera;
        private static readonly string filename = "screen";
        private static Vector2 dim = new Vector2(1920, 1080);
        private static int index;
        private static List<string> camNames = new List<string>();
        private static string path;
        private static int files = 1;

        [MenuItem("Window/" + MiscUtils.name + "/Screenshot")]
        private static void Init() {
            Screenshot window = (Screenshot) GetWindow(typeof(Screenshot));
            window.Show();
            path = Application.dataPath + $"/{MiscUtils.name}/Screenshots/";
            if (!Directory.Exists(path)) {
                path = Application.dataPath + $"/{MiscUtils.name}/";
            }
            window.minSize = new Vector2(300, 80);
            window.titleContent = new GUIContent("Screenshot");
            window.position = MiscUtils.CenteredSize(window.minSize);
        }

        private void OnGUI() {
            List<Camera> cameras = FindObjectsOfType<Camera>().ToList();
            camNames = cameras.Select(c => c.name).ToList();
            index = EditorGUILayout.Popup(index, camNames.ToArray());
            camera = cameras[index];
            dim = EditorGUILayout.Vector2Field("Screenshot dimensions", dim);
            if (GUILayout.Button("Take screenshot")) {
                if (path != null && path != "") {
                    if (path.Contains(".")) {
                        int last = 0;
                        for (int i = path.Length - 1; i >= 0; --i) {
                            if (path[i] == '/') {
                                last = i + 1;
                                break;
                            }
                        }
                        path = path.Substring(0, last);
                    }
                    files = (new DirectoryInfo(path)).GetFiles().Length / 2 + 1;
                }
                path = EditorUtility.SaveFilePanel("Save screenshot", path, filename + files, "png");
                Shot();
                AssetDatabase.Refresh();
            }
        }

        private static void Shot() {
            if (camera != null) {
                #if !UNITY_WEBPLAYER
                    File.WriteAllBytes(path, MiscUtils.CurrentScreen(camera, (int) dim.x, (int) dim.y).EncodeToPNG());
                #else
				    Debug.Log("Doesn't work in Webplayer");
                #endif
            }
        }
    }
}