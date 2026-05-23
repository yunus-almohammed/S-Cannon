using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class _Bool : VisualElement {

        public const string tooltip = "Bool global variable";

        public _Bool() {
            name = "_Bool";
            tint = Utils.ElementsInput;
            AddJoint(VisualJoint.RIGHT, 1);
            options.Add("ZERO (0)");
            Values.Add("0");
            Values.Add("0");
            Values.Add("0");
            CalculateHeight();
        }

        public override void Show() {
            base.Show();
            HandleInputs(name.Substring(1));
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateInput(2);
        }
    }
}