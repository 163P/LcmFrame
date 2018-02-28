Imports IniLib
Imports FileOperationLibrary.USTClass
Module MainMod

    Sub Main()

        Dim a As New IO.FileInfo(Replace(Command, """", ""))
        Console.WriteLine("LCM Frame V0.1")
        Console.WriteLine("##########################")
        Console.WriteLine("监视中。请不要关闭此窗口。")
        Console.WriteLine("##########################")
        Console.WriteLine("LCM Project")
        If a.Extension = ".lsdf" Or a.Extension = ".LSDF" Then
            Dim w As New LSDF_Converter.ToUST

            w.GotoUst(Replace(Command, """", ""), System.IO.Path.GetFullPath(a.FullName) & "_LCM.ust")

            Dim p As New Process
            Dim info As New ProcessStartInfo
            Dim ini = IniLib.ReadParser.ReadFileAsync(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase & "Setting.ini").Result

            info.FileName = GetINI("UTAU", "UTAUPath", "", ini)
            info.Arguments = System.IO.Path.GetFullPath(a.FullName) & "_LCM.ust"
            p.StartInfo = info
            p.Start()
            p.WaitForExit()

            Dim u As New LSDF_Converter.ToLSDF
            u.GotoLSDF(ReadAUst(System.IO.Path.GetFullPath(a.FullName) & "_LCM.ust"), System.IO.Path.GetFullPath(a.FullName) & "_NewLSDF")
            FileIO.FileSystem.DeleteFile(Replace(Command, """", ""))

            FileIO.FileSystem.RenameFile(System.IO.Path.GetFullPath(a.FullName) & "_NewLSDF", FileIO.FileSystem.GetName(Replace(Command, """", "")))

            FileIO.FileSystem.DeleteFile(System.IO.Path.GetFullPath(a.FullName) & "_LCM.ust")
        End If
    End Sub
    Public Function GetINI(Section As String, Parameter As String, DefaultValue As String, ini As IniLib.IniFile)

        Dim s = ini(Section)(Parameter)
        If (String.IsNullOrEmpty(s)) Then
            Return DefaultValue
        End If
        Return s
    End Function
End Module
