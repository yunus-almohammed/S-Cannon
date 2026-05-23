using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class EPSILON : VisualElement {

        public const string tooltip = "Constant value: 1.0e-4";

        public EPSILON() {
            name = "EPSILON";
            tint = Utils.ElementsConstant;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "1.0e-4");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst();
        }
    }
}