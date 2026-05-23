using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class DefaultUV : VisualElement {

        public const string tooltip = "Default texture coordinates";

        public DefaultUV() {
            name = "DefaultUV";
            tint = Utils.ElementsCamera;
            AddJointsGroup(VisualJoint.RIGHT,
                "X axis", "Texture X coordinates",
                "Y axis", "Texture Y coordinates",
                "XY", "Both texture coordinates"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            for (int i = 0; i < joints.Count; ++i) {
                output.Add("i.texcoord" + joints[i].component);
            }
            return output;
        }
    }
}