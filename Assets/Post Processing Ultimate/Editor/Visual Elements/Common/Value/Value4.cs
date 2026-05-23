using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Value4 : VisualElement {

        public const string tooltip = "Quadruple index value (4 + LMB)";

        public Value4() {
            name = "Value4";
            tint = Utils.ElementsValue;
            AddJointsGroup(VisualJoint.RIGHT,
                "", "",
                "", "",
                "", "",
                "", "",
                "XYZW", "All values"
            );
            Values.Add("0.0");
            Values.Add("0.0");
            Values.Add("0.0");
            Values.Add("0.0");
            CalculateHeight();
        }

        public override void Show() {
            base.Show();
            HandleValue(4);
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateValue(precision);
        }
    }
}