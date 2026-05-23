using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace DawidMoza.PostProcessingUltimate {
	[Serializable]
	[PostProcess(typeof(PosterizeRenderer), PostProcessEvent.AfterStack, "PPU/Posterize")]
	public sealed class Posterize : PostProcessEffectSettings {
		[Tooltip("Number of colors in each channel")]
		public IntParameter Colors = new IntParameter {value = 8};
	}

	public sealed class PosterizeRenderer : PostProcessEffectRenderer<Posterize> {
		public override void Render(PostProcessRenderContext context) {
			var sheet = context.propertySheets.Get(Shader.Find("PPU/Posterize"));
			sheet.properties.SetFloat("_Colors", settings.Colors);
			context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
		}
	}
}