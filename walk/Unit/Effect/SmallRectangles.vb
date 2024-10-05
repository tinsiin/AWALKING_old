Public Class SmallRectangles
    Inherits UnitBase


    Private Br1 As New SolidBrush(Color.FromArgb(80, 0, 0, 0))

    Dim SampleRect As Rectangle

    Dim Msize As Integer

    Public Sub New(ByVal Form As Rectangle, ByVal size As Integer)
        SampleRect = Form

        Msize = size

        Me.Width = Msize
        Me.Height = Msize

        Me.Left = Common.Random.Next(SampleRect.Width * 2 + 150)
        Me.Top = SampleRect.Height + Msize
    End Sub

    Public Overrides Sub Draw(g As Graphics)
        g.FillEllipse(Br1, Me.Rectangle)

        Me.Top -= Common.Random.Next(6)
        Me.Left -= Common.Random.Next(7)

        If Common.Random.Next(100) = 14 Then
            Msize -= 1
        End If

        Me.Width = Msize
        Me.Height = Msize
    End Sub


    Public Overrides Function IsDead() As Boolean

        If Me.Top < -Msize OrElse Me.Left < -Msize OrElse Msize <= 0 Then
            Return True
        End If

        Return False

    End Function

End Class
