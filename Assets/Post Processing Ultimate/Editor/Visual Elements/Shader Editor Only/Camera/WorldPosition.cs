using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class WorldPosition : VisualElement {

        public const string tooltip = "Global transform pixel position";

        public WorldPosition() {
            name = "WorldPosition";
            tint = Utils.ElementsCamera;
            AddJointsGroup(VisualJoint.RIGHT, 
                "X Value", "Global transform pixel X position",
                "Y Value", "Global transform pixel Y position",
                "Z Value", "Global transform pixel Z position",
                "XYZ", "Global transform pixel position"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            for (int i = 0; i < 4; ++i) {
                output.Add("(i.worldDirection * LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoordStereo)) + _WorldSpaceCameraPos)" + joints[i].component);
            }
            return output;
        }
    }
}