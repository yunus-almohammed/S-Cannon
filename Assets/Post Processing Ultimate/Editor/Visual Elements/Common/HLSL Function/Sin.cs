using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Sin : VisualElement {

        public const string tooltip = "Returns the sine of the specified value (LAlt + S + LMB)";

        public Sin() {
            name = "Sin";
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