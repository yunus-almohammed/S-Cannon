using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    internal static class Generator {

        internal enum RenderPipeline { 
            BIRP,
            HDRP,
            URP
        }

        internal static void Generate(List<List<VisualElement>> allElements, Settings settings, List<VisualProperty> properties, string shaderPath, RenderQueue renderQueue, bool functionEditor) {
            if (functionEditor) {
                GenerateFunction(allElements, settings, properties, shaderPath);
                return;
            }
            bool world = World(allElements);
            List<string> evaluations = new List<string>();
            string precision = settings.Values[3] == "0" ? "half" : "float";
            string csPath = (shaderPath.Substring(0, shaderPath.Length - 7) + ".cs");
            string name = csPath.Substring(0, csPath.Length - 3);
            for (int i = name.Length - 1; i >= 0; --i) {
                if (name[i] == '/' || name[i] == '\\') {
                    name = name.Substring(i + 1);
                    break;
                }
            }
            csPath = csPath.Substring(0, csPath.IndexOf(name));
            name = name.Replace(" ", "");
            csPath += name + ".cs";
            List<VisualProperty> props = FilterProps(properties, name);
            props.Reverse();
            List<string> tempRenders = TempRenderTextures(renderQueue.tempTextures, Loops(renderQueue));
            List<string> parameters = Parameters(props);
            List<string> inputs = Inputs(props);
            List<string> fiel = Fiel(props, allElements, precision);
            List<string> ques = Queue(renderQueue, allElements);
            List<string> rels = ReleaseTemporaryTextures(renderQueue);
            List<List<string>> frag = new List<List<string>>();
            for (int i = 0; i < allElements.Count; ++i) {
                frag.Add(Frag(allElements[i], precision, false));
            }
            string csContent = GenerateCs(settings, world, Spline(properties, evaluations), evaluations, name, tempRenders, parameters, inputs, ques, rels);
            string shaderContent = GenerateShader(allElements, settings, properties, renderQueue, world, precision, fiel, frag);
            File.WriteAllBytes(shaderPath, System.Text.Encoding.UTF8.GetBytes(shaderContent));
            File.WriteAllBytes(csPath, System.Text.Encoding.UTF8.GetBytes(csContent));
            AssetDatabase.Refresh();
        }

        private static string GenerateShader(List<List<VisualElement>> allElements, Settings settings, List<VisualProperty> properties, RenderQueue renderQueue, bool world, string precision, List<string> fiel, List<List<string>> frag) {
            string content = DataAccess.Save(allElements, settings, properties, renderQueue, false);
            string varyings = world ? "VaryingsExtended" : "VaryingsDefault";
            content += "Shader \"" + settings.Values[1] + "\" {";
            content += "\n\n\tHLSLINCLUDE";
            List<string> texFiel = TexFiel(allElements, properties, precision);
            List<string> tempFiel = TempFiel(allElements);
            List<string> customs = Customs(allElements);
            content += "\n\n\t\t\t#include \"Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl\"";
            for (int i = 0; i < customs.Count; ++i) {
                content += "\n\t\t\t" + customs[i];
            }
            if (world) {
                content += "\n\t\t\tuniform float4x4 _ProjMat;";
                content += "\n\t\t\t";
                content += "\n\t\t\tstruct VaryingsExtended {";
                content += "\n\t\t\t\tfloat4 vertex : SV_POSITION;";
                content += "\n\t\t\t\tfloat2 texcoord : TEXCOORD0;";
                content += "\n\t\t\t\tfloat2 texcoordStereo : TEXCOORD1;";
                content += "\n\t\t\t\tfloat3 worldDirection : TEXCOORD2;";
                content += "\n\t\t\t};";
                content += "\n\t\t\t";
                content += "\n\t\t\tVaryingsExtended VertExtended(AttributesDefault v) {";
                content += "\n\t\t\t\tVaryingsExtended o;";
                content += "\n\t\t\t\to.vertex = float4(v.vertex.xy, 0.0, 1.0);";
                content += "\n\t\t\t\to.texcoord = TransformTriangleVertexToUV(v.vertex.xy);";
                content += "\n\t\t\t\t#if UNITY_UV_STARTS_AT_TOP";
                content += "\n\t\t\t\t\to.texcoord = o.texcoord * float2(1.0, -1.0) + float2(0.0, 1.0);";
                content += "\n\t\t\t\t#endif";
                content += "\n\t\t\t\to.texcoordStereo = TransformStereoScreenSpaceTex(o.texcoord, 1.0);";
                content += "\n\t\t\t\tfloat4 clip = float4(o.texcoord.xy * 2 - 1, 0.0, 1.0);";
                content += "\n\t\t\t\to.worldDirection = mul(_ProjMat, clip) - _WorldSpaceCameraPos;";
                content += "\n\t\t\t\treturn o;";
                content += "\n\t\t\t}";
            }
            content += "\n";
            content += "\n\t\t\tTEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);";
            if (DepthOrWorld(allElements)) {
                content += "\n\t\t\tTEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);";
            }
            for (int j = 0; j < fiel.Count; ++j) {
                content += "\n\t\t\t" + fiel[j];
            }
            for (int j = 0; j < texFiel.Count; ++j) {
                content += "\n\t\t\t" + texFiel[j];
            }
            for (int j = 0; j < tempFiel.Count; ++j) {
                content += "\n\t\t\t" + tempFiel[j];
            }
            for (int i = 0; i < allElements.Count; ++i) {
                content += "\n\n\t\t\t" + precision + "3 Frag" + i + " (" + varyings + " i) : SV_Target {";
                for (int j = 0; j < frag[i].Count; ++j) {
                    content += "\n\t\t\t\t" + frag[i][j];
                }
                content += "\n\t\t\t}";
            }
            content += "\n\n\tENDHLSL";
            content += "\n\n\tSubShader {";
            content += "\n\n\t\tCull Off ZWrite Off ZTest Always";
            for (int i = 0; i < allElements.Count; ++i) {
                content += "\n\n\t\tPass {";
                content += "\n\n\t\t\tHLSLPROGRAM";
                content += "\n";
                content += "\n\t\t\t\t#pragma vertex " + varyings.Replace("Varyings", "Vert");
                content += "\n\t\t\t\t#pragma fragment Frag" + i;
                content += "\n";
                content += "\n\t\t\tENDHLSL";
                content += "\n\t\t}";
            }
            content += "\n\t}";
            content += "\n}";
            return content;
        }

        private static string GenerateCs(Settings settings, bool world, bool spline, List<string> evaluations, string name, List<string> tempRenders, List<string> parameters, List<string> inputs, List<string> ques, List<string> rels) {
            string content = "using System;\nusing UnityEngine;\nusing UnityEngine.Rendering.PostProcessing;\n\nnamespace " + Utils.nameSpace + " {\n\t[Serializable]";
            content += "\n\t[PostProcess(typeof(" + name + "Renderer), PostProcessEvent." + (settings.Values[0] == "0" ? "AfterStack" : settings.Values[0] == "1" ? "BeforeStack" : "BeforeTransparent") + ", \"" + settings.Values[1] + "\")]";
            content += "\n\tpublic sealed class " + name + " : PostProcessEffectSettings {";
            for (int i = 0; i < parameters.Count; ++i) {
                content += "\n\t\t" + parameters[i];
            }
            for (int i = 0; i < tempRenders.Count; ++i) {
                content += "\n\t\t" + tempRenders[i];
            }
            if (spline) {
                content += "\n\n\t\tpublic void CreateEvaluations(){";
                content += "\n\t\t\tColor pixel = Color.black;";
                for (int i = 0; i < evaluations.Count; ++i) {
                    content += "\n\t\t\t" + evaluations[i] + " = new Texture2D(128, 1) {wrapMode = TextureWrapMode.Clamp}; ";
                    content += "\n\t\t\tfor (int i = 0; i < 128; ++i) {";
                    content += "\n\t\t\t\tpixel.a = " + evaluations[i].Substring(0, evaluations[i].Length - 10) + ".value.cachedData[i];";
                    content += "\n\t\t\t\t" + evaluations[i] + ".SetPixel(i, 0, pixel);";
                    content += "\n\t\t\t}";
                    content += "\n\t\t\t" + evaluations[i] + ".Apply();";
                }
                content += "\n\t\t}";
            }
            content += "\n\t}\n\n\tpublic sealed class " + name + "Renderer : PostProcessEffectRenderer<" + name + "> {\n\t\tpublic override void Render(PostProcessRenderContext context) {";
            content += "\n\t\t\tvar sheet = context.propertySheets.Get(Shader.Find(\"" + settings.Values[1] + "\"));";
            for (int i = 0; i < rels.Count; ++i) {
                content += "\n\t\t\t" + rels[i];
            }
            if (spline) {
                content += "\n\t\t\tsettings.CreateEvaluations();";
            }
            if (world) {
                content += "\n\t\t\tMatrix4x4 projMat = GL.GetGPUProjectionMatrix(context.camera.projectionMatrix, false);";
                content += "\n\t\t\tprojMat[15] = projMat[14] = projMat[11] = 0;";
                content += "\n\t\t\t++projMat[15];";
                content += "\n\t\t\tsheet.properties.SetMatrix(\"_ProjMat\", Matrix4x4.Inverse(projMat * context.camera.worldToCameraMatrix) * Matrix4x4.TRS(new Vector3(0, 0, -1 * projMat[10]), Quaternion.identity, Vector3.one));";
            }
            for (int i = 0; i < inputs.Count; ++i) {
                content += "\n\t\t\t" + inputs[i];
            }
            for (int i = 0; i < ques.Count; ++i) {
                content += "\n\t\t\t" + ques[i];
            }
            content += "\n\t\t}\n\t}\n}";
            return content;
        }

        private static void GenerateFunction(List<List<VisualElement>> allElements, Settings settings, List<VisualProperty> properties, string path) {
            string name;
            string precision = settings.Values[3] == "0" ? "half" : "float";
            name = path.Substring(0, path.Length - 5);
            string outputSize = (int.Parse(allElements[0][0].Values[0]) + 1).ToString(CultureInfo.InvariantCulture);
            for (int i = name.Length - 1; i >= 0; --i) {
                if (name[i] == '/') {
                    name = name.Substring(i + 1);
                    break;
                }
            }
            List<string> frag = Frag(allElements[0], precision, true, outputSize);
            string output = DataAccess.Save(allElements, settings, properties, null, true);
            output += precision + outputSize + " " + name + "(";
            for (int i = 0; i < properties.Count; ++i) {
                if (i != 0) {
                    output += ", ";
                }
                output += precision + properties[i].name.Substring(5) + " " + properties[i].Values[0];
            }
            output += "){";
            for (int i = 0; i < frag.Count; ++i) {
                output += "\n\t" + frag[i];
            }
            output += "\n}";
            File.WriteAllBytes(path, System.Text.Encoding.UTF8.GetBytes(output));
            AssetDatabase.Refresh();
            ShaderEditor[] windows = Resources.FindObjectsOfTypeAll<ShaderEditor>();
            if (windows.Length == 1) {
                windows[0].Refresh();
            } else {
                AssetDatabase.ImportAsset("Assets/Post Processing Ultimate/Shaders", ImportAssetOptions.ImportRecursive);
            }
        }


        private static bool Spline(List<VisualProperty> properties, List<string> evaluations) {
            bool output = false;
            foreach (VisualProperty visualProperty in properties) {
                if (visualProperty.name == "Spline") {
                    output = true;
                    evaluations.Add(visualProperty.Values[0] + "EVALUATION");
                }
            }
            return output;
        }

        private static bool Loops(RenderQueue renderQueue) {
            for (int i = 0; i < renderQueue.passes.Count; ++i) {
                if (renderQueue.passes[i].Iterations != 1 || renderQueue.passes[i].Variable != 0) {
                    return true;
                }
            }
            return false;
        }


        internal static bool World(List<List<VisualElement>> elements) {
            foreach (List<VisualElement> pass in elements) {
                foreach (VisualElement element in pass) {
                    if (element.name == "WorldPosition") {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static bool DepthOrWorld(List<List<VisualElement>> elements) {
            foreach (List<VisualElement> pass in elements) {
                foreach (VisualElement element in pass) {
                    if (element.name == "CameraDepth" || element.name == "WorldPosition") {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static List<VisualProperty> FilterProps(List<VisualProperty> properties, string name) {
            List<VisualProperty> output = new List<VisualProperty>();
            for (int i = 0; i < properties.Count; ++i) {
                if (properties[i].Values[0] == name) {
                    Debug.Log("Property name same as class name detected: " + properties[i].Values[0] + " is now " + properties[i].Values[0] + "Variable");
                    properties[i].Values[0] += "Variable";
                }
            }
            bool check;
            for (int i = properties.Count - 1; i >= 0; --i) {
                if (i > 0) {
                    check = true;
                    for (int j = i - 1; j >= 0; --j) {
                        if (properties[i].Values[0] == properties[j].Values[0]) {
                            check = false;
                            Debug.Log("Same property names found: " + properties[i].Values[0]);
                            break;
                        }
                    }
                    if (check) {
                        output.Add(properties[i]);
                    }
                } else {
                    output.Add(properties[0]);
                }
            }
            return output;
        }

        internal static List<string> Parameters(List<VisualProperty> properties) {
            List<string> output = new List<string>();
            for (int i = 0; i < properties.Count; ++i) {
                switch (properties[i].name) {
                    case "Float":
                    output.Add("[Tooltip(\"" + properties[i].Values[1] + "\")]");
                    output.Add("public FloatParameter " + properties[i].Values[0] + " = new FloatParameter {value = " + properties[i].Values[2] + "f};");
                    break;
                    case "Int":
                    output.Add("[Tooltip(\"" + properties[i].Values[1] + "\")]");
                    output.Add("public IntParameter " + properties[i].Values[0] + " = new IntParameter {value = " + properties[i].Values[2] + "};");
                    break;
                    case "IntSlider":
                    output.Add("[Range(" + properties[i].Values[3] + ", " + properties[i].Values[4] + "), Tooltip(\"" + properties[i].Values[1] + "\")]");
                    output.Add("public IntParameter " + properties[i].Values[0] + " = new IntParameter {value = " + properties[i].Values[2] + "};");
                    break;
                    case "FloatSlider":
                    output.Add("[Range(" + properties[i].Values[3] + "f, " + properties[i].Values[4] + "f), Tooltip(\"" + properties[i].Values[1] + "\")]");
                    output.Add("public FloatParameter " + properties[i].Values[0] + " = new FloatParameter {value = " + properties[i].Values[2] + "f};");
                    break;
                    case "Color":
                    output.Add("[Tooltip(\"" + properties[i].Values[1] + "\")]");
                    output.Add("public ColorParameter " + properties[i].Values[0] + " = new ColorParameter {value = new Color(" + properties[i].Values[2] + "f, " + properties[i].Values[3] + "f, " + properties[i].Values[4] + "f, " + properties[i].Values[5] + "f)};");
                    break;
                    case "Vector2":
                    output.Add("[Tooltip(\"" + properties[i].Values[1] + "\")]");
                    output.Add("public Vector2Parameter " + properties[i].Values[0] + " = new Vector2Parameter {value = new Vector2(" + properties[i].Values[2] + "f, " + properties[i].Values[3] + "f)};");
                    break;
                    case "Vector3":
                    output.Add("[Tooltip(\"" + properties[i].Values[1] + "\")]");
                    output.Add("public Vector3Parameter " + properties[i].Values[0] + " = new Vector3Parameter {value = new Vector3(" + properties[i].Values[2] + "f, " + properties[i].Values[3] + "f, " + properties[i].Values[4] + "f)};");
                    break;
                    case "Vector4":
                    output.Add("[Tooltip(\"" + properties[i].Values[1] + "\")]");
                    output.Add("public Vector4Parameter " + properties[i].Values[0] + " = new Vector4Parameter {value = new Vector4(" + properties[i].Values[2] + "f, " + properties[i].Values[3] + "f, " + properties[i].Values[4] + "f, " + properties[i].Values[5] + "f)};");
                    break;
                    case "Texture2D":
                    output.Add("[Tooltip(\"" + properties[i].Values[1] + "\")]");
                    output.Add("public TextureParameter " + properties[i].Values[0] + " = new TextureParameter {value = null};");
                    break;
                    case "Spline":
                    output.Add("[SerializeField]");
                    output.Add("internal Texture2D " + properties[i].Values[0] + "EVALUATION;");
                    output.Add("[Tooltip(\"" + properties[i].Values[1] + "\")]");
                    output.Add("public SplineParameter " + properties[i].Values[0] + " = new SplineParameter {value = new Spline(new AnimationCurve(new Keyframe(0f, 0f, 1f, 1f), new Keyframe(1f, 1f, 1f, 1f)), 0f, false, new Vector2(0f, 1f))};");
                    break;
                    case "Bool":
                    output.Add("[Tooltip(\"" + properties[i].Values[1] + "\")]");
                    output.Add("public BoolParameter " + properties[i].Values[0] + " = new BoolParameter {value = " + properties[i].Values[2].ToLower() + "};");
                    break;
                }
            }
            return output;
        }

        internal static List<string> Inputs(List<VisualProperty> properties) {
            List<string> output = new List<string>();
            for (int i = 0; i < properties.Count; ++i) {
                switch (properties[i].name) {
                    case "Float":
                    output.Add("sheet.properties.SetFloat(\"_" + properties[i].Values[0] + "\", settings." + properties[i].Values[0] + ");");
                    break;
                    case "Int":
                    output.Add("sheet.properties.SetFloat(\"_" + properties[i].Values[0] + "\", settings." + properties[i].Values[0] + ");");
                    break;
                    case "IntSlider":
                    output.Add("sheet.properties.SetFloat(\"_" + properties[i].Values[0] + "\", settings." + properties[i].Values[0] + ");");
                    break;
                    case "FloatSlider":
                    output.Add("sheet.properties.SetFloat(\"_" + properties[i].Values[0] + "\", settings." + properties[i].Values[0] + ");");
                    break;
                    case "Color":
                    output.Add("sheet.properties.SetColor(\"_" + properties[i].Values[0] + "\", settings." + properties[i].Values[0] + ");");
                    break;
                    case "Vector2":
                    output.Add("sheet.properties.SetVector(\"_" + properties[i].Values[0] + "\", settings." + properties[i].Values[0] + ".value);");
                    break;
                    case "Vector3":
                    output.Add("sheet.properties.SetVector(\"_" + properties[i].Values[0] + "\", settings." + properties[i].Values[0] + ".value);");
                    break;
                    case "Vector4":
                    output.Add("sheet.properties.SetVector(\"_" + properties[i].Values[0] + "\", settings." + properties[i].Values[0] + ");");
                    break;
                    case "Texture2D":
                    output.Add("if (settings." + properties[i].Values[0] + ".value != null) { sheet.properties.SetTexture(\"_" + properties[i].Values[0] + "\", settings." + properties[i].Values[0] + "); }");
                    break;
                    case "Spline":
                    output.Add("sheet.properties.SetTexture(\"_" + properties[i].Values[0] + "\", settings." + properties[i].Values[0] + "EVALUATION);");
                    break;
                    case "Bool":
                    output.Add("sheet.properties.SetFloat(\"_" + properties[i].Values[0] + "\", settings." + properties[i].Values[0] + " ? 1f : 0f);");
                    break;
                }
            }
            return output;
        }

        internal static List<string> Queue(RenderQueue renderQueue, List<List<VisualElement>> allElements) {
            List<string> render = new List<string>();
            string source;
            string destination;
            string pass;
            string iterator;
            for (int i = 0; i < renderQueue.tempTextures; ++i) {
                render.Add("settings.tempRT" + i + " = RenderTexture.GetTemporary(Screen.width, Screen.height);");
            }
            for (int i = 0; i < renderQueue.passes.Count; ++i) {
                pass = renderQueue.passes[i].Pass.ToString(CultureInfo.InvariantCulture);
                for (int j = 0; j < allElements[int.Parse(pass)].Count; ++j) {
                    if (allElements[int.Parse(pass)][j].name == "TempTex") {
                        if (!allElements[int.Parse(pass)][j].Values[0].Contains("(0)")) {
                            render.Add("sheet.properties.SetTexture(\"_" + allElements[int.Parse(pass)][j].Values[0] + "\", settings." + allElements[int.Parse(pass)][j].Values[0] + ");");
                        }
                    }
                }
                if (renderQueue.inputOptions[renderQueue.passes[i].Input] == "Game") {
                    source = "context.source";
                } else {
                    source = "settings." + renderQueue.inputOptions[renderQueue.passes[i].Input];
                }
                if (renderQueue.outputOptions[renderQueue.passes[i].Output] == "Screen") {
                    destination = "context.destination";
                } else {
                    destination = "settings." + renderQueue.outputOptions[renderQueue.passes[i].Output];
                }
                if (renderQueue.passes[i].Variable == 0) {
                    iterator = renderQueue.passes[i].Iterations.ToString(CultureInfo.InvariantCulture);
                } else {
                    iterator = "settings." + Utils.UniqueToName(renderQueue.properties, renderQueue.passes[i].Variable);
                }
                if (renderQueue.passes[i].Iterations == 1 && renderQueue.passes[i].Variable == 0) {
                    render.Add("context.command.BlitFullscreenTriangle(" + source + ", " + destination + ", sheet, " + pass + ");");
                } else {
                    render.Add("settings.loopRT0 = RenderTexture.GetTemporary(Screen.width, Screen.height);");
                    render.Add("context.command.BlitFullscreenTriangle(" + source + ", settings.loopRT0);");
                    render.Add("for (int i = 0; i < " + iterator + "; ++i) {");
                    render.Add("\tsettings.loopRT1 = RenderTexture.GetTemporary(Screen.width, Screen.height);");
                    render.Add("\tcontext.command.BlitFullscreenTriangle(settings.loopRT0, settings.loopRT1, sheet, " + pass + ");");
                    render.Add("\tRenderTexture.ReleaseTemporary(settings.loopRT0);");
                    render.Add("\tsettings.loopRT0 = settings.loopRT1;");
                    render.Add("}");
                    render.Add("context.command.BlitFullscreenTriangle(settings.loopRT0, " + destination + ");");
                }
            }
            return render;
        }

        internal static List<string> ReleaseTemporaryTextures(RenderQueue renderQueue) {
            List<string> render = new List<string>();
            for (int i = 0; i < renderQueue.tempTextures; ++i) {
                render.Add("RenderTexture.ReleaseTemporary(settings.tempRT" + i + ");");
            }
            for (int i = 0; i < renderQueue.passes.Count; ++i) {
                if (renderQueue.passes[i].Iterations != 1 || renderQueue.passes[i].Variable != 0) {
                    render.Add("RenderTexture.ReleaseTemporary(settings.loopRT0);");
                    break;
                }
            }
            return render;
        }

        internal static List<string> TempRenderTextures(int tempTextures, bool loops) {
            List<string> textures = new List<string>();
            for (int i = 0; i < tempTextures; ++i) {
                textures.Add("internal RenderTexture tempRT" + i + ";");
            }
            if (loops) {
                textures.Add("internal RenderTexture loopRT0;");
                textures.Add("internal RenderTexture loopRT1;");
            }
            return textures;
        }

        internal static List<string> TexFiel(List<List<VisualElement>> elements, List<VisualProperty> properties, string precision) {
            HashSet<string> fiel = new HashSet<string>();
            foreach (List<VisualElement> pass in elements) {
                foreach (VisualElement element in pass) {
                    if (element.name == "TexelSize") {
                        fiel.Add(precision + "4 " + Utils.UniqueToName(properties, int.Parse(element.Values[0]), Utils.texelMap) + "_TexelSize;");
                    }
                }
            }
            return fiel.ToList();
        }

        internal static List<string> TempFiel(List<List<VisualElement>> elements) {
            HashSet<string> fiel = new HashSet<string>();
            foreach (List<VisualElement> pass in elements) {
                foreach (VisualElement element in pass) {
                    if (element.name == "TempTex" && !element.Values[0].Contains("(0)")) {
                        fiel.Add("TEXTURE2D_SAMPLER2D(_" + element.Values[0] + ", sampler_" + element.Values[0] + ");");
                    }
                }
            }
            return fiel.ToList();
        }


        internal static List<string> Customs(List<List<VisualElement>> elements) {
            HashSet<string> fiel = new HashSet<string>();
            foreach (List<VisualElement> pass in elements) {
                foreach (VisualElement element in pass) {
                    if (element.name.Contains("Custom") && element.Values[0].Contains("Assets/")) {
                        fiel.Add("#include \"../../" + element.Values[0].Substring(element.Values[0].IndexOf("Assets/") + 7) + "\"");
                    }
                }
            }
            return fiel.ToList();
        }


        internal static List<string> Fiel(List<VisualProperty> properties, List<List<VisualElement>> allElements, string precision = "half") {
            List<string> fiel = new List<string>();
            for (int i = 0; i < properties.Count; ++i) {
                switch (properties[i].name) {
                    case "Float":
                    fiel.Add("uniform " + precision + " _" + properties[i].Values[0] + ";");
                    break;
                    case "Int":
                    fiel.Add("uniform " + precision + " _" + properties[i].Values[0] + ";");
                    break;
                    case "IntSlider":
                    fiel.Add("uniform " + precision + " _" + properties[i].Values[0] + ";");
                    break;
                    case "FloatSlider":
                    fiel.Add("uniform " + precision + " _" + properties[i].Values[0] + ";");
                    break;
                    case "Color":
                    fiel.Add("uniform " + precision + "4 _" + properties[i].Values[0] + ";");
                    break;
                    case "Vector2":
                    fiel.Add("uniform " + precision + "2 _" + properties[i].Values[0] + ";");
                    break;
                    case "Vector3":
                    fiel.Add("uniform " + precision + "3 _" + properties[i].Values[0] + ";");
                    break;
                    case "Vector4":
                    fiel.Add("uniform " + precision + "4 _" + properties[i].Values[0] + ";");
                    break;
                    case "Texture2D":
                    fiel.Add("TEXTURE2D_SAMPLER2D(_" + properties[i].Values[0] + ", sampler_" + properties[i].Values[0] + ");");
                    break;
                    case "Spline":
                    fiel.Add("TEXTURE2D_SAMPLER2D(_" + properties[i].Values[0] + ", sampler_" + properties[i].Values[0] + ");");
                    break;
                    case "Bool":
                    fiel.Add("uniform bool _" + properties[i].Values[0] + ";");
                    break;
                }
            }
            string tempData = "";
            bool check;
            for (int i = 0; i < allElements.Count; ++i) {
                for (int j = 0; j < allElements[i].Count; ++j) {
                    check = true;
                    if (allElements[i][j].name == "SpecialTex") {
                        tempData = "TEXTURE2D_SAMPLER2D(" + allElements[i][j].Values[0] + ", sampler" + allElements[i][j].Values[0] + ");";
                    }
                    for (int k = 0; k < fiel.Count; ++k) {
                        if (tempData == fiel[k]) {
                            check = false;
                            break;
                        } else {
                            check = true;
                        }
                    }
                    if (check && tempData != "") {
                        fiel.Add(tempData);
                    }
                }
            }
            return fiel;
        }

        internal static List<string> Frag(List<VisualElement> elements, string precision, bool functionEditor, string outputSize = "") {
            List<string> frag = new List<string>();
            List<int> variables = Variables(elements);
            List<int> predefinedMacros = PredefinedMacroChecker(elements);
            for (int i = 0; i < elements.Count; ++i) {
                if (elements[i].name.Contains("Iterator") || elements[i].name.Contains("Loop")) {
                    frag.Add(precision + " IteratorVariable = 0.0;");
                    break; 
                }
            }
            for (int i = 0; i < variables.Count; ++i) {
                frag.Add(precision + elements[variables[i]].name.Substring(elements[variables[i]].name.Length - 1) + " var" + elements[variables[i]].Values[0] + " = 0.0;");
            }
            if (functionEditor) {
                frag.Add(precision + outputSize + " Output = 0.0;");
            } else {
                frag.Add(precision + "3 CameraOutput = 0.0;");
            }
            for (int i = 0; i < predefinedMacros.Count; ++i) {
                VisualElement me = elements[predefinedMacros[i]];
                frag.Add("#if (" + me.options[int.Parse(me.Values[0])] + ")");
                frag.Add("\tbool " + me.Values[1] + " = true;");
                frag.Add("#else");
                frag.Add("\tbool " + me.Values[1] + " = false;");
                frag.Add("#endif");
            }
            for (int i = 0; i < variables.Count; ++i) {
                VisualElement me = elements[variables[i]];
                int firstLeft = me.GetFirstLeftJointIndex();
                string sign = VisualElement.loopOptions[int.Parse(me.Values[2])];
                string startValue = Demand(elements, me.joints[firstLeft + 2], precision);
                string iterationsCount = "(" + startValue + " + " + Demand(elements, me.joints[firstLeft + 1], precision) + ")";
                frag.Add("for (IteratorVariable = " + startValue + "; IteratorVariable < " + iterationsCount + "; ++IteratorVariable){");
                frag.Add("\tvar" + me.Values[0] + " " + sign + "= " + Demand(elements, me.joints[firstLeft + 0], precision) + ";");
                frag.Add("}");
            }
            frag.AddRange(elements[0].Generate(elements, 0, precision));
            if (functionEditor) {
                frag.Add("return Output;");
            } else {
                frag.Add("return CameraOutput;");
            }
            return frag;
        }

        internal static List<int> Variables(List<VisualElement> elements) {
            List<int> order = VariablesOrder(elements);
            order.Reverse();
            List<int> output = new List<int>();
            for (int i = 0; i < order.Count; ++i) {
                if (elements[order[i]].name.Contains("VarLoop")) {
                    elements[order[i]].Values[0] = output.Count.ToString();
                    output.Add(order[i]);
                }
            }
            return output;
        }

        internal static List<int> PredefinedMacroChecker(List<VisualElement> elements) {
            List<int> output = new List<int>();
            for (int i = 0; i < elements.Count; ++i) {
                if (elements[i].name == "Checker") {
                    elements[i].Values[1] = "pre" + output.Count;
                    output.Add(i);
                }
            }
            return output;
        }

        internal static List<int> VariablesOrder(List<VisualElement> elements, int index = 0, List<int> variablesOrder = null) {
            if (variablesOrder == null) {
                variablesOrder = new List<int>();
            } else {
                variablesOrder.Remove(index);
                variablesOrder.Add(index);
            }
            foreach (VisualJoint joint in elements[index].joints) {
                if (joint.type == VisualJoint.LEFT && joint.Connected()) {
                    VariablesOrder(elements, joint.connectedSerial[0], variablesOrder);
                }
            }
            return variablesOrder;
        }

        internal static string Demand(List<VisualElement> elements, VisualJoint joint, string precision) {
            return joint.Connected() ? Demand(elements, joint.connectedSerial[0], joint.connectedSerial[1], joint.size, precision) : "0.0";
        }

        internal static string Demand(List<VisualElement> elements, VisualJoint joint, int destinedSize, string precision) {
            return joint.Connected() ? Demand(elements, joint.connectedSerial[0], joint.connectedSerial[1], destinedSize, precision) : "0.0";
        }

        internal static string Demand(List<VisualElement> elements, int element, int joint, int destinedSize, string precision) {
            string output;
            string data;
            int actualSize;
            data = elements[element].Generate(elements, destinedSize, precision)[joint];
            actualSize = elements[element].joints[joint].size;
            if (actualSize == 0) {
                actualSize = destinedSize;
            }
            if (destinedSize > actualSize && actualSize != 1) {
                output = precision + destinedSize + "(" + data;
                for (int i = 0; i < destinedSize - actualSize; ++i) {
                    output += ", 0.0";
                }
                output += ")";
            } else if (destinedSize < actualSize) {
                output = data + ".";
                for (int i = 0; i < destinedSize; ++i) {
                    output += Utils.components[i][1];
                }
            } else {
                output = data;
            }
            return output;
        }
    }
}
