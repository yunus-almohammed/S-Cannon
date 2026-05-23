using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Exp2 : VisualElement {

        public const string tooltip = "Returns the base 2 exponential, or 2x, of the specified value (LAlt + E + LMB)";

        public Exp2() {
            name = "Exp2";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "Value", "Input value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}