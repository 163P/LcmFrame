Imports System.Runtime.CompilerServices
Imports System.Xml

<Extension()> Module ExtensionModule
    <Extension()>
    Public Sub AddEleAttr(ByVal src As XmlElement, doc As XmlDocument, name As String, value As String)

        Dim attr As XmlAttribute = doc.CreateAttribute(name)
        attr.Value = value
        src.Attributes.Append(attr)

    End Sub

    <Extension()>
    Public Sub AddChildNode(ByVal src As XmlElement, doc As XmlDocument, name As String, innertext As String)

        Dim elem As XmlElement = doc.CreateElement(name)
        elem.InnerText = innertext
        src.AppendChild(elem)

    End Sub
End Module
