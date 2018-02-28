Imports System.Xml
Imports LCMTypeLibrary.OSPClass
Module Main

    Sub Main()

        Dim SingerDir As String
        Console.WriteLine("Input SingerDir")
        Console.WriteLine("e.g. :D:\LCM_DEMO\")
        SingerDir = Console.ReadLine()

        Dim Vowel As ospfile
        Dim A As OSPFile
        'Dim V As ospfile
        Dim ALL As ospfile
        Dim AU As ospfile
        Dim CH_pack As ospfile
        Dim E As ospfile
        Dim EaT As ospfile
        Dim EN As ospfile
        Dim tI As ospfile '实为I
        Dim Ipp As ospfile
        Dim Ip As ospfile
        Dim Initial As ospfile
        Dim L As ospfile
        Dim Low As ospfile
        Dim NG As ospfile
        Dim SO As ospfile
        Dim Vowel2 As ospfile
        Dim Wen_Pack As ospfile
        Dim You_Pack As ospfile

        Vowel = ReadOTOFile(SingerDir & "Vowel\oto.ini", "Vowel")
        A = ReadOTOFile(SingerDir & "A\oto.ini", "A")
        AU = ReadOTOFile(SingerDir & "AU\oto.ini", "AU")
        CH_pack = ReadOTOFile(SingerDir & "CH_pack\oto.ini", "CH_pack")
        E = ReadOTOFile(SingerDir & "E\oto.ini", "E")
        EaT = ReadOTOFile(SingerDir & "ea\oto.ini", "EA")
        EN = ReadOTOFile(SingerDir & "EN\oto.ini", "EN")
        tI = ReadOTOFile(SingerDir & "I\oto.ini", "I")
        Ipp = ReadOTOFile(SingerDir & "i''\oto.ini", "i''")
        Ip = ReadOTOFile(SingerDir & "i'\oto.ini", "i'")
        Initial = ReadOTOFile(SingerDir & "Initial\oto.ini", "Initial")
        L = ReadOTOFile(SingerDir & "L\oto.ini", "L")
        Low = ReadOTOFile(SingerDir & "Low\oto.ini", "LOW")
        NG = ReadOTOFile(SingerDir & "NG\oto.ini", "NG")
        Vowel2 = ReadOTOFile(SingerDir & "Vowel2\oto.ini", "Vowel2")
        SO = ReadOTOFile(SingerDir & "SO\oto.ini", "SO")
        Wen_Pack = ReadOTOFile(SingerDir & "Wen_Pack\oto.ini", "Wen_Pack")
        You_Pack = ReadOTOFile(SingerDir & "You_Pack\oto.ini", "You_Pack")


        'V = ReadOTOFile("D:\LCM-Baibai\V\oto.ini", "V")

        ALL.otos = A.otos.Concat(Vowel.otos).ToArray
        ALL.otos = ALL.otos.Concat(AU.otos).ToArray
        ALL.otos = ALL.otos.Concat(E.otos).ToArray
        ALL.otos = ALL.otos.Concat(CH_pack.otos).ToArray
        ALL.otos = ALL.otos.Concat(EaT.otos).ToArray
        ALL.otos = ALL.otos.Concat(EN.otos).ToArray
        ALL.otos = ALL.otos.Concat(tI.otos).ToArray
        ALL.otos = ALL.otos.Concat(Ipp.otos).ToArray
        ALL.otos = ALL.otos.Concat(Ip.otos).ToArray
        ALL.otos = ALL.otos.Concat(Initial.otos).ToArray
        ALL.otos = ALL.otos.Concat(L.otos).ToArray
        ALL.otos = ALL.otos.Concat(Low.otos).ToArray
        ALL.otos = ALL.otos.Concat(NG.otos).ToArray
        ALL.otos = ALL.otos.Concat(Vowel2.otos).ToArray
        ALL.otos = ALL.otos.Concat(SO.otos).ToArray
        ALL.otos = ALL.otos.Concat(Wen_Pack.otos).ToArray
        ALL.otos = ALL.otos.Concat(You_Pack.otos).ToArray

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

        For I = 0 To ALL.otos.Count - 1
            If ALL.otos(I).Symbol <> "" Then
                xmlNode = doc.CreateNode(XmlNodeType.Element, Replace(ALL.otos(I).Symbol, " ", "_"), "")

                Wav = doc.CreateElement("Wav")
                Wav.InnerText = ALL.otos(I).Wav
                xmlNode.AppendChild(Wav)

                Offset = doc.CreateElement("Offset")
                Offset.InnerText = ALL.otos(I).Offset
                xmlNode.AppendChild(Offset)

                Consonant = doc.CreateElement("Consonant")
                Consonant.InnerText = ALL.otos(I).Consonant
                xmlNode.AppendChild(Consonant)

                Cutoff = doc.CreateElement("Cutoff")
                Cutoff.InnerText = ALL.otos(I).Cutoff
                xmlNode.AppendChild(Cutoff)

                Preutterance = doc.CreateElement("Preutterance")
                Preutterance.InnerText = ALL.otos(I).Preutterance
                xmlNode.AppendChild(Preutterance)

                Overlap = doc.CreateElement("Overlap")
                Overlap.InnerText = ALL.otos(I).Overlap
                xmlNode.AppendChild(Overlap)

                root.AppendChild(xmlNode)
            End If
        Next

        doc.Save("d:\temp\1.xml")
    End Sub

End Module
