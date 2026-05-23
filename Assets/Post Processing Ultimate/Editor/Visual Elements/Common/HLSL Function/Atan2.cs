using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Atan2 : VisualElement {

        public const string tooltip = "Returns the arctangent of two values (x,y)";

        public Atan2() {
            name = "Atan2";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "X Value", "The x value");
            AddJoint(VisualJoint.LEFT, 0, "Y Value", "The y value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}