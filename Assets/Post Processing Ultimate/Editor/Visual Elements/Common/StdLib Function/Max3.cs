using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Max3 : VisualElement {

        public const string tooltip = "Selects the greatest of x, y and z";

        public Max3() {
            name = "Max3";
            tint = Utils.ElementsPPS;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "X Value", "The x input value");
            AddJoint(VisualJoint.LEFT, 0, "Y Value", "The y input value");
            AddJoint(VisualJoint.LEFT, 0, "Z Value", "The z input value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name);
        }
    }
}