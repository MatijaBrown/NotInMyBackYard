#version 330 core

in vec2 pass_tcoord;

out vec4 out_Colour;

uniform sampler2D image;

void main(void) {
	out_Colour = texture(image, pass_tcoord);
}