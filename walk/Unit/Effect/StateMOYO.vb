Public Class StateMOYO
    Inherits UnitBase

    Private Br As New SolidBrush(Color.BlanchedAlmond)
    Private Br2 As New SolidBrush(Color.FromArgb(255, 160, 210, 40))
    Private toumeiBr As New SolidBrush(Color.FromArgb(20, 0, 0, 0))
    Dim stepcount As Integer
    Private X1 As Integer = 70
    Private deltaX1 As Integer = -2

    Private X2 As Integer = 140
    Private deltaX2 As Integer = 3

    Dim edge As Integer = 3 '縁取りの太さ

    Dim Str As String = "→"

    Dim fnt As New Font("ＭＳ ゴシック", 49, FontStyle.Bold)


    Public Overrides Sub Draw(g As Graphics)
        g.FillRectangle(toumeiBr, Common.Display.ViewArea)
        g.DrawRectangle(SystemPens.ControlText, 105, 48, 20, 200)
        g.FillEllipse(Br, X1, 30, 11, 23)
        g.FillEllipse(Br2, X2, 30, 11, 23)
        g.FillEllipse(Br2, X1, 150, 11, 23)
        g.FillEllipse(Br, X2, 150, 11, 23)


        '縁取り部分の文字を描画
        For i As Integer = -edge To edge
            For ii As Integer = -edge To edge
                g.DrawString(Str, fnt, Brushes.Black, 162 + i, 184 + ii)
            Next ii
        Next i

        g.DrawString(Str, fnt, Brushes.White, 162, 184)



        stepcount += 1
        X1 += deltaX1
        X2 += deltaX2
    End Sub

    Public Overrides Function IsDead() As Boolean
        If stepcount >= 20 Then
            Return True
        Else
            Return False
        End If

    End Function


End Class
