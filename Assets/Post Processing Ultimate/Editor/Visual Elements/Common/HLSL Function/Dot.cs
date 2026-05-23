using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Dot : VisualElement {

        public const string tooltip = "Returns the dot product of two vectors (LAlt + D + LMB)";

        public Dot() {
            name = "Dot";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "X Value", "The first vector");
            AddJoint(VisualJoint.LEFT, 0, "Y Value", "The second vector");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}