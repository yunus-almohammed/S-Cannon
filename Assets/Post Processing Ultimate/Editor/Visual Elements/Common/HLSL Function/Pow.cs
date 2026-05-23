using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Pow : VisualElement {

        public const string tooltip = "Returns the specified value raised to the specified power (P + LMB)";

        public Pow() {
            name = "Pow";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "X Value", "The specified value");
            AddJoint(VisualJoint.LEFT, 0, "Y Value", "The specified power");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}