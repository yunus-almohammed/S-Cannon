using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class HALF_PI : VisualElement {

        public const string tooltip = "Constant value: 1.57079632679";

        public HALF_PI() {
            name = "HALF_PI";
            tint = Utils.ElementsConstant;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "1.57079632679");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst();
        }
    }
}