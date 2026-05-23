using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class CameraDepth : VisualElement {

        public const string tooltip = "Main texture depth";

        public CameraDepth() {
            name = "CameraDepth";
            tint = Utils.ElementsCamera;
            AddJoint(VisualJoint.RIGHT, 1, "Depth", "Texture depth value");
            AddJointsGroup(VisualJoint.LEFT,
                "X axis", "Texture X coordinates",
                "Y axis", "Texture Y coordinates",
                "XY", "Both texture coordinates"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            List<string> demands = new List<string> {
                Generator.Demand(elements, joints[1], precision),
                Generator.Demand(elements, joints[2], precision),
                Generator.Demand(elements, joints[3], precision)
            };
            for (int i = 0; i < 4; ++i) {
                if (joints[3].Connected()) {
                    output.Add("SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, " + demands[2] + ").x");
                } else {
                    output.Add("SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, " + precision + "2(" + demands[0] + "," + demands[1] + ")).x");
                }
            }
            return output;
        }
    }
}