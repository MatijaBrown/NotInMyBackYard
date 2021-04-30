#version 400 core

layout (location = 0) in vec2 vertex;
layout (location = 1) in vec2 texCoord;

out vec2 pass_texCoord;

uniform mat4 transformation;
uniform vec2 viewSize;

void main(void) {
	pass_texCoord = texCoord;
	gl_Position = transformation * vec4(2.0 * vertex.x / viewSize.x - 1.0, 2.0 * vertex.y / viewSize.y - 1.0, 0.0, 1.0);
}