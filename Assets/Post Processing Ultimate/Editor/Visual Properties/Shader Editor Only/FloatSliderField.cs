using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class FloatSliderField : VisualProperty {

        public FloatSliderField() {
            position = new Rect(0, 0, 212, 109);
            name = "FloatSlider";
            tooltip = "Float slider property";
            Values.Add("FloatSlider_Name");
            Values.Add("Tooltip");
            Values.Add("0");
            Values.Add("0");
            Values.Add("1");
        }

        public override void Show() {
            base.Show();
            Values[1] = EditorGUI.TextField(new Rect(position.x + 2, (position.y + 2 * (20) + 2), position.width - 4, 16), Values[1]);
            Values[2] = GUI.HorizontalSlider(new Rect(position.x + 2, (position.y + 3 * (20) + 2), 124, 16), float.Parse(Values[2], CultureInfo.InvariantCulture), float.Parse(Values[3], CultureInfo.InvariantCulture), float.Parse(Values[4], CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
            Values[2] = EditorGUI.FloatField(new Rect(position.x + 132, (position.y + 3 * (20) + 2), 78, 16), float.Parse(Values[2], CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
            Values[3] = EditorGUI.FloatField(new Rect(position.x + 2, (position.y + 4 * (20) + 2), position.width / 2 - 6, 16), float.Parse(Values[3], CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
            Values[4] = EditorGUI.FloatField(new Rect(position.width / 2 + 4, (position.y + 4 * (20) + 2), position.width / 2 - 6, 16), float.Parse(Values[4], CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
        }
    }
}