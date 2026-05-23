using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class IntSliderField : VisualProperty {

        public IntSliderField() {
            position = new Rect(0, 0, 212, 109);
            name = "IntSlider";
            tooltip = "Int slider property";
            Values.Add("IntSlider_Name");
            Values.Add("Tooltip");
            Values.Add("0");
            Values.Add("0");
            Values.Add("1");
        }

        public override void Show() {
            base.Show();
            Values[1] = EditorGUI.TextField(new Rect(position.x + 2, (position.y + 2 * (20) + 2), position.width - 4, 16), Values[1]);
            Values[2] = Mathf.Floor(GUI.HorizontalSlider(new Rect(position.x + 2, (position.y + 3 * (20) + 2), 124, 16), int.Parse(Values[2]), int.Parse(Values[3]), int.Parse(Values[4]))).ToString(CultureInfo.InvariantCulture);
            Values[2] = Mathf.Floor(EditorGUI.IntField(new Rect(position.x + 132, (position.y + 3 * (20) + 2), 78, 16), int.Parse(Values[2]))).ToString(CultureInfo.InvariantCulture);
            Values[3] = EditorGUI.IntField(new Rect(position.x + 2, (position.y + 4 * (20) + 2), position.width / 2 - 6, 16), int.Parse(Values[3])).ToString(CultureInfo.InvariantCulture);
            Values[4] = EditorGUI.IntField(new Rect(position.width / 2 + 4, (position.y + 4 * (20) + 2), position.width / 2 - 6, 16), int.Parse(Values[4])).ToString(CultureInfo.InvariantCulture);
        }
    }
}