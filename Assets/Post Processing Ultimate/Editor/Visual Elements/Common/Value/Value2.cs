using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Value2 : VisualElement {

        public const string tooltip = "Double index value (2 + LMB)";

        public Value2() {
            name = "Value2";
            tint = Utils.ElementsValue;
            AddJointsGroup(VisualJoint.RIGHT,
                "", "",
                "", "",
                "XY", "All values"
            );
            Values.Add("0.0");
            Values.Add("0.0");
            CalculateHeight();
        }

        public override void Show() {
            base.Show();
            HandleValue(2);
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateValue(precision);
        }
    }
}