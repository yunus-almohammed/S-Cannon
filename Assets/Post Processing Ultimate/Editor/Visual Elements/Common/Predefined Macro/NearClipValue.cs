using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class NearClipValue : VisualElement {

        public const string tooltip = "Defined to the value of near clipping plane. Direct3D-like platforms use 0.0 while OpenGL-like platforms use –1.0";

        public NearClipValue() {
            name = "NearClipValue";
            tint = Utils.ElementsPredefined;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "UNITY_NEAR_CLIP_VALUE");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst("UNITY_NEAR_CLIP_VALUE");
        }
    }
}