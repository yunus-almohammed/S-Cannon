const ppu = []

ppu["header_ultimate"] = `
<a href="index.html" id="name">
	<p id="nameP">Post Processing Ultimate</p>
</a>
<hr>
<a href="index.html" class="ta">Home</a>
<a href="samples.html" class="ta">Effects</a>
<a href="nodes.html" class="ta">Nodes</a>
<a href="tutorials.html" class="ta">Tutorials</a>
<a href="misc.html" class="ta">Misc</a>
`

ppu["header_simple"] = `
<a href="index.html" id="name">
	<p id="nameP">Simple Post Processing</p>
</a>
<hr>
<a href="index.html" class="ta">Home</a>
<a href="samples.html" class="ta">Effects</a>
<a href="misc.html" class="ta">Misc</a>
`

ppu["navigation_misc"] = `
<li><a href="#pi" class="na">Picker</a></li>
<li><a href="#bl" class="na">Blender</a></li>
<li><a href="#screenshot" class="na">Screenshot</a></li>
<li><a href="#stackEffect" class="na">Stack Effect</a></li>
`

ppu["content_misc"] = `
<a name="pi"><h1>Picker</h1></a>
<hr>
<p>- Add Post Processing Volume to a camera</p>
<p>- Add Unity Color Grading effect to the Volume profile</p>
<p>- Set effect Mode to Low Definition Range</p>
<p>- Open picker (Window/Post Processing Ultimate/LUT Picker), select camera and find the most suitable LUT texture</p>
<img src="../ppu_common/images/misc/picker.png" class="wide">
<a name="bl"><h1>Blender</h1></a>
<hr>
<p>- Add Post Processing Volume to a camera</p>
<p>- Add Unity Color Grading effect to the Volume profile</p>
<p>- Set effect Mode to Low Definition Range</p>
<p>- Open blender (Window/Post Processing Ultimate/LUT Blender), select camera</p>
<img src="../ppu_common/images/misc/blender.png" class="wide">
<p>- Select LUT textures and blend them</p>
<p>- Save new texture, it can be saved inside PPU LUT textures folder, so the texture will be avaiable in LUT Picker window</p>
<a name = "screenshot"><h1>Screenshot</h1></a>
<hr>
<p>- Open screenshot tool (Window/Post Processing Ultimate/Screenshot), select camera</p>
<img src="../ppu_common/images/misc/screenshot.png" class="wide">
<p>- Adjust dimensions</p>
<p>- Take screenshot and choose location</p>
<a name="stackEffect"><h1>Stack Effect</h1></a>
<hr>
<img src="../ppu_common/images/misc/stackEffect.png" class="wide">
<p>- Specify either GameObject, Component, PostProcessVolume or PostProcessProfile and effect name</p>
`

ppu["navigation_samples"] = `
<li><a href="#blur" class="na">Blur</a></li>
<li><a href="#ca" class="na">Chromatic Abberation</a></li>
<li><a href="#cc" class="na">Color Curves</a></li>
<li><a href="#ce" class="na">Crossed Eyes</a></li>
<li><a href="#dis" class="na">Distortion</a></li>
<li><a href="#dru" class="na">Drunk</a></li>
<li><a href="#ed" class="na">Edge Detection</a></li>
<li><a href="#ec" class="na">Eight Colors</a></li>
<li><a href="#ec2" class="na">Eight Colors 2</a></li>
<li><a href="#fd" class="na">Fade</a></li>
<li><a href="#fog" class="na">Fog</a></li>
<li><a href="#fra" class="na">Frame</a></li>
<li><a href="#gli" class="na">Glitch</a></li>
<li><a href="#gli2" class="na">Glitch 2</a></li>
<li><a href="#hbloom" class="na">Horizontal Bloom</a></li>
<li><a href="#ic" class="na">Industrial Camera</a></li>
<li><a href="#ir" class="na">Infrared</a></li>
<li><a href="#inv" class="na">Invert</a></li>
<li><a href="#noc" class="na">Noctovision</a></li>
<li><a href="#noi" class="na">Noise</a></li>
<li><a href="#of" class="na">Old Film</a></li>
<li><a href="#over" class="na">Overlay</a></li>
<li><a href="#pix" class="na">Pixelize</a></li>
<li><a href="#pos" class="na">Posterize</a></li>
<li><a href="#rb" class="na">Radial Blur</a></li>
<li><a href="#rb2" class="na">Radial Blur 2</a></li>
<li><a href="#sl" class="na">Scan Lines</a></li>
<li><a href="#sss" class="na">Screen Space Shadows</a></li>
<li><a href="#sc" class="na">Select Color</a></li>
<li><a href="#sep" class="na">Sepia</a></li>
<li><a href="#sharp" class="na">Sharpen</a></li>
<li><a href="#splat" class="na">Splatter</a></li>
<li><a href="#thres" class="na">Threshold</a></li>
`

ppu["content_samples"] = `
<a name = "blur"><h1>Blur</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/blur.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect Intensity</td>
		</tr>
		<tr>
			<td class = "left">Iterations</td>
			<td class = "right">Effect Iterations</td>
		</tr>
	</table>
</div>
<a name = "ca"><h1>Chromatic Abberation</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/ca.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Iterations</td>
			<td class = "right">Effect Iterations</td>
		</tr>
		<tr>
			<td class = "left">Offset</td>
			<td class = "right">Channels offset</td>
		</tr>
	</table>
</div>
<a name = "cc"><h1>Color Curves</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/cc.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Red</td>
			<td class = "right">Red channel curve</td>
		</tr>
		<tr>
			<td class = "left">Green</td>
			<td class = "right">Green channel curve</td>
		</tr>
		<tr>
			<td class = "left">Blue</td>
			<td class = "right">Blue channel curve</td>
		</tr>
		<tr>
			<td class = "left">Tint</td>
			<td class = "right">Image filter</td>
		</tr>
	</table>
</div>
<a name = "ce"><h1>Crossed Eyes</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/ce.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Shift</td>
			<td class = "right">Horizontal shift</td>
		</tr>
	</table>
</div>
<a name = "dis"><h1>Distortion</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/dis.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect intensity</td>
		</tr>
		<tr>
			<td class = "left">Coords</td>
			<td class = "right">New coords</td>
		</tr>
	</table>
</div>
<a name = "dru"><h1>Drunk</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/dru.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Horizontal</td>
			<td class = "right">Horizontal wobble</td>
		</tr>
		<tr>
			<td class = "left">Vertical</td>
			<td class = "right">Vertical wobble</td>
		</tr>
		<tr>
			<td class = "left">Speed</td>
			<td class = "right">Animation speed</td>
		</tr>
	</table>
</div>
<a name = "ed"><h1>Edge Detection</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/ed.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Edge</td>
			<td class = "right">Edge offset</td>
		</tr>
	</table>
</div>
<a name = "ec"><h1>Eight Colors</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/ec.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">One Eigth</td>
			<td class = "right">0.000 to 0.125 luminosity</td>
		</tr>
		<tr>
			<td class = "left">Two Eigths</td>
			<td class = "right">0.125 to 0.250 luminosity</td>
		</tr>
		<tr>
			<td class = "left">Three Eigths</td>
			<td class = "right">0.250 to 0.375 luminosity</td>
		</tr>
		<tr>
			<td class = "left">Four Eigths</td>
			<td class = "right">0.375 to 0.500 luminosity</td>
		</tr>
		<tr>
			<td class = "left">Five Eigths</td>
			<td class = "right">0.500 to 0.625 luminosity</td>
		</tr>
		<tr>
			<td class = "left">Six Eigths</td>
			<td class = "right">0.625 to 0.750 luminosity</td>
		</tr>
		<tr>
			<td class = "left">Seven Eigths</td>
			<td class = "right">0.750 to 0.875 luminosity</td>
		</tr>
		<tr>
			<td class = "left">Full</td>
			<td class = "right">0.875 to 1.000 luminosity</td>
		</tr>
	</table>
</div>
<a name = "ec2"><h1>Eight Colors 2</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/ec2.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Start</td>
			<td class = "right">Base color</td>
		</tr>
		<tr>
			<td class = "left">One Eigth</td>
			<td class = "right">0.000 to 0.125 luminosity</td>
		</tr>
		<tr>
			<td class = "left">Two Eigths</td>
			<td class = "right">0.125 to 0.250 luminosity</td>
		</tr>
		<tr>
			<td class = "left">Three Eigths</td>
			<td class = "right">0.250 to 0.375 luminosity</td>
		</tr>
		<tr>
			<td class = "left">Four Eigths</td>
			<td class = "right">0.375 to 0.500 luminosity</td>
		</tr>
		<tr>
			<td class = "left">Five Eigths</td>
			<td class = "right">0.500 to 0.625 luminosity</td>
		</tr>
		<tr>
			<td class = "left">Six Eigths</td>
			<td class = "right">0.625 to 0.750 luminosity</td>
		</tr>
		<tr>
			<td class = "left">Seven Eigths</td>
			<td class = "right">0.750 to 0.875 luminosity</td>
		</tr>
		<tr>
			<td class = "left">Full</td>
			<td class = "right">0.875 to 1.000 luminosity</td>
		</tr>
	</table>
</div>
<a name = "fd"><h1>Fade</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/fd.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect intensity</td>
		</tr>
	</table>
</div>
<a name = "fog"><h1>Fog</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/fog.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Color</td>
			<td class = "right">Fog color</td>
		</tr>
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect Intensity</td>
		</tr>
		<tr>
			<td class = "left">Distance</td>
			<td class = "right">Distance at which fog should appear</td>
		</tr>
		<tr>
			<td class = "left">Height</td>
			<td class = "right">Fog height</td>
		</tr>
	</table>
</div>
<a name = "fra"><h1>Frame</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/fra.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Horizontal</td>
			<td class = "right">Horizontal frame</td>
		</tr>
		<tr>
			<td class = "left">Vertical</td>
			<td class = "right">Vertical frame</td>
		</tr>
	</table>
</div>
<a name = "gli"><h1>Glitch</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/gli.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Speed</td>
			<td class = "right">Animation speed</td>
		</tr>
		<tr>
			<td class = "left">Size</td>
			<td class = "right">Bug size</td>
		</tr>
	</table>
</div>
		<a name = "gli2"><h1>Glitch 2</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/gli2.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect intensity</td>
		</tr>
		<tr>
			<td class = "left">Speed</td>
			<td class = "right">Animation speed</td>
		</tr>
		<tr>
			<td class = "left">Saturation</td>
			<td class = "right">Glitch saturation</td>
		</tr>
		<tr>
			<td class = "left">Lines</td>
			<td class = "right">Number of lines</td>
		</tr>
	</table>
</div>
<a name = "hbloom"><h1>Horizontal Bloom</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/hblur.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect intensity</td>
		</tr>
		<tr>
			<td class = "left">Threshold</td>
			<td class = "right">Minimum luminosity</td>
		</tr>
		<tr>
			<td class = "left">Quality</td>
			<td class = "right">Number of blur iterations</td>
		</tr>
	</table>
</div>
<a name = "ic"><h1>Industrial Camera</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/ic.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect Intensity</td>
		</tr>
		<tr>
			<td class = "left">Lines</td>
			<td class = "right">Lines visibility</td>
		</tr>
	</table>
</div>
<a name = "ir"><h1>Infrared</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/ir.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect intensity</td>
		</tr>
	</table>
</div>
<a name = "inv"><h1>Invert</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/inv.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect intensity</td>
		</tr>
	</table>
</div>
<a name = "noc"><h1>Noctovision</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/noc.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Green</td>
			<td class = "right">Green intensity</td>
		</tr>
		<tr>
			<td class = "left">Noise</td>
			<td class = "right">Noise Intensity</td>
		</tr>
	</table>
</div>
<a name = "noi"><h1>Noise</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/noi.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect intensity</td>
		</tr>
		<tr>
			<td class = "left">Saturation</td>
			<td class = "right">Noise saturation</td>
		</tr>
	</table>
</div>
<a name = "of"><h1>Old Film</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/of.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Noise</td>
			<td class = "right">Noise Intensity</td>
		</tr>
		<tr>
			<td class = "left">Oscillation</td>
			<td class = "right">Oscillation amplitude</td>
		</tr>
	</table>
</div>
<a name = "over"><h1>Overlay</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/over.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect intensity</td>
		</tr>
		<tr>
			<td class = "left">Image</td>
			<td class = "right">Overlay texture</td>
		</tr>
	</table>
</div>
<a name = "pix"><h1>Pixelize</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/pix.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Horizontal</td>
			<td class = "right">Horizontal resolution</td>
		</tr>
		<tr>
			<td class = "left">Vertical</td>
			<td class = "right">Vertical resolution</td>
		</tr>
	</table>
</div>
<a name = "pos"><h1>Posterize</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/pos.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Colors</td>
			<td class = "right">Number of colors in each channel</td>
		</tr>
	</table>
</div>
<a name = "rb"><h1>Radial Blur</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/rb.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Iterations</td>
			<td class = "right">Effect iterations</td>
		</tr>
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect Intensity</td>
		</tr>
	</table>
</div>
<a name = "rb2"><h1>Radial Blur 2</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/rb2.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect Intensity</td>
		</tr>
	</table>
</div>
<a name = "sl"><h1>Scan Lines</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/sl.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect intensity</td>
		</tr>
		<tr>
			<td class = "left">Width</td>
			<td class = "right">Line width</td>
		</tr>
		<tr>
			<td class = "left">Gap</td>
			<td class = "right">Gap between lines</td>
		</tr>
	</table>
</div>
<a name = "sss"><h1>Screen Space Shadows</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/sss.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect intensity</td>
		</tr>
		<tr>
			<td class = "left">Power</td>
			<td class = "right">Effect power</td>
		</tr>
	</table>
</div>
<a name = "sc"><h1>Select Color</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/sc.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect intensity</td>
		</tr>
		<tr>
			<td class = "left">Selected</td>
			<td class = "right">Selected color</td>
		</tr>
	</table>
</div>
<a name = "sep"><h1>Sepia</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/sep.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Color</td>
			<td class = "right">Sepia color</td>
		</tr>
	</table>
</div>
<a name = "sharp"><h1>Sharpen</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/sharp.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect Intensity</td>
		</tr>
	</table>
</div>
<a name = "splat"><h1>Splatter</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/splat.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect intensity</td>
		</tr>
		<tr>
			<td class = "left">Image</td>
			<td class = "right">Situation texture</td>
		</tr>
	</table>
</div>
<a name = "thres"><h1>Threshold</h1></a>
<hr>
<div class = "node">
	<div class = "image-slider">
		<div><img src = "../ppu_common/images/effects/a00.png"/></div>
		<img src = "../ppu_common/images/effects/thres.png"/>
	</div>
	<br>
	<br>
	<table class = "table1">
		<tr>
			<td class = "left">Intensity</td>
			<td class = "right">Effect Intensity</td>
		</tr>
		<tr>
			<td class = "left">Level</td>
			<td class = "right">Threshold level</td>
		</tr>
	</table>
</div>
`

ppu["footer"] = `
<label class = "support">Support: </label><a href = "mailto:support@dawidmoza.pl" class = "fa">support@dawidmoza.pl</a>
<br>
<label class = "support">&copy; Dawid Moza</label>
`