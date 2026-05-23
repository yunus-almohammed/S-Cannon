using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Variable2 : VisualElement {

        public const string tooltip = "Double index variable";

        public Variable2() {
            name = "Variable2";
            tint = Utils.ElementsVariable;
            AddJointsGroup(VisualJoint.RIGHT, 
                "X Value", "First output value",
                "Y Value", "Second output value",
                "XY", "Both output values"
            );
            AddJointsGroup(VisualJoint.LEFT,
                "X Value", "First input value",
                "Y Value", "Second input value",
                "XY", "Both input values"
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