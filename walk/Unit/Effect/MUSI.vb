Public Class MUSI    '虫　向きベクトル　円に近い動き
    Inherits UnitBase

    Dim P1 As New Pen(Color.FromArgb(4, 2, 0, 3))
    Dim B2 As New SolidBrush(Color.FromArgb(4, 255, 115, 0))

    '描画座標X  はme.leftのunitbaseの奴で代用
    '描画座標Y
    Private m_Angle As Single  '向きの角度
    Private m_Speed As Single = 7  '速度
    Private size As Single = 120  '半径
    Private Pos_x As Single
    Private Pos_y As Single
    Public Sub New()
        Me.Width = size
        Me.Height = size

        Pos_x = Common.Random.Next(Common.Display.ViewArea.Width - Me.Width + 1)
        Pos_y = Common.Random.Next(Common.Display.ViewArea.Height - Me.Height + 1)

    End Sub
    Public Overrides Sub Draw(g As Graphics)

        Pos_x = Math.Cos(m_Angle * 3.14 / 180) * m_Speed
        Pos_y = Math.Sin(m_Angle * 3.14 / 180) * m_Speed
        m_Angle += Common.Random.Next(Common.MyStat.Lv)
        m_Speed = Common.MyStat.Lv / 3

        If m_Angle >= 100000 Then
            m_Angle = 0
        End If

        g.FillEllipse(B2, Pos_x, Pos_y, size, size)
        g.DrawEllipse(P1, Pos_x, Pos_y, size, size)
    End Sub

    Public Overrides Function IsDead() As Boolean
        Return False

    End Function


End Class
