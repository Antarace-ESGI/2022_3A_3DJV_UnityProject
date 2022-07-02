// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Skybox/CustomSkyboxShader"
{
    Properties
    {
        _Tint ("Tint Color", Color) = (.5, .5, .5, .5)
        [Gamma] _Exposure ("Exposure", Range(0, 8)) = 1.0
        _Rotation ("Rotation", Range(0, 360)) = 0
        [NoScaleOffset] _FrontTex ("Front [+Z]   (HDR)", 2D) = "grey" {}
        [NoScaleOffset] _BackTex ("Back [-Z]   (HDR)", 2D) = "grey" {}
        [NoScaleOffset] _LeftTex ("Left [+X]   (HDR)", 2D) = "grey" {}
        [NoScaleOffset] _RightTex ("Right [-X]   (HDR)", 2D) = "grey" {}
        [NoScaleOffset] _UpTex ("Up [+Y]   (HDR)", 2D) = "grey" {}
        [NoScaleOffset] _DownTex ("Down [-Y]   (HDR)", 2D) = "grey" {}
    }

    SubShader
    {
        Tags
        {
            "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox"
        }
        Cull Off ZWrite Off

        CGINCLUDE
        #include "UnityCG.cginc"

        half4 _Tint;
        half _Exposure;
        float _Rotation;

        float3 RotateAroundYInDegrees(float3 vertex, float degrees)
        {
            float alpha = degrees * UNITY_PI / 180.0;
            float sina, cosa;
            sincos(alpha, sina, cosa);
            float2x2 m = float2x2(cosa, -sina, sina, cosa);
            return float3(mul(m, vertex.xz), vertex.y).xzy;
        }

        struct appdata_t
        {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
            UNITY_VERTEX_INPUT_INSTANCE_ID
        };

        struct v2f
        {
            float4 vertex : SV_POSITION;
            float2 texcoord : TEXCOORD0;
            UNITY_VERTEX_OUTPUT_STEREO
        };

        v2f vert(appdata_t v)
        {
            v2f o;
            UNITY_SETUP_INSTANCE_ID(v);
            UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
            float3 rotated = RotateAroundYInDegrees(v.vertex, _Rotation);
            o.vertex = UnityObjectToClipPos(rotated);
            o.texcoord = v.texcoord;
            return o;
        }

        #define iterations 17
        #define formuparam 0.53

        #define volsteps 20
        #define stepsize 0.1

        #define zoom   0.800
        #define tile   0.850
        #define speed  0.010

        #define brightness 0.0015
        #define darkmatter 0.300
        #define distfading 0.730
        #define saturation 0.850

        #define mod(x, y) (x-y*floor(x/y))


        half4 skybox_frag(v2f i, sampler2D smp, half4 smpDecode)
        {
            float2 uv = i.texcoord - .5;
            float3 dir = float3(uv * zoom, 1.);
            float time = _Time.y * speed + .25;

            float a1 = .5; // + iMouse.x / iResolution.x * 2.;
            float a2 = .8; // + iMouse.y / iResolution.y * 2.;
            float2x2 rot1 = float2x2(cos(a1), sin(a1), -sin(a1), cos(a1));
            float2x2 rot2 = float2x2(cos(a2), sin(a2), -sin(a2), cos(a2));
            dir.xz = mul(dir.xz, rot1);
            dir.xy = mul(dir.xy, rot2);
            float3 from = float3(1., .5, 0.5);
            from += float3(time * 2., time, -2.);
            from.xz = mul(from.xz, rot1);
            from.xy = mul(from.xy, rot2);

            float s = 0.1, fade = 1.;
            float3 v = 0.;
            for (int r = 0; r < volsteps; r++)
            {
                float3 p = from + s * dir * .5;
                p = abs(tile - mod(p, tile * 2.));
                float pa, a = pa = 0.;
                for (int i = 0; i < iterations; i++)
                {
                    p = abs(p) / dot(p, p) - formuparam;
                    a += abs(length(p) - pa);
                    pa = length(p);
                }
                float dm = max(0.,darkmatter - a * a * .001);
                a *= a * a;
                if (r > 6) fade *= 1. - dm;

                v += fade;
                v += float3(s, s * s, s * s * s * s) * a * brightness * fade;
                fade *= distfading;
                s += stepsize;
            }
            v = lerp(float3(length(v), length(v), length(v)), v,saturation);
            return float4(v * .01, 1.);
        }
        ENDCG

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            sampler2D _FrontTex;
            half4 _FrontTex_HDR;
            half4 frag(v2f i) : SV_Target { return skybox_frag(i, _FrontTex, _FrontTex_HDR); }
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            sampler2D _BackTex;
            half4 _BackTex_HDR;
            half4 frag(v2f i) : SV_Target { return skybox_frag(i, _BackTex, _BackTex_HDR); }
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            sampler2D _LeftTex;
            half4 _LeftTex_HDR;
            half4 frag(v2f i) : SV_Target { return skybox_frag(i, _LeftTex, _LeftTex_HDR); }
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            sampler2D _RightTex;
            half4 _RightTex_HDR;
            half4 frag(v2f i) : SV_Target { return skybox_frag(i, _RightTex, _RightTex_HDR); }
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            sampler2D _UpTex;
            half4 _UpTex_HDR;
            half4 frag(v2f i) : SV_Target { return skybox_frag(i, _UpTex, _UpTex_HDR); }
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            sampler2D _DownTex;
            half4 _DownTex_HDR;
            half4 frag(v2f i) : SV_Target { return skybox_frag(i, _DownTex, _DownTex_HDR); }
            ENDCG
        }
    }
}