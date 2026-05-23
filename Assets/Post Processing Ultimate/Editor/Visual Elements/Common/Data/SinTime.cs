using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class SinTime : VisualElement {

        public const string tooltip = "_SinTime";

        public SinTime() {
            name = "SinTime";
            tint = Utils.ElementsData;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "Sine of Time / 20",
                "Y Value", "Sine of Time",
                "Z Value", "Sine of Time * 2",
                "W Value", "Sine of Time * 3",
                "XYZW", "Sine of Time"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateData("_SinTime");
        }
    }
}