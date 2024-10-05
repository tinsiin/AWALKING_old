Public Class DeathRed
    Inherits UnitBase

    Private Br As New SolidBrush(Color.Red)
    Private Br_A_Color As Integer = 255

    Public Sub New()
        Me.Width = Common.Display.ViewArea.Width
        Me.Height = Common.Display.ViewArea.Height

    End Sub

    Public Overrides Sub Draw(g As Graphics)

        g.FillRectangle(Br, Me.Rectangle)

        Br_A_Color -= 4
        If Br_A_Color < 0 Then
            Br_A_Color = 0
        End If
        Br.Color = Color.FromArgb(Br_A_Color, 237, 28, 36)

    End Sub

    Public Overrides Function IsDead() As Boolean
        If Br_A_Color = 0 Then
            Return True
        Else
            Return False
        End If

    End Function


End Class
