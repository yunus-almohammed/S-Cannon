using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Screen : VisualElement {

        public const string tooltip = "_ScreenParams";

        public Screen() {
            name = "Screen";
            tint = Utils.ElementsData;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "Width",
                "Y Value", "Height",
                "Z Value", "1 + 1 / width",
                "W Value", "1 + 1 / height",
                "XYZW", "Screen parameters"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateData("_ScreenParams");
        }
    }
}