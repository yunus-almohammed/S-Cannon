using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class FLT_MAX : VisualElement {

        public const string tooltip = "Maximum representable floating-point number: 3.402823466e+38";

        public FLT_MAX() {
            name = "FLT_MAX";
            tint = Utils.ElementsConstant;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "3.402823466e+38");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst();
        }
    }
}