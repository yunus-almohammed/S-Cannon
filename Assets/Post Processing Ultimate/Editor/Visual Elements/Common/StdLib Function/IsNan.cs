using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class IsNan : VisualElement {

        public const string tooltip = "Determines if the specified value is NAN";

        public IsNan() {
            name = "IsNan";
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