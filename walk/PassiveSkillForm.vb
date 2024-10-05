Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Text
Imports System.IO
Imports System.Reflection.Assembly

Public Class PassiveSkillForm

    Private g As Graphics
    Private units As New UnitCollection

    Private Ismoratorium As Boolean = True



    Private Sub PassiveSkillForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Init()
    End Sub

    Private Sub BackDraw(ByVal g As Graphics)
        For i = 0 To 6000 Step 100 'Common.Random.Next(3, 100)
            g.DrawBezier(Pens.Yellow, New Point(i, i + Common.Random.Next(200, 400)), New Point(i * 1.5 - Common.Random.Next(200, 400), i), New Point(Me.Width / 2, Me.Height / 2), New Point(i - Common.Random.Next(i), i - Common.Random.Next(i)))
        Next
    End Sub

    Private Sub Init()
        '▼描画用のGraphicsクラスの確保(技術的な処理)
        If g Is Nothing Then
            '初回のみ生成
            Dim bmp As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height)
            Me.BackgroundImage = bmp
            g = Graphics.FromImage(bmp)
            g.InterpolationMode = InterpolationMode.Low
        End If


        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Common.Hakai = False Then
            g.Clear(Color.Red)
        End If

        If Ismoratorium Then
        End If

        Dim DeadUnits As New List(Of UnitBase)

        BackDraw(g)

        For Each unit As UnitBase In units.Effectes     '←このプロパティを区分けしてそれぞれエフェクトと文字とテキストの表示順序を変えられる

            'ユニット消滅判定(ここでは左端に行ったユニットが消滅ユニット一覧に追加される。
            If unit.IsDead Then
                DeadUnits.Add(unit)
                Continue For
            End If

            'ユニットの移動と描画
            unit.Draw(g)

        Next


        For Each unit As UnitBase In units.Texts     '←このプロパティを区分けしてそれぞれエフェクトと文字とテキストの表示順序を変えられる

            'ユニット消滅判定(ここでは左端に行ったユニットが消滅ユニット一覧に追加される。
            If unit.IsDead Then
                DeadUnits.Add(unit)
                Continue For
            End If

            'ユニットの移動と描画
            unit.Draw(g)

        Next



        '消滅したユニットを一覧から削除
        For Each unit As UnitBase In DeadUnits
            units.Remove(unit)
        Next




        '画面を更新
        Me.Invalidate()

    End Sub

    Private Sub PassiveSkillForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Common.stageMIDI()

        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
    End Sub
End Class