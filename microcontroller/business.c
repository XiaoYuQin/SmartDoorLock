#include "user_sys.h"
#include "uart.h"
#include "business.h"
#include <string.h>
#include <stdio.h>
#include <reg52.h>

#define LOCKER_TIMER 7

extern app_Event getApp;

extern uint8 uartData[20];
extern uint8 uartIndex;

sbit wela2=P1^1;	//定义位选
int timer;
uint8 code table[]={0x3f,0x06,0x5b,0x4f,0x66,0x6d,0x7d,0x07,0x7f,0x6f};//不带小数点的共阴数码管段值
sbit lockerPin=P1^7;	//定义位选


typedef enum{
	LOCK,
	UNLOCK
}LOCKER_STATUS;

LOCKER_STATUS lockerStatus = LOCK;



void userBusinessEventHandle()
{
	uint8 i;
	for(i=0;i<EVENT_NUMBER;i++)
	{
		if((getApp.eventEDable_t[i]==ENABLE)&&(getApp.eventResultFlag_t[i]==ENABLE)&&(getApp.eventDoCount_t[i]<getApp.eventSetCount_t[i]))
		{
			userSystemSetEventDone(i);
			switch(getApp.eventId_t[i])
			{
				case EVENT_UART_RECIVE:
				{
					uartSendStr(uartData);
					dealLocker(uartData);
					uartDataClean();
					setEvent(EVENT_UART_RECIVE, ENABLE,20,1);
				}
				break;
				case EVENT_LOCK_LOCKER:
				{
					lockerStatus = UNLOCK;
					reportLockerStatus();
					uartSendStr("UNLOCK\r\n");
				}
				break;
				case EVENT_LOCKER_STATUS_RSP:
				{
					reportLockerStatus();
					setEvent(EVENT_LOCKER_STATUS_RSP, ENABLE,300,1);
				}
				break;
				/*锁倒计时*/
				case EVENT_LOCKER_DAOJISHI:
				{
					
			
					P0=table[timer];	//传递数据
					P1 |= 0x01;		//打开位选
					P1 &= 0xF9;
					wela2 = 0;	   //关闭位选
					timer = timer -1;
						

					if(timer < -1)
					{
						timer =LOCKER_TIMER;
						setEvent(EVENT_LOCKER_LOCKED, ENABLE,1,1);
						setEvent(EVENT_LOCKER_DAOJISHI, DISABLE,0,0);
						lockerStatus = LOCK;
						reportLockerStatus();
					}
					else
					{					
						uartSendStr("EVENT_LOCKER_DAOJISHI\r\n");
						setEvent(EVENT_LOCKER_DAOJISHI, ENABLE,200,1);
						lockerStatus = UNLOCK;
						reportLockerStatus();						
					}
				}
				break;
				case EVENT_LOCKER_LOCKED:
					P0 =0x40;	//传递数据
					P1 |= 0x01;		//打开位选
					P1 &= 0xF9;
					wela2 = 0;	   //关闭位选
					break;
				default:
					break;
			
			}
		}
		else if((getApp.eventEDable_t[i]==ENABLE)&&(getApp.eventDoCount_t[i]>=getApp.eventSetCount_t[i]))
		{
			getApp.eventEDable_t[i]=DISABLE;
		}
	}
}
void dealLocker(uint8 *cmd)
{
	if(uartIndex < 5 ) return;
	if(uartIndex == 0) return;
	
	if((cmd[0] == 'U')&&(cmd[1] == 'N')&&cmd[2] == 'L'&&cmd[3] == 'O'&&cmd[4] == 'C'&&cmd[5] == 'K'){
		lockerPin = 1;
		timer = LOCKER_TIMER;
		uartSendStr("open locker 5s\r\n");
		lockerStatus = UNLOCK;
		reportLockerStatus();
		//setEvent(EVENT_LOCK_LOCKER, ENABLE,500,1);
		setEvent(EVENT_LOCKER_DAOJISHI, ENABLE,1,1);
	}
	if((cmd[0] == 'T')&&(cmd[1] == 'O')&&cmd[2] == 'L'&&cmd[3] == 'O'&&cmd[4] == 'C'&&cmd[5] == 'K'){

		timer = 0;
		lockerStatus = LOCK;
		reportLockerStatus();
		setEvent(EVENT_LOCKER_LOCKED, ENABLE,1,1);
		setEvent(EVENT_LOCKER_DAOJISHI, DISABLE,0,0);
	}
}
void reportLockerStatus()
{
	if(lockerStatus == UNLOCK)
	{
		lockerPin = 1;
		uartSendStr("unlock\r\n");
	}
	else
	{
		lockerPin = 0;
		uartSendStr("lock\r\n");
	}
}
