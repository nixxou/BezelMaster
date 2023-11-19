/*------------------.
| :: Description :: |
'-------------------/

    Layer (version 0.2)

    Author: CeeJay.dk
    License: MIT

    About:
    Blends an image with the game.
    The idea is to give users with graphics skills the ability to create effects using a layer just like in an image editor.
    Maybe they could use this to create custom CRT effects, custom vignettes, logos, custom hud elements, toggable help screens and crafting tables or something I haven't thought of.

    Ideas for future improvement:
    * More blend modes
    * Tiling control
    * A default Layer texture with something useful in it

    History:
    (*) Feature (+) Improvement (x) Bugfix (-) Information (!) Compatibility

    Version 0.2 by seri14 & Marot Satil
    * Added the ability to scale and move the layer around on XY axis
*/

#include "ReShade.fxh"

#ifndef BEZEL_DISABLE
#define BEZEL_DISABLE 0
#endif

#if LAYER_SINGLECHANNEL
    #define TEXFORMAT R8
#else
    #define TEXFORMAT RGBA8
#endif

#include "ReShadeUI.fxh"

uniform float2 Bezel_Pos < __UNIFORM_DRAG_FLOAT2
    ui_label = "Layer Position";
    ui_min = 0.0; ui_max = 1.0;
    ui_step = (1.0 / 200.0);
> = float2(0.5, 0.5);

uniform float Bezel_Resize_X < __UNIFORM_DRAG_FLOAT1
    ui_label = "Layer Resize X";
    ui_min = -1000.0; ui_max = 1000.0;
    ui_step = 1.0;
> = 0.0;

uniform float Bezel_Resize_Y < __UNIFORM_DRAG_FLOAT1
    ui_label = "Layer Resize Y";
    ui_min = -1000; ui_max = 1000;
    ui_step = 1;
> = 0.0;

uniform float Bezel_Resize < __UNIFORM_DRAG_FLOAT1
    ui_label = "Layer Scale";
    ui_min = (1.0 / 100.0); ui_max = 4.0;
    ui_step = (1.0 / 250.0);
> = 1.0;

uniform float Bezel_Blend < __UNIFORM_COLOR_FLOAT1
    ui_label = "Layer Blend";
    ui_tooltip = "How much to blend layer with the original image.";
    ui_min = 0.0; ui_max = 1.0;
    ui_step = (1.0 / 255.0); // for slider and drag
> = 1.0;

texture Layer_Tex <
    source = "bezel.png";
> {
    Format = TEXFORMAT;
    Width  = BUFFER_WIDTH;
    Height = BUFFER_HEIGHT;
};

sampler Layer_Sampler
{
    Texture  = Layer_Tex;
    AddressU = BORDER;
    AddressV = BORDER;
};

void PS_Bezel(float4 pos : SV_Position, float2 texCoord : TEXCOORD, out float4 passColor : SV_Target)
{
		const float4 backColor = tex2D(ReShade::BackBuffer, texCoord);
		const float2 pixelSize = 1.0 / (float2(((Bezel_Resize_X/1000.0)*BUFFER_WIDTH)+BUFFER_WIDTH, ((Bezel_Resize_Y/1000.0)*BUFFER_HEIGHT)+BUFFER_HEIGHT) * Bezel_Resize / BUFFER_SCREEN_SIZE);
		const float4 layer     = tex2D(Layer_Sampler, texCoord * pixelSize + Bezel_Pos * (1.0 - pixelSize));

		passColor   = lerp(backColor, layer, layer.a * Bezel_Blend);
		passColor.a = backColor.a;
		
	
}

technique Bezel
{
#if BEZEL_DISABLE == 0
    pass
    {
		VertexShader = PostProcessVS;
		PixelShader  = PS_Bezel;
    }
#endif
}
