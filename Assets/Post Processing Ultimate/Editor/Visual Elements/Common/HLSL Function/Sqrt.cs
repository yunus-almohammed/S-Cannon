using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Sqrt : VisualElement {

        public const string tooltip = "Returns the square root of the specified floating-point value, per component";

        public Sqrt() {
            name = "Sqrt";
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