Imports System.Xml
Imports LCMTypeLibrary.StructureClass
Public Class ToLSDF

    Public Function GotoLSDF(UST As LSDFFileSystem, Filename As String) As Boolean
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
            Dim LD As New LcmDictionary.MainClass

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
                If i = 0 Then
                    xmlNode.InnerText = LD.GetSymbol("R", UST.Section(i).Lyric).Symbol1 & "|60"
                Else
                    xmlNode.InnerText = LD.GetSymbol(UST.Section(i - 1).Lyric, UST.Section(i).Lyric).Symbol1 & "|60"
                End If

                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("Phoneme2")
                If i = 0 Then
                    xmlNode.InnerText = LD.GetSymbol("R", UST.Section(i).Lyric).Symbol2
                Else
                    xmlNode.InnerText = LD.GetSymbol(UST.Section(i - 1).Lyric, UST.Section(i).Lyric).Symbol2
                End If
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
                xmlNode.InnerText = "0"
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("Mo")
                xmlNode.InnerText = "0"
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("Mc")
                xmlNode.InnerText = "0"
                xmlNote(i).AppendChild(xmlNode)

                xmlNode = doc.CreateElement("MG")
                xmlNode.InnerText = "0"
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

End Class
