PUBLIC  UnsharpMasking

_TEXT  SEGMENT
UnsharpMasking  PROC        ; rcx -> inImg, rdx -> outImg, r8 -> width, r9 -> height
.data
    ;iMiddle             WORD       0
    ;iX                  WORD       0
    ;iY                  WORD       0
    ;inImage             DB         0
    ;iLoop3Iterator      WORD       0
    ;iLoop4Iterator      WORD       0
    ;pixelR              BYTE       0
    ;pixelG              BYTE       0
    ;pixelB              BYTE       0
    ;xn                  WORD       0
    ;yn                  WORD       0

    ; Local variables
    accR                BYTE        0
    accG                BYTE        0
    accB                BYTE        0
    originalR           BYTE        0
    originalG           BYTE        0
    originalB           BYTE        0

    ; Loop iteretors
    iIteratorX          QWORD       0
    iIteratorY          QWORD       0
    iLoop1Iterator      QWORD       0
    iLoop2Iterator      QWORD       0

    ; Auxiliary pointers
    iOutputIterator     QWORD       0

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
    mov [iOutputIterator], 0


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



; Rewrite the top border of the image to the output
TopBorderLoop:
    mov [iIteratorX], 0

TopBorderLoop_body:

    mov rcx, [iIteratorX]       ; Take R channel of input pixel
    add rcx, [pInChannelR]
    mov al, BYTE PTR [rcx]      ; al stores the value of R channel
    
    mov rcx, [iOutputIterator]
    add rcx, [pOutChannelR]
    mov [rcx], al               ; Write R channel to output pixel

    mov rcx, [iIteratorX]       ; Take G channel of input pixel
    add rcx, [pInChannelG]
    mov al, BYTE PTR [rcx]
    
    mov rcx, [iOutputIterator]       
    add rcx, [pOutChannelG]
    mov [rcx], al               ; Write G channel to output pixel

    mov rcx, [iIteratorX]       ; Take B channel of input pixel
    add rcx, [pInChannelB]
    mov al, BYTE PTR [rcx]
    
    mov rcx, [iOutputIterator]       
    add rcx, [pOutChannelB]
    mov [rcx], al               ; Write B channel to output pixel


    ; Moves output iterator to the next pixel
    inc [iOutputIterator]
    
    ; Increments iterator of TopBorderLoop and checks the TopBorderLoop condition
    inc [iIteratorX]
    xor rax, rax
    mov eax, [imgWidth]
    cmp rax, [iIteratorX]
    jne TopBorderLoop_body



; Center of the image
LoopCenterY:
    mov [iIteratorY], 1

LoopCenterY_body:
    
    ; Rewrite the left border

    mov rax, [iIteratorY]       ; Compute offset on input image
    mul [imgWidth]
    mov rdx, rax                ; rdx stores the offset on input image


    mov rcx, rdx                ; Compute pinter to pixel R channel value
    add rcx, [pInChannelR]
    mov al, BYTE PTR [rcx]      ; al stores the value of R channel
    
    mov rcx, [iOutputIterator]  ; Compute pinter to output pixel R channel value
    add rcx, [pOutChannelR]
    mov [rcx], al               ; Write R channel to output pixel
    

    mov rcx, rdx                ; Compute pinter to pixel G channel value
    add rcx, [pInChannelG]
    mov al, BYTE PTR [rcx]      ; al stores the value of G channel
    
    mov rcx, [iOutputIterator]  ; Compute pinter to output pixel G channel value     
    add rcx, [pOutChannelG]
    mov [rcx], al               ; Write G channel to output pixel

    
    mov rcx, rdx                ; Compute pinter to pixel B channel value
    add rcx, [pInChannelB]
    mov al, BYTE PTR [rcx]      ; al stores the value of B channel
    
    mov rcx, [iOutputIterator]  ; Compute pinter to output pixel B channel value       
    add rcx, [pOutChannelB]
    mov [rcx], al               ; Write B channel to output pixel


    ; Moves output iterator to the next pixel
    inc [iOutputIterator]


    ; Process the center of the image
    LoopCenterX:
        mov [iIteratorX], 1

    LoopCenterX_body:

        xor rax, rax
        mov eax, [imgWidth]
        mul [iIteratorY]
        add rax, [iIteratorX]
        mov rdx, rax                ; rdx stores offset of input pixel

        ; Value of original channel R
        mov rax, [pInChannelR]
        add rax, rdx
        xor rbx, rbx
        mov bl, BYTE PTR [rax]
        mov BYTE PTR [originalR], bl

        ; Value of original channel G
        mov rax, [pInChannelG]
        add rax, rdx
        xor rbx, rbx
        mov bl, BYTE PTR [rax]
        mov BYTE PTR [originalG], bl

        ; Value of original channel B
        mov rax, [pInChannelB]
        add rax, rdx
        xor rbx, rbx
        mov bl, BYTE PTR [rax]
        mov BYTE PTR [originalB], bl

        mov [accR], 0       ; Initialize acc 'array'
        mov [accG], 0
        mov [accB], 0    
        
        ; Process neigbourhood of the pixel
        Loop1:
            mov [iLoop1Iterator], 0

        Loop1_body:
            Loop2:
                mov [iLoop2Iterator], 0

            Loop2_body:

                mov rax, [pInChannelR]
                movq mm0, rax
                
                mov rax, [pInChannelG]
                movq mm1, rax
                
                mov rax, [pInChannelB]
                movq mm2, rax


                mov rax, [iIteratorY]
                add rax, [iLoop2Iterator]
                sub rax, 1
                mul [imgWidth]
                add rax, [iIteratorX]
                add rax, [iLoop1Iterator]
                sub rax, 1
                movq mm3, rax
                
                paddq mm0, mm3
                paddq mm1, mm3
                paddq mm2, mm3

                mov rax, 12
                mul [iLoop2Iterator]
                mov rbx, rax
                mov rax, 4
                mul [iLoop1Iterator]
                add rax, rbx
                add rax, [pKernel]
                mov rbx, [rax]                ; Move kernel value to the rbx


                movq rax, mm0
                mov rax, [rax]
                mul ebx
                add [accR], al
                
                movq rax, mm1
                mov rax, [rax]
                mul ebx
                add [accG], al
                
                movq rax, mm2
                mov rax, [rax]
                mul ebx
                add [accB], al

                ; Increments iterator of Loop2 and checks the Loop2 condition
                inc [iLoop2Iterator]
                xor rax, rax
                mov eax, 3
                cmp rax, [iLoop2Iterator]
                jne Loop2_body

            ; Increments iterator of Loop1 and checks the Loop1 condition
            inc [iLoop1Iterator]
            xor rax, rax
            mov eax, 3
            cmp rax, [iLoop1Iterator]
            jne Loop1_body
                 

        ; Channel R
        xor rax, rax
        mov al, [originalR]
        sub al, BYTE PTR [accR]
        add al, [originalR]         ; Compute new value of R

        mov rbx, [iOutputIterator]
        add rbx, [pOutChannelR]     ; Compute address of output pixel

        mov [rbx], al               ; Move new value

	    ; Channel G
        xor rax, rax
        mov al, [originalG]
        sub al, BYTE PTR [accG]
        add al, [originalG]         ; Compute new value of G

        mov rbx, [iOutputIterator]
        add rbx, [pOutChannelG]     ; Compute address of output pixel
        
        mov [rbx], al               ; Move new value
        
	    ; Channel B
        xor rax, rax
        mov al, [originalB]
        sub al, BYTE PTR [accB]
        add al, [originalB]         ; Compute new value of B

        mov rbx, [iOutputIterator]
        add rbx, [pOutChannelB]     ; Compute address of output pixel
        
        mov [rbx], al               ; Move new value
        
    

        ; Moves output iterator to the next pixel
        inc [iOutputIterator]

        ; Increments iterator of LoopCenterX and checks the LoopCenterX condition
        inc [iIteratorX]
        xor rax, rax
        mov eax, [imgWidth]
        add rax, -1
        cmp rax, [iIteratorX]
        jne LoopCenterX_body




    ; Rewrite the right border
    mov rax, [iIteratorY]       ; Compute offset on input image
    mul [imgWidth]
    add eax, [imgWidth]
    add rax, -1
    mov rdx, rax                ; rdx stores the offset on input image


    mov rcx, rdx                ; Compute pinter to pixel R channel value
    add rcx, [pInChannelR]
    mov al, BYTE PTR [rcx]      ; al stores the value of R channel
    
    mov rcx, [iOutputIterator]  ; Compute pinter to output pixel R channel value
    add rcx, [pOutChannelR]
    mov [rcx], al               ; Write R channel to output pixel
    

    mov rcx, rdx                ; Compute pinter to pixel G channel value
    add rcx, [pInChannelG]
    mov al, BYTE PTR [rcx]      ; al stores the value of G channel
    
    mov rcx, [iOutputIterator]  ; Compute pinter to output pixel G channel value     
    add rcx, [pOutChannelG]
    mov [rcx], al               ; Write G channel to output pixel

    
    mov rcx, rdx                ; Compute pinter to pixel B channel value
    add rcx, [pInChannelB]
    mov al, BYTE PTR [rcx]      ; al stores the value of B channel
    
    mov rcx, [iOutputIterator]  ; Compute pinter to output pixel B channel value       
    add rcx, [pOutChannelB]
    mov [rcx], al               ; Write B channel to output pixel


    ; Moves output iterator to the next pixel
    inc [iOutputIterator]


    ; Increments iterator of LoopCenterY and checks the LoopCenterY condition
    inc [iIteratorY]
    xor rax, rax
    mov eax, [imgHeight]
    cmp rax, [iIteratorY]
    jne LoopCenterY_body



    ret
    

UnsharpMasking ENDP

_TEXT  ENDS

END