#version 400 core

in vec2 pass_texCoord;

out vec4 out_Colour;

uniform sampler2D tex;

void main(void) {
	out_Colour = texture(tex, pass_texCoord);
}