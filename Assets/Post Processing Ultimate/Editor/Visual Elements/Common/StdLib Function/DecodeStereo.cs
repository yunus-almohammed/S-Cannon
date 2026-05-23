using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class DecodeStereo : VisualElement {

        public const string tooltip = "Decodes normals stored in _CameraDepthNormalsTexture";

        public DecodeStereo() {
            name = "DecodeStereo";
            tint = Utils.ElementsPPS;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "X output value",
                "Y Value", "Y output value",
                "Z Value", "Z output value",
                "XYZ", "Output value"
            );
            AddJointsGroup(VisualJoint.LEFT,
                "X Value", "X input value",
                "Y Value", "Y input value",
                "Z Value", "Z input value",
                "W Value", "W input value",
                "XYZW", "Input value"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            List<string> demands = new List<string> {
                Generator.Demand(elements, joints[4], precision),
                Generator.Demand(elements, joints[5], precision),
                Generator.Demand(elements, joints[6], precision),
                Generator.Demand(elements, joints[7], precision),
                Generator.Demand(elements, joints[8], precision)
            };
            for (int i = 0; i < 4; ++i) {
                if (joints[8].Connected()) {
                    output.Add("DecodeViewNormalStereo(" + demands[4] + ")" + joints[i].component);
                } else {
                    output.Add("DecodeViewNormalStereo(" + precision + "4(" + demands[0] + "," + demands[1] + "," + demands[2] + "," + demands[3] + ")" + joints[i].component);
                }
            }
            return output;
        }
    }
}