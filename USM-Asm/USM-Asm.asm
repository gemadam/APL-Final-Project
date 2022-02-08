PUBLIC  UnsharpMasking

_TEXT  SEGMENT

; =======================================================================================
;  Macro computes address of the value in 2D array
;   _x - X index
;   _y - Y index
;   _w - Width of the array
;  Address is returned in rax, offset in rbx.
; =======================================================================================
IndexMacro MACRO _x, _y, _w

    ; Convert index to address
    xor rax, rax
    mov rax, _y             ; How many rows to skip
    mul _w                  ; How many values to skip vertically
    add rax, _x             ; How many values to skip horizontally
    mov rbx, 4              ; REAL4 size
    mul rbx                 ; Multiply by REAL4 size

ENDM


; =======================================================================================
;  Macro reads the input data structure
;   _pInputStruct - Address of where address of the input structure will be stored
;   _imgWidth - Address of where image width will be stored
;   _imgHeight - Address of where image height will be stored
;   _pKernel - 
;   _pInChannelR - 
;   _pInChannelG - 
;   _pInChannelB - 
;   _pOutChannelR - 
;   _pOutChannelG - 
;   _pOutChannelB - 
; =======================================================================================
ProcessInputStructureMacro MACRO _pInputStruct, _imgWidth, _imgHeight, _pKernel, _pInChannelR, _pInChannelG, _pInChannelB, _pOutChannelR, _pOutChannelG, _pOutChannelB

    mov rax, rcx                           ; 1st argument of the function is a pointer to the input structure
    mov _pInputStruct, rax                 ; Save the pointer to the input structure

    mov ebx, [rax]                         ; Obtain the value of the 1st structure member
    mov DWORD PTR _imgWidth, ebx           ; Save the value in imgWidth variable

    mov ebx, [rax + 4]                     ; Move to the 2nd element of the structure
    mov DWORD PTR _imgHeight, ebx          ; Save the value in imgHeight variable

    mov rbx, [rax + 8]                     ; Move to the 3rd element of the structure
    mov QWORD PTR _pKernel, rbx            ; Save this pointer as pKernel

    mov rbx, [rax + 16]                    ; Move to the 4th element of the structure
    mov QWORD PTR _pInChannelR, rbx        ; Save this pointer as pInChannelR

    mov rbx, [rax + 24]                    ; Move to the 5th element of the structure
    mov QWORD PTR _pInChannelG, rbx        ; Save this pointer as pInChannelG

    mov rbx, [rax + 32]                    ; Move to the 5th element of the structure
    mov QWORD PTR _pInChannelB, rbx        ; Save this pointer as pInChannelB

    mov rbx, [rax + 40]                    ; Move to the 6th element of the structure
    mov QWORD PTR _pOutChannelR, rbx       ; Save this pointer as pOutChannelR

    mov rbx, [rax + 48]                    ; Move to the 7th element of the structure
    mov QWORD PTR _pOutChannelG, rbx       ; Save this pointer as pOutChannelG

    mov rbx, [rax + 56]                    ; Move to the 8th element of the structure
    mov QWORD PTR _pOutChannelB, rbx       ; Save this pointer as pOutChannelB

ENDM


; =======================================================================================
;  Unsharp Masking Algorithm
; =======================================================================================
UnsharpMasking  PROC
.data
    ; Local variables
    iOffset             QWORD       0
    pixelDataTypeSize   QWORD       4
    initNewPixelVal     REAL4       1.0

    ; Loop iteretors
    iIteratorX                   QWORD       0
    iIteratorY                   QWORD       0
    iNeighbourhoodIterator       QWORD       0

    ; Auxiliary pointers
    pNeigbour           QWORD       0

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

    ; Initialize local variables with default values
    mov [iIteratorX], 0
    mov [iIteratorY], 0
    mov [iNeighbourhoodIterator], 0
    mov [iOffset], 0


    ; Processing of the input structure
    ProcessInputStructureMacro [pInputStruct], [imgWidth], [imgHeight], [pKernel], [pInChannelR], [pInChannelG], [pInChannelB], [pOutChannelR], [pOutChannelG], [pOutChannelB]


    ; Registers r8-10 will be used for storing addresses of input channels
    xor r8, r8                              ; Make sure r8-10 are clean
    xor r9, r9
    xor r10, r10

    mov r8, [pInChannelR]                   ; Address of input pixel R channel
    mov r9, [pInChannelG]                   ; Address of input pixel G channel
    mov r10, [pInChannelB]                  ; Address of input pixel B channel
    
    ; Registers r11-13 will be used for storing addresses of output channels
    xor r11, r11                            ; Make sure r11-13 are clean
    xor r12, r12
    xor r13, r13

    mov r11, [pOutChannelR]                 ; Address of output pixel R channel
    mov r12, [pOutChannelG]                 ; Address of output pixel G channel
    mov r13, [pOutChannelB]                 ; Address of output pixel B channel

    ; Register r14 will be used for storing pixel address increment
    xor r14, r14                            ; Make sure r14 is clean
    mov r14, [pixelDataTypeSize]

    ; Register r15 will be used for storing kernel address
    xor r15, r15                            ; Make sure r15 is clean
    mov r15, [pKernel]


    ; Rewrite the top border of the image to the output
    TopBorderLoop:                          ; Loop initialization
        mov [iIteratorX], 0

    TopBorderLoop_body:                     ; Loop body

        ; Rewrite input to output
        call RewritePixelFromInputToOutput
        
        ; After each loop iteration
        inc [iIteratorX]                    ; Increment loop counter
        xor rax, rax
        mov eax, [imgWidth]                 ; Prepare condition
        cmp rax, [iIteratorX]               ; Check loop condition
        jne TopBorderLoop_body              ; Repeat loop until condition is met



    ; Center of the image
    LoopCenterY:                            ; Loop initialization
        mov [iIteratorY], 1

    LoopCenterY_body:                       ; Loop body

        ; Rewrite left border pixel
        call RewritePixelFromInputToOutput


        ; Process the center of the image
        LoopCenterX:                                                ; Loop initialization
            mov [iIteratorX], 1

        LoopCenterX_body:                                           ; Loop body

            IndexMacro [iIteratorX], [iIteratorY], [imgWidth]
            add rax, [pInChannelR]                                              ; Obtain address of channel R value
            mov ebx, REAL4 PTR [rax]
            mov REAL4 PTR [r11], ebx                                            ; Rewrite R value from input to the output
            
            add r8, r14                                                         ; Input channel R
            add r11, r14                                                        ; Output channel R

            
            IndexMacro [iIteratorX], [iIteratorY], [imgWidth]
            add rax, [pInChannelG]                                              ; Obtain address of channel G value
            mov ebx, REAL4 PTR [rax]
            mov REAL4 PTR [r12], ebx                                            ; Rewrite G value from input to the output
            
            add r9, r14                                                         ; Input channel G
            add r12, r14                                                        ; Output channel G

            
            IndexMacro [iIteratorX], [iIteratorY], [imgWidth]                   
            add rax, [pInChannelB]                                              ; Obtain address of channel B value
            mov ebx, REAL4 PTR [rax]
            mov REAL4 PTR [r13], ebx                                            ; Rewrite B value from input to the output
            
            add r10, r14                                                        ; Input channel B
            add r13, r14                                                        ; Output channel B


            ; Increments iterator of LoopCenterX and checks the LoopCenterX condition
            inc [iIteratorX]
            xor rax, rax
            mov eax, [imgWidth]
            add rax, -1
            cmp rax, [iIteratorX]
            jne LoopCenterX_body


        ; Rewrite the right border pixel
        call RewritePixelFromInputToOutput


        ; Increments iterator of LoopCenterY and checks the LoopCenterY condition
        inc [iIteratorY]
        xor rax, rax
        mov eax, [imgHeight]
        add rax, -1
        cmp rax, [iIteratorY]
        jne LoopCenterY_body


    ; Rewrite the bottom border of the image to the output
    BottomBorderLoop:                       ; Loop initialization
        mov [iIteratorX], 0

    BottomBorderLoop_body:                  ; Loop body

        ; Rewrite input to output
        call RewritePixelFromInputToOutput
        
        ; After each loop iteration
        inc [iIteratorX]                    ; Increment loop counter
        xor rax, rax
        mov eax, [imgWidth]                 ; Prepare condition
        cmp rax, [iIteratorX]               ; Check loop condition
        jne BottomBorderLoop_body           ; Repeat loop until condition is met


    ret
    

UnsharpMasking ENDP



; =======================================================================================
;  Funtion rewrites pixel channels from input to output and moves addresses by one pixel
;   r8  - Address of input R channel
;   r9  - Address of input G channel
;   r10 - Address of input B channel
;   r11 - Address of output R channel
;   r12 - Address of output G channel
;   r13 - Address of output B channel
; =======================================================================================
RewritePixelFromInputToOutput  PROC
.data

.code

    ; Rewrite input to output
    mov eax, REAL4 PTR [r8]             ; Change address of input R channel to value under it
    mov REAL4 PTR [r11], eax            ; Rewrite R value from input to the output

    mov eax, REAL4 PTR [r9]             ; Change address of input G channel to value under it
    mov REAL4 PTR [r12], eax            ; Rewrite G value from input to the output

    mov eax, REAL4 PTR [r10]            ; Change address of input B channel to value under it
    mov REAL4 PTR [r13], eax            ; Rewrite B value from input to the output

    ; Move addresses by one pixel
    add r8, r14                         ; Input channel R
    add r9, r14                         ; Input channel G
    add r10, r14                        ; Input channel B
    add r11, r14                        ; Output channel R
    add r12, r14                        ; Output channel G
    add r13, r14                        ; Output channel B

    ret

RewritePixelFromInputToOutput ENDP


_TEXT  ENDS

END