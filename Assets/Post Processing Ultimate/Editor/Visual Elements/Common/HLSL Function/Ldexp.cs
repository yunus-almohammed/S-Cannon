using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Ldexp : VisualElement {

        public const string tooltip = "Returns the result of multiplying the specified value by two, raised to the power of the specified exponent";

        public Ldexp() {
            name = "Ldexp";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "X Value", "The specified value");
            AddJoint(VisualJoint.LEFT, 0, "Exp", "The specified exponent");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}