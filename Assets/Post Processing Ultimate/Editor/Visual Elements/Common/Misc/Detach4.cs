using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Detach4 : VisualElement {

        public const string tooltip = "Detach size 4 value into single ones";

        public Detach4() {
            name = "Detach4";
            tint = Utils.ElementsMisc;
            AddJoint(VisualJoint.RIGHT, 1, "X", "Output value", ".x");
            AddJoint(VisualJoint.RIGHT, 1, "Y", "Output value", ".y");
            AddJoint(VisualJoint.RIGHT, 1, "Z", "Output value", ".z");
            AddJoint(VisualJoint.RIGHT, 1, "W", "Output value", ".w");
            AddJoint(VisualJoint.LEFT, 4, "XYZW", "Size 4 value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateDetach(elements, precision);
        }
    }
}