using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class VarLoop4 : VisualElement {

        public const string tooltip = "Performs arithmetic operations on a quadruple value";

        public VarLoop4() {
            name = "VarLoop4";
            tint = Utils.ElementsVariable;
            jointsOffset = 2;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "First output value",
                "Y Value", "Second output value",
                "Z Value", "Third output value",
                "W Value", "Fourth output value",
                "XYZW", "All output values"
            );
            AddJoint(VisualJoint.LEFT, 4, "Input", "Input value");
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