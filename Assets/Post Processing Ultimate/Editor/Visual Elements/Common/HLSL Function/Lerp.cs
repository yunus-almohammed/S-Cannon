using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Lerp : VisualElement {

        public const string tooltip = "Performs a linear interpolation (L + LMB)";

        public Lerp() {
            name = "Lerp";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "X Value", "The first-floating point value");
            AddJoint(VisualJoint.LEFT, 0, "Y Value", "The second-floating point value");
            AddJoint(VisualJoint.LEFT, 0, "S Value", "A value that linearly interpolates between the x parameter and the y parameter");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}