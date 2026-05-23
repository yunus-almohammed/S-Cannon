using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace DawidMoza.PostProcessingUltimate {
    internal static class MiscUtils {

        internal const string name = "Post Processing Ultimate";

        internal static Texture2D CurrentScreen(Camera camera, int width = 854, int height = 480) {
            RenderTexture rt = RenderTexture.GetTemporary(width, height, 16);
            camera.targetTexture = rt;
            Texture2D image = new Texture2D(width, height, TextureFormat.RGB24, false);
            camera.Render();
            RenderTexture.active = rt;
            image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            image.Apply();
            camera.targetTexture = null;
            RenderTexture.active = null;
            RenderTexture.ReleaseTemporary(rt);
            return image;
        }

        internal static List<Camera> CamerasThatContainsEffect(string effectName) {
            List<Camera> tempCameras = Object.FindObjectsOfType<Camera>().ToList();
            List<Camera> cameras = new List<Camera>();
            for (int i = 0; i < tempCameras.Count; ++i) {
                if (tempCameras[i] != null && tempCameras[i].enabled) {
                    PostProcessVolume volume = tempCameras[i].GetComponent<PostProcessVolume>();
                    if (volume == null) {
                        continue;
                    }
                    for (int j = 0; j < volume.sharedProfile.settings.Count; ++j) {
                        if (volume.sharedProfile.settings[j].ToString().Contains(effectName)) {
                            cameras.Add(tempCameras[i]);
                            break;
                        }
                    }
                }
            }
            return cameras;
        }

        internal static Rect CenteredSize(Vector2 size) { 
            return new Rect((UnityEngine.Screen.currentResolution.width - size.x) / 2, (UnityEngine.Screen.currentResolution.height - size.y) / 2, size.x, size.y);
        }
    }
}
