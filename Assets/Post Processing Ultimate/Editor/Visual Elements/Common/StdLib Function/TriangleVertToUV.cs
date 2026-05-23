using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class TriangleVertToUV : VisualElement {

        public const string tooltip = "Vertex manipulation";

        public TriangleVertToUV() {
            name = "TriangleVertToUV";
            tint = Utils.ElementsPPS;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "X output value",
                "Y Value", "Y output value",
                "XY", "Output value"
            );
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
                Generator.Demand(elements, joints[3], precision),
                Generator.Demand(elements, joints[4], precision),
                Generator.Demand(elements, joints[5], precision)
            };
            for (int i = 0; i < 3; ++i) {
                if (joints[5].Connected()) {
                    output.Add("TransformTriangleVertexToUV(" + demands[2] + joints[i].component);
                } else {
                    output.Add("TransformTriangleVertexToUV(" + precision + "2(" + demands[0] + "," + demands[1] + ")" + joints[i].name);
                }
            }
            return output;
        }
    }
}