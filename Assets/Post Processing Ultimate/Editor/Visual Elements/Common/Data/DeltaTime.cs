using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class DeltaTime : VisualElement {

        public const string tooltip = "unity_DeltaTime";

        public DeltaTime() {
            name = "DeltaTime";
            tint = Utils.ElementsData;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "Delta time",
                "Y Value", "1 / delta time",
                "Z Value", "Smooth delta time",
                "W Value", "1 / smooth delta time",
                "XYZW", "Delta time"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateData("unity_DeltaTime");
        }
    }
}