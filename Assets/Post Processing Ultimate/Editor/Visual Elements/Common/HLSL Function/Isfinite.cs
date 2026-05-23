using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Isfinite : VisualElement {

        public const string tooltip = "Determines if the specified floating-point value is finite";

        public Isfinite() {
            name = "Isfinite";
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