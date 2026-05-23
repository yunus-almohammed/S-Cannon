using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Max : VisualElement {

        public const string tooltip = "Selects the greater of x and y";

        public Max() {
            name = "Max";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "X Value", "The x input value");
            AddJoint(VisualJoint.LEFT, 0, "Y Value", "The y input value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}