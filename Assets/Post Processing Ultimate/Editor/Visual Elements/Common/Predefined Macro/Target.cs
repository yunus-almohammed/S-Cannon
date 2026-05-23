using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Target : VisualElement {

        public const string tooltip = "Defined to a numeric value that matches the Shader target compilation model";

        public Target() {
            name = "Target";
            tint = Utils.ElementsPredefined;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "SHADER_TARGET");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst("SHADER_TARGET");
        }
    }
}