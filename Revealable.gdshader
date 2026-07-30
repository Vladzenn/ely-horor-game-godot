shader_type canvas_item;

uniform vec2 flashlight_position;
uniform float flashlight_rotation;

uniform sampler2D flashlight_texture;

uniform float flashlight_scale = 300.0;


void fragment()
{
    vec4 sprite_color = texture(TEXTURE, UV);
    vec2 pixel_position = FRAGCOORD.xy;

    vec2 local_position = pixel_position - flashlight_position;
    float angle = -flashlight_rotation;

    float c = cos(angle);
    float s = sin(angle);

    local_position = vec2(
        local_position.x * c - local_position.y * s,
        local_position.x * s + local_position.y * c
    );

    vec2 flashlight_uv =
        local_position / flashlight_scale;

    flashlight_uv += vec2(0.5);

    if (
        flashlight_uv.x < 0.0 ||
        flashlight_uv.x > 1.0 ||
        flashlight_uv.y < 0.0 ||
        flashlight_uv.y > 1.0
    )
    {
        sprite_color.a = 0.0;
    }
    else
    {
        float reveal =
            texture(
                flashlight_texture,
                flashlight_uv
            ).r;
        sprite_color.a *= reveal;
    }
    COLOR = sprite_color;
}