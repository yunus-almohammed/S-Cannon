using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class VarLoop1 : VisualElement {

        public const string tooltip = "Performs arithmetic operations on a single value";

        public VarLoop1() {
            name = "VarLoop1";
            tint = Utils.ElementsVariable;
            jointsOffset = 2;
            AddJoint(VisualJoint.RIGHT, 1, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 1, "Input", "Input value");
            AddJoint(VisualJoint.LEFT, 1, "Iterations", "Number of loops");
            AddJoint(VisualJoint.LEFT, 1, "Start", "Start iterator value");
            Values.Add("0");
            Values.Add("x");
            Values.Add("0");
            CalculateHeight();
        }

        public override void Show() {
            base.Show();
            Values[2] = EditorGUI.Popup(new Rect(position.x, position.y + 1 * 20 + 2, position.width, 16), int.Parse(Values[2]), loopOptions.ToArray()).ToString(CultureInfo.InvariantCulture);
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateVarLoop();
        }
    }
}