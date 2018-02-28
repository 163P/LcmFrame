Imports LCMTypeLibrary.OSPClass
Public Class OSPClass
    ''' <summary>
    ''' 不知道为了干什么而开的读取lm的坑
    ''' </summary>
    ''' <param name="file"></param>
    ''' <returns></returns>
    Public Function ReadLMFile(filename As String) As OSPFile
        Dim result As New OSPFile

        Dim doc As New Xml.XmlDocument
        doc.Load(filename)

        'Dim i As Short
        'k = CInt(Replace(doc.SelectSingleNode("LSDFile").SelectSingleNode("Tracks").SelectSingleNode("Track1").SelectSingleNode("Notes").LastChild.Name, "N", ""))
        'ReDim Preserve ReadXml.Section(k)
        'Do
        '    ReadXml.Section(i) = ReadSection(doc, i)
        '    i = i + 1
        '    If i = k + 1 Then
        '        Exit Do
        '    End If
        'Loop

        Return result
    End Function

    Public Shared Function ReadAOSPSection(doc As Xml.XmlDocument, symbol As String) As Marks
        Return New Marks With {
        .Consonant = doc.SelectSingleNode("LMFile").SelectSingleNode(symbol).SelectSingleNode("Consonant").InnerText,
        .Wav = doc.SelectSingleNode("LMFile").SelectSingleNode(symbol).SelectSingleNode("Wav").InnerText,
        .Cutoff = doc.SelectSingleNode("LMFile").SelectSingleNode(symbol).SelectSingleNode("Cutoff").InnerText,
        .Preutterance = doc.SelectSingleNode("LMFile").SelectSingleNode(symbol).SelectSingleNode("Preutterance").InnerText,
        .Overlap = doc.SelectSingleNode("LMFile").SelectSingleNode(symbol).SelectSingleNode("Overlap").InnerText,
        .Offset = doc.SelectSingleNode("LMFile").SelectSingleNode(symbol).SelectSingleNode("Offset").InnerText
        }
    End Function
End Class
