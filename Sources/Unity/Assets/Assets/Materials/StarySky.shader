Shader "Unlit/StarySky"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            // START STAR SKY

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


            // UNITY
            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv - .5;
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
                    p = abs(tile - mod(p,tile * 2.));
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

            // END STAR SKY
            ENDCG
        }
    }
}