Shader "Custom/CRT_Screen"
{
    Properties
    {
        _BlitTexture ("Base (RGB)", 2D) = "white" {}
        _InvertIntensity ("Invert Intensity", Range(0, 1)) = 0
        _Zoom ("Zoom", Range(0.5, 1.5)) = 1.0
        _Curvature ("Curvature Intensity", Range(0, 5)) = 0.5
        _VignetteWidth ("Vignette Width", Range(0, 1)) = 0.75
        _Abberation ("Chromatic Abberation", Range(0, 10)) = 1.0
        _ScanlineCount ("Scanline Count", Float) = 300.0
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.5
        _Brightness ("Brightness", Range(0, 2)) = 1.2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline" }
        LOD 100
        ZWrite On Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc" // Or "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl" for pure URP

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _BlitTexture;
            float _InvertIntensity;
            float _Zoom;
            float4 _MainTex_ST;
            float _Curvature;
            float _VignetteWidth;
            float _Abberation;
            float _ScanlineCount;
            float _ScanlineIntensity;
            float _Brightness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // --- 1. CURVATURE FUNCTION ---
            float2 CurveUV(float2 uv)
            {
                // 1. Apply Zoom (Center based)
                uv = (uv - 0.5) / _Zoom + 0.5;

                // 2. Apply Curvature
                uv = uv * 2.0 - 1.0; 
                float2 offset = abs(uv.yx) / 3.0; 
                uv = uv + uv * (offset * offset) * _Curvature;
                uv = uv * 0.5 + 0.5;
                
                return uv;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Apply Curve
                float2 curvedUV = CurveUV(i.uv);

                // Cutoff edges (Bezel)
                if (curvedUV.x < 0.0 || curvedUV.x > 1.0 || curvedUV.y < 0.0 || curvedUV.y > 1.0)
                {
                    return float4(0,0,0,1);
                }

                // --- 2. CHROMATIC ABBERATION ---
                // We distort R and B coordinates slightly based on distance from center
                float2 center = float2(0.5, 0.5);
                float dist = distance(curvedUV, center);
                float split = _Abberation * 0.005 * dist;

                // Calculate shifted UVs
                float2 uvR = float2(curvedUV.x + split, curvedUV.y);
                float2 uvB = float2(curvedUV.x - split, curvedUV.y);

                // Sample with edge check: if the shifted UV is outside 0-1, return black for that channel
                float r = (uvR.x < 0.0 || uvR.x > 1.0) ? 0.0 : tex2D(_BlitTexture, uvR).r;
                float g = tex2D(_BlitTexture, curvedUV).g; // Green is centered, usually safe
                float b = (uvB.x < 0.0 || uvB.x > 1.0) ? 0.0 : tex2D(_BlitTexture, uvB).b;

                float3 col = float3(r, g, b);

                // --- NEW INVERSION LOGIC ---
                // We calculate the inverted color
                float3 invertedCol = 1.0 - col;
                // We lerp between normal and inverted based on intensity
                col = lerp(col, invertedCol, _InvertIntensity);

                // --- 3. SCANLINES ---
                // Simple sine wave based on Y UV
                float scanline = sin(curvedUV.y * _ScanlineCount * 3.14159 * 2.0);
                // Map from [-1, 1] to [1 - Intensity, 1]
                scanline = scanline * 0.5 + 0.5; 
                scanline = 1.0 - _ScanlineIntensity + (scanline * _ScanlineIntensity);
                
                col *= scanline;

                // --- 4. VIGNETTE ---
                // Darken corners
                float vig = 1.0 - smoothstep(_VignetteWidth, 1.0, dist * 1.5);
                col *= vig;

                // Boost Brightness to compensate for scanline darkening
                col *= _Brightness;

                // col = float3(1, 0, 0); // Turn everything bright red
                return float4(col, 1.0);
            }
            ENDHLSL
        }
    }
}