using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    [System.Serializable]
    public class ShaderEditor : VisualEditor {

        [MenuItem("Window/Post Processing Ultimate/Shader Editor")]
        private static void Init() {
            ShaderEditor window = (ShaderEditor) GetWindow(typeof(ShaderEditor));
            window.minSize = new Vector2(400, 100);
            window.position = MiscUtils.CenteredSize(new Vector2(UnityEngine.Screen.currentResolution.width * 0.75f, UnityEngine.Screen.currentResolution.height * 0.75f));
            window.wantsMouseMove = true;
            window.wantsMouseEnterLeaveWindow = true;
            window.Show();
            window.titleContent = new GUIContent($"Shader Editor - {Utils.appVersion}");
        }

        internal override void Awake() {
            isFunctionEditor = false;
            base.Awake();
            lastDir = "Assets/Post Processing Ultimate/Shaders";
        }
    }
}