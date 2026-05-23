using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace DawidMoza.PostProcessingUltimate {
	[Serializable]
	[PostProcess(typeof(FadeRenderer), PostProcessEvent.AfterStack, "PPU/Fade")]
	public sealed class Fade : PostProcessEffectSettings {
		[Range(0f, 1f), Tooltip("Effect intensity")]
		public FloatParameter Intensity = new FloatParameter {value = 0.5f};
	}

	public sealed class FadeRenderer : PostProcessEffectRenderer<Fade> {
		public override void Render(PostProcessRenderContext context) {
			var sheet = context.propertySheets.Get(Shader.Find("PPU/Fade"));
			sheet.properties.SetFloat("_Intensity", settings.Intensity);
			context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
		}
	}
}