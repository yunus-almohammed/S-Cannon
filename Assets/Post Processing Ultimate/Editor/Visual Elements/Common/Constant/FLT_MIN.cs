using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class FLT_MIN : VisualElement {

        public const string tooltip = "Minimum representable positive floating-point number: 1.175494351e-38";

        public FLT_MIN() {
            name = "FLT_MIN";
            tint = Utils.ElementsConstant;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "1.175494351e-38");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst();
        }
    }
}