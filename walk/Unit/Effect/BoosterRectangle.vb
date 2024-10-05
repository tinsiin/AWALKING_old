Public Class BoosterRectangle
    Inherits UnitBase


    Private Br1 As New SolidBrush(Color.FromArgb(12, 0, 0, 0))

    Dim stepcount As Integer
    Dim Msize As Integer
    Private DeltaX As Integer
    Private DeltaY As Integer



    Public Sub New(ByVal MotherUnit As Rectangle, ByVal size As Integer, ByVal DeltaMaxX As Integer, ByVal DeltaMaxY As Integer)

        Msize = size

        Me.Width = Msize
        Me.Height = Msize

        DeltaX = Common.Random.Next(1, DeltaMaxX)
        DeltaY = Common.Random.Next(-DeltaMaxY, DeltaMaxY + 1)

        Me.Left = Common.Random.Next(MotherUnit.Left, MotherUnit.Left + MotherUnit.Width + 1)
        Me.Top = Common.Random.Next(MotherUnit.Top, MotherUnit.Top + MotherUnit.Height + 1)
    End Sub

    Public Overrides Sub Draw(g As Graphics)
        g.FillEllipse(Br1, Me.Rectangle)

        Me.Top += DeltaY
        Me.Left += DeltaX

        If Common.Random.Next(9) = 4 Then
            Msize -= 1

            Me.Width = Msize
            Me.Height = Msize

        ElseIf Common.Random.Next(60) = 11 Then '透明になるよ
            Dim A As Integer = Br1.Color.A
            A -= Common.Random.Next(1, 4)
            If A < 0 Then
                A = 0
            End If
            Br1.Color = Color.FromArgb(A, 0, 0, 0)
        End If

        stepcount += 1
    End Sub


    Public Overrides Function IsDead() As Boolean

        If stepcount > 1000 Then
            Return True
        End If

        Return False

    End Function

End Class
