#version 400 core

out vec4 out_Colour;

uniform vec4 colour;

void main(void) {
	out_Colour = colour;

	if (out_Colour.w < 0.5)
		discard;
}