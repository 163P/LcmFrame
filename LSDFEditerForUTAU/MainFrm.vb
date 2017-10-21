Public Class MainFrm
    Dim k As New XMLReadClass
    Dim ust As XMLReadClass.USTFileSystem
    Dim utau As New UTAUDriver
    Dim NowArg2 As UTAUDriver.ResampleArgs
    Dim NowArg1 As UTAUDriver.ResampleArgs
    Dim WavToolArgs1 As UTAUDriver.WavToolArgs
    Dim WavToolArgs2 As UTAUDriver.WavToolArgs
    Dim OTO1 As XMLReadClass.OTO
    Dim OTO2 As XMLReadClass.OTO
    Dim MainArgs As UTAUDriver.MainArgs
    Dim bat As New batClass
    Dim doc As New Xml.XmlDocument
    Private Sub TextBox8_LostFocus(sender As Object, e As EventArgs) Handles TextBox8.LostFocus
        TextBox8.ReadOnly = True
    End Sub

    Private Sub TextBox8_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TextBox8.MouseDoubleClick
        TextBox8.ReadOnly = False
        TextBox8.SelectAll()
    End Sub

    Private Sub TextBox7_LostFocus(sender As Object, e As EventArgs) Handles TextBox7.LostFocus
        TextBox7.ReadOnly = True
    End Sub

    Private Sub TextBox7_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TextBox7.MouseDoubleClick
        TextBox7.ReadOnly = False
        TextBox7.SelectAll()
    End Sub
    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Panel1.Width = 580 * (TrackBar1.Value / TrackBar1.Maximum)
        Panel2.Left = 580 * (TrackBar1.Value / TrackBar1.Maximum) + 3
        TextBox1.Text = TrackBar1.Value
    End Sub

    Private Sub TrackBar2_Scroll(sender As Object, e As EventArgs) Handles TrackBar2.Scroll
        TextBox2.Text = TrackBar2.Value
    End Sub

    Private Sub TrackBar3_Scroll(sender As Object, e As EventArgs) Handles TrackBar3.Scroll
        TextBox3.Text = TrackBar3.Value
    End Sub

    Private Sub TrackBar4_Scroll(sender As Object, e As EventArgs) Handles TrackBar4.Scroll
        TextBox4.Text = TrackBar4.Value
    End Sub

    Private Sub TrackBar5_Scroll(sender As Object, e As EventArgs) Handles TrackBar5.Scroll
        TextBox5.Text = TrackBar5.Value
    End Sub

    Private Sub MainFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '初始化主参数
        MainArgs.SingerDir = "D:\LCM-Baibai\"
        MainArgs.TempDir = "d:\"
        '初始化LSDF文件
        OFD.ShowDialog()
        ust = k.ReadXml(OFD.FileName)

        For i = 0 To ust.Section.Count - 1
            ListBox1.Items.Add(ust.Section(i).Lyric)
        Next
        '初始化LM文件
        doc.Load(MainArgs.SingerDir & "\Marks.lm")

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        TextBox7.Text = Mid(ust.Section(ListBox1.SelectedIndex).symbol1, 1, InStr(ust.Section(ListBox1.SelectedIndex).symbol1, "|") - 1)
        TextBox8.Text = ust.Section(ListBox1.SelectedIndex).symbol2

        TrackBar1.Maximum = ust.Section(ListBox1.SelectedIndex).Length
        TrackBar1.Value = Replace(ust.Section(ListBox1.SelectedIndex).symbol1, Mid(ust.Section(ListBox1.SelectedIndex).symbol1, 1, InStr(ust.Section(ListBox1.SelectedIndex).symbol1, "|")), "")
        Panel1.Width = 580 * (TrackBar1.Value / TrackBar1.Maximum)
        Panel2.Left = 580 * (TrackBar1.Value / TrackBar1.Maximum) + 3
        TextBox1.Text = TrackBar1.Value

        TrackBar2.Value = ust.Section(ListBox1.SelectedIndex).Mt
        TrackBar3.Value = ust.Section(ListBox1.SelectedIndex).Mo
        TrackBar4.Value = ust.Section(ListBox1.SelectedIndex).Mc
        TrackBar5.Value = ust.Section(ListBox1.SelectedIndex).MG
        TextBox2.Text = TrackBar2.Value
        TextBox3.Text = TrackBar3.Value
        TextBox4.Text = TrackBar4.Value
        TextBox5.Text = TrackBar5.Value
        TextBox6.Text = ust.Section(ListBox1.SelectedIndex).Flags
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ust.Section(ListBox1.SelectedIndex).symbol1 = TextBox7.Text & "|" & TrackBar1.Value
        ust.Section(ListBox1.SelectedIndex).Mt = TrackBar2.Value
        ust.Section(ListBox1.SelectedIndex).Mo = TrackBar3.Value
        ust.Section(ListBox1.SelectedIndex).MG = TrackBar5.Value
        ust.Section(ListBox1.SelectedIndex).Mc = TrackBar4.Value
        ust.Section(ListBox1.SelectedIndex).symbol2 = TextBox8.Text
        ust.Section(ListBox1.SelectedIndex).Flags = TextBox6.Text
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SFD.ShowDialog()
        k.GotoLSDF(ust, SFD.FileName)
        MsgBox("保存完毕。")
    End Sub

    Private Sub MainFrm_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        ListBox1.SelectedIndex = ListBox1.Items.Count - 1
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OTO1 = k.ReadALMSection(doc, Replace(TextBox7.Text, " ", "_"))
        OTO2 = k.ReadALMSection(doc, Replace(TextBox8.Text, " ", "_"))

        NowArg1.Length = ((TextBox1.Text / 8 / ust.BPM) * 1000) + OTO1.Preutterance - OTO2.Preutterance + OTO2.Overlap
        If NowArg1.Length Mod 100 > 50 Then
            NowArg1.Length = NowArg1.Length - NowArg1.Length Mod 100 + 100
        Else
            NowArg1.Length = NowArg1.Length - NowArg1.Length Mod 100
        End If
        'del MainArgs.SingerDir &
        NowArg1.Input = OTO1.Wav
        NowArg1.Out = MainArgs.TempDir & "1.wav"
        NowArg1.Modulation = ust.Section(ListBox1.SelectedIndex).Modulation
        NowArg1.Offset = OTO1.Offset
        NowArg1.PitchBend = "!" & ust.BPM & " AA#83#"
        NowArg1.PITMain = "C4"
        NowArg1.Velocity = ust.Section(ListBox1.SelectedIndex).Velocity
        NowArg1.Volume = ust.Section(ListBox1.SelectedIndex).Intensity
        NowArg1.Con = OTO1.Consonant
        NowArg1.EndBlank = OTO1.Cutoff
        NowArg1.Flags = "Mt" & TextBox2.Text & "Mo" & TextBox3.Text & "Mc" & TextBox4.Text & "MG" & TextBox5.Text & TextBox6.Text

        NowArg2.Length = (((ust.Section(ListBox1.SelectedIndex).Length - TextBox1.Text) / 8 / ust.BPM) * 1000) + OTO2.Preutterance
        If NowArg2.Length Mod 100 > 50 Then
            NowArg2.Length = NowArg2.Length - NowArg2.Length Mod 100 + 100
        Else
            NowArg2.Length = NowArg2.Length - NowArg2.Length Mod 100
        End If
        'del MainArgs.SingerDir &
        NowArg2.Input = OTO2.Wav
        NowArg2.Out = MainArgs.TempDir & "2.wav"
        NowArg2.Modulation = ust.Section(ListBox1.SelectedIndex).Modulation
        NowArg2.Offset = OTO2.Offset
        NowArg2.PitchBend = "!" & ust.BPM & " AA#83#"
        NowArg2.PITMain = "C4"
        NowArg2.Velocity = ust.Section(ListBox1.SelectedIndex).Velocity
        NowArg2.Volume = ust.Section(ListBox1.SelectedIndex).Intensity
        NowArg2.Con = OTO2.Consonant
        NowArg2.EndBlank = OTO2.Cutoff
        NowArg2.Flags = "Mt" & TextBox2.Text & "Mo" & TextBox3.Text & "Mc" & TextBox4.Text & "MG" & TextBox5.Text & TextBox6.Text

        'utau.StartUTAUResample(utau.NewResample("E:\utau\moresampler.exe", "MORE"), NowArg1, True, False)
        ' utau.StartUTAUResample(utau.NewResample("E:\utau\moresampler.exe", "MORE"), NowArg2, True, False)

        'Dim TempWAV As New IO.FileStream(MainArgs.TempDir & "0.wav",
        ' IO.FileMode.Create,
        'IO.FileAccess.Write)
        ' TempWAV.Close()
        With WavToolArgs1
            .BPM = ust.BPM
            .File1 = "0.wav"
            .File2 = NowArg1.Out
            .Length = TextBox1.Text
            .p1 = 0
            .v2 = 100
            .v3 = 100
            .p2Overlap = OTO1.Overlap
            .p3nextOverlap = OTO2.Overlap
            .Pre = OTO1.Preutterance
            .Overlap = OTO1.Overlap
        End With


        ' utau.StartUTAUWavTool(utau.NewWavtool("E:\utau\wavtoolex.exe", "More"), WavToolArgs, True, True, MainArgs.TempDir)
        With WavToolArgs2
            .BPM = ust.BPM
            .File1 = "0.wav"
            .File2 = NowArg2.Out
            .Length = ust.Section(ListBox1.SelectedIndex).Length - TextBox1.Text
            .p1 = 0
            .v1 = 0
            .v2 = 100
            .v3 = 100
            .p2Overlap = OTO2.Overlap
            .p3nextOverlap = 35
            .Pre = OTO2.Preutterance
            .Overlap = OTO2.Overlap
        End With
        Dim s As System.IO.StreamWriter = New System.IO.StreamWriter("D:\temp.bat", True, System.Text.Encoding.UTF8)
        bat.WriteBat(utau.NewResample("E:\utau\moresampler.exe", "MORE"), utau.NewWavtool("E:\utau\wavtoolex.exe", "More"), NowArg1, WavToolArgs1, NowArg2, WavToolArgs2, MainArgs, s)
        s.Close()
        bat.StartBat("d:\temp.bat", False, True)
        Dim player As New System.Media.SoundPlayer("D:\temp.wav")
        player.PlaySync()
        IO.File.Delete("d:\2.wav")
        IO.File.Delete("d:\1.wav")
        IO.File.Delete("d:\temp.wav")
        IO.File.Delete("D:\temp.bat")
        ' utau.StartUTAUWavTool(utau.NewWavtool("E:\utau\wavtoolex.exe", "More"), WavToolArgs, True, False, MainArgs.TempDir)
    End Sub
End Class
