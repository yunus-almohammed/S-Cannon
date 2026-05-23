using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace DawidMoza.PostProcessingUltimate {
	[Serializable]
	[PostProcess(typeof(NoiseRenderer), PostProcessEvent.AfterStack, "PPU/Noise")]
	public sealed class Noise : PostProcessEffectSettings {
		[Range(0f, 1f), Tooltip("Effect intenisty")]
		public FloatParameter Intensity = new FloatParameter {value = 0.5f};
		[Range(0f, 1f), Tooltip("Noise saturation")]
		public FloatParameter Saturation = new FloatParameter {value = 0.5f};
		internal RenderTexture tempRT0;
	}

	public sealed class NoiseRenderer : PostProcessEffectRenderer<Noise> {
		public override void Render(PostProcessRenderContext context) {
			var sheet = context.propertySheets.Get(Shader.Find("PPU/Noise"));
			RenderTexture.ReleaseTemporary(settings.tempRT0);
			sheet.properties.SetFloat("_Intensity", settings.Intensity);
			sheet.properties.SetFloat("_Saturation", settings.Saturation);
			settings.tempRT0 = RenderTexture.GetTemporary(Screen.width, Screen.height);
			context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
		}
	}
}