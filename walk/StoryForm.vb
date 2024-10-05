Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Text
Imports System.IO
Imports System.Reflection.Assembly

Public Class StoryForm

    Private g As Graphics
    Private units As New UnitCollection
    Private BackImg As Bitmap

    Private Ismoratorium As Boolean = True
    Private ParticleNumber As Integer = 2



    Private Sub StoryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Init()
    End Sub

    Private Sub Init()



        Me.Text = "第0話"


        '▼描画用のGraphicsクラスの確保(技術的な処理)
        If g Is Nothing Then
            '初回のみ生成
            Dim bmp As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height)
            Me.BackgroundImage = bmp
            g = Graphics.FromImage(bmp)
            g.InterpolationMode = InterpolationMode.NearestNeighbor
        End If

        '初期画像
        BackImg = Image.FromFile(Application.StartupPath & "\image\story.jpg")
        '実際はIndexによって読み込むテキストファイルで制御する。↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

        Timer1.Enabled = True
    End Sub

    Private Sub BackDraw()
        g.DrawImage(BackImg, 0, 0, 456, 399)
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Common.Hakai = False Then
            g.Clear(Color.Red)
        End If


        If Ismoratorium Then
            If Common.Random.Next(ParticleNumber) = 0 Then
                units.Add(New SmallRectangles(Me.ClientRectangle, 3))
            End If
        End If

        Dim DeadUnits As New List(Of UnitBase)

        If Common.Hakai = False Then
            BackDraw()
        End If


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

    Private Sub StoryForm_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        My.Computer.Audio.Play(Application.StartupPath & "\sound\oo.wav")
        units.Texts.Clear()

        units.Add(New StoryStr(Me.ClientRectangle, 1, "。。。"))

        ParticleNumber = Common.Random.Next(1, 4)
    End Sub

    Private Sub StoryForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Common.stageMIDI()

        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
    End Sub
End Class