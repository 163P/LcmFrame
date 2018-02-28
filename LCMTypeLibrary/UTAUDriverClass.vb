Public Class UTAUDriverClass
    Structure Resample
        Dim Filename As String
        Dim Name As String
    End Structure
    Structure MainArgs
        Dim TempDir As String
        Dim SingerDir As String
    End Structure
    Structure WavTool
        Dim Filename As String
        Dim Name As String
    End Structure
    Structure ResampleArgs
        Dim Input As String
        Dim Out As String
        Dim PITMain As String
        Dim Velocity As Short
        Dim Flags As String
        Dim Offset As Double
        Dim Length As Short
        Dim Con As Double
        Dim EndBlank As Double
        Dim Volume As Short
        Dim Modulation As Short
        Dim PitchBend As String
    End Structure
    Structure WavToolArgs
        Dim File1 As String
        Dim File2 As String
        Dim BPM As Short
        Dim Length As Short
        Dim Pre As Double
        Dim p1 As Short '0
        Dim p2Overlap As Short
        Dim p3nextOverlap As Short
        Dim v1 As Short '0
        Dim v2 As Short '100
        Dim v3 As Short '100
        Dim Overlap As Double
    End Structure
End Class
