#include "user_sys.h"
#include "uart.h"
#include "business.h"
#include <string.h>
#include <stdio.h>
#include <reg52.h>


extern app_Event getApp;

extern uint8 uartData[20];
extern uint8 uartIndex;

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
					lockerStatus = LOCK;
					reportLockerStatus();
					uartSendStr("lock locker\r\n");
				}
				break;
				case EVENT_LOCKER_STATUS_RSP:
				{
					reportLockerStatus();
					setEvent(EVENT_LOCKER_STATUS_RSP, ENABLE,300,1);
				}
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
		uartSendStr("open locker 5s\r\n");
		lockerStatus = UNLOCK;
		reportLockerStatus();
		setEvent(EVENT_LOCK_LOCKER, ENABLE,500,1);
	}
}
void reportLockerStatus()
{
	if(lockerStatus == UNLOCK)
		uartSendStr("unlock\r\n");
	else
		uartSendStr("lock\r\n");
}
