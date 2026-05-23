using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class StartsAtTop : VisualElement {

        public const string tooltip = "Always defined with value of 1 or 0. A value of 1 is on platforms where Texture V coordinate is 0 at the \"top\" of the Texture. Direct3D-like platforms use value of 1; OpenGL-like platforms use value of 0";

        public StartsAtTop() {
            name = "StartsAtTop";
            tint = Utils.ElementsPredefined;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "UNITY_UV_STARTS_AT_TOP");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst("UNITY_UV_STARTS_AT_TOP");
        }
    }
}