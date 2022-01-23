PUBLIC  UnsharpMasking

_TEXT  SEGMENT
UnsharpMasking  PROC        ; rcx -> inImg, rdx -> outImg, r8 -> width, r9 -> height
.data
    iMiddle             WORD       0
    iX                  WORD       0
    iY                  WORD       0
    originalR           BYTE       0
    originalG           BYTE       0
    originalB           BYTE       0
    inImage             DB         0
    iLoop3Iterator      WORD       0
    iLoop4Iterator      WORD       0
    pixelR              BYTE       0
    pixelG              BYTE       0
    pixelB              BYTE       0
    accR                BYTE       0
    accG                BYTE       0
    accB                BYTE       0
    xn                  WORD       0
    yn                  WORD       0


    ; Loop iteretors
    iIteratorX          QWORD      0
    iIteratorY          QWORD      0

    ; Auxiliary pointers

    ; Input structure pointers
    pInputStruct        QWORD       0
    imgWidth            DWORD       0
    imgHeight           DWORD       0
    pKernel             QWORD       0
    pInChannelR         QWORD       0
    pInChannelG         QWORD       0
    pInChannelB         QWORD       0
    pOutChannelR        QWORD       0
    pOutChannelG        QWORD       0
    pOutChannelB        QWORD       0

.code

    ; Processing of the input structure
    mov rax, rcx                        ; 1st argument of the function is a pointer to the input structure
    mov [pInputStruct], rax             ; Save the pointer to the input structure

    mov rbx, [rax]                      ; Obtain the address of the 1st structure member
    mov ebx, [rax]                      ; Obtain the value under the pWidth pointer
    mov DWORD PTR [imgWidth], ebx       ; Save the value in imgWidth variable

    mov rbx, [rax + 4]                  ; Move to the 2nd element of the structure
    mov ebx, [rax]                      ; Obtain the value under the pWidth pointer
    mov DWORD PTR [imgHeight], ebx      ; Save the value in imgHeight variable

    mov rbx, [rax + 8]                  ; Move to the 3rd element of the structure
    mov QWORD PTR [pKernel], rbx        ; Save this pointer as pKernel

    mov rbx, [rax + 16]                 ; Move to the 4th element of the structure
    mov QWORD PTR [pInChannelR], rbx    ; Save this pointer as pInChannelR

    mov rbx, [rax + 24]                 ; Move to the 5th element of the structure
    mov QWORD PTR [pInChannelG], rbx    ; Save this pointer as pInChannelG

    mov rbx, [rax + 32]                 ; Move to the 5th element of the structure
    mov QWORD PTR [pInChannelB], rbx    ; Save this pointer as pInChannelB

    mov rbx, [rax + 40]                 ; Move to the 6th element of the structure
    mov QWORD PTR [pOutChannelR], rbx   ; Save this pointer as pOutChannelR

    mov rbx, [rax + 48]                 ; Move to the 7th element of the structure
    mov QWORD PTR [pOutChannelG], rbx   ; Save this pointer as pOutChannelG

    mov rbx, [rax + 56]                 ; Move to the 8th element of the structure
    mov QWORD PTR [pOutChannelB], rbx   ; Save this pointer as pOutChannelB









    ret
    

UnsharpMasking ENDP

_TEXT  ENDS

END