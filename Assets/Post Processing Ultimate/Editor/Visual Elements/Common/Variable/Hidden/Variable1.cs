using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Variable1 : VisualElement {

        public const string tooltip = "Single index variable";

        public Variable1() {
            name = "Variable1";
            tint = Utils.ElementsVariable;
            AddJoint(VisualJoint.RIGHT, 1, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 1, "Input", "Input value");
            Values.Add("0");
            Values.Add("x");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateVariable(elements, precision);
        }
    }
}