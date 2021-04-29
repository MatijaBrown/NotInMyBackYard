#version 330

layout (location = 0) in vec2 vertex;
layout (location = 1) in vec2 tcoord;

out vec2 pass_tcoord;

uniform mat4 transformation;
uniform mat4 projection;

void main(void) {
	pass_tcoord = tcoord;
	gl_Position = projection * transformation * vec4(vertex, 0.0, 1.0);
}