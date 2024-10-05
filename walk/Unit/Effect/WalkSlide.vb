Public Class WalkSlide
    Inherits UnitBase

    Dim DeltaMove As Integer = 7
    Dim DeltaWide As Integer = 5
    Dim ToumeiBr As New SolidBrush(Color.FromArgb(210, 2, 55, 25))
    Dim stepCount As Integer

    Public Sub New()
        Me.Width = 4
        Me.Height = 5
        Me.Top = Common.Display.ViewArea.Height / 2
        Me.Left = (Common.Display.ViewArea.Width / 2) - Me.Width / 2

    End Sub

    Public Overrides Sub Draw(g As Graphics)
        stepCount += 1


        g.FillRectangle(ToumeiBr, Rectangle)

        Me.Top += DeltaMove
        Me.Left -= DeltaWide
        Me.Width += DeltaWide * 2
        If stepCount Mod 3 = 0 Then
            Me.Height += 1
        End If

    End Sub

    Public Overrides Function IsDead() As Boolean
        If Me.Top > Common.Display.ViewArea.Height Then
            Return True
        Else
            Return False
        End If
    End Function




End Class
