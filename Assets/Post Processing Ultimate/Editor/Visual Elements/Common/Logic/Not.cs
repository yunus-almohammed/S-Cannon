using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Not : VisualElement {

        public const string tooltip = "Handles NOT statement";

        public Not() {
            name = "Not";
            tint = Utils.ElementsLogic;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "!Value");
            AddJoint(VisualJoint.LEFT, 0, "Value", "The input value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            List<string> demands = new List<string> {
                Generator.Demand(elements, joints[1], destinedSize, precision),
            };
            output.Add("!" + demands[0]);
            return output;
        }
    }
}