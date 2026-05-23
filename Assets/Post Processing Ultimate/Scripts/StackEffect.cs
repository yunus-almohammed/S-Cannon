using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace DawidMoza.PostProcessingUltimate {
	public static class StackEffect {

		// Example
		// ColorGrading colorGrading = (ColorGrading) StackEffect.GetEffect(Camera.main, "ColorGrading");

		public static PostProcessEffectSettings GetEffect(GameObject source, string name) {
			if (source == null || name == null) {
				return null;
			}
			PostProcessVolume postProcessVolume = source.GetComponent<PostProcessVolume>();
			return GetEffect(postProcessVolume, name);
		}

		public static PostProcessEffectSettings GetEffect(Component source, string name) {
			if (source == null || name == null) {
				return null;
			}
			PostProcessVolume postProcessVolume = source.GetComponent<PostProcessVolume>();
			return GetEffect(postProcessVolume, name);

		}

		public static PostProcessEffectSettings GetEffect(PostProcessVolume source, string name) {
			if (source == null || name == null || source.sharedProfile == null) {
				return null;
			}
			return GetEffect(source.sharedProfile, name);
		}

		public static PostProcessEffectSettings GetEffect(PostProcessProfile source, string name) {
			if (source == null || name == null || source.settings == null) {
				return null;
			}
			List<PostProcessEffectSettings> settings = source.settings;
			foreach (PostProcessEffectSettings setting in settings) {
				if (setting.ToString().Contains(name)) {
					return setting;
				}
			}
			return null;
		}
	}
}