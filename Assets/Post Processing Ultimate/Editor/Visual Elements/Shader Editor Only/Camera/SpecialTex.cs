using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class SpecialTex : VisualElement {

        public const string tooltip = "Special texture sampler";

        public SpecialTex() {
            name = "SpecialTex";
            tint = Utils.ElementsCamera;
            jointsOffset = 2;
            AddJointsGroup(VisualJoint.RIGHT,
                "Red", "Texture red channel",
                "Green", "Texture green channel",
                "Blue", "Texture blue channel",
                "Alpha", "Texture alpha channels",
                "RGBA", "All texture channels"
            );
            AddJointsGroup(VisualJoint.LEFT,
                "X axis", "Texture X coordinates",
                "Y axis", "Texture Y coordinates",
                "XY", "Both texture coordinates"
            );
            options.Add("_CameraGBufferTexture0");
            options.Add("_CameraGBufferTexture1");
            options.Add("_CameraGBufferTexture2");
            options.Add("_CameraGBufferTexture3");
            options.Add("_CameraReflectionsTexture");
            Values.Add(options[0]);
            CalculateHeight();
        }

        public override void Show() {
            base.Show();
            Values[0] = options[EditorGUI.Popup(new Rect(position.x, (position.y + 1 * (20) + 2), position.width, 16), options.IndexOf(Values[0]), options.ToArray())];
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            List<string> demands = new List<string> { 
                Generator.Demand(elements, joints[5], precision),
                Generator.Demand(elements, joints[6], precision),
                Generator.Demand(elements, joints[7], precision)
            };
            for (int i = 0; i < 5; ++i) {
                if (joints[7].Connected()) {
                    output.Add("SAMPLE_TEXTURE2D(" + Values[0] + ", sampler" + Values[0] + ", " + demands[2] + ")" + joints[i].component);
                } else {
                    output.Add("SAMPLE_TEXTURE2D(" + Values[0] + ", sampler" + Values[0] + ", " + precision + "2(" + demands[0] + "," + demands[1] + "))" + joints[i].component);
                }
            }
            return output;
        }
    }
}