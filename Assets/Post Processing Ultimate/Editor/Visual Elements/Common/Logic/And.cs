using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class And : VisualElement {

        public const string tooltip = "Handles AND statement";

        public And() {
            name = "And";
            tint = Utils.ElementsLogic;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "X && Y");
            AddJoint(VisualJoint.LEFT, 0, "X Value", "The first value");
            AddJoint(VisualJoint.LEFT, 0, "Y Value", "The second value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            List<string> demands = new List<string> {
                Generator.Demand(elements, joints[1], destinedSize, precision),
                Generator.Demand(elements, joints[2], destinedSize, precision)
            };
            output.Add("(" + demands[0] + " && " + demands[1] + ")");
            return output;
        }
    }
}