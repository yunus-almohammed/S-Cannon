using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class TempTex : VisualElement {

        public const string tooltip = "Temp texture sampler";

        public TempTex() {
            name = "TempTex";
            tint = Utils.ElementsCamera;
            jointsOffset = 2;
            AddJointsGroup(VisualJoint.RIGHT,
                "Red", "Texture red channel",
                "Green", "Texture green channel",
                "Blue", "Texture blue channel",
                "RGB", "All texture channels"
            );
            AddJointsGroup(VisualJoint.LEFT,
                "X axis", "Texture X coordinates",
                "Y axis", "Texture Y coordinates",
                "XY", "Both texture coordinates"
            );
            CalculateHeight();
            Values.Add("");
        }

        public override void Show() {
            base.Show();
            options = renderQueue.inputOptions.Skip(1).Prepend("ZERO (0)").ToList(); 
            Values[0] = options[EditorGUI.Popup(new Rect(position.x, (position.y + 1 * (20) + 2), position.width, 16), Utils.IndexIfAvailable(options, Values[0]), options.ToArray())];
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            List<string> demands = new List<string> {
                Generator.Demand(elements, joints[4], precision),
                Generator.Demand(elements, joints[5], precision),
                Generator.Demand(elements, joints[6], precision)
            };
            for (int i = 0; i < 4; ++i) {
                if (!Values[0].Contains("(0)")) {
                    if (joints[6].Connected()) {
                        output.Add("SAMPLE_TEXTURE2D(_" + Values[0] + ", sampler_" + Values[0] + ", " + demands[2] + ")" + joints[i].component);
                    } else {
                        output.Add("SAMPLE_TEXTURE2D(_" + Values[0] + ", sampler_" + Values[0] + ", " + precision + "2(" + demands[0] + "," + demands[1] + "))" + joints[i].component);
                    }
                } else {
                    output.Add("0.0");
                }
            }
            return output;
        }
    }
}