Imports IniLib
Public Class ToUST
    Dim k As Short
    Dim Theini As New IniLib.IniFile
    ' 结构声明段
    ''' <summary>
    ''' UST文件整体结构
    ''' </summary>
    Public Structure USTFileSystem
        Dim BPM As Short
        Dim Section() As Section
    End Structure
    ''' <summary>
    ''' UST单个音符结构
    ''' </summary>
    Public Structure Section
        Dim Length As String
        Dim Lyric As String
        ''' <summary>
        ''' NoteNum.
        ''' </summary>
        Dim NoteString As String
        Dim Velocity As String
        Dim Flags As String
        Dim PreUtterance As String
        Dim Overlap As Single
        Dim Envelope As String
        Dim PBType As String
        Dim PitchBend As String
        Dim PBStart As String
        Dim VBR As String
        Dim Intensity As String
        Dim Modulation As String
        Public Structure Phonemes
            Dim num As Short
            Dim Phoneme1 As String
            Dim Phoneme2 As String
            Dim Phoneme3 As String
        End Structure
    End Structure

    Public Function ReadSection(doc As Xml.XmlDocument, i As Short) As Section

        ReadSection.Length = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Length").InnerText
        ReadSection.Lyric = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Lyric").InnerText
        ReadSection.NoteString = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("NoteNum").InnerText
        ReadSection.Velocity = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Velocity").InnerText
        ReadSection.Flags = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Flags").InnerText
        ReadSection.PreUtterance = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("PreUtterance").InnerText
        ReadSection.Overlap = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Overlap").InnerText
        ReadSection.Envelope = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Envelope").InnerText
        ReadSection.PBType = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("PBType").InnerText
        ReadSection.PitchBend = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("PitchBend").InnerText
        ReadSection.PBStart = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("PBStart").InnerText
        ReadSection.VBR = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("VBR").InnerText
        ReadSection.Intensity = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Intensity").InnerText
        ReadSection.Modulation = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Modulation").InnerText

    End Function

    Public Function ReadXml(filename As String) As USTFileSystem

        Dim doc As New Xml.XmlDocument
        doc.Load(filename)

        ReadXml.BPM = doc.SelectSingleNode("LSDFile").SelectSingleNode("Info").SelectSingleNode("BPM").InnerText

        Dim i As Short
        k = CInt(Replace(doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").LastChild.Name, "N", ""))
        ReDim Preserve ReadXml.Section(k)
        Do
            ReadXml.Section(i) = ReadSection(doc, i)
            i = i + 1
            If i = k + 1 Then
                Exit Do
            End If
        Loop
    End Function

    Public Sub WriteINI(Section As String, Parameter As String, Value As String, ByRef ini As IniFile)

        ini.Add(Section)
        If Value = "" And Parameter = "" Then
            Exit Sub
        Else
            ini(Section).Add(Parameter, Value)
        End If
    End Sub

    Public Function GotoUst(filename As String, out As String) As Boolean


        Dim UstINI As New IniFile

            Dim ust As USTFileSystem
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
