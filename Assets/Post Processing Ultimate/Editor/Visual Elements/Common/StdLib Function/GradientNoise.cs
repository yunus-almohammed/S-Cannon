using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class GradientNoise : VisualElement {

        public const string tooltip = "Interleaved gradient function from Jimenez 2014";

        public GradientNoise() {
            name = "GradientNoise";
            tint = Utils.ElementsPPS;
            AddJoint(VisualJoint.RIGHT, 1, "Output", "Output value");
            AddJointsGroup(VisualJoint.LEFT,
                "X Value", "X input value",
                "Y Value", "Y input value",
                "XY", "Input value"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            List<string> demands = new List<string> {
                Generator.Demand(elements, joints[1], precision),
                Generator.Demand(elements, joints[2], precision),
                Generator.Demand(elements, joints[3], precision)
            };
            if (joints[3].Connected()) {
                output.Add("GradientNoise(" + demands[2] + ")");
            } else {
                output.Add("GradientNoise(" + precision + "2(" + demands[0] + "," + demands[1] + "))");
            }
            return output;
        }
    }
}