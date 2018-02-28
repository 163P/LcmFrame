Public Class ConstClass
    Public Shared ReadOnly VowelA As String() = {"R", "a", "o", "e", "i", "u", "v"}
    Public Shared ReadOnly VowelB As String() = {"ang", "ong", "ing", "eng", "an", "ai", "ui", "ao", "ou", "iu", "ie", "ve", "er", "en", "in", "un", "vn", "ei"}
    Public Shared ReadOnly VowelC As String() = {"iao", "ian", "iang", "uai", "uan", "uang", "van", "iong", "ia", "ua", "uo"}

    ''' <summary>
    ''' VowelEndingA
    ''' </summary>
    Public Shared ReadOnly VE_l As String() = {"ai", "ei", "ui"}
    Public Shared ReadOnly VE_au As String() = {"ao", "ou", "iu"}
    Public Shared ReadOnly VE_e_at As String() = {"ie", "ve"}
    Public Shared ReadOnly VE_n As String() = {"an", "en", "in", "un", "vn"}
    Public Shared ReadOnly VE_ng As String() = {"ang", "eng", "ing", "ong"}
    Public Shared ReadOnly VE_iz As String() = {"zi", "ci", "si"}
    Public Shared ReadOnly VE_ir As String() = {"zhi", "chi", "shi"}
    Public Shared ReadOnly VE_r As String() = {"er"}




    Public Shared ReadOnly SyllablesWhole1 As String() = {"zhi", "chi", "shi"} '整体音节1，以ir结尾
    Public Shared ReadOnly SyllablesWhole2 As String() = {"zi", "ci", "si"} '整体音节2，以iz结尾
    Public Shared ReadOnly SyllablesWhole3 As String() = {"er"} '整体音节3，以r结尾
    Public Shared ReadOnly SyllablesWhole4 As String() = {"R"} '整体音节4，R
    Public Shared ReadOnly VowelDigit1 As String() = {"a", "o", "e", "i", "u", "v"} '一位韵母
    Public Shared ReadOnly VowelDigit2 As String() = {"an", "ai", "ui", "ao", "ou", "iu", "ie", "ve", "er", "en", "in", "un", "vn", "ei"， "ia", "ua", "uo"}
    '二位韵母
    Public Shared ReadOnly VowelDigit3 As String() = {"iao", "ian", "uai", "uan", "ang", "ong", "ing", "eng", "van"}  '三位韵母
    Public Shared ReadOnly VowelDigit4 As String() = {"iang", "iong", "uang"}  '四位韵母

End Class
