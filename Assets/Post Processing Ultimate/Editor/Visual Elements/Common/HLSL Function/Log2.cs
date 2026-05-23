using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Log2 : VisualElement {

        public const string tooltip = "Returns the base-2 logarithm of the specified value";

        public Log2() {
            name = "Log2";
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