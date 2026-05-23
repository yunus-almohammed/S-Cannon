using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Append2 : VisualElement {

        public const string tooltip = "Appends single inputs into size 2 output";

        public Append2() {
            name = "Append2";
            tint = Utils.ElementsMisc;
            AddJoint(VisualJoint.RIGHT, 2, "XY", "Appended value");
            AddJoint(VisualJoint.LEFT, 1, "X", "Input value");
            AddJoint(VisualJoint.LEFT, 1, "Y", "Input value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateAppend(elements, precision);
        }
    }
}