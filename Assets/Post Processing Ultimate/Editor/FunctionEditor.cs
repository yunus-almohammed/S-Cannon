using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {

    public class FunctionEditor : VisualEditor {

        [MenuItem("Window/Post Processing Ultimate/Function Editor")]
        private static void Init() {
            FunctionEditor window = (FunctionEditor) GetWindow(typeof(FunctionEditor));
            window.minSize = new Vector2(400, 100);
            window.position = MiscUtils.CenteredSize(new Vector2(UnityEngine.Screen.currentResolution.width * 0.75f, UnityEngine.Screen.currentResolution.height * 0.75f));
            window.wantsMouseMove = true;
            window.wantsMouseEnterLeaveWindow = true;
            window.Show();
            window.titleContent = new GUIContent($"Function Editor - {Utils.appVersion}");   
        }

        internal override void Awake() {
            isFunctionEditor = true;
            base.Awake();
            lastDir = "Assets/Post Processing Ultimate/Functions";   
        }
    }
}