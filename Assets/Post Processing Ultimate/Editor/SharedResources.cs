using System.Collections.Generic;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {

    static class SharedResources {

        internal static readonly List<GUIStyle> visualElementStyles = new List<GUIStyle>();

        internal static readonly Dictionary<KeyCode, string> shortcut = new Dictionary<KeyCode, string>() {
            { KeyCode.A, "Add"},
            { KeyCode.S, "Sub"},
            { KeyCode.M, "Mul"},
            { KeyCode.D, "Div"},
            { KeyCode.E, "Exp"},
            { KeyCode.F, "Floor"},
            { KeyCode.L, "Lerp"},
            { KeyCode.N, "Normalize"},
            { KeyCode.P, "Pow"},
            { KeyCode.R, "Round"},
            { KeyCode.T, "Tan"},
            { KeyCode.I, "If"},
            { KeyCode.Alpha1, "Value1"},
            { KeyCode.Alpha2, "Value2"},
            { KeyCode.Alpha3, "Value3"},
            { KeyCode.Alpha4, "Value4"},
            { KeyCode.C, "Custom"}
        };

        internal static readonly Dictionary<KeyCode, string> shortcutAlt = new Dictionary<KeyCode, string>() {
            { KeyCode.M, "Mod"},
            { KeyCode.C, "Clamp"},
            { KeyCode.D, "Dot"},
            { KeyCode.E, "Exp2"},
            { KeyCode.F, "FWidth"},
            { KeyCode.L, "Length"},
            { KeyCode.R, "Reflect"},
            { KeyCode.S, "Sin"},
            { KeyCode.T, "Trunc"}
        };

        static SharedResources() {
            visualElementStyles = VisualElementStyles();
        }

        internal static Texture2D Line() {
            Texture2D line = new Texture2D(1, 16, TextureFormat.RGBA32, false, true) { wrapMode = TextureWrapMode.Clamp };
            for (int i = 0; i < 5; ++i) {
                line.SetPixel(0, i, new Color(1, 1, 1, 0));
            }
            for (int i = 5; i < 11; ++i) {
                line.SetPixel(0, i, new Color(1, 1, 1, 1));
            }
            for (int i = 11; i < 16; ++i) {
                line.SetPixel(0, i, new Color(1, 1, 1, 0));
            }
            line.Apply();
            return line;
        }

        internal static List<List<Texture2D>> VisualEditorArrows() {
            Texture2D circleFull = Resources.Load("UI/CircleFull") as Texture2D;
            Texture2D arrowFull = Resources.Load("UI/ArrowFull") as Texture2D;
            List<List<Texture2D>> visualEditorArrows = new List<List<Texture2D>>();
            visualEditorArrows.Add(new List<Texture2D>());
            visualEditorArrows.Add(new List<Texture2D>());
            for (int i = 0; i < 6; ++i) {
                visualEditorArrows[0].Add(Utils.Tint(circleFull, Utils.sizeColors[i]));
            }
            for (int i = 0; i < 7; ++i) {
                visualEditorArrows[1].Add(Utils.Tint(arrowFull, Utils.sizeColors[i]));
            }
            return visualEditorArrows;
        }

        internal static List<List<Texture2D>> VisualJointIcons() {
            Texture2D circleFull = Resources.Load("UI/CircleFull") as Texture2D;
            Texture2D circleEmpty = Resources.Load("UI/CircleEmpty") as Texture2D;
            Texture2D arrowFull = Resources.Load("UI/ArrowFull") as Texture2D;
            Texture2D arrowEmpty = Resources.Load("UI/ArrowEmpty") as Texture2D;
            List<List<Texture2D>> visualJointIcons = new List<List<Texture2D>>();
            visualJointIcons.Add(new List<Texture2D>());
            visualJointIcons.Add(new List<Texture2D>());
            for (int i = 0; i < 5; ++i) {
                visualJointIcons[0].Add(Utils.Tint(circleEmpty, Utils.sizeColors[i]));
            }
            for (int i = 0; i < 5; ++i) {
                visualJointIcons[0].Add(Utils.Tint(circleFull, Utils.sizeColors[i]));
            }
            for (int i = 0; i < 5; ++i) {
                visualJointIcons[1].Add(Utils.Tint(arrowEmpty, Utils.sizeColors[i]));
            }
            for (int i = 0; i < 5; ++i) {
                visualJointIcons[1].Add(Utils.Tint(arrowFull, Utils.sizeColors[i]));
            }
            return visualJointIcons;
        }

        internal static List<GUIStyle> VisualElementStyles() {
            List<GUIStyle> visualElementStyles = new List<GUIStyle>();
            visualElementStyles.Add(new GUIStyle());
            visualElementStyles[0].alignment = TextAnchor.MiddleCenter;
            visualElementStyles[0].fontSize = 12;
            visualElementStyles[0].font = Resources.Load("Fonts/FORCED SQUARE") as Font;
            visualElementStyles.Add(new GUIStyle(visualElementStyles[0]));
            visualElementStyles[1].alignment = TextAnchor.MiddleLeft;
            visualElementStyles[1].normal.textColor = Color.white;
            visualElementStyles.Add(new GUIStyle(visualElementStyles[1]));
            visualElementStyles[2].alignment = TextAnchor.MiddleRight;
            visualElementStyles[2].clipping = TextClipping.Clip;
            visualElementStyles.Add(new GUIStyle(visualElementStyles[0]));
            visualElementStyles[3].normal.textColor = Color.white;
            visualElementStyles[3].active.textColor = Color.white;
            visualElementStyles[3].focused.textColor = Color.white;
            visualElementStyles.Add(new GUIStyle(visualElementStyles[2]));
            visualElementStyles[4].alignment = TextAnchor.MiddleCenter;
            return visualElementStyles;
        }
    }
}
