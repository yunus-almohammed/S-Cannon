using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class FOUR_PI : VisualElement {

        public const string tooltip = "Constant value: 12.56637061436";

        public FOUR_PI() {
            name = "FOUR_PI";
            tint = Utils.ElementsConstant;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "12.56637061436");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst();
        }
    }
}