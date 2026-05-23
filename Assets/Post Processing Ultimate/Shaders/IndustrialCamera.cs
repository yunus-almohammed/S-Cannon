using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace DawidMoza.PostProcessingUltimate {
	[Serializable]
	[PostProcess(typeof(IndustrialCameraRenderer), PostProcessEvent.AfterStack, "PPU/IndustrialCamera")]
	public sealed class IndustrialCamera : PostProcessEffectSettings {
		[Range(0f, 1f), Tooltip("Effect intensity")]
		public FloatParameter Intensity = new FloatParameter {value = 0.5f};
		[Range(0f, 1f), Tooltip("Lines visibility")]
		public FloatParameter Lines = new FloatParameter {value = 0.5f};
		internal RenderTexture tempRT0;
	}

	public sealed class IndustrialCameraRenderer : PostProcessEffectRenderer<IndustrialCamera> {
		public override void Render(PostProcessRenderContext context) {
			var sheet = context.propertySheets.Get(Shader.Find("PPU/IndustrialCamera"));
			RenderTexture.ReleaseTemporary(settings.tempRT0);
			sheet.properties.SetFloat("_Intensity", settings.Intensity);
			sheet.properties.SetFloat("_Lines", settings.Lines);
			settings.tempRT0 = RenderTexture.GetTemporary(Screen.width, Screen.height);
			context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
		}
	}
}