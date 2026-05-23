using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Value1 : VisualElement {

        public const string tooltip = "Single index value (1 + LMB)";

        public Value1() {
            name = "Value1";
            tint = Utils.ElementsValue;
            AddJoint(VisualJoint.RIGHT, 1);
            Values.Add("0.0");
            CalculateHeight();
        }

        public override void Show() {
            base.Show();
            HandleValue(1);
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateValue(precision);
        }
    }
}