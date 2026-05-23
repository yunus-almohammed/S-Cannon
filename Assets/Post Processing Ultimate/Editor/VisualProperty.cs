using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {

    public class VisualProperty : object {

        public Rect position;
        public string name;
        public bool selected;
        public List<string> Values = new List<string>();
        public int unique = -1;
        internal List<GUIStyle> style = new List<GUIStyle>();
        internal Texture2D nodeBack;
        internal string tooltip;

        public virtual void Show() {
            //It's here because aligning during serialization is prohibited.
            if (style.Count == 0) {
                style.Clear();
                style.Add(new GUIStyle());
                style[0].alignment = TextAnchor.MiddleCenter;
                style[0].fontSize = 12;
                style[0].font = Resources.Load("Fonts/FORCED SQUARE") as Font;
                style.Add(new GUIStyle(style[0]));
                style[1].alignment = TextAnchor.MiddleLeft;
                style[1].normal.textColor = Utils.TextColor(EditorGUIUtility.isProSkin);
                style.Add(new GUIStyle(style[1]));
                style[2].alignment = TextAnchor.MiddleRight;
            }
            if (unique == 0) {
                unique = Random.Range(1, 4096);
            }
            if (nodeBack == null) {
                nodeBack = Utils.CreateRoundedTexture(848, 80, 40, true, true, true, true);
            }
            EditorGUI.DrawRect(new Rect(position.x, position.y + 10, position.width, position.height - 10), Utils.PropertyColor(EditorGUIUtility.isProSkin));
            GUI.DrawTexture(new Rect(position.x, position.y, position.width, 20), nodeBack);
            EditorGUI.LabelField(new Rect(position.x, position.y, position.width, 20), new GUIContent(name, tooltip), style[0]);
            Values[0] = EditorGUI.TextField(new Rect(position.x + 2, (position.y + 1 * (20) + 2), position.width - 4, 16), Values[0]).Replace(" ", "");
        }
    }
}