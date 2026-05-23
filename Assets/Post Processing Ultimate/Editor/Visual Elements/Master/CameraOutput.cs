using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class CameraOutput : VisualElement {

        public const string tooltip = "Single pixel color value";

        public CameraOutput() {
            name = "CameraOutput";
            tint = Utils.ElementsMaster;
            AddJointsGroup(VisualJoint.LEFT,
                "Red", "Pixel red channel",
                "Green", "Pixel green channel",
                "Blue", "Pixel blue channel",
                "RGB", "All pixel channels"
            );
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateMaster(elements, precision, "CameraOutput");
        }
    }
}