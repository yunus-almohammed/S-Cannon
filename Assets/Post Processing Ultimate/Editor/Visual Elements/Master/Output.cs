using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class Output : VisualElement {

        public const string tooltip = "Function output";

        public Output() {
            name = "Output";
            tint = Utils.ElementsMaster;
            jointsOffset = 2;
            options.Add("1");
            options.Add("2");
            options.Add("3");
            options.Add("4");
            Values.Add("0");
            Arrange();
        }

        public override void Arrange() {
            if (int.Parse(Values[0]) + (int.Parse(Values[0]) == 0 ? 1 : 2) != joints.Count) {
                joints.Clear();
                AddJoints(int.Parse(Values[0]) + 1, VisualJoint.LEFT, "Output", "Output");
                CalculateHeight();
            }
        }

        public override void Show() {
            base.Show();
            Values[0] = EditorGUI.Popup(new Rect(position.x, position.y + 1 * (20) + 2, position.width, 16), int.Parse(Values[0]), options.ToArray()).ToString(CultureInfo.InvariantCulture);
            Arrange();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateMaster(elements, precision, "Output");
        }
    }
}