Public Class DeathExplode
    Inherits UnitBase


    Private Pen1 As New Pen(Brushes.Black)
    Private Pen_A_Color As Integer = 255


    Public Sub New()
        Me.Left = Common.Display.ViewArea.Width / 2
        Me.Top = Common.Display.ViewArea.Height / 2
    End Sub

    Dim stepcount As Integer
    Public Overrides Sub Draw(g As Graphics)
        For i = 0 To 5
            g.DrawLine(Pen1, Me.Left, Me.Top, Me.Left + Common.Random.Next(-Common.Display.ViewArea.Width, Common.Display.ViewArea.Width), Me.Top + Common.Random.Next(-Common.Display.ViewArea.Height, Common.Display.ViewArea.Height))
        Next

        If Pen_A_Color > 0 AndAlso stepcount Mod 2 = 0 Then
            Pen_A_Color -= 1
        End If
        Pen1.Color = Color.FromArgb(Pen_A_Color, 0, 0, 0)

        stepcount += 1
    End Sub


    Public Overrides Function IsDead() As Boolean
        If Pen_A_Color = 0 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
