using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Detach2 : VisualElement {

        public const string tooltip = "Detach size 2 value into single ones";

        public Detach2() {
            name = "Detach2";
            tint = Utils.ElementsMisc;
            AddJoint(VisualJoint.RIGHT, 1, "X", "Output value", ".x");
            AddJoint(VisualJoint.RIGHT, 1, "Y", "Output value", ".y");
            AddJoint(VisualJoint.LEFT, 2, "XY", "Size 2 value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateDetach(elements, precision);
        }
    }
}