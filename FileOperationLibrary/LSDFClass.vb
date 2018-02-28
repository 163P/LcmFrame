Imports LCMTypeLibrary.StructureClass
Imports IniLib
Public Class LSDFClass
    ' 结构声明段
    Shared k As Short
    Public Shared Function ReadSection(doc As Xml.XmlDocument, i As Short) As LCMTypeLibrary.StructureClass.Section
        Dim asoidhasiopfhasdiofhasdiof = New LCMTypeLibrary.StructureClass.Section


        asoidhasiopfhasdiofhasdiof.Length = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Length").InnerText
        asoidhasiopfhasdiofhasdiof.Lyric = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Lyric").InnerText
        asoidhasiopfhasdiofhasdiof.NoteString = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("NoteNum").InnerText
        asoidhasiopfhasdiofhasdiof.Velocity = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Velocity").InnerText
        asoidhasiopfhasdiofhasdiof.Flags = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Flags").InnerText
        asoidhasiopfhasdiofhasdiof.PreUtterance = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("PreUtterance").InnerText
        asoidhasiopfhasdiofhasdiof.Overlap = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Overlap").InnerText
        asoidhasiopfhasdiofhasdiof.Envelope = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Envelope").InnerText
        asoidhasiopfhasdiofhasdiof.PBType = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("PBType").InnerText
        asoidhasiopfhasdiofhasdiof.PitchBend = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("PitchBend").InnerText
        asoidhasiopfhasdiofhasdiof.PBStart = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("PBStart").InnerText
        asoidhasiopfhasdiofhasdiof.VBR = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("VBR").InnerText
        asoidhasiopfhasdiofhasdiof.Intensity = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Intensity").InnerText
        asoidhasiopfhasdiofhasdiof.Modulation = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Modulation").InnerText
        asoidhasiopfhasdiofhasdiof.Phoneme1 = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Phoneme1").InnerText
        asoidhasiopfhasdiofhasdiof.Phoneme2 = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Phoneme2").InnerText
        asoidhasiopfhasdiofhasdiof.Mt = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Mt").InnerText
        asoidhasiopfhasdiofhasdiof.Mo = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Mo").InnerText
        asoidhasiopfhasdiofhasdiof.Mc = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Mc").InnerText
        asoidhasiopfhasdiofhasdiof.Mg = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("MG").InnerText

        Return asoidhasiopfhasdiofhasdiof
    End Function

    Public Shared Function ReadXml(filename As String) As LSDFFileSystem

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
End Class
