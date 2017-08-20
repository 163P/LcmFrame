Imports IniLib
Imports XMLClass.XmlHelper
Imports System.Xml
Imports System.Runtime.CompilerServices
Public Class ToLSDF

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

    Public Function ReadASection(UST As IniFile, Section As Short) As Section


        ReadASection.Length = GetINI("#" & Section.ToString("0000"), "Length", "480", UST)
        ReadASection.Lyric = GetINI("#" & Section.ToString("0000"), "Lyric", "a", UST)
        ReadASection.NoteString = GetINI("#" & Section.ToString("0000"), "NoteNum", "999", UST)
        ReadASection.Velocity = GetINI("#" & Section.ToString("0000"), "Velocity", "100", UST)
        ReadASection.Flags = GetINI("#" & Section.ToString("0000"), "Flags", "", UST)
        ReadASection.PreUtterance = GetINI("#" & Section.ToString("0000"), "PreUtterance", "0", UST)
        ReadASection.Overlap = GetINI("#" & Section.ToString("0000"), "Overlap", "0", UST)
        ReadASection.Envelope = GetINI("#" & Section.ToString("0000"), "Envelope", "", UST)
        ReadASection.PBType = GetINI("#" & Section.ToString("0000"), "PBType", "5", UST)
        ReadASection.PitchBend = GetINI("#" & Section.ToString("0000"), "PitchBend", "", UST)
        ReadASection.PBStart = GetINI("#" & Section.ToString("0000"), "PBStart", "0", UST)
        ReadASection.VBR = GetINI("#" & Section.ToString("0000"), "VBR", "", UST)
        ReadASection.Intensity = GetINI("#" & Section.ToString("0000"), "Intensity", "100", UST)
        ReadASection.Modulation = GetINI("#" & Section.ToString("0000"), "Modulation", "0", UST)
    End Function

    Public Function GetINI(Section As String, Parameter As String, DefaultValue As String, ini As IniFile)

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
    Public Function ReadAUst(Filename As String) As USTFileSystem
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
