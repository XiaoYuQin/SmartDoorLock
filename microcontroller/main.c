#include <reg52.h>	   //���ļ��ж�����52��һЩ���⹦�ܼĴ���
#include "timer.h"
#include "uart.h"
#include "user_sys.h"
#include "business.h"

sbit LED1=P1^7;//�����˿�

void init()
{
	InitUART();	
	uartSendStr("int..\r\n");
	//Init_Timer0();
	TIM2Inital();
	userSystemEventInit();		
	uartSendStr("int end...\r\n");	
}
void main()
{ 
  	init();

	LED1  = 0; //��P1_7��Ϊ�͵�ƽ ,����LED
 		
	setEvent(EVENT_UART_RECIVE, ENABLE,30,1);
	setEvent(EVENT_LOCKER_STATUS_RSP, ENABLE,300,1);
	uartDataClean();
	while(1)
	{
		userBusinessEventHandle();
	}
}


