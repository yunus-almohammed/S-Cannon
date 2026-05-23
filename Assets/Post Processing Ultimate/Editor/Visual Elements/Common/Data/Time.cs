using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Time : VisualElement {

        public const string tooltip = "_Time";

        public Time() {
            name = "Time";
            tint = Utils.ElementsData;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "Time / 20",
                "Y Value", "Time",
                "Z Value", "Time * 2",
                "W Value", "Time * 3",
                "XYZW", "Time"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateData("_Time");
        }
    }
}