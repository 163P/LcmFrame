Public Class MainClass
    Dim VowelA As String() = {"R", "a", "o", "e", "i", "u", "v"}
    Dim VowelB As String() = {"ang", "ong", "ing", "eng", "an", "ai", "ui", "ao", "ou", "iu", "ie", "ve", "er", "en", "in", "un", "vn"}
    Dim VowelC As String() = {"iao", "ian", "iang", "uai", "uan", "uang", "van", "iong", "ia", "ua", "uo"}

    ''' <summary>
    ''' VowelEndingA
    ''' </summary>
    Dim VE_l As String() = {"ai", "ei", "ui"}
    Dim VE_au As String() = {"ao", "ou", "iu"}
    Dim VE_e_at As String() = {"ie", "ve"}
    Dim VE_n As String() = {"an", "en", "in", "un", "vn"}
    Dim VE_ng As String() = {"ang", "eng", "ing", "ong"}
    Dim VE_iz As String() = {"zi", "ci", "si"}
    Dim VE_ir As String() = {"zhi", "chi", "shi"}

    Dim VS_a As String() = {"ai", "an", "a"}
    Dim VS_ao As String() = {"ao", "ang"}
    Dim VS_o As String() = {"ou", "ong"}
    Dim VS_e As String() = {"er", "e"}
    Dim VS_eu As String() = {"eng", "en"}
    Dim VS_i As String() = {"ing", "in", "ie", "iao", "ian", "iang", "ia", "iong", "i"}
    Dim VS_u As String() = {"uai", "uang", "uan", "un", "ui", "ua", "uo", "u"}
    Dim VS_v As String() = {"vn", "van", "v"}
    Dim VS_el As String() = {"ei"}

    ' Public Length As Short
    Structure LCMSymbol
        Dim Symbol1 As String
        Dim Symbol2 As String
        ''' <summary>
        ''' 拆音发音之前的发音
        ''' </summary>
        Dim LycSt As String
        ''' <summary>
        ''' 需要进行拆音的发音
        ''' </summary>
        Dim LycNow As String
    End Structure
    Enum VowelType
        VowelA
        VowelB
        VowelC
        Unknown
    End Enum
    Enum VEType
        a
        o
        e
        i
        u
        v
        iz
        ir
        l
        au
        e_at
        n
        ng
        r
        Unknown
    End Enum
    Structure Vowel
        Dim Type As VowelType
        Dim Vowel As String
    End Structure
    Structure VowelEnding
        Dim Type As VEType
        Dim VowelEnding As String
    End Structure

    Public Function GetSymbol(LycSt As String, LycNow As String) As LCMSymbol
        If LycNow = "R" Then
            GetSymbol.Symbol1 = "R"
            GetSymbol.Symbol2 = ""
            '单韵母时将第二记号留空
        ElseIf GetVowelType(LycNow).Type = VowelType.VowelA And GetCon(LycNow) = "" Then
            GetSymbol.Symbol1 = GetVEType(LycSt).VowelEnding & " " & GetCon(LycNow) & " " & GetVowelStart(LycNow)
            GetSymbol.Symbol2 = ""
        ElseIf GetVowelType(LycNow).Vowel = "ian" Then
            GetSymbol.Symbol1 = GetVEType(LycSt).VowelEnding & " " & GetCon(LycNow) & "ian"
            GetSymbol.Symbol2 = ""
        ElseIf GetCon(LycNow) = "" Then
            GetSymbol.Symbol1 = GetVEType(LycSt).VowelEnding & " " & GetVowelStart(LycNow)
            GetSymbol.Symbol2 = "_" & GetVowelType(LycNow).Vowel
        ElseIf LycSt = "zi" Or LycSt = "ci" Or LycSt = "si" Then
            GetSymbol.Symbol1 = "iz " & GetCon(LycNow) & "_" & GetVowelStart(LycNow)
            GetSymbol.Symbol2 = "_" & GetVowelType(LycNow).Vowel
        ElseIf LycSt = "zhi" Or LycSt = "chi" Or LycSt = "shi" Then
            GetSymbol.Symbol1 = "ir " & GetCon(LycNow) & "_" & GetVowelStart(LycNow)
            GetSymbol.Symbol2 = "_" & GetVowelType(LycNow).Vowel
        Else

            GetSymbol.Symbol1 = GetVEType(LycSt).VowelEnding & " " & GetCon(LycNow) & "_" & GetVowelStart(LycNow)
            GetSymbol.Symbol2 = "_" & GetVowelType(LycNow).Vowel
        End If






        With GetSymbol
            .LycNow = LycNow
            .LycSt = LycSt
        End With
    End Function
    Public Function GetCon(Lyc As String) As String
        GetCon = Replace(Lyc, GetVowelType(Lyc).Vowel, "")
    End Function
    Public Function GetVowelType(Lyc As String) As Vowel


        For i = 0 To VowelC.Length - 1
            If InStr(Lyc, VowelC(i)) <> 0 Then
                GetVowelType.Type = VowelType.VowelC
                GetVowelType.Vowel = VowelC(i)
                Exit Function
            End If
        Next


        For i = 0 To VowelB.Length - 1
            If InStr(Lyc, VowelB(i)) <> 0 Then
                GetVowelType.Type = VowelType.VowelB
                GetVowelType.Vowel = VowelB(i)
                Exit Function

             End If
        Next

        For i = 0 To VowelA.Length - 1
            If InStr(Lyc, VowelA(i)) <> 0 Then
                GetVowelType.Type = VowelType.VowelA
                GetVowelType.Vowel = VowelA(i)
                Exit Function

            End If
        Next

        GetVowelType.Type = VowelType.Unknown
        GetVowelType.Vowel = "a"
    End Function
    Public Function GetVEType(Lyc As String) As VowelEnding
        If GetVowelType(Lyc).Type = VowelType.VowelA Then
            GetVEType.VowelEnding = GetVowelType(Lyc).Vowel
            Exit Function
        End If

        For i = 0 To VE_l.Length - 1
            If InStr(Lyc, VE_l(i)) <> 0 Then
                GetVEType.Type = VEType.l
                GetVEType.VowelEnding = "l"
                Exit Function
            End If
        Next
        For i = 0 To VE_au.Length - 1
            If InStr(Lyc, VE_au(i)) <> 0 Then
                GetVEType.Type = VEType.au
                GetVEType.VowelEnding = "au"
                Exit Function
            End If
        Next
        For i = 0 To VE_ng.Length - 1
            If InStr(Lyc, VE_ng(i)) <> 0 Then
                GetVEType.Type = VEType.ng
                GetVEType.VowelEnding = "ng"
                Exit Function
            End If
        Next
        For i = 0 To VE_n.Length - 1
            If InStr(Lyc, VE_n(i)) <> 0 Then
                GetVEType.Type = VEType.n
                GetVEType.VowelEnding = "n"
                Exit Function
            End If
        Next

        For i = 0 To VE_e_at.Length - 1
            If InStr(Lyc, VE_e_at(i)) <> 0 Then
                GetVEType.Type = VEType.e_at
                GetVEType.VowelEnding = "e@"
                Exit Function
            End If
        Next
        For i = 0 To VE_ir.Length - 1
            If InStr(Lyc, VE_ir(i)) <> 0 Then
                GetVEType.Type = VEType.ir
                GetVEType.VowelEnding = "ir"
                Exit Function
            End If
        Next
        For i = 0 To VE_iz.Length - 1
            If InStr(Lyc, VE_iz(i)) <> 0 Then
                GetVEType.Type = VEType.iz
                GetVEType.VowelEnding = "iz"
                Exit Function
            End If
        Next

        GetVEType.Type = VEType.Unknown
        GetVEType.VowelEnding = "a"
    End Function
    Public Function GetVowelStart(Lyc As String) As String
        For i = 0 To VS_e.Length - 1
            If InStr(Lyc, VS_e(i)) <> 0 And InStr(Lyc, "en") = 0 Then
                GetVowelStart = "e"
                Exit Function
            Else
                Exit For
            End If
        Next
        For i = 0 To VS_a.Length - 1
            If InStr(Lyc, VS_a(i)) <> 0 And InStr(Lyc, "ia") = 0 Then
                GetVowelStart = "a"
                Exit Function
            End If
        Next
        For i = 0 To VS_u.Length - 1
            If InStr(Lyc, VS_u(i)) <> 0 Then
                If InStr(Lyc, "iu") <> 0 Then
                    GetVowelStart = "i"
                    Exit Function
                End If
                GetVowelStart = "u"
                Exit Function
            End If
        Next

        For i = 0 To VS_v.Length - 1
            If InStr(Lyc, VS_v(i)) <> 0 Then
                GetVowelStart = "v"
                Exit Function
            End If
        Next
        For i = 0 To VS_i.Length - 1
            If InStr(Lyc, VS_i(i)) <> 0 Then
                GetVowelStart = "i"
                Exit Function
            End If
        Next
        For i = 0 To VS_ao.Length - 1
            If InStr(Lyc, VS_ao(i)) <> 0 Then
                GetVowelStart = "ao"
                Exit Function
            End If
        Next



        For i = 0 To VS_el.Length - 1
            If InStr(Lyc, VS_el(i)) <> 0 Then
                GetVowelStart = "el"
                Exit Function
            End If
        Next

        For i = 0 To VS_eu.Length - 1
            If InStr(Lyc, VS_eu(i)) <> 0 Then
                GetVowelStart = "eu"
                Exit Function
            End If
        Next



        For i = 0 To VS_o.Length - 1
            If InStr(Lyc, VS_o(i)) <> 0 Then
                GetVowelStart = "o"
                Exit Function
            End If
        Next



        GetVowelStart = "a"
    End Function
End Class
