Imports LCMTypeLibrary.StructureClass
Imports IniLib
Public Class USTClass
    Public Shared Function ReadASection(UST As IniFile, Section As Short) As LCMTypeLibrary.StructureClass.Section

        Dim result = New LCMTypeLibrary.StructureClass.Section

        result.Length = GetINI("#" & Section.ToString("0000"), "Length", "480", UST)
        result.Lyric = GetINI("#" & Section.ToString("0000"), "Lyric", "a", UST)
        result.NoteString = GetINI("#" & Section.ToString("0000"), "NoteNum", "999", UST)
        result.Velocity = GetINI("#" & Section.ToString("0000"), "Velocity", "100", UST)
        result.Flags = GetINI("#" & Section.ToString("0000"), "Flags", "", UST)
        result.PreUtterance = GetINI("#" & Section.ToString("0000"), "PreUtterance", "0", UST)
        result.Overlap = GetINI("#" & Section.ToString("0000"), "Overlap", "0", UST)
        result.Envelope = GetINI("#" & Section.ToString("0000"), "Envelope", "", UST)
        result.PBType = GetINI("#" & Section.ToString("0000"), "PBType", "5", UST)
        result.PitchBend = GetINI("#" & Section.ToString("0000"), "PitchBend", "", UST)
        result.PBStart = GetINI("#" & Section.ToString("0000"), "PBStart", "0", UST)
        result.VBR = GetINI("#" & Section.ToString("0000"), "VBR", "", UST)
        result.Intensity = GetINI("#" & Section.ToString("0000"), "Intensity", "100", UST)
        result.Modulation = GetINI("#" & Section.ToString("0000"), "Modulation", "0", UST)

        Return result
    End Function
    Public Shared Function GetINI(Section As String, Parameter As String, DefaultValue As String, ini As IniFile)

        Dim s = ini(Section)(Parameter)
        If (String.IsNullOrEmpty(s)) Then
            Return DefaultValue
        End If
        Return s
    End Function
    ''' <summary>
    ''' 读取整个UST，返回一个USTFileSystem
    ''' </summary>
    ''' <param name="Filename">UST路径</param>
    ''' <returns></returns>
    Public Shared Function ReadAUst(Filename As String) As LSDFFileSystem
        Dim UST = IniLib.ReadParser.ReadFileAsync(Filename).Result

        ReadAUst.BPM = GetINI("#SETTING", "Tempo", "120", UST)
        Dim i As Short = 1
        ReDim ReadAUst.Section(1)
        Do
            If UST.Count - 1 = i Then

                ReDim Preserve ReadAUst.Section(i - 2)
                Exit Do
            End If
            ReadAUst.Section(i - 1) = ReadASection(UST, i - 1)
            ReDim Preserve ReadAUst.Section(i)
            i = i + 1
        Loop
    End Function
    Public Shared Sub WriteINI(Section As String, Parameter As String, Value As String, ByRef ini As IniFile)

        ini.Add(Section)
        If Value = "" And Parameter = "" Then
            Exit Sub
        Else
            ini(Section).Add(Parameter, Value)
        End If
    End Sub
End Class
