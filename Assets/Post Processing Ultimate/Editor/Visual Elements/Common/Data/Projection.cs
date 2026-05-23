using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Projection : VisualElement {

        public const string tooltip = "_ProjectionParams";

        public Projection() {
            name = "Projection";
            tint = Utils.ElementsData;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "1 (-1 flipped)",
                "Y Value", "Near",
                "Z Value", "Far",
                "W Value", "1 / far",
                "XYZW", "Projection parameters"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateData("_ProjectionParams");
        }
    }
}