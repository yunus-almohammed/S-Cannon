using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Rcp : VisualElement {

        public const string tooltip = "Returns 1.0 / value";

        public Rcp() {
            name = "Rcp";
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