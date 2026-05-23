using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Append4 : VisualElement {

        public const string tooltip = "Appends single inputs into size 4 output";

        public Append4() {
            name = "Append4";
            tint = Utils.ElementsMisc;
            AddJoint(VisualJoint.RIGHT, 4, "XYZW", "Appended value");
            AddJoint(VisualJoint.LEFT, 1, "X", "Input value");
            AddJoint(VisualJoint.LEFT, 1, "Y", "Input value");
            AddJoint(VisualJoint.LEFT, 1, "Z", "Input value");
            AddJoint(VisualJoint.LEFT, 1, "W", "Input value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateAppend(elements, precision);
        }
    }
}