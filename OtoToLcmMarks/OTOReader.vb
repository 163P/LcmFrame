Module OTOReader
    Structure OTO
        Dim Wav As String
        Dim Symbol As String
        Dim Offset As Double
        Dim Consonant As Double
        Dim Cutoff As Double
        Dim Preutterance As Double
        Dim Overlap As Double
    End Structure
    Structure OTOFile
        Dim otos() As OTO
    End Structure
    Public Function ReadAOto(line As String, Part As String) As OTO
        ReadAOto.Wav = Part & "\" & Mid(line, 1, InStr(line, "=") - 1)
        Dim Other As String = Replace(line, Mid(line, 1, InStr(line, "=") - 1) & "=", "")

        Dim arry As String()
        arry = Split(Other, ",")
        ReadAOto.Symbol = arry(0)
        ReadAOto.Offset = arry(1)
        ReadAOto.Consonant = arry(2)
        ReadAOto.Cutoff = arry(3)
        ReadAOto.Preutterance = arry(4)
        ReadAOto.Overlap = arry(5)

    End Function

    Public Function ReadOTOFile(filename As String, Part As String) As OTOFile
        Dim theotofile As IO.StreamReader = New IO.StreamReader(filename, System.Text.Encoding.UTF8)
        Dim lineNum As Short = theotofile.ReadToEnd.Split(vbCrLf).Count
        theotofile.Close()
        theotofile = New IO.StreamReader(filename, System.Text.Encoding.UTF8)
        For i = 0 To lineNum - 2

            ReDim Preserve ReadOTOFile.otos(i)

            ReadOTOFile.otos(i) = ReadAOto(theotofile.ReadLine, Part)


        Next

    End Function
End Module
