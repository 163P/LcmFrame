Imports LCMTypeLibrary.OSPClass
Imports LCMTypeLibrary.UTAUDriverClass
Imports FileOperationLibrary.LSDFClass
Imports FileOperationLibrary.OSPClass
Imports LCMTypeLibrary.StructureClass
Module Main
    '这是用*LSDF*文件生成“temp.bat”的东西
    '程序需要接受的参数如下：
    'LSDF的路径
    '程序生成的temp.bat和temp_helper.bat在ust同路径下
    Dim nowResample As Resample
    Dim nowWavTool As WavTool
    Dim MainA As MainArgs
    Dim ResampleArg1 As ResampleArgs '第一个音素的参数
    Dim WavToolArgs1 As WavToolArgs
    Dim osp1 As Marks
    Dim ResampleArg2 As ResampleArgs '第二个音素的参数
    Dim WavToolArgs2 As WavToolArgs
    Dim osp2 As Marks
    Dim ResampleArg3 As ResampleArgs '第三个音素的参数
    Dim WavToolArgs3 As WavToolArgs
    Dim osp3 As Marks
    Dim ResampleArgAfter As ResampleArgs '下一个音符首音素的参数
    Dim WavToolArgsAfter As WavToolArgs
    Dim osp4 As Marks
    Dim OSPdoc As New Xml.XmlDocument

    Dim lsdfFile As LSDFFileSystem = ReadXml(Command()) '打开LSDF文档
    Dim FIX As String
    Dim S As Short
    Dim i As Short = 0 '音素数,合成序列数
    Dim n As Short = 0 '音符数
    Sub Main()
        OSPdoc.Load(MainA.SingerDir & "\Marks.osp") '加载OSP文件

        Dim bat As System.IO.StreamWriter = New System.IO.StreamWriter(MainA.TempDir & "\temp.bat", True, System.Text.Encoding.UTF8) '建立bat文件

        WriteHead(nowResample, nowWavTool, NewWavtoolArg(lsdfFile, n, 1), MainA, bat)

        Do
            i = 0
            ResampleArg1 = NewResampleArg(lsdfFile, n, 1) 'reArg赋值
            WavToolArgs1 = NewWavtoolArg(lsdfFile, n, 1)

            ResampleArg2 = NewResampleArg(lsdfFile, n, 2) 'reArg赋值
            WavToolArgs2 = NewWavtoolArg(lsdfFile, n, 2)
            ResampleArg3 = NewResampleArg(lsdfFile, n, 3) 'reArg赋值
            WavToolArgs3 = NewWavtoolArg(lsdfFile, n, 3)



        Loop
    End Sub
    Private Function GetOSPMark(section As Short, phoneme As Short) As Marks
        If phoneme = 1 Then
            GetOSPMark = ReadAOSPSection(OSPdoc, lsdfFile.Section(section).Phoneme1)
        ElseIf phoneme = 2 Then
            GetOSPMark = ReadAOSPSection(OSPdoc, lsdfFile.Section(section).Phoneme2)
        ElseIf phoneme = 3 Then
            GetOSPMark = ReadAOSPSection(OSPdoc, lsdfFile.Section(section).Phoneme3)
        Else
            GetOSPMark = ReadAOSPSection(OSPdoc, lsdfFile.Section(section).Phoneme1)
        End If
    End Function

    Private Function NewWavtoolArg(xml As LSDFFileSystem, section As Short, phoneme As Short) As WavToolArgs
        Dim theWavtoolArg As WavToolArgs

        Dim osp1 As Marks = GetOSPMark(section, phoneme)
        Dim osp2 As Marks
        If phoneme = xml.Section(section).PhonemeNum Then
            osp2 = GetOSPMark(section + 1, 1)
        Else
            osp2 = GetOSPMark(section, phoneme + 1)
        End If

        With theWavtoolArg
            .File1 = MainA.TempDir & "\temp.wav"
            .File2 = MainA.TempDir & "\" & i & ".wav"
            .BPM = xml.BPM
            .Length = xml.Section(i).Length
            .Pre = osp1.Preutterance
            .p1 = 0
            .p2Overlap = osp1.Overlap
            .p3nextOverlap = osp2.Overlap
            .v1 = 0
            .v2 = 0
            .v3 = 0
            .Overlap = osp1.Overlap
        End With
        Return theWavtoolArg
    End Function

    ''' <summary>
    ''' 给reArg赋值。
    ''' </summary>
    ''' <param name="xml">LSDF文件</param>
    ''' <returns></returns>
    Private Function NewResampleArg(xml As LSDFFileSystem, section As Short, phoneme As Short) As ResampleArgs
        Dim osp As Marks = GetOSPMark(section, phoneme)
        Dim theResampleArg As ResampleArgs
        With theResampleArg
            .Input = MainA.SingerDir & "\" & osp.Wav
            .Out = MainA.TempDir & "\" & i & ".wav"
            .PITMain = xml.Section(i).NoteString
            .Velocity = xml.Section(i).Velocity
            .Flags = xml.Section(i).Flags
            .Offset = osp.Offset
            .Length = xml.Section(i).Length
            .Con = osp.Consonant
            .EndBlank = osp.Cutoff
            .Volume = xml.Section(i).Intensity
            .Modulation = xml.Section(i).Modulation
            .PitchBend = xml.Section(i).PitchBend
        End With
        Return theResampleArg
    End Function
    Public Sub WriteCommandLine(steam As IO.StreamWriter, command As String, name As String, Value As String)
        steam.WriteLine("@" & command & " " & name & "=" & Value)
    End Sub
    Public Sub WriteBATLine(steam As IO.StreamWriter, command As String, Value As String)
        steam.WriteLine("@" & command & " " & Value)
    End Sub
    Public Sub WriteHead(Resampler As Resample, Wavtool As WavTool, WavToolArgs1 As WavToolArgs, MainArgs As MainArgs, steam As IO.StreamWriter)

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

    End Sub
    '先行发音和Overlap要写到LSDF里去 
    ''' <summary>
    ''' 添加一个休止符。
    ''' </summary>
    ''' <param name="Length">长度</param>
    ''' <param name="tempo">BPM</param>
    ''' <param name="pre">先行发音</param>
    ''' <param name="pre2">下一个音符的先行发音</param>
    ''' <param name="overlap2">下一个音符的重叠</param>
    Public Sub WriteARNote(steam As IO.StreamWriter, Length As Short, tempo As Short, pre As Short, pre2 As Short, overlap2 As Short, oto1 As Marks, oto2 As Marks)
        '输出示例：
        '@"%tool%" "%output%" "%oto%\R.wav" 0 480@120+0.0 0 0
        Dim fix As String
        fix = oto1.Preutterance - oto2.Preutterance + oto2.Overlap
        WriteBATLine(steam, """%tool%"" ""%output%"" ""%oto%\R.wav"" 0", Length & tempo & "+" & fix & " 0 0")

    End Sub

    Public Sub WriteANote(num As Short, ResamplerArgs1 As ResampleArgs, WavToolArgs1 As WavToolArgs, WTA2 As WavToolArgs, oto1 As Marks, oto2 As Marks, steam As IO.StreamWriter)
        WriteCommandLine(steam, "set", "params", ResamplerArgs1.Volume & " " & ResamplerArgs1.Modulation & " !" & WavToolArgs1.BPM & " " & ResamplerArgs1.PitchBend)
        WriteCommandLine(steam, "set", "flag", """" & ResamplerArgs1.Flags & """")
        WriteCommandLine(steam, "set", "env", WavToolArgs1.p1 & " " & WavToolArgs1.p2Overlap & " " & WavToolArgs1.p3nextOverlap & " 0 " & WavToolArgs1.v2 & " " & WavToolArgs1.v3 & " 0 " & WavToolArgs1.p2Overlap)
        WriteCommandLine(steam, "set", "vel", ResamplerArgs1.Velocity)
        WriteCommandLine(steam, "set", "temp", """%cachedir%\" & num & ".wav""")

        WriteBATLine(steam, "echo", "生成第" & num & "个")

        FIX = oto1.Preutterance - oto2.Preutterance + oto2.Overlap
        S = ResamplerArgs1.Length / 8 / WavToolArgs1.BPM + FIX

        WriteBATLine(steam, "call", "%helper% ""%oto%\" & ResamplerArgs1.Input & """ " & ResamplerArgs1.PITMain & " " & WavToolArgs1.Length & "@" & WavToolArgs1.BPM & "+" & FIX & " " & WavToolArgs1.Pre & " " & ResamplerArgs1.Offset & " " & S & " " & ResamplerArgs1.Con & " " & ResamplerArgs1.EndBlank & " " & num)

    End Sub

End Module
