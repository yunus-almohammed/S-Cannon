using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace DawidMoza.PostProcessingUltimate {
	[Serializable]
	[PostProcess(typeof(RadialBlur2Renderer), PostProcessEvent.AfterStack, "PPU/RadialBlur2")]
	public sealed class RadialBlur2 : PostProcessEffectSettings {
		[Range(0f, 1f), Tooltip("Effect intensity")]
		public FloatParameter Intensity = new FloatParameter {value = 0.3f};
	}

	public sealed class RadialBlur2Renderer : PostProcessEffectRenderer<RadialBlur2> {
		public override void Render(PostProcessRenderContext context) {
			var sheet = context.propertySheets.Get(Shader.Find("PPU/RadialBlur2"));
			sheet.properties.SetFloat("_Intensity", settings.Intensity);
			context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
		}
	}
}