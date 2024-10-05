Public Class MagicRoot
    Inherits UnitBase

    Private Br As New SolidBrush(Color.Blue)
    Private Br2 As New SolidBrush(Color.FromArgb(0, 20, 20, 240))
    Private Br_A_Color As Integer = 255
    Private Br_A_Color2 As Integer = 20

    Dim Fls As Boolean = False

    Dim stepcount As Integer


    Public Overrides Sub Draw(g As Graphics)
        Me.Width = Common.Random.Next(10, 71)
        Me.Height = Common.Random.Next(10, 71)
        Me.Left = Common.Random.Next(Common.Display.ViewArea.Width - Me.Width + 1)
        Me.Top = Common.Random.Next(Common.Display.ViewArea.Height - Me.Height + 1)

        g.FillRectangle(Br2, Common.Display.ViewArea)
        g.FillRectangle(Br, Me.Rectangle)

        Br_A_Color -= 5
        If Br_A_Color < 0 Then
            Br_A_Color = 0
        End If
        Br.Color = Color.FromArgb(Br_A_Color, 0, 0, 255)

        If stepcount Mod 2 = 0 Then

            If Fls Then
                Br_A_Color2 -= 1
            Else
                Br_A_Color2 += 1

                If Br_A_Color2 >= 21 Then
                    Fls = True
                End If
            End If
        End If

        If Br_A_Color2 < 0 Then
            Br_A_Color2 = 0
        End If

        Br2.Color = Color.FromArgb(Br_A_Color2, 20, 20, 240)

        stepcount += 1
    End Sub

    Public Overrides Function IsDead() As Boolean
        If Br_A_Color = 0 Then
            Return True
        Else
            Return False
        End If

    End Function


End Class
