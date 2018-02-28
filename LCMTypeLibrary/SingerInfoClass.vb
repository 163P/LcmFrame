Public Class SingerInfoClass
    Structure SingerLibrary
        Dim VoiceVersion As Integer
        Dim Name_CHN As String
        Dim Name_ENG As String
        Dim BaseSoftware As SSoftware
        Dim CV As String
        Dim OtoVersion As Integer
        Dim Publisher As String
        Dim Type As SingerType
    End Structure

    Enum SingerType
        Standard
    End Enum

    Enum SSoftware
        UTAU
        LCM_Native
        Sharpkey
        MUTA
        Others
    End Enum
End Class
