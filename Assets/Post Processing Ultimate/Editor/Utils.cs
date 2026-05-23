using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {

    internal static class Utils {

        internal const int jointSize = 12;
        internal const int jointHorizontalOffset = 12;
        internal const int jointVerticalOffset = 4;

        internal const string nameSpace = "DawidMoza.PostProcessingUltimate";
        internal const string shaderName = "PPU";
        internal const string appVersion = "1.5.0";

        internal static string[] components = { ".x", ".y", ".z", ".w" };

        internal static readonly Color[] sizeColors = { Color.white, Color.yellow, new Color(1, 0, 1), Color.cyan, Color.blue, Color.green, Color.red };

        internal static readonly Color lightColor = new Color(0.7f, 0.7f, 0.7f);
        internal static readonly Color darkColor = new Color(0.1f, 0.1f, 0.1f);
        internal static readonly Color white75 = new Color(1, 1, 1, 0.75f);
        internal static readonly Color white50 = new Color(1, 1, 1, 0.50f);
        internal static readonly Color white25 = new Color(1, 1, 1, 0.25f);

        internal static readonly Color ElementsCamera = new Color(0.25f, 0.25f, 1);
        internal static readonly Color ElementsArithmetic = new Color(0, 1, 1);
        internal static readonly Color ElementsConstant = new Color(1, 0.75f, 0);
        internal static readonly Color ElementsCustom = new Color(0.15f, 0.15f, 0.35f);
        internal static readonly Color ElementsData = new Color(0.5f, 0.25f, 0.25f);
        internal static readonly Color ElementsMisc = new Color(0.75f, 0, 0);
        internal static readonly Color ElementsHLSL = new Color(0.25f, 1, 0.25f);
        internal static readonly Color ElementsInput = new Color(0.5f, 0.5f, 0.5f);
        internal static readonly Color ElementsLogic = new Color(0.5f, 0.75f, 0);
        internal static readonly Color ElementsPPS = new Color(1, 0.25f, 0.75f);
        internal static readonly Color ElementsPredefined = new Color(0.6f, 0.6f, 1);
        internal static readonly Color ElementsValue = new Color(1, 0.25f, 0.25f);
        internal static readonly Color ElementsVariable = new Color(0.5f, 0, 0.75f);
        internal static readonly Color ElementsMaster = new Color(0.1f, 0.1f, 0.1f);

        internal const string basePath = "Assets/Post Processing Ultimate/Editor/";

        internal static readonly Dictionary<int, string> texelMap = new Dictionary<int, string>() { { -2, "_MainTex" }, { -3, "_CameraDepthTexture" } };

        internal static string UniqueToName(List<VisualProperty> properties, int unique, Dictionary<int, string> additionalValues = null) {
            if (additionalValues != null && additionalValues.ContainsKey(unique)) {
                return additionalValues[unique];
            }
            if (properties.Select(x => x.unique).Contains(unique)) { 
                return properties.Where(x => x.unique == unique).FirstOrDefault().Values[0];
            }
            return "";
        }

        internal static int IndexIfAvailable(List<string> list, string text) {
            if (list.Contains(text)) {
                return list.IndexOf(text);
            }
            return 0;
        }

        internal static Texture2D Tint(Texture2D texture, Color tint) {
            Texture2D output = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false, true) { wrapMode = TextureWrapMode.Clamp };
            for (int i = 0; i < texture.width; ++i) {
                for (int j = 0; j < texture.height; ++j) {
                    output.SetPixel(i, j, texture.GetPixel(i, j) * tint);
                }
            }
            output.Apply();
            return output;
        }

        internal static bool ContainsEachOther(string a, string b) {
            a = a.ToLower();
            b = b.ToLower();
            return a.Contains(b) || b.Contains(a);
        }

        internal static string GetTooltip(string name) {
            return Type.GetType(nameSpace + "." + name, true).GetField("tooltip").GetValue(null).ToString();
        }

        internal static List<ContextFoldout> MergeContextFoldouts(params List<ContextFoldout>[] foldouts) {
            List<ContextFoldout> first = foldouts[0];
            for (int i = 1; i < foldouts.Length; ++i) {
                first.AddRange(foldouts[i]);
                first.Sort(delegate (ContextFoldout a, ContextFoldout b) {
                    return String.Compare(a.name, b.name);
                });
            }
            return first;
        }

        internal static List<ContextFoldout> GetFoldersAndFiles(string path) {
            List<ContextFoldout> output = new List<ContextFoldout>();
            string[] folders = GetFolderNames(path);
            foreach (string folder in folders) {
                output.Add(new ContextFoldout(folder));
                int last = output.Count - 1;
                string[] files = GetFileNames(path + "/" + folder);
                foreach (string file in files) {
                    output[last].buttons.Add(new ContextButton(file));
                }
            }
            return output;
        }

        internal static string[] GetFileNames(string path) {
            string[] files = Directory.GetFiles(basePath + path, "*.cs");
            for (int i = 0; i < files.Length; ++i) {
                files[i] = files[i].Substring(basePath.Length + path.Length + "\\".Length);
                files[i] = files[i].Substring(0, files[i].IndexOf('.'));
            }
            return files;
        }

        internal static string[] GetFolderNames(string path) {
            string[] folders = Directory.GetDirectories(basePath + path);
            for (int i = 0; i < folders.Length; ++i) {
                folders[i] = folders[i].Substring(basePath.Length + path.Length + "\\".Length);
            }
            return folders;
        }

        internal static List<VisualProperty> ConvertCustomToProperties(CustomData customData) {
            List<VisualProperty> properties = new List<VisualProperty>();
            for (int i = 0; i < customData.InputSize.Count; ++i) {
                properties.Add(CreateProperty("Input" + customData.InputSize[i] + "Field"));
                properties[i].Values[0] = customData.InputName[i];
            }
            return properties;
        }

        internal static VisualElement Create(string name) {
            return (VisualElement) Activator.CreateInstance(Type.GetType(nameSpace + "." + name, true));
        }

        internal static VisualProperty CreateProperty(string name) {
            return (VisualProperty) Activator.CreateInstance(Type.GetType(nameSpace + "." + name, true));
        }

        internal static GUIStyle PrepareTextColors(GUIStyle style, Color color) {
            style.focused.textColor = color;
            style.normal.textColor = color;
            style.active.textColor = color;
            style.onActive.textColor = color;
            style.onFocused.textColor = color;
            style.onNormal.textColor = color;
            return style;
        }

        internal static Color PositiveOrClear(int x, int y, int width, int height, Texture2D tex) {
            if (x < 0 || x >= width || y < 0 || y >= height) {
                return Color.clear;
            } else {
                return tex.GetPixel(x, y);
            }
        }

        internal static float distance(float x, float y, float xCenter, float yCenter) {
            return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(xCenter - x), 2) + Mathf.Pow(Mathf.Abs(yCenter - y), 2));
        }

        internal static Texture2D CircleAA(Texture2D output, int x, int y, float radius, int width, int height, float distance) {
            float level = distance - radius;
            if (level < 0) {
                output.SetPixel(x, y, Color.white);
            } else if (level < 1) {
                output.SetPixel(x, y, new Color(1, 1, 1, 0.3f));
            } else if (level < 2) {
                output.SetPixel(x, y, new Color(1, 1, 1, 0.1f));
            } else {
                output.SetPixel(x, y, Color.clear);
            }
            return output;
        }

        internal static Texture2D CreateRoundedTexture(int width, int height, float radius, bool topLeft, bool topRight, bool bottomLeft, bool bottomRight) {
            Texture2D output = new Texture2D(width, height, TextureFormat.RGBA32, false, true) { wrapMode = TextureWrapMode.Clamp };
            for (int y = 0; y < height; ++y) {
                for (int x = 0; x < width; ++x) {
                    bool top = y >= height - radius;
                    bool bottom = y < radius;
                    bool right = x >= width - radius;
                    bool left = x < radius;
                    if (!bottom && !top || !left && !right) {
                        output.SetPixel(x, y, Color.white);
                    } else if (topLeft && top && left) {
                        float dist = distance(x, y, radius, height - radius);
                        CircleAA(output, x, y, radius, width, height, dist);
                    } else if (bottomLeft && bottom && left) {
                        float dist = distance(x, y, radius, radius);
                        CircleAA(output, x, y, radius, width, height, dist);
                    } else if (topRight && top && right) {
                        float dist = distance(x, y, width - radius, height - radius);
                        CircleAA(output, x, y, radius, width, height, dist);
                    } else if (bottomRight && bottom && right) {
                        float dist = distance(x, y, width - radius, radius);
                        CircleAA(output, x, y, radius, width, height, dist);
                    } else {
                        output.SetPixel(x, y, Color.white);
                    }
                }
            }
            output.Apply();
            return output;
        }

        internal static bool isInsideCircle(float x, float y, float xCenter, float yCenter, float radius) {
            return distance(x, y, xCenter, yCenter) < radius;
        }

        internal static Color TextColor(bool isProSkin) {
            return isProSkin ? Utils.lightColor : Utils.darkColor;
        }

        internal static Color BackgroundColor(bool isProSkin) {
            return isProSkin ? Utils.darkColor : Utils.lightColor;
        }

        internal static Color PropertyColor(bool isProSkin) {
            return isProSkin ? new Color(0.25f, 0.25f, 0.25f) : new Color(0.85f, 0.85f, 0.85f);
        }

        internal static float RoundWithSteps(float input, float step) {
            int times = (int) Mathf.Floor(input / step);
            float lower = Mathf.Abs(input - step * times);
            float higher = Mathf.Abs(input - step * (times + 1));
            if (lower > higher) {
                ++times;
            }
            return step * times;
        }
    }
}