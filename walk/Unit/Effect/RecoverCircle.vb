Public Class RecoverCircle
    Inherits UnitBase

    Private Pe As New Pen(Color.Orchid, 3)
    Private Pe_A_Color As Integer = 255


    Dim stepcount As Integer

    Public Sub New()
        Me.Left = 200
        Me.Top = 160
        Me.Width = 170
        Me.Height = 166
    End Sub


    Public Overrides Sub Draw(g As Graphics)
        Me.Left -= 2
        Me.Top -= 2

        g.DrawEllipse(Pe, Me.Rectangle)

        Pe_A_Color -= 4
        If Pe_A_Color < 0 Then
            Pe_A_Color = 0
        End If
        Pe.Color = Color.FromArgb(Pe_A_Color, 218, 112, 214)


        stepcount += 1
    End Sub

    Public Overrides Function IsDead() As Boolean
        If Pe_A_Color = 0 Then
            Return True
        Else
            Return False
        End If

    End Function


End Class
