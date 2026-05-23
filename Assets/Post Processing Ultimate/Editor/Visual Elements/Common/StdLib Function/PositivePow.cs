using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class PositivePow : VisualElement {

        public const string tooltip = "PositivePow remove this warning when you know the value is positive and avoid inf/NAN";

        public PositivePow() {
            name = "PositivePow";
            tint = Utils.ElementsPPS;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "X Value", "The specified value");
            AddJoint(VisualJoint.LEFT, 0, "Y Value", "The specified power");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name);
        }
    }
}