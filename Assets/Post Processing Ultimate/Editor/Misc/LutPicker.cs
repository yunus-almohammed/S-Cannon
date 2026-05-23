using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace DawidMoza.PostProcessingUltimate {
    public class LutPicker : EditorWindow {

        internal List<string> camNames = new List<string>();
        internal int selection = 0;
        internal int tempSelection = 0;
        internal Vector2 scrollPos;
        internal static LutPicker window;
        internal float width;
        internal float height;
        internal List<Texture2D> lutTextures = new List<Texture2D>();
        internal Camera cam;
        internal List<Texture2D> processedTextures = new List<Texture2D>();
        internal Texture2D ldr;
        internal bool stopped;
        internal ColorGrading colorGrading;

        private const string lutsDirectory = "PPU LUT textures";

        [MenuItem("Window/" + MiscUtils.name + "/LUT Picker")]
        private static void Init() {
            window = (LutPicker) GetWindow(typeof(LutPicker));
            window.minSize = new Vector2(700, 600);
            window.titleContent = new GUIContent("LUT Picker");
            window.position = MiscUtils.CenteredSize(window.minSize);
            window.Show();
        }

        internal void Awake() {
            stopped = false;
            List<Camera> cameras = MiscUtils.CamerasThatContainsEffect("ColorGrading");
            camNames = cameras.Select(c => c.name).ToList();
            if (cameras.Count > 0) {
                cam = cameras[selection];
            } else {
                return;
            }
            lutTextures = Resources.LoadAll(lutsDirectory).Select(r => (Texture2D) r).ToList();
            colorGrading = (ColorGrading) StackEffect.GetEffect(cam, "ColorGrading");
            if (colorGrading.ldrLut.value != null) {
                ldr = (Texture2D) colorGrading.ldrLut.value;
            }
            processedTextures.Clear();
            for (int i = 0; i < lutTextures.Count; ++i) {
                colorGrading.ldrLut.value = lutTextures[i];
                processedTextures.Add(MiscUtils.CurrentScreen(cam));
            }
            colorGrading.ldrLut.value = ldr;
            Clear(false);
        }

        internal void Click(int i) {
            EditorUtility.SetDirty(cam);
            EditorUtility.SetDirty(cam.GetComponent<PostProcessVolume>().sharedProfile);
            colorGrading.ldrLut.value = lutTextures[i];
            AssetDatabase.SaveAssets();
        }

        internal void Clear(bool full) {
            if (full) {
                for (int i = 0; i < processedTextures.Count; ++i) {
                    DestroyImmediate(processedTextures[i]);
                }
            }
            EditorUtility.UnloadUnusedAssetsImmediate(true);
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }

        internal void OnDestroy() {
            Clear(true);
        }

        internal void OnGUI() {
            width = (position.width / 2) - 16f;
            height = 0.5625f * ((position.width / 2) - 4);
            if (cam != null && cam.enabled && processedTextures.Count > 0 && lutTextures.Count == processedTextures.Count && !stopped) {
                if (processedTextures[0] == null) {
                    Awake();
                }
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                selection = EditorGUILayout.Popup("Camera:", selection, camNames.ToArray());
                if (selection != tempSelection) {
                    tempSelection = selection;
                    Awake();
                }
                for (int i = 0; i < (processedTextures.Count + 1) / 2; ++i) {
                    EditorGUILayout.BeginHorizontal();
                    for (int j = 0; j < 2; ++j) {
                        int index = 2 * i + j;
                        EditorGUILayout.BeginVertical();
                        if (GUILayout.Button(processedTextures[index], GUILayout.Width(width), GUILayout.Height(height))) {
                            Click(index);
                        }
                        GUILayout.Label(lutTextures[index].name);
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndScrollView();
            } else {
                stopped = true;
                if (GUILayout.Button("Click me when Color Grading is attached to an active camera.", GUILayout.Width(position.width - 4), GUILayout.Height(position.height - 4))) {
                    Awake();
                }
            }
        }
    }
}