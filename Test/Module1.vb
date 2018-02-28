Module Module1

    Sub Main()
        Dim k As New LcmDictionary.MainClass

        Dim tIn As System.IO.StreamReader = New IO.StreamReader("D:\CVVChinese.txt")
        Dim tOut As IO.StreamWriter = New IO.StreamWriter("D:\out.txt")
        Console.SetIn(tIn)
        Console.SetOut(tOut)

        Dim Line As String
        'Dim i = 0
        Do
            Line = Console.ReadLine
            ' i = i + 1
            If Line = Nothing Then Exit Do
            Dim a() = Split(Line, ",")

            Console.WriteLine(Line & "=" & k.GetSymbol(a(0), a(1)).Symbol1 & "," & k.GetSymbol(a(0), a(1)).Symbol2)
            'Debug.Print("Doing-Line" & i)
        Loop
        tIn.Close()
        tOut.Flush()
        tOut.Close()
    End Sub

End Module
