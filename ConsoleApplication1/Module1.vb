Imports IniLib

Module Module1

    Sub Main()
        Dim data2 = New IniFile
        data2.Add("SETTING")
        '  data2("SETTING").Add("kkk", "222")
        '  data2("SETTING")("kkk") = "444444"

        'Dim data = IniLib.ReadParser.ReadFileAsync("1.ust").Result
        Task.WaitAll(WriteParser.WriteFileAsync(data2, "3.ust", True))


    End Sub

End Module
