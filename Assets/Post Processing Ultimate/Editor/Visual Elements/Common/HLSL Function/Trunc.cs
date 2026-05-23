using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Trunc : VisualElement {

        public const string tooltip = "Truncates a floating-point value to the integer component (LAlt + T + LMB)";

        public Trunc() {
            name = "Trunc";
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