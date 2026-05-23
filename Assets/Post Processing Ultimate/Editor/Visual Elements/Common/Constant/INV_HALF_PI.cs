using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class INV_HALF_PI : VisualElement {

        public const string tooltip = "Constant value: 0.636619772367";

        public INV_HALF_PI() {
            name = "INV_HALF_PI";
            tint = Utils.ElementsConstant;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "0.636619772367");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst();
        }
    }
}