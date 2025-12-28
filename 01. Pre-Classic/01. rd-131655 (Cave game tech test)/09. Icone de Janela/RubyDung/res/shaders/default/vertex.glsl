#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aColor;
layout (location = 2) in vec2 aTexCoord;

out vec3 ourColor;
out vec2 TexCoord;

void main()
{
    gl_Position = vec4(aPosition.x, aPosition.y, aPosition.z, 1.0f);

    ourColor = vec3(aColor.x, aColor.y, aColor.z);

    TexCoord = vec2(aTexCoord.x, aTexCoord.y);
}
