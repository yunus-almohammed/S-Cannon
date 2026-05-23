using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Fwidth : VisualElement {

        public const string tooltip = "Returns the absolute value of the partial derivatives of the specified value (LAlt + F + LMB)";

        public Fwidth() {
            name = "Fwidth";
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