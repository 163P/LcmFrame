Imports System.Xml
Public Class XMLReadClass
    ' 结构声明段
    Dim k As Short
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
        Dim symbol1 As String
        Dim symbol2 As String
        Dim Mt As Short
        Dim Mc As Short
        Dim MG As Short
        Dim Mo As Short
    End Structure

    Public Structure OTO
        Dim Wav As String
        Dim Symbol As String
        Dim Offset As Double
        Dim Consonant As Double
        Dim Cutoff As Double
        Dim Preutterance As Double
        Dim Overlap As Double
    End Structure
    Public Structure OTOFile
        Dim otos() As OTO
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
        ReadSection.symbol1 = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Phoneme1").InnerText
        ReadSection.symbol2 = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Phoneme2").InnerText
        ReadSection.Mt = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Mt").InnerText
        ReadSection.Mo = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Mo").InnerText
        ReadSection.Mc = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("Mc").InnerText
        ReadSection.MG = doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").SelectSingleNode("N" & i.ToString("0000")).SelectSingleNode("MG").InnerText

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

    Public Function GotoLSDF(UST As USTFileSystem, Filename As String) As Boolean
        Try
            Dim doc As New Xml.XmlDocument
            Dim dec As Xml.XmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "")
            doc.AppendChild(dec)

            Dim root As XmlElement = doc.CreateElement("LSDFile")
            doc.AppendChild(root)

            Dim xmlNode As XmlNode
            Dim xmlInfo As XmlNode
            Dim xmlTracks As XmlNode
            Dim xmlTrack1 As XmlNode
            Dim xmlNotes As XmlNode
            Dim xmlNote(UST.Section.Count) As XmlNode

            xmlInfo = doc.CreateNode(XmlNodeType.Element, "Info", "")
            xmlNode = doc.CreateElement("Version")
            xmlNode.InnerText = "0.1"
            xmlInfo.AppendChild(xmlNode)

            xmlNode = doc.CreateElement("Tracks")
            xmlNode.InnerText = "1"
            xmlInfo.AppendChild(xmlNode)

            xmlNode = doc.CreateElement("BPM")
            xmlNode.InnerText = UST.BPM.ToString
            xmlInfo.AppendChild(xmlNode)

            xmlTracks = doc.CreateNode(XmlNodeType.Element, "Tracks", "")
            xmlTrack1 = doc.CreateNode(XmlNodeType.Element, "Track1", "")

            xmlNode = doc.CreateElement("LibraryID")
            xmlNode.InnerText = "KDXC3EF4NSD5F2XPDSN"
            xmlTrack1.AppendChild(xmlNode)

            root.AppendChild(xmlInfo)

            xmlNotes = doc.CreateNode(XmlNodeType.Element, "Notes", "")

            Do
                Dim i As Short

                '创建音符序列号
                xmlNote(i) = doc.CreateNode(XmlNodeType.Element, "N" & i.ToString("0000"), "")

                '创建参数
                xmlNode = doc.CreateElement("Length")
                xmlNode.InnerText = UST.Section(i).Length.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("Lyric")
                xmlNode.InnerText = UST.Section(i).Lyric
                xmlNote(i).AppendChild(xmlNode)
                '音素在这里 代码已补充
                xmlNode = doc.CreateElement("Phoneme1")
                xmlNode.InnerText = UST.Section(i).symbol1
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("Phoneme2")
                xmlNode.InnerText = UST.Section(i).symbol2
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("NoteNum")
                xmlNode.InnerText = UST.Section(i).NoteString.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("Velocity")
                xmlNode.InnerText = UST.Section(i).Velocity.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("Flags")
                xmlNode.InnerText = UST.Section(i).Flags.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("PreUtterance")
                xmlNode.InnerText = UST.Section(i).PreUtterance.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("Overlap")
                xmlNode.InnerText = UST.Section(i).Overlap.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("Envelope")
                xmlNode.InnerText = UST.Section(i).Envelope.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("PBType")
                xmlNode.InnerText = UST.Section(i).PBType.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("PitchBend")
                xmlNode.InnerText = UST.Section(i).PitchBend.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("PBStart")
                xmlNode.InnerText = UST.Section(i).PBStart.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("VBR")
                xmlNode.InnerText = UST.Section(i).VBR.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("Intensity")
                xmlNode.InnerText = UST.Section(i).Intensity.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("Modulation")
                xmlNode.InnerText = UST.Section(i).Intensity.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("Mt")
                xmlNode.InnerText = UST.Section(i).Mt.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("Mo")
                xmlNode.InnerText = UST.Section(i).Mo.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("Mc")
                xmlNode.InnerText = UST.Section(i).Mc.ToString
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("MG")
                xmlNode.InnerText = UST.Section(i).MG.ToString
                xmlNote(i).AppendChild(xmlNode)


                xmlNotes.AppendChild(xmlNote(i))

                i = i + 1
                If i >= UST.Section.Count Then
                    Exit Do
                End If
            Loop

            xmlTrack1.AppendChild(xmlNotes)
            xmlTracks.AppendChild(xmlTrack1)
            root.AppendChild(xmlTracks)


            doc.Save(Filename)
            GotoLSDF = True
        Catch ex As Exception
            GotoLSDF = False
        End Try
    End Function

    Public Function ReadLMFile(file As String) As OTOFile

    End Function

    Public Function ReadALMSection(doc As Xml.XmlDocument, symbol As String) As OTO
        ReadALMSection.Consonant = doc.SelectSingleNode("LMFile").SelectSingleNode(symbol).SelectSingleNode("Consonant").InnerText
        ReadALMSection.Overlap = doc.SelectSingleNode("LMFile").SelectSingleNode(symbol).SelectSingleNode("Overlap").InnerText
        ReadALMSection.Wav = doc.SelectSingleNode("LMFile").SelectSingleNode(symbol).SelectSingleNode("Wav").InnerText
        ReadALMSection.Cutoff = doc.SelectSingleNode("LMFile").SelectSingleNode(symbol).SelectSingleNode("Cutoff").InnerText
        ReadALMSection.Preutterance = doc.SelectSingleNode("LMFile").SelectSingleNode(symbol).SelectSingleNode("Preutterance").InnerText
        ReadALMSection.Overlap = doc.SelectSingleNode("LMFile").SelectSingleNode(symbol).SelectSingleNode("Overlap").InnerText
        ReadALMSection.Offset = doc.SelectSingleNode("LMFile").SelectSingleNode(symbol).SelectSingleNode("Offset").InnerText
    End Function

End Class
