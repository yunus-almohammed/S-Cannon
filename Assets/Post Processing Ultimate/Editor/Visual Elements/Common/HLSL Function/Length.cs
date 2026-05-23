using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Length : VisualElement {

        public const string tooltip = "Returns the length of the specified floating-point vector (LAlt + L + LMB)";

        public Length() {
            name = "Length";
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