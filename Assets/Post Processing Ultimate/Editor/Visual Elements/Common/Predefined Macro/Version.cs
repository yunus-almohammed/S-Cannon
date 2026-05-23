using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Version : VisualElement {

        public const string tooltip = "Contains the numeric value of the Unity version";

        public Version() {
            name = "Version";
            tint = Utils.ElementsPredefined;
            AddJoint(VisualJoint.RIGHT, 1, "Value", "UNITY_VERSION");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst("UNITY_VERSION");
        }
    }
}