Module MainMod

    Sub Main()
        Dim a As New IO.FileInfo(Replace(Command, """", ""))


        If a.Extension = ".lsdf" Or a.Extension = ".LSDF" Then
            Dim w As New LSDF_Converter.ToUST

            w.GotoUst(Replace(Command, """", ""), System.IO.Path.GetFullPath(a.FullName) & "_LCM.ust")

            Dim p As New Process
            Dim info As New ProcessStartInfo
            info.FileName = "C:\Program Files (x86)\UTAU\utau.exe"
            info.Arguments = System.IO.Path.GetFullPath(a.FullName) & "_LCM.ust"
            p.StartInfo = info
            p.Start()
            p.WaitForExit()

            Dim u As New LSDF_Converter.ToLSDF
            u.GotoLSDF(u.ReadAUst(System.IO.Path.GetFullPath(a.FullName) & "_LCM.ust"), System.IO.Path.GetFullPath(a.FullName) & "_NewLSDF")
            FileIO.FileSystem.DeleteFile(Replace(Command, """", ""))

            FileIO.FileSystem.RenameFile(System.IO.Path.GetFullPath(a.FullName) & "_NewLSDF", FileIO.FileSystem.GetName(Replace(Command, """", "")))

            FileIO.FileSystem.DeleteFile(System.IO.Path.GetFullPath(a.FullName) & "_LCM.ust")
        End If
    End Sub

End Module
