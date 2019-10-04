using UnityEngine;
using System.Collections;

#define normal 0
#define gold 1
#define luckyword 2

enum ScoreType
{
	normal,
	gold,
	luckyword
};

public class Score : MonoBehaviour {
/***********************************************************
	函数名：int scoreUpload(ScoreType Type, int number)
	功能：实现客户端分数上传至服务器
	入参：ScoreType Type：记分类型
			0: 分数根据number确定
			1：金笔直接加500分
			2：幸运字直接加1000分
			int number：合成块个数
			2：+100
			3：+300
			4：+800
	出参：无
	返回值：
		0-成功
		1-入参错误
	调用函数：Update
	修改记录：
		20191005  新增函数实现
***********************************************************/
	int scoreUpload(ScoreType Type, int number) {
	switch(Type)
	{
		case normal:
			switch (number)
			{
				case 2: 
					m_score += 100;
				case 3: 
					m_score += 300;
				case 4: 
					m_score += 800;
					
			}
		case gold:	
			m_score += 500;
		case luckyword:	
			m_score += 1000;			
	}
}
