
.MODEL FLAT, STDCALL

INCLUDE \masm32\include\masm32rt.inc

.data

MsgBoxCaption DB "This is message box displayed from UnsharpMasking function in Asm version"


.code

DllEntry PROC hInstDLL:DWORD, rason:DWORD, reserved1:DWORD

	MOV EAX, 1
	RET

DllEntry ENDP



UnsharpMasking PROC

	INVOKE MessageBox, NULL, ADDR MsgBoxCaption, ADDR MsgBoxCaption, MB_OK
	RET

UnsharpMasking ENDP



END