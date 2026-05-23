using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class HALF_MAX : VisualElement {

        public const string tooltip = "Constant value: 65504.0";

        public HALF_MAX() {
            name = "HALF_MAX";
            tint = Utils.ElementsConstant;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "65504.0");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst();
        }
    }
}