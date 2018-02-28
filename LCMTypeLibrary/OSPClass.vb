Public Class OSPClass
    Structure Marks
        Dim Wav As String
        Dim Symbol As String
        Dim Offset As Double
        Dim Consonant As Double
        Dim Cutoff As Double
        Dim Preutterance As Double
        Dim Overlap As Double
    End Structure
    'Original Sound Positioning
    '原音定位
    Structure OSPFile
        Dim otos() As Marks
    End Structure
End Class
