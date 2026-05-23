using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class WorldSpace : VisualElement {

        public const string tooltip = "_WorldSpaceCameraPos";

        public WorldSpace() {
            name = "WorldSpace";
            tint = Utils.ElementsData;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "World space camera X position",
                "Y Value", "World space camera Y position",
                "Z Value", "World space camera Z position",
                "XYZ", "World space camera position"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateData("_WorldSpaceCameraPos");
        }
    }
}