using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class IntField : VisualProperty {

        public IntField() {
            position = new Rect(0, 0, 212, 89);
            name = "Int";
            tooltip = "Int property";
            Values.Add("Int_Name");
            Values.Add("Tooltip");
            Values.Add("0");
        }

        public override void Show() {
            base.Show();
            Values[1] = EditorGUI.TextField(new Rect(position.x + 2, (position.y + 2 * (20) + 2), position.width - 4, 16), Values[1]);
            Values[2] = EditorGUI.IntField(new Rect(position.x + 2, (position.y + 3 * (20) + 2), position.width - 4, 16), int.Parse(Values[2])).ToString(CultureInfo.InvariantCulture);
        }
    }
}