using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class TexelSize : VisualElement {

        public const string tooltip = "Texture pixel size";

        public TexelSize() {
            name = "TexelSize";
            tint = Utils.ElementsCamera;
            jointsOffset = 2;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "Texel X value",
                "Y Value", "Texel Y value",
                "Z Value", "Texel Z value",
                "W Value", "Texel W value",
                "XYZW", "Texel size"
            );
            Values.Add("");
            CalculateHeight();
        }

        public override void Show() {
            base.Show();
            options.Clear();
            names.Clear();
            options.Add("-2");
            names.Add("_MainTex");
            if (Generator.DepthOrWorld(allElements)) {
                options.Add("-3");
                names.Add("_CameraDepthTexture");
            }
            names.AddRange(properties.Where(x => x.name == "Texture2D" || x.name == "Spline").Select(x => x.Values[0]));
            options.AddRange(properties.Where(x => x.name == "Texture2D" || x.name == "Spline").Select(x => x.unique.ToString()));
            Values[0] = options[EditorGUI.Popup(new Rect(position.x, position.y + 1 * 20 + 2, position.width, 16), BackwardCopatibilityIndexIfAvailable(), names.ToArray())];
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            for (int i = 0; i < 5; ++i) {
                output.Add(Utils.UniqueToName(properties, int.Parse(Values[0]), Utils.texelMap) + "_TexelSize" + joints[i].component);
            }
            return output;
        }

        private int BackwardCopatibilityIndexIfAvailable() {
            if (!options.Contains(Values[0])) {
                return Utils.IndexIfAvailable(names, Values[0]);
            }
            return Utils.IndexIfAvailable(options, Values[0]);
        }
    }
}