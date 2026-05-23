using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Value3 : VisualElement {

        public const string tooltip = "Triple index value (3 + LMB)";

        public Value3() {
            name = "Value3";
            tint = Utils.ElementsValue;
            AddJointsGroup(VisualJoint.RIGHT,
                "", "",
                "", "",
                "", "",
                "XYZ", "All values"
            );
            Values.Add("0.0");
            Values.Add("0.0");
            Values.Add("0.0");
            CalculateHeight();
        }

        public override void Show() {
            base.Show();
            HandleValue(3);
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateValue(precision);
        }
    }
}