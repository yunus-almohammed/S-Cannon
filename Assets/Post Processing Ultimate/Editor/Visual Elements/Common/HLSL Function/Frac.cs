using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Frac : VisualElement {

        public const string tooltip = "Returns the fractional (or decimal) part of x, which is greater than or equal to 0 and less than 1";

        public Frac() {
            name = "Frac";
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