using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Clamp : VisualElement {

        public const string tooltip = "Clamps the specified value to the specified minimum and maximum range (LAlt + C + LMB)";

        public Clamp() {
            name = "Clamp";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "Value", "Input value");
            AddJoint(VisualJoint.LEFT, 0, "Min", "The specified minimum range");
            AddJoint(VisualJoint.LEFT, 0, "Max", "The specified maximum range");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}