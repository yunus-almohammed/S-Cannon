using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Luminance : VisualElement {

        public const string tooltip = "unity_ColorSpaceLuminance";

        public Luminance() {
            name = "Luminance";
            tint = Utils.ElementsData;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "Color space luminance X value",
                "Y Value", "Color space luminance Y value",
                "Z Value", "Color space luminance Z value",
                "W Value", "Color space luminance W value",
                "XYZW", "Color space luminance"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateData("unity_ColorSpaceLuminance");
        }
    }
}