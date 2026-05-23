using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Ortho : VisualElement {

        public const string tooltip = "unity_OrthoParams";

        public Ortho() {
            name = "Ortho";
            tint = Utils.ElementsData;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "Width",
                "Y Value", "Height",
                "Z Value", "Unused",
                "W Value", "Is ortographic?",
                "XYZW", "Ortographic parameters"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateData("unity_OrthoParams");
        }
    }
}