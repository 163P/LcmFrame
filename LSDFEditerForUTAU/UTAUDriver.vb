Imports LCMTypeLibrary.UTAUDriverClass
Public Class UTAUDriver

    Public Function StartUTAUResample(Resample As Resample, args As ResampleArgs, ifsee As Boolean, ifwait As Boolean)
        Dim p As New Process
        Dim info As New ProcessStartInfo
        info.FileName = Resample.Filename
        info.Arguments = Chr(34) & args.Input & Chr(34) & " " & Chr(34) & args.Out & Chr(34) & " " & args.PITMain & " " & args.Velocity & " """ & args.Flags & """ " & args.Offset & " " & args.Length & " " & args.Con & " " & args.EndBlank & " " & args.Volume & " " & args.Modulation & " " & args.PitchBend
        If ifsee = False Then info.WindowStyle = ProcessWindowStyle.Hidden
        p.StartInfo = info
        p.Start()
        If ifwait = True Then p.WaitForExit()
        Return 0
    End Function
    Public Function StartUTAUWavTool(wavtool As WavTool, args As WavToolArgs, ifsee As Boolean, ifwait As Boolean, WorkingDir As String)
        Dim p As New Process
        Dim info As New ProcessStartInfo
        info.FileName = wavtool.Filename
        info.Arguments = """" & args.File1 & """ """ & args.File2 & """ 0 " & args.Length & "@" & args.BPM & "+" & args.Pre & " " & args.p1 & " " & args.p2Overlap & " " & args.p3nextOverlap & " 0 " & args.v2 & " " & args.v3 & " 0 " & args.Overlap
        If ifsee = False Then info.WindowStyle = ProcessWindowStyle.Hidden
        info.WorkingDirectory = WorkingDir
        p.StartInfo = info
        p.Start()
        If ifwait = True Then p.WaitForExit()
        Return 0
    End Function
    Public Function GetPITArgs(bpm As Double, pit As Double()) As String
        Dim TmpArgs As String = "!" & bpm & " "
        Return 0
    End Function
    Public Function GetResampleArgs() As ResampleArgs
        Return New ResampleArgs
    End Function
    Public Function ToBase64(pit As Double) As Byte
        Dim Ten As Short
        If Ten < 0 Then
            Ten = 4096 - Ten
        End If
        Dim Two As Byte
        Two = CByte(Convert.ToByte(Ten, 2).ToString("00000000000"))
        Return 0
    End Function
    Public Sub LoadSinger(Dir As String)

    End Sub
    Public Function NewResample(Filename As String, Name As String) As Resample
        NewResample.Filename = Filename
        NewResample.Name = Name
    End Function
    Public Function NewWavtool(Filename As String, Name As String) As WavTool
        NewWavtool.Filename = Filename
        NewWavtool.Name = Name
    End Function
End Class
