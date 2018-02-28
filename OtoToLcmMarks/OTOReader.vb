Imports LCMTypeLibrary.OSPClass
Module OTOReader

    Public Function ReadAOto(line As String, Part As String) As Marks
        ReadAOto.Wav = Part & "\" & Mid(line, 1, InStr(line, "=") - 1)
        Dim Other As String = Replace(line, Mid(line, 1, InStr(line, "=") - 1) & "=", "")

        Dim arry As String()
        arry = Split(Other, ",")
        ReadAOto.Symbol = arry(0)
        If arry(1) <> "" Then ReadAOto.Offset = arry(1)
        If arry(2) <> "" Then ReadAOto.Consonant = arry(2)
        If arry(3) <> "" Then ReadAOto.Cutoff = arry(3)
        If arry(4) <> "" Then ReadAOto.Preutterance = arry(4)
        If arry(5) <> "" Then ReadAOto.Overlap = arry(5)

    End Function

    Public Function ReadOTOFile(filename As String, Part As String) As OSPFile
        Dim theotofile As IO.StreamReader = New IO.StreamReader(filename, System.Text.Encoding.UTF8)
        Dim lineNum As Short = theotofile.ReadToEnd.Split(vbCrLf).Count
        theotofile.Close()
        theotofile = New IO.StreamReader(filename, System.Text.Encoding.UTF8)

        Dim re = New OSPFile

        For i = 0 To lineNum - 2

            ReDim Preserve re.otos(i)

            re.otos(i) = ReadAOto(theotofile.ReadLine, Part)


        Next

        Return re
    End Function
End Module
