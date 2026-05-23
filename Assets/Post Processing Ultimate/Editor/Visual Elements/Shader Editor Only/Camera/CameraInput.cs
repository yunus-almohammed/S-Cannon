using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class CameraInput : VisualElement {

        public const string tooltip = "Main texture sampler";

        public CameraInput() {
            name = "CameraInput";
            tint = Utils.ElementsCamera;
            AddJointsGroup(VisualJoint.RIGHT,
                "Red", "Texture red channel",
                "Green", "Texture green channel",
                "Blue", "Texture blue channel",
                "RGB", "All texture channels"
            );
            joints[3].component = ".xyz";
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
                Generator.Demand(elements, joints[4], precision),
                Generator.Demand(elements, joints[5], precision),
                Generator.Demand(elements, joints[6], precision)
            };
            for (int i = 0; i < 4; ++i) {
                if (joints[6].Connected()) {
                    output.Add("SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, " + demands[2] + ")" + joints[i].component);
                } else {
                    output.Add("SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, " + precision + "2(" + demands[0] + "," + demands[1] + "))" + joints[i].component);
                }
            }
            return output;
        }
    }
}