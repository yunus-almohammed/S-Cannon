using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Cross : VisualElement {

        public const string tooltip = "Returns the cross product of two floating-point, 3D vectors";

        public Cross() {
            name = "Cross";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 3, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 3, "X Value", "First floating-point, 3D vector");
            AddJoint(VisualJoint.LEFT, 3, "Y Value", "Second floating-point, 3D vector");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}