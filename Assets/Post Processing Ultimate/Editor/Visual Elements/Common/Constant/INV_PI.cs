using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class INV_PI : VisualElement {

        public const string tooltip = "Constant value: 0.31830988618";

        public INV_PI() {
            name = "INV_PI";
            tint = Utils.ElementsConstant;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "0.31830988618");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst();
        }
    }
}