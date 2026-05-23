using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Variable3 : VisualElement {

        public const string tooltip = "Triple index variable";

        public Variable3() {
            name = "Variable3";
            tint = Utils.ElementsVariable;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "First output value",
                "Y Value", "Second output value",
                "Z Value", "Third output value",
                "XYZ", "All output values"
            );
            AddJointsGroup(VisualJoint.LEFT,
                "X Value", "First input value",
                "Y Value", "Second input value",
                "Z Value", "Third input value",
                "XYZ", "All input values"
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