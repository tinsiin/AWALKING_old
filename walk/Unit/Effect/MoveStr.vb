Public Class MoveStr
    Inherits UnitBase

    Dim DeltaMove As Integer = 1
    Dim StepCount As Integer



    Dim Str As String
    Dim ToumeiBr As New SolidBrush(Color.FromArgb(192, 255, 255, 255))
    Dim fnt As New Font("MS UI Gothic", 8.7)


    Dim fakeheight As Integer
    Public Overrides Sub Draw(g As Graphics)
        StepCount += 1

        g.FillRectangle(ToumeiBr, Me.Left, Me.Top, Me.Width, fakeheight)
        g.DrawString(Str, fnt, Brushes.Black, Me.Left, Me.Top)


        If StepCount Mod 2 = 0 Then
            Me.Top -= DeltaMove
        End If

    End Sub


    Public Sub New(ByVal Tex As String)
        Dim line As Integer

        Str = Tex

        line = Str.Replace(vbCrLf, vbLf).Split(vbLf(0), vbCr(0)).Count

        Me.fakeheight = (fnt.Size * 1.5) * Line

        Me.Width = Str.Length * fnt.Size + fnt.Size * 4.5
        Me.Height = 40 + Me.fakeheight
        Me.Left = 0
        Me.Top = Common.Display.ViewArea.Height - Me.fakeheight

        If Me.Width > Common.Display.ViewArea.Width Then
            Me.Width = Common.Display.ViewArea.Width
        End If
    End Sub

    Public Overrides Function IsDead() As Boolean
        If Me.Top < -Me.Height Then
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
