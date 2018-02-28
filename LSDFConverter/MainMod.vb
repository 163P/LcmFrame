
Imports FileOperationLibrary.USTClass
Module MainMod

    Sub Main()

        Dim a As New IO.FileInfo(Replace(Command, """", ""))
        If a.Extension = ".ust" Or a.Extension = ".UST" Then
            Dim w As New LSDF_Converter.ToLSDF

            w.GotoLSDF(ReadAUst(Replace(Command, """", "")), System.IO.Path.GetFullPath(a.FullName) & "_LCM.LSDF")

        ElseIf a.Extension = ".lsdf" Or a.Extension = ".LSDF" Then
            Dim w As New LSDF_Converter.ToUST

            w.GotoUst(Replace(Command, """", ""), System.IO.Path.GetFullPath(a.FullName) & "_LCM.ust")
        End If
    End Sub



End Module
