using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Variable4 : VisualElement {

        public const string tooltip = "Quadruple index variable";

        public Variable4() {
            name = "Variable4";
            tint = Utils.ElementsVariable;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "First output value",
                "Y Value", "Second output value",
                "Z Value", "Third output value",
                "W Value", "Fourth output value",
                "XYZW", "All output values"
            );
            AddJointsGroup(VisualJoint.LEFT,
                "X Value", "First input value",
                "Y Value", "Second input value",
                "Z Value", "Third input value",
                "W Value", "Fourth input value",
                "XYZW", "All input values"
            );
            Values.Add("0");
            Values.Add("x");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateVariable(elements, precision);
        }
    }
}