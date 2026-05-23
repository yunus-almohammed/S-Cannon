using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Saturate : VisualElement {

        public const string tooltip = "Clamps the specified value within the range of 0 to 1";

        public Saturate() {
            name = "Saturate";
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