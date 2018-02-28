Public Class StructureClass
    ' 结构声明段
    ''' <summary>
    ''' UST文件整体结构
    ''' </summary>
    Public Structure LSDFFileSystem
        Dim BPM As Short
        Dim Section() As Section
    End Structure
    ''' <summary>
    ''' 单个音符结构
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
        Dim PhonemeNum As Short
        Dim Phoneme1 As String
        Dim Phoneme2 As String
        Dim Phoneme3 As String
        Dim Mt As Short
        Dim Mo As Short
        Dim Mc As Short
        Dim Mg As Short
    End Structure
End Class
