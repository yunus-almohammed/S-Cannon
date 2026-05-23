using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace DawidMoza.PostProcessingUltimate {
	[Serializable]
	[PostProcess(typeof(EdgeDetectionRenderer), PostProcessEvent.AfterStack, "PPU/EdgeDetection")]
	public sealed class EdgeDetection : PostProcessEffectSettings {
		[Range(0f, 1f), Tooltip("Edge Offset")]
		public FloatParameter Edge = new FloatParameter {value = 0.5f};
	}

	public sealed class EdgeDetectionRenderer : PostProcessEffectRenderer<EdgeDetection> {
		public override void Render(PostProcessRenderContext context) {
			var sheet = context.propertySheets.Get(Shader.Find("PPU/EdgeDetection"));
			sheet.properties.SetFloat("_Edge", settings.Edge);
			context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
		}
	}
}