Imports LCMTypeLibrary.ConstClass
Public Class MainClass

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
    Public Function GetSymbol2(Lyc As String) As String
        If Lyc = "zi" Then
            Return "iz"
        ElseIf Lyc = "ci" Then
            Return "iz"
        ElseIf Lyc = "si" Then
            Return "iz"
        ElseIf Lyc = "zhi" Then
            Return "ir"
        ElseIf Lyc = "chi" Then
            Return "ir"
        ElseIf Lyc = "shi" Then
            Return "ir"
        Else
            Return GetVowel(Lyc)
        End If
    End Function
    Public Function GetStart(Lyc As String) As String
        If Lyc = "zi" Then
            Return "i"
        ElseIf Lyc = "ci" Then
            Return "i"
        ElseIf Lyc = "si" Then
            Return "i"
        ElseIf Lyc = "zhi" Then
            Return "i"
        ElseIf Lyc = "chi" Then
            Return "i"
        ElseIf Lyc = "shi" Then
            Return "i"
        ElseIf GetVowel(Lyc) = "eng" Then
            Return "eu"
        ElseIf GetVowel(Lyc) = "ao" Then
            Return "au"
        ElseIf GetVowel(Lyc) = "ei" Then
            Return "el"

        Else
            Return Left(GetVowel(Lyc), 1)
        End If
    End Function

    Public Function GetCon(Lyc As String) As String
        If Lyc = "zi" Then
            Return "z"
        ElseIf Lyc = "ci" Then
            Return "c"
        ElseIf Lyc = "si" Then
            Return "s"
        ElseIf Lyc = "zhi" Then
            Return "zh"
        ElseIf Lyc = "chi" Then
            Return "ch"
        ElseIf Lyc = "shi" Then
            Return "sh"
        Else
            Return Replace(Lyc, GetVowel(Lyc), "")
        End If
    End Function

    ''' <summary>
    ''' 获得韵尾
    ''' </summary>
    ''' <param name="Lyc">韵母</param>
    ''' <returns></returns>
    Public Function GetVEType(Lyc As String) As String

        GetVEType = "a"

        If Compare(Lyc, VE_l, 50, "l") <> Nothing Then
            GetVEType = Compare(Lyc, VE_l, 50, "l")
        ElseIf Compare(Lyc, VE_au, 50, "au") <> Nothing Then
            GetVEType = Compare(Lyc, VE_au, 50, "au")
        ElseIf Compare(Lyc, VE_ng, 50, "ng") <> Nothing Then
            GetVEType = Compare(Lyc, VE_ng, 50, "ng")
        ElseIf Compare(Lyc, VE_n, 50, "n") <> Nothing Then
            GetVEType = Compare(Lyc, VE_n, 50, "n")
        ElseIf Compare(Lyc, VE_e_at, 50, "ea") <> Nothing Then
            GetVEType = Compare(Lyc, VE_e_at, 50, "ea")
        ElseIf Compare(Lyc, VE_ir, 50, "ir") <> Nothing Then
            GetVEType = Compare(Lyc, VE_ir, 50, "ir")
        ElseIf Compare(Lyc, VE_iz, 50, "iz") <> Nothing Then
            GetVEType = Compare(Lyc, VE_iz, 50, "iz")
        ElseIf Compare(Lyc, VE_r, 50, "r") <> Nothing Then
            GetVEType = Compare(Lyc, VE_r, 50, "r")
        ElseIf Compare(Lyc, VowelA, 50) <> Nothing <> Nothing Then
            GetVEType = Compare(Lyc, VowelA, 50)
        End If

    End Function
    Public Function ReplaceV(Str As String) As String
        ReplaceV = Replace(Str, "qu", "qv")
        ReplaceV = Replace(ReplaceV, "ju", "jv")
        ReplaceV = Replace(ReplaceV, "xu", "xv")
        ReplaceV = Replace(ReplaceV, "ue", "ve")
        ReplaceV = Replace(ReplaceV, "yu", "yv")
        ReplaceV = Replace(ReplaceV, "yuan", "yvan")
        ReplaceV = Replace(ReplaceV, "yun", "yvn")
        ReplaceV = Replace(ReplaceV, "juan", "jvan")
        ReplaceV = Replace(ReplaceV, "quan", "qvan")
        ReplaceV = Replace(ReplaceV, "xuan", "xvan")
        ReplaceV = Replace(ReplaceV, "qun", "qvn")
        ReplaceV = Replace(ReplaceV, "xun", "xvn")
        ReplaceV = Replace(ReplaceV, "jun", "jvn")
    End Function
    ''' <summary>
    ''' 获取发音标记。第一个音符前需加R。
    ''' </summary>
    ''' <param name="LycSt"></param>
    ''' <param name="LycNow"></param>
    ''' <returns></returns>
    Public Function GetSymbol(LycSt As String, LycNow As String) As LCMSymbol

        LycSt = ReplaceV(LycSt)
        LycNow = ReplaceV(LycNow)

        Dim VowelST = GetVowel(LycSt)
        Dim VowelNow = GetVowel(LycNow)
        If VowelST = Nothing Or VowelNow = Nothing Then Throw (New InvalidValueException("Lyc值无效。"))
        Dim VET_ST = GetVEType(VowelST)

        If GetCon(LycNow) = "" Then
            GetSymbol.Symbol1 = VET_ST & " " & GetStart(LycNow)
        Else
            GetSymbol.Symbol1 = VET_ST & " " & GetCon(LycNow) & "_" & GetStart(LycNow)
        End If


        GetSymbol.Symbol2 = "_" & GetSymbol2(LycNow)


        Return Fix(New LCMSymbol With {.Symbol1 = GetSymbol.Symbol1,
            .Symbol2 = GetSymbol.Symbol2})
    End Function
    ''' <summary>
    ''' 获得韵母部（包括介母）
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns>默认返回0</returns>
    Public Function GetVowel(str As String) As String
        GetVowel = 0
        If Compare(str, SyllablesWhole1, 50) <> Nothing Then
            GetVowel = Compare(str, SyllablesWhole1, 50)
        ElseIf Compare(str, SyllablesWhole2, 50) <> Nothing Then
            GetVowel = Compare(str, SyllablesWhole2, 50)
        ElseIf Compare(str, SyllablesWhole3, 50) <> Nothing Then
            GetVowel = Compare(str, SyllablesWhole3, 50)
        ElseIf Compare(str, SyllablesWhole4, 50) <> Nothing Then
            GetVowel = Compare(str, SyllablesWhole4, 50)
        ElseIf Compare(str, VowelDigit4, 3) <> Nothing Then
            GetVowel = Compare(str, VowelDigit4, 4)
        ElseIf Compare(str, VowelDigit3, 3) <> Nothing Then
            GetVowel = Compare(str, VowelDigit3, 3)
        ElseIf Compare(str, VowelDigit2, 3) <> Nothing Then
            GetVowel = Compare(str, VowelDigit2, 2)
        ElseIf Compare(str, VowelDigit1, 3) <> Nothing Then
            GetVowel = Compare(str, VowelDigit1, 1)
        End If
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

    Public Function Fix(sy As LCMSymbol) As LCMSymbol
        Fix = sy
        If sy.Symbol2 = "_ian" Then
            Fix.Symbol2 = "Null"
            Fix.Symbol1 = Replace(sy.Symbol1, "_i", "y_an")
            'ElseIf sy.Symbol2 = "_ei" Then
            '    Fix.Symbol1 = Replace(sy.Symbol1, "_e", "_el")
            '    Fix.Symbol2 = "Null"
            'ElseIf sy.Symbol2 = "_eng" Then
            '    Fix.Symbol1 = Replace(sy.Symbol1, "_e", "_eu")
            '    Fix.Symbol2 = "Null"
            'ElseIf sy.Symbol2 = "_ao" Then
            '    Fix.Symbol1 = Replace(sy.Symbol1, "_a", "_au")
            '    Fix.Symbol2 = "Null"
            'ElseIf sy.Symbol2 = "_ang" Then
            '    Fix.Symbol1 = Replace(sy.Symbol1, "_a", "_au")
            '    Fix.Symbol2 = "Null"
        End If
    End Function
End Class
