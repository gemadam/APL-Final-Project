PUBLIC  UnsharpMasking

_TEXT  SEGMENT
UnsharpMasking  PROC        ; rcx -> inImg, rdx -> outImg, r8 -> width, r9 -> height
.data
    iMiddle             WORD       0
    iIteratorX          QWORD      0
    iIteratorY          QWORD      0
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

.code

    ; Organize parameters passed to function
    mov r10, rcx         ; r10 - Pointer to beginning of input array
    mov r11, rdx         ; r11 - Pointer to beginning of output array
    mov r12, r8          ; r12 - Width of the image
    mov r13, r9          ; r13 - Height of the image

    ; Set rax and edx 0
    xor rax, rax
    xor edx, edx

    ; Compute value of iMiddle
    mov ax, 3
    mov bx, 2
    div bx
    mov [iMiddle], ax


Loop1:
    mov [iIteratorX], 0

Loop1_body:

    Loop2:
        mov [iIteratorY], 0

    Loop2_body:

        ; Compute address of currently processed input pixel
        mov rax, r12
        mul [iIteratorX]
        add rax, [iIteratorY]
        add rax, r10
        mov r8, rax                  ; r8 stores address of currently processed input pixel

        ; Compute address of currently processed output pixel
        sub rax, r10
        add rax, r11
        mov r9, rax                  ; r9 stores address of currently processed output pixel

        mov rbx, r8

        mov al, [rbx]
        mov [originalR], al
        mov al, [rbx+1]
        mov [originalG], al
        mov al, [rbx+2]
        mov [originalB], al

        mov [accR], 0
        mov [accG], 0
        mov [accB], 0

        Loop3:
            mov [iLoop3Iterator], 0

        Loop3_body:
            Loop4:
                mov [iLoop4Iterator], 0

            Loop4_body:

                mov rcx, [iIteratorX]
                add cx, [iLoop3Iterator]
                sub cx, [iMiddle]
                mov [xn], cx

                mov rcx, [iIteratorY]
                add cx, [iLoop4Iterator]
                sub cx, [iMiddle]
                mov [yn], cx



                ; Compute address of pixel
                mov rax, r12
                mul [xn]
                add ax, [yn]
                add rax, r10
                mov rdx, rax             ; rdx stores the address of pixel


                mov al, [rdx]
                mov [pixelR], al
                mov [accR], al          ; @todo accR += pixelR * kernel[iLoop3Iterator][iLoop4Iterator];
                
                mov al, [rdx]
                mov [pixelG], al
                mov [accG], al          ; @todo

                mov al, [rdx]
                mov [pixelB], al
                mov [accB], al          ; @todo
                

                ; Increments iterator and checks the loop condition
                inc [iLoop4Iterator]
                cmp [iLoop4Iterator], 3
                jne Loop4_body



            ; Increments iterator and checks the loop condition
            inc [iLoop3Iterator]
            cmp [iLoop3Iterator], 3
            jne Loop3_body


            movzx rax, [originalR]
            sub al, [accR]
            mov rbx, 2
            mul rbx
            movzx rbx, [originalR]
            add rax, rbx

            xor rdx, rdx
            mov ebx, 255
            div ebx

            mov [r9], dl


            mov dl, [originalG]
            sub al, [accG]
            mov rbx, 2
            mul rbx
            movzx rbx, [originalG]
            add rax, rbx

            xor rdx, rdx
            mov ebx, 255
            div ebx

            mov [r9+1], dl


            mov dl, [originalB]
            sub al, [accB]
            mov rbx, 2
            mul rbx
            movzx rbx, [originalB]
            add rax, rbx

            xor rdx, rdx
            mov ebx, 255
            div ebx

            mov [r9+2], dl


        ; Increments iterator and checks the loop condition
        inc [iIteratorY]
        cmp r13, [iIteratorY]
        jne Loop2_body

        


    ; Increments iterator and checks the loop condition
    inc [iIteratorX]
    cmp r12, [iIteratorX]
    jne Loop1_body

    ret

UnsharpMasking ENDP


_TEXT  ENDS

END          ; END directive required at end of file