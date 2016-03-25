#include <reg52.h>	   //此文件中定义了52的一些特殊功能寄存器
#include "timer.h"
#include "uart.h"
#include "user_sys.h"
#include "business.h"




sbit lockerPin=P1^7;	//定义位选


void init()
{

	lockerPin = 0;
	
	Init_Timer0();

	InitUART();	
	uartSendStr("int..\r\n");
	//Init_Timer0();
//	TIM2Inital();

	userSystemEventInit();		
	uartSendStr("int end...\r\n");	

}
void main()
{ 
//uint i; 
  	init();

	
	
	//LED1  = 0; //置P1_7口为低电平 ,点亮LED
 		
	setEvent(EVENT_UART_RECIVE, ENABLE,30,1);
	setEvent(EVENT_LOCKER_STATUS_RSP, ENABLE,300,1);
	setEvent(EVENT_LOCKER_LOCKED, ENABLE,1,1);

	uartDataClean();	
	while(1)
	{
		/*for(i=0;i<10;i++)	  
		{
			P0=table[i];	//传递数据
			P1 = 0x01;		//打开位选
			delayms(5);
			wela2 = 0;	   //关闭位选
			delayms(1000); //延时1S
		}	 */
		userBusinessEventHandle();

	}
}


