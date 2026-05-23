using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class Vector2Field : VisualProperty {

        public Vector2Field() {
            position = new Rect(0, 0, 212, 89);
            name = "Vector2";
            tooltip = "Vector2 property";
            Values.Add("Vector2_Name");
            Values.Add("Tooltip");
            Values.Add("0");
            Values.Add("0");
            Values.Add("0");
            Values.Add("0");
        }

        public override void Show() {
            base.Show();
            Values[1] = EditorGUI.TextField(new Rect(position.x + 2, (position.y + 2 * (20) + 2), position.width - 4, 16), Values[1]);
            EditorGUI.LabelField(new Rect(position.x + 2, (position.y + 3 * (20) + 2), position.width, 16), "X:", style[1]);
            EditorGUI.LabelField(new Rect(position.x + 108, (position.y + 3 * (20) + 2), position.width, 16), "Y:", style[1]);
            Values[2] = EditorGUI.FloatField(new Rect(position.x + 16, (position.y + 3 * (20) + 2), 88, 16), float.Parse(Values[2], CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
            Values[3] = EditorGUI.FloatField(new Rect(position.x + 122, (position.y + 3 * (20) + 2), 88, 16), float.Parse(Values[3], CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
        }
    }
}