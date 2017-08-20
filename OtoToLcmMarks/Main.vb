Imports System.Xml
Module Main

    Sub Main()
        Dim Vowel As OTOFile
        Dim A As OTOFile
        Dim V As OTOFile
        Dim ALL As OTOFile
        Vowel = ReadOTOFile("D:\LCM-Baibai\Vowel\oto.ini", "Vowel")
        A = ReadOTOFile("D:\LCM-Baibai\A\oto.ini", "A")
        V = ReadOTOFile("D:\LCM-Baibai\V\oto.ini", "V")
        ALL.otos = A.otos.Concat(V.otos).ToArray
        ALL.otos = ALL.otos.Concat(Vowel.otos).ToArray

        Dim doc As New Xml.XmlDocument
        Dim dec As Xml.XmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "")
        doc.AppendChild(dec)

        Dim root As XmlElement = doc.CreateElement("LMFile")
        doc.AppendChild(root)

        Dim xmlNode As XmlNode
        Dim Wav As XmlNode
        Dim Offset As XmlNode
        Dim Consonant As XmlNode
        Dim Cutoff As XmlNode
        Dim Preutterance As XmlNode
        Dim Overlap As XmlNode

        For i = 0 To ALL.otos.Count - 1
            If ALL.otos(i).Symbol <> "" Then
                xmlNode = doc.CreateNode(XmlNodeType.Element, Replace(ALL.otos(i).Symbol, " ", "_"), "")

            Wav = doc.CreateElement("Wav")
            Wav.InnerText = ALL.otos(i).Wav
            xmlNode.AppendChild(Wav)

            Offset = doc.CreateElement("Offset")
            Offset.InnerText = ALL.otos(i).Offset
            xmlNode.AppendChild(Offset)

            Consonant = doc.CreateElement("Consonant")
            Consonant.InnerText = ALL.otos(i).Consonant
            xmlNode.AppendChild(Consonant)

            Cutoff = doc.CreateElement("Cutoff")
            Cutoff.InnerText = ALL.otos(i).Cutoff
            xmlNode.AppendChild(Cutoff)

            Preutterance = doc.CreateElement("Preutterance")
            Preutterance.InnerText = ALL.otos(i).Preutterance
            xmlNode.AppendChild(Preutterance)

            Overlap = doc.CreateElement("Overlap")
            Overlap.InnerText = ALL.otos(i).Overlap
            xmlNode.AppendChild(Overlap)

                root.AppendChild(xmlNode)
            End If
        Next

        doc.Save("d:\1.xml")
    End Sub

End Module
