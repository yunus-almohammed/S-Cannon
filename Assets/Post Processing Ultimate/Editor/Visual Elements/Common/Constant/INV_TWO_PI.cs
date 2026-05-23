using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class INV_TWO_PI : VisualElement {

        public const string tooltip = "Constant value: 0.15915494309";

        public INV_TWO_PI() {
            name = "INV_TWO_PI";
            tint = Utils.ElementsConstant;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "0.15915494309");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst();
        }
    }
}