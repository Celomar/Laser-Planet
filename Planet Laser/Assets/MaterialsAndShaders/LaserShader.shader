Shader "Unlit/LaserShader"
{
    Properties
    {
        // Color property for material inspector, default to white
        _InnerColor ("Inner Color", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlinePortion ("Outline Portion", Range(0.0,1.0)) = 0.1
        _MinIntensity ("Min Intensity", Range(0.0,1.0)) = 0.4
        _LaserSpeed ("Laser Speed", Float) = 10.0
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };
            
            v2f vert (float4 pos : POSITION, float2 uv : TEXCOORD0)
            {
                v2f ret;
                ret.pos = UnityObjectToClipPos(pos);
                ret.uv = uv;
                return ret;
            }
            
            fixed4 _InnerColor;
            fixed4 _OutlineColor;
            fixed _OutlinePortion;
            fixed _MinIntensity;
            float _LaserSpeed;

            fixed4 frag (v2f input) : SV_Target
            {
                const float PI = 3.14159265f;
                fixed4 multiplier = cos((_Time.y - input.uv.x) * 2.0 * PI * _LaserSpeed);
                multiplier = (multiplier + 1.0) / 2.0;  // put it between 0 and 1
                multiplier = lerp(1.0, _MinIntensity, multiplier);  // 0 to 1 value to interpolate between max and min intensities
                
                float verticalMultiplier = (cos(input.uv.y * 2.0 * PI) + 1.0) / 2.0; // add vertical highlight
                fixed isOutline = fixed( verticalMultiplier >= 1.0 - _OutlinePortion );
                verticalMultiplier = (1.0 - (1.0-verticalMultiplier) / _OutlinePortion) * isOutline;
                
                fixed4 color = lerp(_InnerColor, _OutlineColor, verticalMultiplier);
                return color;
            }
            ENDCG
        }
    }
}
