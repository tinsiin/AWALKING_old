Public Class Faster_HAE     '早いハエ
    Inherits UnitBase

    Private deltaX As Integer = -7

    Private deltaY As Integer
    Private deltaMaxY As Integer = 5
    Private Up_Down_Switch As Boolean
    Dim Br As New SolidBrush(Color.FromArgb(44, 0, 0, 0))


    Public Sub New(ByVal PlusX As Integer)
        Me.Width = 9
        Me.Height = 6
        Me.Left = Common.Display.ViewArea.Width + Me.Width + PlusX * Common.Random.Next(7, 24)
        Me.Top = Common.Random.Next(Common.Display.ViewArea.Height - Me.Height + 1)

    End Sub
    Public Overrides Sub Draw(g As Graphics)

        g.FillEllipse(Br, Me.Left, Me.Top + deltaY, Me.Width, Me.Height)

        If Up_Down_Switch Then
            If deltaY < deltaMaxY Then
                deltaY += 1
                If deltaY = deltaMaxY Then
                    Up_Down_Switch = False
                End If
            End If
        Else
            If deltaY > -deltaMaxY Then
                deltaY -= 1
                If deltaY = -deltaMaxY Then
                    Up_Down_Switch = True
                End If
            End If
        End If

        Me.Left += deltaX

        'ブースター発生
        If Common.Random.Next(24) = 1 Then
            Common.RequestUnitAdd(New BoosterRectangle(Me.Rectangle, 6, 1, 1))
        End If
    End Sub

    Public Overrides Function IsDead() As Boolean
        If Me.Left < -Me.Width - 100 Then
            Return True
        Else
            Return False
        End If

    End Function


End Class
