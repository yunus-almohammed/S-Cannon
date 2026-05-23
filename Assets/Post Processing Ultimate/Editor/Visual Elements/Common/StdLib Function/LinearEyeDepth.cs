using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class LinearEyeDepth : VisualElement {

        public const string tooltip = "Computes linear eye depth space from value";

        public LinearEyeDepth() {
            name = "LinearEyeDepth";
            tint = Utils.ElementsPPS;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "Value", "Input value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name);
        }
    }
}