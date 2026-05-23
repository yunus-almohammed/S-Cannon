using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class If : VisualElement {

        public const string tooltip = "Checks if statement is true or false and returns values (I + LMB)";

        public If() {
            name = "If";
            tint = Utils.ElementsLogic;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "If output");
            AddJoint(VisualJoint.LEFT, 0, "Statement", "True or false expression");
            AddJoint(VisualJoint.LEFT, 0, "True", "True value");
            AddJoint(VisualJoint.LEFT, 0, "False", "False value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            List<string> demands = new List<string> {
                Generator.Demand(elements, joints[1], destinedSize, precision),
                Generator.Demand(elements, joints[2], destinedSize, precision),
                Generator.Demand(elements, joints[3], destinedSize, precision)
            };
            output.Add("(" + demands[0] + " ? " + demands[1] + " : " + demands[2] + ")");
            return output;
        }
    }
}