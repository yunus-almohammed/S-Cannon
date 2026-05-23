using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class ZBuffer : VisualElement {

        public const string tooltip = "_ZBufferParams";

        public ZBuffer() {
            name = "ZBuffer";
            tint = Utils.ElementsData;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "1 - far / near",
                "Y Value", "Far / near",
                "Z Value", "X / far",
                "W Value", "Y / far",
                "XYZW", "Z buffer parameters"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateData("_ZBufferParams");
        }
    }
}