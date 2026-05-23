using System;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {

    [Serializable]
    public class VisualPass : object {

        public Rect position;
        public int Input = 0;
        public int Output = 0;
        public int Pass = 0;
        public int Iterations = 1;
        public int Variable = 0;
        private RenderQueue renderQueue;
        private GUIStyle labelFieldStyle;
        private GUIStyle popupStyle;

        public VisualPass(RenderQueue renderQueue) {
            position = new Rect(8, 0, 212, 24);
            this.renderQueue = renderQueue;
        }

        public void SetRenderQueue(RenderQueue renderQueue) {
            this.renderQueue = renderQueue;
        }

        public void Show() {
            //It's here because aligning during serialization is prohibited.
            if (labelFieldStyle == null) {
                labelFieldStyle = new GUIStyle {
                    alignment = TextAnchor.MiddleCenter,
                    clipping = TextClipping.Clip
                };
                labelFieldStyle.normal.textColor = EditorGUIUtility.isProSkin ? Utils.lightColor : Utils.darkColor;
            }
            if (popupStyle == null) {
                popupStyle = new GUIStyle(EditorStyles.popup);
                popupStyle.alignment = TextAnchor.MiddleRight;
            }
            Input = Mathf.Clamp(EditorGUI.Popup(new Rect(28 + position.x, position.y, 65, 16), Input, renderQueue.inputOptions.ToArray(), popupStyle), 0, renderQueue.inputOptions.Count - 1);
            Output = Mathf.Clamp(EditorGUI.Popup(new Rect(28 + 69 + position.x, position.y, 65, 16), Output, renderQueue.outputOptions.ToArray(), popupStyle), 0, renderQueue.outputOptions.Count - 1);
            if (Input == Output && Input != 0) {
                Output = 0;
            }
            Pass = Mathf.Clamp(EditorGUI.Popup(new Rect(28 + 138 + position.x, position.y, 46, 16), Pass, renderQueue.passOptions.ToArray()), 0, renderQueue.passOptions.Count - 1);
            EditorGUI.LabelField(new Rect(28 + position.x, position.y + 18, 65, 16), new GUIContent("LOOPS:", "How many times pass should be done"), labelFieldStyle);
            if (Variable != 0) {
                GUI.enabled = false;
            }
            Iterations = Mathf.Max(EditorGUI.IntField(new Rect(28 + 69 + position.x, position.y + 18, 32, 16), Iterations), 0);
            GUI.enabled = true;
            Variable = int.Parse(renderQueue.uniques[Mathf.Clamp(EditorGUI.Popup(new Rect(28 + 69 + 36 + position.x, position.y + 18, 79, 16), Utils.IndexIfAvailable(renderQueue.uniques, Variable.ToString()), renderQueue.variableOptions.ToArray()), 0, renderQueue.variableOptions.Count - 1)]);
        }
    }
}