using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Normalize : VisualElement {

        public const string tooltip = "Normalizes the specified floating-point vector according to x / length(x) (N + LMB)";

        public Normalize() {
            name = "Normalize";
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