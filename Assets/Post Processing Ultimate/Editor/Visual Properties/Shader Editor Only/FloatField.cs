using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class FloatField : VisualProperty {

        public FloatField() {
            position = new Rect(0, 0, 212, 89);
            name = "Float";
            tooltip = "Float property";
            Values.Add("Float_Name");
            Values.Add("Tooltip");
            Values.Add("0");
        }

        public override void Show() {
            base.Show();
            Values[1] = EditorGUI.TextField(new Rect(position.x + 2, (position.y + 2 * (20) + 2), position.width - 4, 16), Values[1]);
            Values[2] = EditorGUI.FloatField(new Rect(position.x + 2, (position.y + 3 * (20) + 2), position.width - 4, 16), float.Parse(Values[2], CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
        }
    }
}