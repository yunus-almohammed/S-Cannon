using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class CosTime : VisualElement {

        public const string tooltip = "_CosTime";

        public CosTime() {
            name = "CosTime";
            tint = Utils.ElementsData;
            AddJointsGroup(VisualJoint.RIGHT, 
                "X Value", "Cosine of Time / 20",
                "Y Value", "Cosine of Time",
                "Z Value", "Cosine of Time * 2",
                "W Value", "Cosine of Time * 3",
                "XYZW", "Cosine of Time"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateData("_CosTime");
        }
    }
}