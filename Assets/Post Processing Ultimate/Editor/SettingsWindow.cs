using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class SettingsWindow : EditorWindow {

        internal UserSettings uSets;
        internal int curHeight;

        internal void Awake() {
            minSize = new Vector2(500, 70);
            wantsMouseMove = true;
            wantsMouseEnterLeaveWindow = true;
            titleContent = new GUIContent("Settings");
            try {
                uSets = JsonUtility.FromJson<UserSettings>(System.IO.File.ReadAllText("Assets/Post Processing Ultimate/Editor/config.usets"));
            } catch {
                uSets = new UserSettings();
            }
        }

        internal void OnGUI() {
            curHeight = 4;
            uSets.SwitchMMB = EditorGUI.Toggle(new Rect(position.width - 109, curHeight, 105, 16), uSets.SwitchMMB);
            EditorGUI.LabelField(new Rect(4, curHeight, position.width - 113, 16), "Switch MMB with LMB during group selection");
            uSets.AutoSave = EditorGUI.Toggle(new Rect(position.width - 109, curHeight += 16, 105, 16), uSets.AutoSave);
            EditorGUI.LabelField(new Rect(4, curHeight, position.width - 113, 16), "Autosave when mouse button up");
            uSets.ToolbarScale = EditorGUI.Slider(new Rect(position.width - 109, curHeight += 16, 105, 16), uSets.ToolbarScale, 1.0f, 2.0f);
            uSets.ToolbarScale = Utils.RoundWithSteps(uSets.ToolbarScale, 0.1f);
            EditorGUI.LabelField(new Rect(4, curHeight, position.width - 113, 16), "Toolbar scale (changes visible after saving)");
            if (GUI.Button(new Rect(4, curHeight += 16, position.width - 8, 16), new GUIContent("SAVE"))) {
                System.IO.File.WriteAllText("Assets/Post Processing Ultimate/Editor/config.usets", JsonUtility.ToJson(uSets));
                Close();
                if (HasOpenInstances<ShaderEditor>()) {
                    ShaderEditor window = (ShaderEditor) GetWindow(typeof(ShaderEditor));
                    window.LoadSettings();
                }
                if (HasOpenInstances<FunctionEditor>()) {
                    FunctionEditor window = (FunctionEditor) GetWindow(typeof(FunctionEditor));
                    window.LoadSettings();
                }
            }
        }
    }
}