using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class SafeHDR : VisualElement {

        public const string tooltip = "Clamps HDR value within a safe range";

        public SafeHDR() {
            name = "SafeHDR";
            tint = Utils.ElementsPPS;
            AddJointsGroup(VisualJoint.RIGHT,
                "Red", "Red output",
                "Green", "Green output",
                "Blue", "Blue output",
                "Alpha", "Alpha output",
                "Color", "Color output"
            );
            AddJointsGroup(VisualJoint.LEFT, 
                "Red", "Red input",
                "Green", "Green input",
                "Blue", "Blue input",
                "Alpha", "Alpha input",
                "Color", "Color input"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            List<string> demands = new List<string> {
                Generator.Demand(elements, joints[5], precision),
                Generator.Demand(elements, joints[6], precision),
                Generator.Demand(elements, joints[7], precision),
                Generator.Demand(elements, joints[8], precision),
                Generator.Demand(elements, joints[9], precision)
            };
            for (int i = 0; i < 5; ++i) {
                if (joints[9].Connected()) {
                    output.Add("SafeHDR(" + demands[4] + joints[i].component);
                } else {
                    output.Add("SafeHDR(" + precision + "4(" + demands[0] + "," + demands[1] + "," + demands[2] + "," + demands[3] + ")" + joints[i].component);
                }
            }
            return output;
        }
    }
}