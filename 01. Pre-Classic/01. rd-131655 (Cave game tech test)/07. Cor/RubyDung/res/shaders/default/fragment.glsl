#version 330 core
out vec4 FragColor;

in vec3 ourColor;

uniform bool hasColor;

void main()
{
    FragColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);

    if(hasColor)
    {
        FragColor *= vec4(ourColor.x, ourColor.y, ourColor.z, 1.0f);
    }
}
