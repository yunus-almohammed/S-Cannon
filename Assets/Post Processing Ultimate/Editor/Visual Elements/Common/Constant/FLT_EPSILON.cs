using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class FLT_EPSILON : VisualElement {

        public const string tooltip = "Smallest positive number, such that 1.0 + FLT_EPSILON != 1.0: 1.192092896e-07";

        public FLT_EPSILON() {
            name = "FLT_EPSILON";
            tint = Utils.ElementsConstant;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "1.192092896e-07");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst();
        }
    }
}