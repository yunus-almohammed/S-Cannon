using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace DawidMoza.PostProcessingUltimate {
	[Serializable]
	[PostProcess(typeof(InfraredRenderer), PostProcessEvent.AfterStack, "PPU/Infrared")]
	public sealed class Infrared : PostProcessEffectSettings {
		[Range(0f, 1f), Tooltip("Effect intensity")]
		public FloatParameter Intensity = new FloatParameter {value = 0.3f};
	}

	public sealed class InfraredRenderer : PostProcessEffectRenderer<Infrared> {
		public override void Render(PostProcessRenderContext context) {
			var sheet = context.propertySheets.Get(Shader.Find("PPU/Infrared"));
			sheet.properties.SetFloat("_Intensity", settings.Intensity);
			context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
		}
	}
}