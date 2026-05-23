using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Append3 : VisualElement {

        public const string tooltip = "Appends single inputs into size 3 output";

        public Append3() {
            name = "Append3";
            tint = Utils.ElementsMisc;
            AddJoint(VisualJoint.RIGHT, 3, "XYZ", "Appended value");
            AddJoint(VisualJoint.LEFT, 1, "X", "Input value");
            AddJoint(VisualJoint.LEFT, 1, "Y", "Input value");
            AddJoint(VisualJoint.LEFT, 1, "Z", "Input value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateAppend(elements, precision);
        }
    }
}