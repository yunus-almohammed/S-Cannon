using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class PI : VisualElement {

        public const string tooltip = "Constant value: 3.14159265359";

        public PI() {
            name = "PI";
            tint = Utils.ElementsConstant;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "3.14159265359");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst();
        }
    }
}