Imports IniLib
Imports LCMTypeLibrary.StructureClass
Imports FileOperationLibrary.LSDFClass
Imports FileOperationLibrary.USTClass
Public Class ToUST
    Dim k As Short
    Dim Theini As New IniLib.IniFile

    Public Function GotoUst(filename As String, out As String) As Boolean


        Dim UstINI As New IniFile

        Dim ust As LSDFFileSystem
        ust = ReadXml(filename)
            WriteINI("#SETTING", "Tempo", ust.BPM, UstINI)
            WriteINI("#SETTING", "Tracks", "1", UstINI)
            Dim i As Short = 0
            Do
                WriteINI("#" & i.ToString("0000"), "Length", ust.Section(i).Length, UstINI)
                WriteINI("#" & i.ToString("0000"), "Lyric", ust.Section(i).Lyric, UstINI)
                WriteINI("#" & i.ToString("0000"), "NoteNum", ust.Section(i).NoteString, UstINI)
                WriteINI("#" & i.ToString("0000"), "Velocity", ust.Section(i).Velocity, UstINI)
                WriteINI("#" & i.ToString("0000"), "Flags", ust.Section(i).Flags, UstINI)
                WriteINI("#" & i.ToString("0000"), "PreUtterance", ust.Section(i).PreUtterance, UstINI)
                WriteINI("#" & i.ToString("0000"), "Overlap", ust.Section(i).Overlap, UstINI)
                WriteINI("#" & i.ToString("0000"), "Envelope", ust.Section(i).Envelope, UstINI)
                WriteINI("#" & i.ToString("0000"), "PBType", ust.Section(i).PBType, UstINI)
                WriteINI("#" & i.ToString("0000"), "PitchBend", ust.Section(i).PitchBend, UstINI)
                WriteINI("#" & i.ToString("0000"), "PBStart", ust.Section(i).PBStart, UstINI)
                WriteINI("#" & i.ToString("0000"), "VBR", ust.Section(i).VBR, UstINI)
                WriteINI("#" & i.ToString("0000"), "Intensity", ust.Section(i).Intensity, UstINI)
                WriteINI("#" & i.ToString("0000"), "Modulation", ust.Section(i).Modulation, UstINI)
                i = i + 1

                If i = k + 1 Then
                    Exit Do
                End If

            Loop
            WriteINI("[#TRACKEND]", "", "", UstINI)

            Task.WaitAll(WriteParser.WriteFileAsync(UstINI, out, True))

            GotoUst = True

    End Function
End Class
