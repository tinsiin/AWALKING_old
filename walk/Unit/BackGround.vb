Imports System.Drawing
Imports System.Drawing.Drawing2D
Public Class BackGround
    Inherits UnitBase

    Dim P1 As New Pen(Color.FromArgb(99, 4, 0, 6), 4)


    Dim point1 As New Point(35, 32) '街灯先の電灯部分用の
    Dim point2 As New Point(20, 38)
    Dim point3 As New Point(7, 27)
    Dim curvePoints As Point() = {point1, point2, point3}

    Dim point4 As New Point(267, 51) '葉っぱ
    Dim point5 As New Point(207, 95)
    Dim point6 As New Point(160, 85)
    Dim point7 As New Point(147, 58)
    Dim point8 As New Point(169, 14)
    Dim point9 As New Point(194, -3)
    Dim curvePoints2 As Point() = {point4, point5, point6, point7, point8, point9}

    Dim point10 As New Point(-2, 140) '山
    Dim point11 As New Point(113, 122)
    Dim point12 As New Point(254, 170)
    Dim curvePoints3 As Point() = {point10, point11, point12}


    Dim BuLEI As Integer = 3 '葉っぱの動き幅  + Common.Random.Next(-BuLEI, BuLEI + 1)

    Dim StepCount As Integer

    Dim gb As LinearGradientBrush

    Public Sub New()
        gb = New LinearGradientBrush(Common.Display.ViewArea, Color.SkyBlue, Color.White, LinearGradientMode.Vertical)
    End Sub

    Public Sub Back_Update()
        gb = New LinearGradientBrush(Common.Display.ViewArea, Color.FromArgb(255, 135 + 90 * (Common.MyStat.WalkPoint / Common.MyStat.MAX_WalkPoint), 206 - 86 * (Common.MyStat.WalkPoint / Common.MyStat.MAX_WalkPoint), 235 - 235 * (Common.MyStat.WalkPoint / Common.MyStat.MAX_WalkPoint)), Color.FromArgb(255, 255 - 255 * (Common.MyStat.WalkPoint / Common.MyStat.MAX_WalkPoint), 255 - 255 * (Common.MyStat.WalkPoint / Common.MyStat.MAX_WalkPoint), 255 - 255 * (Common.MyStat.WalkPoint / Common.MyStat.MAX_WalkPoint)), LinearGradientMode.Vertical)
    End Sub
    Public Overrides Sub Draw(g As Graphics)

        Dim point4 As New Point(267 + Common.Random.Next(-BuLEI, BuLEI + 1), 51 + Common.Random.Next(-BuLEI, BuLEI + 1)) '葉っぱ
        Dim point5 As New Point(207 + Common.Random.Next(-BuLEI, BuLEI + 1), 95 + Common.Random.Next(-BuLEI, BuLEI + 1))
        Dim point6 As New Point(160 + Common.Random.Next(-BuLEI, BuLEI + 1), 85 + Common.Random.Next(-BuLEI, BuLEI + 1))
        Dim point7 As New Point(147 + Common.Random.Next(-BuLEI, BuLEI + 1), 58 + Common.Random.Next(-BuLEI, BuLEI + 1))
        Dim point8 As New Point(169 + Common.Random.Next(-BuLEI, BuLEI + 1), 14 + Common.Random.Next(-BuLEI, BuLEI + 1))
        Dim point9 As New Point(194 + Common.Random.Next(-BuLEI, BuLEI + 1), -3 + Common.Random.Next(-BuLEI, BuLEI + 1))

        If StepCount Mod 2 = 0 Then
            curvePoints2 = {point4, point5, point6, point7, point8, point9}
        End If

        g.FillRectangle(gb, Common.Display.ViewArea)    'グラデーション

        g.DrawLine(P1, 0, 240, 409, 267) '傾いた地面
        g.DrawLine(P1, 10, 240, 7, 27) '街灯縦
        g.DrawLine(P1, 16, 240, 13, 27)

        g.DrawLine(P1, 7, 27, 35, 32) '街灯天辺

        g.DrawCurve(P1, curvePoints, 2) ' 街灯電灯部分

        g.DrawLine(P1, 300, -20, 171, 70) '木の枝
        g.DrawLine(P1, 193, 53, 168, 38) '木の枝 派生
        g.DrawLine(P1, 204, 48, 195, 81) '木の枝 派生
        g.DrawLine(P1, 226, 33, 232, 57) '木の枝 派生
        g.DrawLine(P1, 232, 28, 200, 9) '木の枝 派生

        g.DrawCurve(P1, curvePoints2, 1) ' 枝の葉っぱ

        g.DrawCurve(P1, curvePoints3, 1) ' 山

        StepCount += 1
        If 4000000 < StepCount Then
            StepCount = 0
        End If
    End Sub

End Class
