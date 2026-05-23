using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class SplineField : VisualProperty {

        public SplineField() {
            position = new Rect(0, 0, 212, 69);
            name = "Spline";
            tooltip = "Spline property";
            Values.Add("Spline_Name");
            Values.Add("Tooltip");
        }

        public override void Show() {
            base.Show();
            Values[1] = EditorGUI.TextField(new Rect(position.x + 2, (position.y + 2 * (20) + 2), position.width - 4, 16), Values[1]);
        }
    }
}