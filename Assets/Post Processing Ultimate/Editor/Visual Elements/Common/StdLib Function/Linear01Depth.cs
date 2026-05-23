using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Linear01Depth : VisualElement {

        public const string tooltip = "Handles orthographic projection correctly";

        public Linear01Depth() {
            name = "Linear01Depth";
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