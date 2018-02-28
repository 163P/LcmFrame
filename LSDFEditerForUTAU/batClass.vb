Imports LCMTypeLibrary.UTAUDriverClass
Public Class batClass

    Public Sub WriteCommandLine(steam As IO.StreamWriter, command As String, name As String, Value As String)
        steam.WriteLine("@" & command & " " & name & "=" & Value)
    End Sub
    Public Sub WriteBATLine(steam As IO.StreamWriter, command As String, Value As String)
        steam.WriteLine("@" & command & " " & Value)
    End Sub
    Public Sub WriteBat(Resampler As Resample, Wavtool As WavTool, ResamplerArgs1 As ResampleArgs, WavToolArgs1 As WavToolArgs, ResamplerArgs2 As ResampleArgs, WavToolArgs2 As WavToolArgs, MainArgs As MainArgs, steam As IO.StreamWriter)

        WriteCommandLine(steam, "rem", "project", "New")
        WriteCommandLine(steam, "set", "loadmodule", "")
        WriteCommandLine(steam, "set", "tempo", WavToolArgs1.BPM)
        WriteCommandLine(steam, "set", "samples", "44100")
        WriteCommandLine(steam, "set", "oto", MainArgs.SingerDir)
        WriteCommandLine(steam, "set", "tool", Wavtool.Filename)
        WriteCommandLine(steam, "set", "resamp", Resampler.Filename)
        WriteCommandLine(steam, "set", "output", "temp.wav")
        WriteCommandLine(steam, "set", "helper", "temp_helper.bat")
        WriteCommandLine(steam, "set", "cachedir", MainArgs.TempDir)
        WriteCommandLine(steam, "set", "flag", "")
        WriteCommandLine(steam, "set", "env", "0 5 35 0 100 100 0")
        WriteCommandLine(steam, "set", "stp", "0")

        steam.WriteLine("")

        WriteBATLine(steam, "del", """%output%"" 2>nul")
        WriteBATLine(steam, "mkdir", """%cachedir%"" 2>nul")

        steam.WriteLine("")

        WriteCommandLine(steam, "set", "params", ResamplerArgs1.Volume & " " & ResamplerArgs1.Modulation & " " & ResamplerArgs1.PitchBend)
        WriteCommandLine(steam, "set", "flag", ResamplerArgs1.Flags)
        WriteCommandLine(steam, "set", "env", WavToolArgs1.p1 & " " & WavToolArgs1.p2Overlap & " " & WavToolArgs1.p3nextOverlap & " 0 " & WavToolArgs1.v2 & " " & WavToolArgs1.v3 & " 0 " & WavToolArgs1.p2Overlap)
        WriteCommandLine(steam, "set", "vel", ResamplerArgs1.Velocity)
        WriteCommandLine(steam, "set", "temp", """%cachedir%\1.wav""")

        WriteBATLine(steam, "echo", "####################--------------------(1/2)")
        WriteBATLine(steam, "call", "%helper% ""%oto%\" & ResamplerArgs1.Input & """ " & ResamplerArgs1.PITMain & " " & WavToolArgs1.Length & "@" & WavToolArgs1.BPM & "+" & WavToolArgs1.Pre & " " & WavToolArgs1.Pre & " " & ResamplerArgs1.Offset & " " & ResamplerArgs1.Length & " " & ResamplerArgs1.Con & " " & ResamplerArgs1.EndBlank & " 1")

        WriteCommandLine(steam, "set", "params", ResamplerArgs2.Volume & " " & ResamplerArgs2.Modulation & " " & ResamplerArgs2.PitchBend)
        WriteCommandLine(steam, "set", "flag", ResamplerArgs2.Flags)
        WriteCommandLine(steam, "set", "env", WavToolArgs2.p1 & " " & WavToolArgs2.p2Overlap & " " & WavToolArgs2.p3nextOverlap & " 0 " & WavToolArgs2.v2 & " " & WavToolArgs2.v3 & " 0 " & WavToolArgs2.p2Overlap)
        WriteCommandLine(steam, "set", "vel", ResamplerArgs2.Velocity)
        WriteCommandLine(steam, "set", "temp", """%cachedir%\2.wav""")

        WriteBATLine(steam, "echo", "####################--------------------(2/2)")
        WriteBATLine(steam, "call", "%helper% ""%oto%\" & ResamplerArgs2.Input & """ " & ResamplerArgs2.PITMain & " " & WavToolArgs2.Length & "@" & WavToolArgs2.BPM & "+" & WavToolArgs2.Pre & " " & WavToolArgs2.Pre & " " & ResamplerArgs2.Offset & " " & ResamplerArgs2.Length & " " & ResamplerArgs2.Con & " " & ResamplerArgs2.EndBlank & " 2")


        WriteBATLine(steam, "if", "not exist ""%output%.whd"" goto E")
        WriteBATLine(steam, "if", "not exist ""%output%.dat"" goto E")

        steam.WriteLine("copy /Y ""%output%.whd"" /B + ""%output%.dat"" /B ""%output%""")
        steam.WriteLine("del ""%output%.whd""")
        steam.WriteLine("del ""%output%.dat""")
        steam.WriteLine(":E")
        steam.WriteLine("")

    End Sub
    Public Sub StartBat(Filename As String, ifsee As Boolean, ifwait As Boolean)
        Dim p As New Process
        p.StartInfo.FileName = Filename

        If ifsee = False Then p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden '将不显示命令窗口
        p.StartInfo.WorkingDirectory = "D:\"
        p.Start()
        If ifwait = True Then p.WaitForExit()
    End Sub
End Class
