using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Step : VisualElement {

        public const string tooltip = "Compares two values, returning 0 or 1 based on which value is greater";

        public Step() {
            name = "Step";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "X Value", "The first floating-point value to compare");
            AddJoint(VisualJoint.LEFT, 0, "Y Value", "The second floating-point value to compare");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}