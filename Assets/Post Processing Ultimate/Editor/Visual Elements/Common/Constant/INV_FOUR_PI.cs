using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class INV_FOUR_PI : VisualElement {

        public const string tooltip = "Constant value: 0.07957747155";

        public INV_FOUR_PI() {
            name = "INV_FOUR_PI";
            tint = Utils.ElementsConstant;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "0.07957747155");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst();
        }
    }
}