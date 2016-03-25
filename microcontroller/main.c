#include <reg52.h>	   //���ļ��ж�����52��һЩ���⹦�ܼĴ���
#include "timer.h"
#include "uart.h"
#include "user_sys.h"
#include "business.h"




sbit lockerPin=P1^7;	//����λѡ


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

	
	
	//LED1  = 0; //��P1_7��Ϊ�͵�ƽ ,����LED
 		
	setEvent(EVENT_UART_RECIVE, ENABLE,30,1);
	setEvent(EVENT_LOCKER_STATUS_RSP, ENABLE,300,1);
	setEvent(EVENT_LOCKER_LOCKED, ENABLE,1,1);

	uartDataClean();	
	while(1)
	{
		/*for(i=0;i<10;i++)	  
		{
			P0=table[i];	//��������
			P1 = 0x01;		//��λѡ
			delayms(5);
			wela2 = 0;	   //�ر�λѡ
			delayms(1000); //��ʱ1S
		}	 */
		userBusinessEventHandle();

	}
}


