using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Ceil : VisualElement {

        public const string tooltip = "Returns the smallest integer value that is greater than or equal to the specified value";

        public Ceil() {
            name = "Ceil";
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