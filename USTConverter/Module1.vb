Imports IniLib
Imports LCMTypeLibrary.StructureClass
Imports FileOperationLibrary.USTClass
Module Module1


    Sub Main()
        Dim UST As LSDFFileSystem = ReadAUst(Replace(Command, """", ""))
        '构建UST文件系统
        Dim a As New IO.FileInfo(Replace(Command, """", ""))
        Dim dic As New LcmDictionary.MainClass '构建词典组件
        Dim USTtoW As New IniFile
        Dim NumtoW As Short = 0 '待写入UST的音符序号
        WriteINI("#SETTING", "Tempo", UST.BPM, USTtoW)
        WriteINI("#SETTING", "Tracks", "1", USTtoW) '头info
        For i = 0 To UST.Section.Length - 1
            If UST.Section(i).Length < 241 Or dic.GetSymbol("R", UST.Section(i).Lyric).Symbol2 = "_eng" Or dic.GetSymbol("R", UST.Section(i).Lyric).Symbol2 = "_ao" Or dic.GetSymbol("R", UST.Section(i).Lyric).Symbol2 = "_ang" Or dic.GetSymbol("R", UST.Section(i).Lyric).Symbol2 = "_ei" Or dic.GetSymbol("R", UST.Section(i).Lyric).Symbol2 = "Null" Or dic.GetSymbol("R", UST.Section(i).Lyric).Symbol2 = "_a" Or dic.GetSymbol("R", UST.Section(i).Lyric).Symbol2 = "_e" Or dic.GetSymbol("R", UST.Section(i).Lyric).Symbol2 = "_o" Or dic.GetSymbol("R", UST.Section(i).Lyric).Symbol2 = "_i" Or dic.GetSymbol("R", UST.Section(i).Lyric).Symbol2 = "_u" Or dic.GetSymbol("R", UST.Section(i).Lyric).Symbol2 = "_v" Or dic.GetSymbol("R", UST.Section(i).Lyric).Symbol2 = "_ir" Or dic.GetSymbol("R", UST.Section(i).Lyric).Symbol2 = "_iz" Then
                '音符长度小于240或为特殊发音符号，此时省略韵母部
                If i = 0 Then
                    '当为第一个音时，补全休止符
                    WriteINI("#" & NumtoW.ToString("0000"), "Length", UST.Section(i).Length, USTtoW)


                    WriteINI("#" & NumtoW.ToString("0000"), "Lyric", dic.GetSymbol("R", UST.Section(i).Lyric).Symbol1, USTtoW)

                    WriteINI("#" & NumtoW.ToString("0000"), "NoteNum", UST.Section(i).NoteString, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "Velocity", UST.Section(i).Velocity, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "Flags", UST.Section(i).Flags, USTtoW)
                    'WriteINI("#" & NumtoW.ToString("0000"), "PreUtterance", UST.Section(i).PreUtterance, USTtoW)
                    'WriteINI("#" & NumtoW.ToString("0000"), "Overlap", UST.Section(i).Overlap, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "Envelope", UST.Section(i).Envelope, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "PBType", UST.Section(i).PBType, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "PitchBend", UST.Section(i).PitchBend, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "PBStart", UST.Section(i).PBStart, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "VBR", UST.Section(i).VBR, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "Intensity", UST.Section(i).Intensity, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "Modulation", UST.Section(i).Modulation, USTtoW)
                    NumtoW = NumtoW + 1
                Else
                    WriteINI("#" & NumtoW.ToString("0000"), "Length", UST.Section(i).Length, USTtoW)



                    WriteINI("#" & NumtoW.ToString("0000"), "Lyric", dic.GetSymbol(UST.Section(i - 1).Lyric, UST.Section(i).Lyric).Symbol1, USTtoW)


                    WriteINI("#" & NumtoW.ToString("0000"), "NoteNum", UST.Section(i).NoteString, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "Velocity", UST.Section(i).Velocity, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "Flags", UST.Section(i).Flags, USTtoW)
                    ' WriteINI("#" & NumtoW.ToString("0000"), "PreUtterance", UST.Section(i).PreUtterance, USTtoW)
                    ' WriteINI("#" & NumtoW.ToString("0000"), "Overlap", UST.Section(i).Overlap, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "Envelope", UST.Section(i).Envelope, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "PBType", UST.Section(i).PBType, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "PitchBend", UST.Section(i).PitchBend, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "PBStart", UST.Section(i).PBStart, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "VBR", UST.Section(i).VBR, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "Intensity", UST.Section(i).Intensity, USTtoW)
                    WriteINI("#" & NumtoW.ToString("0000"), "Modulation", UST.Section(i).Modulation, USTtoW)
                    NumtoW = NumtoW + 1
                End If
            Else '正常拆音

                '第一个

                WriteINI("#" & NumtoW.ToString("0000"), "Length", 120, USTtoW)
                If i <> 0 Then

                    WriteINI("#" & NumtoW.ToString("0000"), "Lyric", dic.GetSymbol(UST.Section(i - 1).Lyric, UST.Section(i).Lyric).Symbol1, USTtoW)
                Else

                    WriteINI("#" & NumtoW.ToString("0000"), "Lyric", dic.GetSymbol("R", UST.Section(i).Lyric).Symbol1, USTtoW)

                End If

                WriteINI("#" & NumtoW.ToString("0000"), "NoteNum", UST.Section(i).NoteString, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "Velocity", UST.Section(i).Velocity, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "Flags", UST.Section(i).Flags, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "PreUtterance", UST.Section(i).PreUtterance, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "Overlap", UST.Section(i).Overlap, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "Envelope", UST.Section(i).Envelope, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "PBType", UST.Section(i).PBType, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "PitchBend", PITDivide(UST.Section(i).PitchBend, UST.Section(i).Length, 1), USTtoW)
                'pit拆分
                WriteINI("#" & NumtoW.ToString("0000"), "PBStart", UST.Section(i).PBStart, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "VBR", UST.Section(i).VBR, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "Intensity", UST.Section(i).Intensity, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "Modulation", UST.Section(i).Modulation, USTtoW)
                NumtoW = NumtoW + 1
                '第二个

                WriteINI("#" & NumtoW.ToString("0000"), "Length", UST.Section(i).Length - 120, USTtoW)
                If i <> 0 Then
                    WriteINI("#" & NumtoW.ToString("0000"), "Lyric", dic.GetSymbol(UST.Section(i - 1).Lyric, UST.Section(i).Lyric).Symbol2, USTtoW)
                Else
                    WriteINI("#" & NumtoW.ToString("0000"), "Lyric", dic.GetSymbol("R", UST.Section(i).Lyric).Symbol2, USTtoW)
                End If
                WriteINI("#" & NumtoW.ToString("0000"), "NoteNum", UST.Section(i).NoteString, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "Velocity", UST.Section(i).Velocity, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "Flags", UST.Section(i).Flags, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "PreUtterance", UST.Section(i).PreUtterance, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "Overlap", UST.Section(i).Overlap, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "Envelope", UST.Section(i).Envelope, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "PBType", UST.Section(i).PBType, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "PitchBend", PITDivide(UST.Section(i).PitchBend, UST.Section(i).Length, 2), USTtoW)
                'pit拆分
                WriteINI("#" & NumtoW.ToString("0000"), "PBStart", UST.Section(i).PBStart, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "VBR", UST.Section(i).VBR, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "Intensity", UST.Section(i).Intensity, USTtoW)
                WriteINI("#" & NumtoW.ToString("0000"), "Modulation", UST.Section(i).Modulation, USTtoW)
                NumtoW = NumtoW + 1
            End If

        Next
        WriteINI("[#TRACKEND]", "", "", USTtoW)

        Task.WaitAll(WriteParser.WriteFileAsync(USTtoW, System.IO.Path.GetFullPath(a.FullName) & "_LCM.ust", True))
    End Sub
    Private Function PITDivide(pit As String, length As Long, part As Long) As String

        Dim b() As String
        b = Split(pit, ",")
        Dim out As String = ""
        If part = 1 Then
            For i = 0 To b.Length * 120 \ length
                out = out & b(i) & ","
            Next
        Else
            For i = b.Length * 120 \ length To b.Length - 1
                out = out & b(i) & ","
            Next
        End If
        Return out
    End Function
End Module
