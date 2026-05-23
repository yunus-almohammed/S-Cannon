using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Distance : VisualElement {

        public const string tooltip = "Returns a distance scalar between two vectors";

        public Distance() {
            name = "Distance";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "X Value", "The first floating-point vector to compare");
            AddJoint(VisualJoint.LEFT, 0, "Y Value", "The second floating-point vector to compare");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}