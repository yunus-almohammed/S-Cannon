using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Exp : VisualElement {

        public const string tooltip = "Returns the base-e exponential, or ex, of the specified value (E + LMB)";

        public Exp() {
            name = "Exp";
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