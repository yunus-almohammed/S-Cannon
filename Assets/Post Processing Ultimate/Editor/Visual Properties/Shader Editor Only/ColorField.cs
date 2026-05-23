using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class ColorField : VisualProperty {

        private Color used = Color.black;

        public ColorField() {
            position = new Rect(0, 0, 212, 89);
            name = "Color";
            tooltip = "Color property";
            Values.Add("Color_Name");
            Values.Add("Tooltip");
            Values.Add("0");
            Values.Add("0");
            Values.Add("0");
            Values.Add("0");
        }

        public override void Show() {
            base.Show();
            used = new Color(float.Parse(Values[2], CultureInfo.InvariantCulture), float.Parse(Values[3], CultureInfo.InvariantCulture), float.Parse(Values[4], CultureInfo.InvariantCulture), float.Parse(Values[5], CultureInfo.InvariantCulture));
            Values[1] = EditorGUI.TextField(new Rect(position.x + 2, (position.y + 2 * (20) + 2), position.width - 4, 16), Values[1]);
            used = EditorGUI.ColorField(new Rect(position.x + 2, position.y + 3 * (20) + 2, position.width - 4, 16), used);
            Values[2] = used.r.ToString(CultureInfo.InvariantCulture);
            Values[3] = used.g.ToString(CultureInfo.InvariantCulture);
            Values[4] = used.b.ToString(CultureInfo.InvariantCulture);
            Values[5] = used.a.ToString(CultureInfo.InvariantCulture);
        }
    }
}