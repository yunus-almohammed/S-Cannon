using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class Checker : VisualElement {

        public const string tooltip = "Checks if destined macro is true or false";

        public Checker() {
            name = "Checker";
            tint = Utils.ElementsPredefined;
            jointsOffset = 2;
            AddJoint(VisualJoint.RIGHT, 1, "Output", "True or false");
            options.Add("SHADER_API_D3D9");
            options.Add("SHADER_API_D3D11");
            options.Add("SHADER_API_GLCORE");
            options.Add("SHADER_API_GLES");
            options.Add("SHADER_API_GLES3");
            options.Add("SHADER_API_METAL");
            options.Add("SHADER_API_VULKAN");
            options.Add("SHADER_API_D3D11_9X");
            options.Add("SHADER_API_PS4");
            options.Add("SHADER_API_PSSL");
            options.Add("SHADER_API_XBOXONE");
            options.Add("SHADER_API_PSP2");
            options.Add("SHADER_API_WIIU");
            options.Add("SHADER_API_MOBILE");
            options.Add("SHADER_TARGET_GLSL");
            options.Add("UNITY_NO_SCREENSPACE_SHADOWS");
            options.Add("UNITY_NO_LINEAR_COLORSPACE");
            options.Add("UNITY_NO_RGBM");
            options.Add("UNITY_NO_DXT5nm");
            options.Add("UNITY_FRAMEBUFFER_FETCH_AVAILABLE");
            options.Add("UNITY_USE_RGBA_FOR_POINT_SHADOWS");
            options.Add("UNITY_ATTEN_CHANNEL");
            options.Add("UNITY_HALF_TEXEL_OFFSET");
            options.Add("UNITY_MIGHT_NOT_HAVE_DEPTH_Texture");
            options.Add("UNITY_CAN_COMPILE_TESSELLATION");
            options.Add("UNITY_REVERSED_Z");
            Values.Add("0");
            Values.Add("0");
            CalculateHeight();
        }

        public override void Show() {
            base.Show();
            Values[0] = EditorGUI.Popup(new Rect(position.x, position.y + 1 * (20) + 2, position.width, 16), int.Parse(Values[0]), options.ToArray()).ToString(CultureInfo.InvariantCulture);
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst(options[int.Parse(Values[0])]);
        }
    }
}