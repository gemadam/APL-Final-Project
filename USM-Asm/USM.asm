.386
.MODEL FLAT, STDCALL

OPTION CASEMAP:NONE

.NOLIST
.NOCREF
INCLUDE D:\masm32\include\masm32rt.inc

.LIST

.data

MsgBoxCaption DB "Kurs Iczeliona. Rozdział nr 2",0 
MsgBoxText    DB "Asembler Win32 jest Wspaniały!",0


.code



DllEntry PROC hInstDLL:DWORD, reason:DWORD, reserved1:DWORD

	MOV EAX, 1
	RET

DllEntry ENDP


UnsharpMasking PROC

	INVOKE MessageBox, NULL, ADDR MsgBoxText, ADDR MsgBoxCaption, MB_OK 
	
UnsharpMasking ENDP


end