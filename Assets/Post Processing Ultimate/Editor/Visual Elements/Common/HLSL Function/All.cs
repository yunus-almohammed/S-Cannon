using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class All : VisualElement {

        public const string tooltip = "Determines if all components of the specified value are non-zero";

        public All() {
            name = "All";
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