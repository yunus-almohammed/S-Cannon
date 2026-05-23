using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class BoolField : VisualProperty {

        private readonly string[] options = new string[] { "False", "True" };

        public BoolField() {
            position = new Rect(0, 0, 212, 89);
            name = "Bool";
            tooltip = "Bool property";
            Values.Add("Bool_Name");
            Values.Add("Tooltip");
            Values.Add("false");
        }

        public override void Show() {
            base.Show();
            Values[1] = EditorGUI.TextField(new Rect(position.x + 2, (position.y + 2 * (20) + 2), position.width - 4, 16), Values[1]);
            Values[2] = EditorGUI.Popup(new Rect(position.x + 2, (position.y + 3 * (20) + 2), position.width - 4, 16), bool.Parse(Values[2]) ? 1 : 0, options) == 0 ? "false" : "true";
        }
    }
}