using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Radians : VisualElement {

        public const string tooltip = "Converts the specified value from degrees to radians";

        public Radians() {
            name = "Radians";
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