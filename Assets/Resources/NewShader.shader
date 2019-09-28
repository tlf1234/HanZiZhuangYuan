//水波纹shader
Shader "Custom/RipperShader" {
	Properties {
		_MainTex("Base",2D)="white"{}
	}
	CGINCLUDE
    #include "UnityCG.cginc"

	sampler2D _MainTex;

	float4 _MainTex_TexelSize;
	//uv到中心点向量长度的系数
	float _distanceFactor;
	//波纹幅度系数
	float _totalFactor;
	//时间系数（速度）
	float _timeFactor;
	//波纹半径
	float _waveWidth;
	//波纹（uv）运动的距离
	float _curWaveDis;
	//鼠标点击屏幕的位置
	float4 _startPos;

	float4 frag(v2f_img i) :SV_Target
	{
		//翻转UV
#if UNITY_UV_STARTS_AT_TOP
		if (_MainTex_TexelSize.y < 0)
			 _startPos.y = 1 - _startPos.y;	
#endif
		//鼠标位置向量 - uv向量 = uv到中心点向量
		float2 vec = _startPos.xy - i.uv;
		//根据屏幕长宽调整波纹为正圆形
		vec *= float2(_ScreenParams.x / _ScreenParams.y, 1);
		//归一化
		float2 normalizedVec = normalize(vec);
		//vec向量的模长
		float magnitudeVec = sqrt(vec.x*vec.x + vec.y*vec.y);
		//根据长度区分在sin中的值（相当于区分出波纹的起伏）来决定偏移系数
		//magnitudeVec在这里都是小于1的，所以我们需要乘以一个比较大的数，比如60，这样就有多个波峰波谷
		//sin函数是（-1，1）的值域，我们希望偏移值很小，所以这里我们缩小100倍
		float sinFactor = sin(magnitudeVec*_distanceFactor+_Time.y*_timeFactor)*_totalFactor*0.01;
		//clamp(x,a,b)
		//返回一个[a, b]范围内的数
		//如果x＜a则返回a
		//如果x＞b则返回b
		//否则返回x
		//计算波纹移动了多少距离，如果超过了波纹最大半径，渐渐淡出
		float fadeFactor = clamp(_waveWidth - abs(_curWaveDis - magnitudeVec), 0, 1);
		//偏移值=uv到中心的方向*偏移系数*能量削弱系数（越往中心采样则看上去波纹的幅度越大）
		float2 offset = normalizedVec * sinFactor*fadeFactor;
		float2 uv = offset + i.uv;
		return tex2D(_MainTex, uv);
	}
	ENDCG
	SubShader {
		Pass
		{
			ZTest Off
			Cull Off
			ZWrite Off
			Fog{Mode off}

			CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
			//加强性能
            #pragma fragmentoption ARB_precision_hint_fastest
			ENDCG
		}
	}
}
