PUBLIC  UnsharpMasking

_TEXT  SEGMENT


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

    mov rax, rcx                        ; 1st argument is a pointer to structure
    mov _pInputStruct, rax              ; Save the pointer to the input structure

    xor rbx, rbx
    mov ebx, [rax]                      ; Obtain the value of the 1st structure member
    mov QWORD PTR _imgWidth, rbx        ; Save the value in imgWidth variable
    
    xor rbx, rbx
    mov ebx, [rax + 4]                  ; Move to the 2nd element of the structure
    mov QWORD PTR _imgHeight, rbx       ; Save the value in imgHeight variable

    mov rbx, [rax + 8]                  ; Move to the 3rd element of the structure
    mov QWORD PTR _pKernel, rbx         ; Save this pointer as pKernel

    mov rbx, [rax + 16]                 ; Move to the 4th element of the structure
    mov QWORD PTR _pInChannelR, rbx     ; Save this pointer as pInChannelR

    mov rbx, [rax + 24]                 ; Move to the 5th element of the structure
    mov QWORD PTR _pInChannelG, rbx     ; Save this pointer as pInChannelG

    mov rbx, [rax + 32]                 ; Move to the 5th element of the structure
    mov QWORD PTR _pInChannelB, rbx     ; Save this pointer as pInChannelB

    mov rbx, [rax + 40]                 ; Move to the 6th element of the structure
    mov QWORD PTR _pOutChannelR, rbx    ; Save this pointer as pOutChannelR

    mov rbx, [rax + 48]                 ; Move to the 7th element of the structure
    mov QWORD PTR _pOutChannelG, rbx    ; Save this pointer as pOutChannelG

    mov rbx, [rax + 56]                 ; Move to the 8th element of the structure
    mov QWORD PTR _pOutChannelB, rbx    ; Save this pointer as pOutChannelB

ENDM


; =======================================================================================
;  Unsharp Masking Algorithm
; =======================================================================================
UnsharpMasking  PROC
.data
    ; Local variables
    iOffset                 QWORD       0
    initNewPixelVal         REAL4       0.0

    ; Loop iteretors
    iIteratorX              QWORD       0
    iIteratorY              QWORD       0
    iNeighbourhoodIterator  QWORD       0

    ; Input structure pointers
    pInputStruct            QWORD       0
    imgWidth                QWORD       0
    imgHeight               QWORD       0
    pKernel                 QWORD       0
    pInChannelR             QWORD       0
    pInChannelG             QWORD       0
    pInChannelB             QWORD       0
    pOutChannelR            QWORD       0
    pOutChannelG            QWORD       0
    pOutChannelB            QWORD       0

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
    mov r14, 4

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
        mov rax, [imgWidth]                 ; Prepare condition
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

            xor rax, rax
            mov eax, [initNewPixelVal]
            movups xmm5, [initNewPixelVal]           ; New R
            movups xmm6, [initNewPixelVal]           ; New G
            movups xmm7, [initNewPixelVal]           ; New B

            LoopNeigbours:
                mov [iNeighbourhoodIterator], -1

            LoopNeigbours_body:                                     ; Loop body


                ; Prepare kernel
                mov rax, [iNeighbourhoodIterator]                   ; Compute kernel offset
                add rax, 1                                          ; How many rows to skip
                mov rbx, 3                                          ; Wdith of the kernel
                mul rbx                                             ; How many values to skip vertically
                mov rbx, 4                                          ; REAL4 size
                mul rbx                                             ; Multiply by REAL4 size
                add rax, [pKernel]                                  ; Add kernel address to the offset
                movups xmm0, [rax]                                  ; Load kernel values


                ; Prepare channels
                mov rax, [iNeighbourhoodIterator]                   ; Compute row index of the neighbours
                mov rax, [iIteratorY]                               ; How many rows to skip
                mov rbx, [imgWidth]
                mul rbx                                             ; How many values to skip vertically
                add rax, [iIteratorX]                               ; How many values to skip horizontally
                sub rax, 1
                mov rbx, 4                                          ; REAL4 size
                mul rbx                                             ; Multiply by REAL4 size
                mov [iOffset], rax                                  ; Save it for now

                mov rbx, [pInChannelR]
                add rbx, [iOffset]                                  ; Address of the first top row neighbour (channel R)
                movups xmm1, [rbx]                                  ; Load R channel values

                mov rbx, [pInChannelG]
                add rbx, [iOffset]                                  ; Address of the first top row neighbour (channel G)
                movups xmm2, [rbx]                                  ; Load G channel values

                mov rbx, [pInChannelB]
                add rbx, [iOffset]                                  ; Address of the first top row neighbour (channel B)
                movups xmm3, [rbx]                                  ; Load B channel values


                ; Apply kernel
                mulps xmm1, xmm0
                mulps xmm2, xmm0
                mulps xmm3, xmm0

                
                ; Update new value of R
                addss xmm5, xmm1                                    ; Add 1st cell
                shufps xmm1, xmm1, 11100101b                        ; Move to 2nd cell
                addss xmm5, xmm1                                    ; Add 2nd cell
                shufps xmm1, xmm1, 11100110b                        ; Move to 3rd cell
                addss xmm5, xmm1                                    ; Add 3rd cell
                
                ; Update new value of G
                addss xmm6, xmm2                                    ; Add 1st cell
                shufps xmm2, xmm2, 11100101b                        ; Move to 2nd cell
                addss xmm6, xmm2                                    ; Add 2nd cell
                shufps xmm2, xmm2, 11100110b                        ; Move to 3rd cell
                addss xmm6, xmm2                                    ; Add 3rd cell
                
                ; Update new value of B
                addss xmm7, xmm3                                    ; Add 1st cell
                shufps xmm3, xmm3, 11100101b                        ; Move to 2nd cell
                addss xmm7, xmm3                                    ; Add 2nd cell
                shufps xmm3, xmm3, 11100110b                        ; Move to 3rd cell
                addss xmm7, xmm3                                    ; Add 3rd cell



                ; Increments iterator of LoopNeigbours and checks the LoopNeigbours condition
                inc [iNeighbourhoodIterator]
                xor rax, rax
                mov eax, 2
                cmp rax, [iNeighbourhoodIterator]
                jne LoopNeigbours_body




            movups REAL4 PTR [r11], xmm5            ; Write R value to the output
            add r8, r14                             ; Input channel R
            add r11, r14                            ; Output channel R

            movups REAL4 PTR [r12], xmm6            ; Write G value to the output
            add r9, r14                             ; Input channel G
            add r12, r14                            ; Output channel G

            movups REAL4 PTR [r13], xmm7            ; Write B value to the output
            add r10, r14                            ; Input channel B
            add r13, r14                            ; Output channel B


            ; Increments iterator of LoopCenterX and checks the LoopCenterX condition
            inc [iIteratorX]
            mov rax, [imgWidth]
            add rax, -1
            cmp rax, [iIteratorX]
            jne LoopCenterX_body


        ; Rewrite the right border pixel
        call RewritePixelFromInputToOutput


        ; Increments iterator of LoopCenterY and checks the LoopCenterY condition
        inc [iIteratorY]
        mov rax, [imgHeight]
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
        mov rax, [imgWidth]                 ; Prepare condition
        cmp rax, [iIteratorX]               ; Check loop condition
        jne BottomBorderLoop_body           ; Repeat loop until condition is met


    ret
    

UnsharpMasking ENDP



; =======================================================================================
;  Function writes channels of the pixel from input to output and moves addresses by one pixel
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
    mov eax, REAL4 PTR [r8]         ; Get value under channel R address
    mov REAL4 PTR [r11], eax        ; Write that value to the output

    mov eax, REAL4 PTR [r9]         ; Get value under channel G address
    mov REAL4 PTR [r12], eax        ; Write that value to the output

    mov eax, REAL4 PTR [r10]        ; Get value under channel B address
    mov REAL4 PTR [r13], eax        ; Write that value to the output

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