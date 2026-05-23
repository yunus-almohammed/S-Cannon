using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace DawidMoza.PostProcessingUltimate {
	[Serializable]
	[PostProcess(typeof(InvertRenderer), PostProcessEvent.AfterStack, "PPU/Invert")]
	public sealed class Invert : PostProcessEffectSettings {
		[Range(0f, 1f), Tooltip("Effect intensity")]
		public FloatParameter Intensity = new FloatParameter {value = 1f};
	}

	public sealed class InvertRenderer : PostProcessEffectRenderer<Invert> {
		public override void Render(PostProcessRenderContext context) {
			var sheet = context.propertySheets.Get(Shader.Find("PPU/Invert"));
			sheet.properties.SetFloat("_Intensity", settings.Intensity);
			context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
		}
	}
}