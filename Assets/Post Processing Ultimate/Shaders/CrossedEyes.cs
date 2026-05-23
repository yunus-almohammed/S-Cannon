using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace DawidMoza.PostProcessingUltimate {
	[Serializable]
	[PostProcess(typeof(CrossedEyesRenderer), PostProcessEvent.AfterStack, "PPU/CrossedEyes")]
	public sealed class CrossedEyes : PostProcessEffectSettings {
		[Range(0f, 1f), Tooltip("Horizontal shift")]
		public FloatParameter Shift = new FloatParameter {value = 0.5f};
	}

	public sealed class CrossedEyesRenderer : PostProcessEffectRenderer<CrossedEyes> {
		public override void Render(PostProcessRenderContext context) {
			var sheet = context.propertySheets.Get(Shader.Find("PPU/CrossedEyes"));
			sheet.properties.SetFloat("_Shift", settings.Shift);
			context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
		}
	}
}