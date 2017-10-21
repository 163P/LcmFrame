Public Class MainClass
    Dim VowelA As String() = {"R", "a", "o", "e", "i", "u", "v"}
    Dim VowelB As String() = {"ang", "ong", "ing", "eng", "an", "ai", "ui", "ao", "ou", "iu", "ie", "ve", "er", "en", "in", "un", "vn", "ei"}
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
    Dim VE_r As String() = {"er"}


    Dim VS_a As String() = {"ai", "an", "a"}
    Dim VS_ao As String() = {"ao", "ang"}
    Dim VS_o As String() = {"ou", "ong"}
    Dim VS_e As String() = {"er", "e"}
    Dim VS_eu As String() = {"eng", "en"}
    Dim VS_i As String() = {"ing", "in", "ie", "iao", "ian", "iang", "ia", "iong", "i"}
    Dim VS_u As String() = {"uai", "uang", "uan", "un", "ui", "ua", "uo", "u"}
    Dim VS_v As String() = {"vn", "van", "v"}
    Dim VS_el As String() = {"ei"}

    Dim SyllablesWhole1 As String() = {"zhi", "chi", "shi"} '整体音节1，以ir结尾
    Dim SyllablesWhole2 As String() = {"zi", "ci", "si"} '整体音节2，以iz结尾
    Dim SyllablesWhole3 As String() = {"er"} '整体音节3，以r结尾
    Dim SyllablesWhole4 As String() = {"R"} '整体音节4，R
    Dim VowelDigit1 As String() = {"a", "o", "e", "i", "u", "v"} '一位韵母
    Dim VowelDigit2 As String() = {"an", "ai", "ui", "ao", "ou", "iu", "ie", "ve", "er", "en", "in", "un", "vn", "ei"， "ia", "ua", "uo"}
    '二位韵母
    Dim VowelDigit3 As String() = {"iao", "ian", "uai", "uan", "ang", "ong", "ing", "eng", "van"}  '三位韵母




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

    'Public Function GetSymbol(LycSt As String, LycNow As String) As LCMSymbol '废弃
    '    If LycNow = "R" Then
    '        GetSymbol.Symbol1 = "R"
    '        GetSymbol.Symbol2 = ""
    '        '单韵母时将第二记号留空
    '    ElseIf GetVowelType(LycNow).Type = VowelType.VowelA And GetCon(LycNow) = "" Then
    '        GetSymbol.Symbol1 = GetVEType(LycSt) & " " & GetCon(LycNow) & " " & GetVowelStart(LycNow)
    '        GetSymbol.Symbol2 = ""
    '    ElseIf GetVowelType(LycNow).Vowel = "ian" Then
    '        GetSymbol.Symbol1 = GetVEType(LycSt) & " " & GetCon(LycNow) & "ian"
    '        GetSymbol.Symbol2 = ""
    '    ElseIf GetCon(LycNow) = "" Then
    '        GetSymbol.Symbol1 = GetVEType(LycSt) & " " & GetVowelStart(LycNow)
    '        GetSymbol.Symbol2 = "_" & GetVowelType(LycNow).Vowel
    '    ElseIf LycSt = "zi" Or LycSt = "ci" Or LycSt = "si" Then
    '        GetSymbol.Symbol1 = "iz " & GetCon(LycNow) & "_" & GetVowelStart(LycNow)
    '        GetSymbol.Symbol2 = "_" & GetVowelType(LycNow).Vowel
    '    ElseIf LycSt = "zhi" Or LycSt = "chi" Or LycSt = "shi" Then
    '        GetSymbol.Symbol1 = "ir " & GetCon(LycNow) & "_" & GetVowelStart(LycNow)
    '        GetSymbol.Symbol2 = "_" & GetVowelType(LycNow).Vowel
    '    Else

    '        GetSymbol.Symbol1 = GetVEType(LycSt) & " " & GetCon(LycNow) & "_" & GetVowelStart(LycNow)
    '        GetSymbol.Symbol2 = "_" & GetVowelType(LycNow).Vowel
    '    End If






    '    With GetSymbol
    '        .LycNow = LycNow
    '        .LycSt = LycSt
    '    End With
    'End Function '废弃
    Public Function GetCon(Lyc As String) As String
        GetCon = Replace(Lyc, GetVowel(Lyc), "")
    End Function
    'Public Function GetVowelType(Lyc As String) As Vowel


    '    For i = 0 To VowelC.Length - 1
    '        If InStr(Lyc, VowelC(i)) <> 0 Then
    '            GetVowelType.Type = VowelType.VowelC
    '            GetVowelType.Vowel = VowelC(i)
    '            Exit Function
    '        End If
    '    Next


    '    For i = 0 To VowelB.Length - 1
    '        If InStr(Lyc, VowelB(i)) <> 0 Then
    '            GetVowelType.Type = VowelType.VowelB
    '            GetVowelType.Vowel = VowelB(i)
    '            Exit Function

    '         End If
    '    Next

    '    For i = 0 To VowelA.Length - 1
    '        If InStr(Lyc, VowelA(i)) <> 0 Then
    '            GetVowelType.Type = VowelType.VowelA
    '            GetVowelType.Vowel = VowelA(i)
    '            Exit Function

    '        End If
    '    Next

    '    GetVowelType.Type = VowelType.Unknown
    '    GetVowelType.Vowel = "a"
    'End Function
    ''' <summary>
    ''' 获得韵尾
    ''' </summary>
    ''' <param name="Lyc">韵母</param>
    ''' <returns></returns>
    Public Function GetVEType(Lyc As String) As String

        GetVEType = "a"

        Select Case Not 0
            Case Compare(Lyc, VE_l, 50, "l")
                GetVEType = Compare(Lyc, VE_l, 50, "l")
            Case Compare(Lyc, VE_au, 50, "au")
                GetVEType = Compare(Lyc, VE_au, 50, "au")
            Case Compare(Lyc, VE_ng, 50, "ng")
                GetVEType = Compare(Lyc, VE_ng, 50, "ng")
            Case Compare(Lyc, VE_n, 50, "n")
                GetVEType = Compare(Lyc, VE_n, 50, "n")
            Case Compare(Lyc, VE_e_at, 50, "e@")
                GetVEType = Compare(Lyc, VE_e_at, 50, "e@")
            Case Compare(Lyc, VE_ir, 50, "ir")
                GetVEType = Compare(Lyc, VE_ir, 50, "ir")
            Case Compare(Lyc, VE_iz, 50, "iz")
                GetVEType = Compare(Lyc, VE_iz, 50, "iz")
            Case Compare(Lyc, VE_r, 50, "r")
                GetVEType = Compare(Lyc, VE_r, 50, "r")
            Case Compare(Lyc, VowelA, 50)
                GetVEType = Compare(Lyc, VowelA, 50)
        End Select

    End Function
    'Public Function GetVowelStart(Lyc As String) As String
    '    For i = 0 To VS_e.Length - 1
    '        If InStr(Lyc, VS_e(i)) <> 0 And InStr(Lyc, "en") = 0 Then
    '            GetVowelStart = "e"
    '            Exit Function
    '        Else
    '            Exit For
    '        End If
    '    Next
    '    For i = 0 To VS_a.Length - 1
    '        If InStr(Lyc, VS_a(i)) <> 0 And InStr(Lyc, "ia") = 0 Then
    '            GetVowelStart = "a"
    '            Exit Function
    '        End If
    '    Next
    '    For i = 0 To VS_u.Length - 1
    '        If InStr(Lyc, VS_u(i)) <> 0 Then
    '            If InStr(Lyc, "iu") <> 0 Then
    '                GetVowelStart = "i"
    '                Exit Function
    '            End If
    '            GetVowelStart = "u"
    '            Exit Function
    '        End If
    '    Next

    '    For i = 0 To VS_v.Length - 1
    '        If InStr(Lyc, VS_v(i)) <> 0 Then
    '            GetVowelStart = "v"
    '            Exit Function
    '        End If
    '    Next
    '    For i = 0 To VS_i.Length - 1
    '        If InStr(Lyc, VS_i(i)) <> 0 Then
    '            GetVowelStart = "i"
    '            Exit Function
    '        End If
    '    Next
    '    For i = 0 To VS_ao.Length - 1
    '        If InStr(Lyc, VS_ao(i)) <> 0 Then
    '            GetVowelStart = "ao"
    '            Exit Function
    '        End If
    '    Next



    '    For i = 0 To VS_el.Length - 1
    '        If InStr(Lyc, VS_el(i)) <> 0 Then
    '            GetVowelStart = "el"
    '            Exit Function
    '        End If
    '    Next

    '    For i = 0 To VS_eu.Length - 1
    '        If InStr(Lyc, VS_eu(i)) <> 0 Then
    '            GetVowelStart = "eu"
    '            Exit Function
    '        End If
    '    Next



    '    For i = 0 To VS_o.Length - 1
    '        If InStr(Lyc, VS_o(i)) <> 0 Then
    '            GetVowelStart = "o"
    '            Exit Function
    '        End If
    '    Next



    '    GetVowelStart = "a"
    'End Function
    ''' <summary>
    ''' 获取发音标记。第一个音符前需加R。
    ''' </summary>
    ''' <param name="LycSt"></param>
    ''' <param name="LycNow"></param>
    ''' <returns></returns>
    Public Function GetSymbol(LycSt As String, LycNow As String) As LCMSymbol

        Dim VowelST = GetVowel(LycSt)
        Dim VowelNow = GetVowel(LycNow)
        If VowelST = 0 Or VowelNow = 0 Then Throw (New InvalidValueException("Lyc值无效。"))
        Dim VET_ST = GetVEType(VowelST)

        GetSymbol.Symbol1 = VET_ST & " " & GetCon(LycSt) & "_" & Left(VowelNow, 1)
        GetSymbol.Symbol2 = "_" & VowelNow

    End Function
    ''' <summary>
    ''' 获得韵母部（包括介母）
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns>默认返回0</returns>
    Public Function GetVowel(str As String) As String
        GetVowel = 0
        Select Case Not Nothing
            Case Compare(str, SyllablesWhole1, 50)
                GetVowel = Compare(str, SyllablesWhole1, 50)
            Case Compare(str, SyllablesWhole2, 50)
                GetVowel = Compare(str, SyllablesWhole2, 50)
            Case Compare(str, SyllablesWhole3, 50)
                GetVowel = Compare(str, SyllablesWhole3, 50)
            Case Compare(str, SyllablesWhole4, 50)
                GetVowel = Compare(str, SyllablesWhole4, 50)
            Case Compare(str, VowelDigit3, 3)
                GetVowel = Compare(str, VowelDigit3, 3)
            Case Compare(str, VowelDigit2, 2)
                GetVowel = Compare(str, VowelDigit2, 2)
            Case Compare(str, VowelDigit1, 1)
                GetVowel = Compare(str, VowelDigit1, 1)
        End Select
    End Function

    ''' <summary>
    ''' 将str从右数第right个字节的内容 与array中的每个元素对比。
    ''' </summary>
    ''' <param name="str">包含需要匹配的字符串的字符串</param>
    ''' <param name="array">待匹配的Array</param>
    ''' <param name="Rightl">从右数第几个字节</param>
    ''' <param name="Re">若匹配到，返回该值</param>
    ''' <returns>若在array中找到该内容，返回Re。若RE=""则返回匹配到的内容。若没有找到，返回 Nothing</returns>
    Public Function Compare(str As String, array As String(), Rightl As Int16， Optional ByVal Re As String = "")

        Compare = Nothing

        If Rightl <> 0 Then Rightl = 50

        If Re = "" Then

            For i = 0 To array.Length - 1 '匹配
                If InStr(Right(str, Rightl), array(i)) <> 0 Then '限制最大处理长度为50个字符
                    Compare = array(i)
                    Exit Function
                End If
            Next

        Else

            For i = 0 To array.Length - 1 '匹配
                If InStr(Right(str, Rightl), array(i)) <> 0 Then '限制最大处理长度为50个字符
                    Compare = Re
                    Exit Function
                End If
            Next
        End If
    End Function
    Public Class InvalidValueException : Inherits ApplicationException
        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub
    End Class
End Class
