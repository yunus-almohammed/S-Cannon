using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Compare : VisualElement {

        public const string tooltip = "Compares two values";

        public Compare() {
            name = "Compare";
            tint = Utils.ElementsLogic;
            AddJoint(VisualJoint.RIGHT, 0, "X > Y", "X greater than Y");
            AddJoint(VisualJoint.RIGHT, 0, "X >= Y", "X greater or equal to Y");
            AddJoint(VisualJoint.RIGHT, 0, "X < Y", "X smaller than Y");
            AddJoint(VisualJoint.RIGHT, 0, "X <= Y", "X smaller or equal to Y");
            AddJoint(VisualJoint.RIGHT, 0, "X == Y", "X equal to Y");
            AddJoint(VisualJoint.RIGHT, 0, "X != Y", "X not equal to Y");
            AddJoint(VisualJoint.LEFT, 0, "X Value", "The first value");
            AddJoint(VisualJoint.LEFT, 0, "Y Value", "The second value");
            AddJoint(VisualJoint.LEFT, 0, "True", "True value");
            AddJoint(VisualJoint.LEFT, 0, "False", "False value");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            List<string> demands = new List<string> {
                Generator.Demand(elements, joints[6], destinedSize, precision),
                Generator.Demand(elements, joints[7], destinedSize, precision),
                Generator.Demand(elements, joints[8], destinedSize, precision),
                Generator.Demand(elements, joints[9], destinedSize, precision)
            };
            output.Add("(" + demands[0] + " > " + demands[1] + " ? " + demands[2] + " : " + demands[3] + ")");
            output.Add("(" + demands[0] + " >= " + demands[1] + " ? " + demands[2] + " : " + demands[3] + ")");
            output.Add("(" + demands[0] + " < " + demands[1] + " ? " + demands[2] + " : " + demands[3] + ")");
            output.Add("(" + demands[0] + " <= " + demands[1] + " ? " + demands[2] + " : " + demands[3] + ")");
            output.Add("(" + demands[0] + " == " + demands[1] + " ? " + demands[2] + " : " + demands[3] + ")");
            output.Add("(" + demands[0] + " != " + demands[1] + " ? " + demands[2] + " : " + demands[3] + ")");
            return output;
        }
    }
}