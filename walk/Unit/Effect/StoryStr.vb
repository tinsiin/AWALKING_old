Public Class StoryStr
    Inherits UnitBase

    Dim StepCount As Integer


    Dim Str As String
    Dim fnt As New Font("MS UI Gothic", 12, FontStyle.Bold)

    Dim ToumeiBr As New SolidBrush(Color.FromArgb(0, 0, 0, 0)) '10
    Dim ToumeiBredge As New SolidBrush(Color.FromArgb(0, 0, 0, 0)) '10
    Dim ToumeiBr2 As New SolidBrush(Color.FromArgb(0, 255, 255, 255)) '10
    Dim Toumei As Integer
    Dim Toumei2 As Integer

    Dim edge As Integer = 2 '縁取りの太さ

    Dim Shadow As Integer = 7 '影の離れ具合

    Public Overrides Sub Draw(g As Graphics)
        StepCount += 1


        If Toumei2 < 255 Then
            Toumei2 += 11
            If Toumei2 > 255 Then
                Toumei2 = 255
            End If
            ToumeiBredge.Color = Color.FromArgb(Toumei2, 0, 0, 0)
            ToumeiBr2.Color = Color.FromArgb(Toumei2, 255, 255, 255)
        End If

        If Toumei < 10 AndAlso StepCount Mod 2 = 0 Then
            Toumei += 1
            ToumeiBr.Color = Color.FromArgb(Toumei, 0, 0, 0)
        End If
        '影部分を表示
        For i As Integer = -edge To edge
                For ii As Integer = -edge To edge
                    g.DrawString(Str, fnt, ToumeiBr, Me.Left + i + Shadow, Me.Top + ii + Shadow)
                Next ii
            Next i



        '縁取り部分の文字を描画
        For i As Integer = -edge To edge
            For ii As Integer = -edge To edge
                g.DrawString(Str, fnt, ToumeiBredge, Me.Left + i, Me.Top + ii)
            Next ii
        Next i
        '本体部分の文字を描画
        g.DrawString(Str, fnt, ToumeiBr2, Me.Left, Me.Top)

        If StepCount Mod 20 = 0 Then
            Dim fntsize As Decimal = 11 + Common.Random.NextDouble()
            If fntsize < 11.8 Then
                fntsize = 11.8
            End If
            fnt = New Font("MS UI Gothic", fntsize, FontStyle.Bold)
        End If


    End Sub


    Public Sub New(ByVal form As Rectangle, ByVal Line As Integer, ByVal Tex As String)

        Str = Tex

        Me.Width = Str.Length * fnt.Size
        Me.Height = fnt.Size

        Me.Left = form.Width + 5 - 435
        Me.Top = form.Height - fnt.Size * 3.5 - (Line - 1) * fnt.Size
    End Sub

    Public Overrides Function IsDead() As Boolean
        If Me.Left < -Me.Width Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Overrides Property BelongTo As BelongTo
        Get
            Return BelongTo.Text
        End Get
        Set(ByVal value As BelongTo)
            MyBase.BelongTo = value
        End Set
    End Property

End Class
