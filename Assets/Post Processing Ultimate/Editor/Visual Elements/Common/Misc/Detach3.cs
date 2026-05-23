using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Detach3 : VisualElement {

        public const string tooltip = "Detach size 3 value into single ones";

        public Detach3() {
            name = "Detach3";
            tint = Utils.ElementsMisc;
            AddJoint(VisualJoint.RIGHT, 1, "X", "Output value", ".x");
            AddJoint(VisualJoint.RIGHT, 1, "Y", "Output value", ".y");
            AddJoint(VisualJoint.RIGHT, 1, "Z", "Output value", ".z");
            AddJoint(VisualJoint.LEFT, 3, "XYZ", "Size 3 value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateDetach(elements, precision);
        }
    }
}