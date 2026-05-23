using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class TWO_PI : VisualElement {

        public const string tooltip = "Constant value: 6.28318530718";

        public TWO_PI() {
            name = "TWO_PI";
            tint = Utils.ElementsConstant;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "6.28318530718");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst();
        }
    }
}