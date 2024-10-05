Imports System.ComponentModel
Imports System.Text
Imports System.IO
Public Class Form1
    Private Units As New UnitCollection
    Private g As Graphics
    Private Back As BackGround

    Public StoryLine As Integer         'ストーリーライン

    ''' <summary>
    ''' UnitAddRequestイベントで追加を依頼されたユニットの一覧。
    ''' ループごとに生成。
    ''' このコレクションに追加されたユニットは画面に出現して活動を開始する。
    ''' </summary>
    ''' <remarks></remarks>
    Private requestedUnits As New List(Of UnitBase)


    Dim SyuuryouDialogFlag As Boolean

    Private UnkoTimer As Integer = 330 '5分30秒 330秒
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If UnkoTimer > 0 Then
            UnkoTimer -= 1
        ElseIf UnkoTimer = 0 Then
            UnkoTimer -= 1
            My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
            If MsgBox("うんこしてたの？", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                My.Computer.Audio.Play(Application.StartupPath & "\sound\h.wav")
                MsgBox("あっそ")
            Else
                My.Computer.Audio.Play(Application.StartupPath & "\sound\h.wav")
                MsgBox("じゃあサッサとゲーム始めろよ")
            End If

        End If
    End Sub
    Private Sub SettingSave()
        Dim writer As StreamWriter = New StreamWriter(Application.StartupPath & "\text\dat3.txt", False, Encoding.UTF8)
        writer.WriteLine("【暗号化キー】")
        writer.WriteLine("1145141919810893364364")
        writer.WriteLine(47)  '初期化してるかどうか してるなら 1
        writer.WriteLine(SyuuryouDialogFlag)  '終了確認フラグさん
        writer.WriteLine(Common.MIDI)  'MIDIフラグさん
        writer.Close()
    End Sub
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")

        If SyuuryouDialogFlag Then
            If MessageBox.Show("ゲーム閉じていいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                e.Cancel = True
            Else
                'MVCloseDevice
                SettingSave()
            End If
        Else
            'MVCloseDevice
            SettingSave()
        End If
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        SyuuryouDialogFlag = CheckBox1.CheckState

        Me.ActiveControl = Nothing
    End Sub
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.CheckState Then
            Common.MIDI = False
        Else
            Common.MIDI = True
        End If

        Me.ActiveControl = Nothing
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        Common.Hakai = CheckBox3.CheckState

        Me.ActiveControl = Nothing
    End Sub



    Private Sub dbg()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetBounds(10, 50, 544, 399, BoundsSpecified.Size)
        Me.Location = New Point(Me.Location.X + 170, Me.Location.Y)


        EventPanel.Location = New Point(266, 9)
        MyturnPanel.Location = New Point(266, 9)
        NormalSkillPanel.Location = New Point(266, 9)
        SkySkillPanel.Location = New Point(266, 9)

        Dim LoadStrs As New List(Of String)
        If File.Exists(Application.StartupPath & "\text\dat3.txt") Then
            Dim sr As New StreamReader(Application.StartupPath & "\text\dat3.txt", Encoding.GetEncoding("UTF-8"))

            While sr.Peek() > -1
                LoadStrs.Add(sr.ReadLine())
            End While
            sr.Dispose()
            If LoadStrs.Count >= 3 AndAlso LoadStrs.Item(2) = 47 Then '1行より多く行がなければロードできない、あと3つ目の数字が47だったらできるお
                SyuuryouDialogFlag = LoadStrs.Item(3)
                Common.MIDI = LoadStrs.Item(4)


                CheckBox1.Checked = SyuuryouDialogFlag

                If Common.MIDI = False Then
                    CheckBox2.Checked = True
                Else
                    CheckBox2.Checked = False
                End If
            End If
        End If


    End Sub

    Private ShopList As New List(Of ItemBase)
    Private Sub ShopOpen()
        '行動の制御
        ActionBtnSeigyo(False)

        ShopList.Clear() '初期化
        ShopMoneyLbl.Text = "お値段はこちらに表示"

        'レアリティとレベルによる選定作業

        '▼このポイントが0になるまで選定し続けるわけ、乱数から決定 
        Dim Teisuu As Integer = Common.Random.Next(1, 4)
        Dim ShopPoint As Integer = Teisuu + Common.Random.Next(2, 4) * ((Common.Random.Next(Common.MyStat.Lv) + 1) * 0.1) '1~3 + 2～3 * (((0～レベル)+1) * 0.1)
        ShopRankLbl.Text = "ショップRank : " & ShopPoint - Teisuu

        Do     '選定メインループ
            Dim Item As ItemBase = Common.AllItems.Item(Common.Random.Next(Common.AllItems.Count))

            If Common.Random.Next(Item.rarity * Common.Random.Next(5, 11)) <= Common.MyStat.Lv * 0.1 Then '5~10*レアリティ分のlevel*0.1
                Dim MinusValue As Integer
                ShopList.Add(Item)
                MinusValue = Item.rarity + 1
                If MinusValue < 1 Then
                    MinusValue = 1
                End If
                ShopPoint -= MinusValue
            End If

        Loop Until ShopPoint <= 0


        '色戻し
        ShopStrLbl.BackColor = Color.DodgerBlue
        ShopStrLbl.ForeColor = Color.Wheat

        ShopStrLbl.Text = ""

        ShopListBox.Items.Clear()

        For Each Item In ShopList           'リストに入れる
            ShopListBox.Items.Add(Item.Name)
        Next

        BuyBtn.Enabled = True
        BuyBtn.Text = "購入"

        ShopPanel.Visible = True

        Me.ActiveControl = Nothing
    End Sub
    Private Sub ShopListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ShopListBox.SelectedIndexChanged
        If ShopListBox.SelectedIndex >= 0 Then
            ShopStrLbl.Text = ShopList.Item(ShopListBox.SelectedIndex).SetumeiStr & "{rarity " & ShopList.Item(ShopListBox.SelectedIndex).rarity & ",使用可能回数" & ShopList.Item(ShopListBox.SelectedIndex).UseableTimes & "回}"
            ShopMoneyLbl.Text = "値段:" & ShopList.Item(ShopListBox.SelectedIndex).Gold & "G"
        End If

        '色戻し
        ShopStrLbl.BackColor = Color.DodgerBlue
        ShopStrLbl.ForeColor = Color.Wheat
    End Sub

    Private Sub BuyBtn_Click(sender As Object, e As EventArgs) Handles BuyBtn.Click
        If ShopListBox.SelectedIndex < 0 Then   '-1の時、アイテムリストボックスには項目がない。
            ShopStrLbl.Text = "アイテムが選ばれてません。"
            '色戻し
            ShopStrLbl.BackColor = Color.DodgerBlue
            ShopStrLbl.ForeColor = Color.Wheat
            My.Computer.Audio.Play(Application.StartupPath & "\sound\error.wav")
        ElseIf ShopList.Item(ShopListBox.SelectedIndex).Gold <= Common.MyStat.Gold Then
            Dim Temp_Str As String = ""

            ItemList.Add(ShopList.Item(ShopListBox.SelectedIndex))

            Common.MyStat.Gold -= ShopList.Item(ShopListBox.SelectedIndex).Gold

            Temp_Str = ShopList.Item(ShopListBox.SelectedIndex).Name & "(" & ShopList.Item(ShopListBox.SelectedIndex).Gold & "G)を購入しました。"

            Dim Temp_Index As Integer = ShopListBox.SelectedIndex

            ShopList.RemoveAt(ShopListBox.SelectedIndex)
            ShopListBox.Items.RemoveAt(ShopListBox.SelectedIndex)

            If ShopListBox.Items.Count > 0 Then
                If Temp_Index = 0 Then
                    ShopListBox.SelectedIndex = 0
                Else
                    ShopListBox.SelectedIndex = Temp_Index - 1
                End If
            End If
            ShopStrLbl.Text = Temp_Str

            '色変え
            ShopStrLbl.BackColor = Color.Cyan
            ShopStrLbl.ForeColor = Color.Green

            '閉店確率
            If Common.Random.Next(Common.MyStat.Lv + 1) = 0 Then
                BuyBtn.Enabled = False
                BuyBtn.Text = "ｼｽﾃﾑｴﾗｰ"
            End If
            My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
        Else
            ShopStrLbl.Text = "お金が足りないです。"
            '色戻し
            ShopStrLbl.BackColor = Color.DodgerBlue
            ShopStrLbl.ForeColor = Color.Wheat
            My.Computer.Audio.Play(Application.StartupPath & "\sound\error.wav")
        End If

        Me.ActiveControl = Nothing
    End Sub

    Private Sub ShopPanelBackBtn_Click(sender As Object, e As EventArgs) Handles ShopPanelBackBtn.Click
        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")

        ShopPanel.Visible = False

        RemoveHandler Shock.ShopOpen, AddressOf ShopOpen

        Me.ActiveControl = Nothing

        '行動の制御
        ActionBtnSeigyo(True)
    End Sub

    Private Sub Init()
        '▼プレイ領域の定義
        Common.Display = New DisplayInfo(New Rectangle(0, 0, 250, 250))

        Units.Clear()
        requestedUnits.Clear()
        RemoveHandler Common.UnitAddRequested, AddressOf UnitAddRequested
        AddHandler Common.UnitAddRequested, AddressOf UnitAddRequested

        RemoveHandler Common.ItemAddRequested, AddressOf ItemAddRequested
        AddHandler Common.ItemAddRequested, AddressOf ItemAddRequested


        'マイステータスの初期化
        Common.MyStat = New AMan

        Back = New BackGround

        'コントロールの位置調整
        ShopPanel.Location = New Point(0, 0)


        '▼描画用のGraphicsクラスの確保(技術的な処理)
        If g Is Nothing Then
            '初回のみ生成
            Dim bmp As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height)
            Me.BackgroundImage = bmp
            g = Graphics.FromImage(bmp)
            g.InterpolationMode = Drawing2D.InterpolationMode.Low
        End If

        '背景描画

        dbg()

        MyCircle = New MUSI

        Timer1.Enabled = True
    End Sub

    Private MyCircle As MUSI
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Me.Text = Units.Count
        If Common.Hakai = False Then
            g.Clear(Color.White)
        End If

        'TLOA算出
        Common.MyStat.TLOACalc()

        '背景描画
        Back.Draw(g)

        For i = 1 To Common.MyStat.Lv
            MyCircle.Draw(g)
        Next

        If Common.Random.Next(1117) <= 4 Then
            For i = 0 To Common.Random.Next(3)
                Units.Add(New Faster_HAE(i))
            Next
        End If

        If Common.EneMan IsNot Nothing Then
            Dim rec As Rectangle = New Rectangle(Common.Display.ViewArea.Left - 100, Common.Display.ViewArea.Top, Common.Display.ViewArea.Width, Common.Display.ViewArea.Height)
            Units.Add(New BoosterRectangle(rec, 6, 1, 1))
        ElseIf Common.Random.Next(4) = 0 Then
            Units.Add(New BoosterRectangle(Common.Display.ViewArea, 6, 1, 1))
        End If


        'ボス時のエフェクト
        If (Common.EneMan IsNot Nothing) AndAlso Common.EneMan.boss Then
            If Common.Random.Next(2.0F) = 0 Then
                Units.Add(New SmallRectangles(Me.ClientRectangle, 4))
            End If
        End If


        '消滅は敵ユニットが画面の左端までいったときや主人公と衝突したときに発生
        Dim DeadUnits As New List(Of UnitBase)


        For Each unit As UnitBase In Units.EventObjectes      '←このプロパティを区分けしてそれぞれエフェクトと文字とテキストの表示順序を変えられる

            'ユニット消滅判定(ここでは左端に行ったユニットが消滅ユニット一覧に追加される。
            If unit.IsDead Then
                DeadUnits.Add(unit)
                Continue For
            End If

            'ユニットの移動と描画
            unit.Draw(g)

        Next

        For Each unit As UnitBase In Units.Effectes     '←このプロパティを区分けしてそれぞれエフェクトと文字とテキストの表示順序を変えられる

            'ユニット消滅判定(ここでは左端に行ったユニットが消滅ユニット一覧に追加される。
            If unit.IsDead Then
                DeadUnits.Add(unit)
                Continue For
            End If

            'ユニットの移動と描画
            unit.Draw(g)

        Next

        For Each unit As UnitBase In Units.Texts     '←このプロパティを区分けしてそれぞれエフェクトと文字とテキストの表示順序を変えられる

            'ユニット消滅判定(ここでは左端に行ったユニットが消滅ユニット一覧に追加される。
            If unit.IsDead Then
                DeadUnits.Add(unit)
                Continue For
            End If

            'ユニットの移動と描画
            unit.Draw(g)

        Next

        '動くテキスト同士の当たり判定  (戦闘時のテキストのかぶりを防ぐための仕組み)
        Dim PrevText As UnitBase = Nothing
        For Each Text As UnitBase In Units.Texts
            If Not PrevText Is Nothing Then
                If IsHit(PrevText, Text) Then
                    PrevText.Top -= PrevText.Height * 1.7
                End If
            End If
            PrevText = Text
        Next



        'ユニットを追加
        For Each unit As UnitBase In requestedUnits
            Units.Add(unit)
        Next
        ' requestedUnits.Clear()   異常解の原因はこれがついてないから

        '消滅したユニットを一覧から削除
        For Each unit As UnitBase In DeadUnits
            Units.Remove(unit)
        Next

        '描画部分より右を覆う色四角
        g.FillRectangle(Brushes.FloralWhite, Common.Display.ViewArea.Width, 0, 400, 400)

        'サブ領域描画　（歩行盤）
        WalkSubDraw()

        '自ステータス描画する
        MyStatesDraw()

        'スキルステータスを描画
        SkillStatesDraw()

        '自分の状態異常描画する
        JyoutaiDraw()

        'コンボ数を3以上は書く
        ComboDraw()


        '画面を更新
        Me.Invalidate()

    End Sub


    Private Sub WalkSubDraw()
        Dim fnt1 As New Font("MS UI Gothic", 11, FontStyle.Italic)
        Dim fnt2 As New Font("MS UI Gothic", 13)
        Dim fnt3 As New Font("MS UI Gothic", 30, FontStyle.Underline)


        g.FillRectangle(Brushes.LightGray, 0, 250, 250, 200)
        g.DrawString("?????", fnt1, Brushes.Beige, 1, Common.Display.ViewArea.Height + 1)

        g.DrawString("位置", fnt2, Brushes.BlueViolet, 80, Common.Display.ViewArea.Height + 20)
        g.DrawString(Common.MyStat.WalkPoint, fnt3, Brushes.AliceBlue, 87, Common.Display.ViewArea.Height + 35)
    End Sub

    Private Sub MyStatesDraw()         '自分のステータスを描画。
        Dim fnt1 As New Font("MS UI Gothic", 13, FontStyle.Bold)
        Dim fnt2 As New Font("MS UI Gothic", 9)
        Dim fnt3 As New Font("ＭＳ 明朝", 11, FontStyle.Bold)

        Dim br1 As New SolidBrush(Color.AntiqueWhite)
        If Common.MyStat.HP <= Common.MyStat.MaxHP / 5 Then
            br1.Color = Color.DarkKhaki
            If Common.MyStat.HP <= 0 Then
                br1.Color = Color.Red
            End If
        End If

        g.FillRectangle(Brushes.Blue, 250, 221, 278, 139)

        g.DrawString("名前 : " & Common.MyStat.Name & "　Lv : " & Common.MyStat.Lv & "　exp : " & Common.MyStat.EXP & "/" & Common.MyStat.MaxEXP, fnt1, br1, 250, 222)
        g.DrawString("HP : " & Common.MyStat.HP & "/" & Common.MyStat.MaxHP & "　MP : " & Common.MyStat.MP & "/" & Common.MyStat.MaxMP & vbCrLf & "TP : " & Common.MyStat.TP & "/" & Common.MyStat.MaxTP & "　所持金 : " & Common.MyStat.Gold & "G", fnt1, br1, 250, 240)    'MPにTP、SPにAP　←SoulPointとActPoint 、お腹　BP（バトルポイントなど）上下するゲージ数値系

        For i = 0 To 7
            Dim PalaWidth As Integer = Math.Floor(Common.MyStat.ElementalExp(i) / (20 + Common.MyStat.ElementalValue(i) * Common.MyStat.ElementalEXPstep) * 30)

            If PalaWidth > 30 Then
                PalaWidth = 30
            End If

            If i > 4 Then
                g.DrawString(Common.ElementalName(i) & vbCrLf & " : " & Common.MyStat.ElementalValue(i), fnt2, Brushes.NavajoWhite, 250 + (i - 5) * 55, 320)

                g.FillRectangle(Brushes.DarkBlue, 250 + (i - 5) * 55, 345, 30, 5)
                g.FillRectangle(Brushes.BlanchedAlmond, 250 + (i - 5) * 55, 345, PalaWidth, 5)
            Else
                g.DrawString(Common.ElementalName(i) & vbCrLf & " : " & Common.MyStat.ElementalValue(i), fnt2, Brushes.NavajoWhite, 250 + i * 55, 280)

                g.FillRectangle(Brushes.DarkBlue, 250 + i * 55, 305, 30, 5)
                g.FillRectangle(Brushes.BlanchedAlmond, 250 + i * 55, 305, PalaWidth, 5)
            End If
        Next

        g.DrawString("【TLOA】" & "(" & Common.ElementalName(Common.MyStat.Elemental) & ")" & vbCrLf & "⇒", fnt3, Brushes.White, 400, 315)
        fnt3 = New Font("ＭＳ 明朝", 8, FontStyle.Bold)
        g.DrawString(Common.MyStat.TLOAName & vbCrLf & " ⁜" & Common.MyStat.TLOApower & " {" & Common.MyStat.MaxTLOA & "]", fnt3, Brushes.White, 419, 332)

    End Sub

    Dim SkillStatesIndex As Integer
    Dim skillDrawF As Boolean
    Private Sub SkillStatesDraw()
        Dim fnt1 As New Font("MS UI Gothic", 11, FontStyle.Italic)
        Dim fnt2 As New Font("MS UI Gothic", 9, FontStyle.Bold)
        Dim fnt3 As New Font("ＭＳ 明朝", 11, FontStyle.Italic Or FontStyle.Bold)

        g.FillRectangle(Brushes.NavajoWhite, 350, 109, 200, 100)

        If skillDrawF Then
            Dim PalaWidth As Integer = Math.Floor(Common.MyStat.SkillExp(SkillStatesIndex) / Common.MyStat.SkillMaxExp(SkillStatesIndex) * 150)

            If PalaWidth > 150 Then
                PalaWidth = 150
            End If

            g.DrawString("【" & Common.MyStat.Skill(0, SkillStatesIndex) & "】Lv" & Common.MyStat.SkillLevel(SkillStatesIndex), fnt1, Brushes.DarkCyan, 350, 110)
            g.DrawString(Common.MyStat.Skill(6, SkillStatesIndex), fnt2, Brushes.DimGray, 350, 127)
            g.DrawString("命中率 " & Common.MyStat.Skill(3, SkillStatesIndex) & "% " & Common.MyStat.Skill(7, SkillStatesIndex), fnt2, Brushes.Olive, 350, 140)

            If Common.MyStat.SkillMaxLevel(SkillStatesIndex) = Common.MyStat.SkillLevel(SkillStatesIndex) Then
                g.FillRectangle(Brushes.Magenta, 355, 158, 150, 16)
                g.DrawString("MAX", fnt3, Brushes.PeachPuff, 355, 158)
            Else
                g.FillRectangle(Brushes.DarkBlue, 355, 158, 150, 16)
                g.FillRectangle(Brushes.Magenta, 355, 158, PalaWidth, 16)

            End If

            g.DrawString(Common.MyStat.Skill(5, SkillStatesIndex), fnt2, Brushes.Black, 350, 184)
        Else
            g.DrawString("SKILL", fnt1, Brushes.DarkCyan, 350, 110)
        End If
    End Sub
    Private Sub JyoutaiDraw()
        Dim Nofnt As New Font("MS UI Gothic", 9)
        Dim str As String = ""

        Dim br1 As New SolidBrush(Color.AntiqueWhite)

        If Common.MyStat.Passive > 0 Then
            str = "<" & Common.PassiveText(Common.MyStat.Passive, 1) & "> " & Common.PassiveText(Common.MyStat.Passive, 0)
        Else
            str = "Nothing⋯⋯"
        End If

        g.FillRectangle(Brushes.Red, 250, 209, 300, Nofnt.Size + 3)
        g.DrawString(str, Nofnt, br1, 250, 209)
    End Sub

    Private Sub ComboDraw()
        Dim fnt1 As New Font("MS UI Gothic", 11, FontStyle.Bold)
        Dim str As String = "SINGLE ATTACK"
        Dim br1 As New SolidBrush(Color.FromArgb(128, 128, 128, 128))

        If Common.MyStat.Combo >= 3 Then
            str = "Combo ATTACK " & Common.MyStat.Combo - 2 & "th"
        End If

        g.FillRectangle(br1, 0, 0, 250, fnt1.Size + 3)
        g.DrawString(str, fnt1, Brushes.FloralWhite, 0, 0)
        br1.Dispose()
    End Sub
    Private Sub UnitAddRequested(ByVal unit As UnitBase)
        requestedUnits.Add(unit)
    End Sub
    Private Sub ItemAddRequested(ByVal item As ItemBase)
        ItemList.Add(item)
    End Sub
    Private Function IsHit(ByVal unit1 As UnitBase, ByVal unit2 As UnitBase) As Boolean

        Dim cloneRect As Rectangle = unit1.Rectangle

        cloneRect.Intersect(unit2.Rectangle)

        If cloneRect.Width > 0 Then
            Return True
        Else
            Return False
        End If

    End Function


    Private Sub WalkBtn_Click(sender As Object, e As EventArgs) Handles WalkBtn.Click

        If Common.MyStat.WalkPoint < Common.MyStat.MAX_WalkPoint Then
            Common.MyStat.WalkPoint += 1
            Units.Add(New WalkSlide)
            My.Computer.Audio.Play(Application.StartupPath & "\sound\hokou.wav")

            EventEndBtn()
            EventRemover()
            StageBtn()

            Encount()

            Back.Back_Update()
        End If

    End Sub
    Private Sub BackBtn_Click(sender As Object, e As EventArgs) Handles BackBtn.Click

        If Common.MyStat.WalkPoint > 0 Then
            If (Common.MyStat.WalkPoint - 1) Mod 100 = 0 Then
                Common.MyStat.WalkPoint -= 1
            Else
                Common.MyStat.WalkPoint -= 2
            End If

            If Common.MyStat.WalkPoint < 0 Then
                Common.MyStat.WalkPoint = 0
            End If

            Units.Add(New BackSlide)
            My.Computer.Audio.Play(Application.StartupPath & "\sound\hokou.wav")

            EventEndBtn()
            EventRemover()

            Encount()

            StageBtn()

            Back.Back_Update()
        End If

    End Sub


    Dim EncountPer As Integer = 8 'エンカウント率
    Private Sub Encount()   'エンカウントする処理まとめる

        If Common.Random.Next(EncountPer) = 0 Then       '←戦闘エンカウント率、休憩地点だった場合はエンカウントしない
            SelectEnemy()
            EncountPer = 8

        Else
            If EncountPer > 3 Then
                EncountPer -= 1
            End If
            If Common.Random.Next(7) = 1 Then '7
                SelectEvent()   '敵に合わなかった場合、イベントが発生する。
            End If
        End If
    End Sub

    Private Sub StageBtn()
        If Common.MyStat.WalkPoint Mod 100 = 0 Then
            Common.MyStat.AreaBtn(True)
        Else
            Common.MyStat.AreaBtn(False)
        End If
    End Sub

    Private Sub KaifukuBtn_Click(sender As Object, e As EventArgs) Handles KaifukuBtn.Click
        My.Computer.Audio.Play(Application.StartupPath & "\sound\kaifuku.wav")

        Units.Add(New MoveStr("全回復しました。（お金を5～10%消費）"))
        Common.MyStat.HP = Common.MyStat.MaxHP
        Common.MyStat.MP = Common.MyStat.MaxMP
        Common.MyStat.Passive = Passive.Neutral
        Common.MyStat.passiveTime = 0
        Common.MyStat.Gold -= Math.Ceiling(Common.MyStat.Gold * (Common.Random.Next(5, 11) * 0.01))
    End Sub


    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")

        Dim NeedGold As Integer = Common.MyStat.Lv * 2 + 8
        Dim Result As Long = MsgBox("セーブしますか？(-" & NeedGold & "G)", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "確認")

        If Result = 6 Then
            If Common.MyStat.Gold < NeedGold Then
                My.Computer.Audio.Play(Application.StartupPath & "\sound\h.wav")
                MsgBox("金が足りません。",, "セーブ失敗")
            Else
                Common.MyStat.Gold -= NeedGold
                'ここにセーブ処理を書く
                Dim writer As StreamWriter = New StreamWriter(Application.StartupPath & "\text\dat1.txt", False, Encoding.UTF8)
                writer.WriteLine("(貴様!よくぞ突破しやがったな！!) ")
                writer.WriteLine("(どうぞパワープレイをお楽しみください。)")
                writer.WriteLine(0)  '初期化してるかどうか してるなら 1
                writer.WriteLine(Common.MyStat.HP)
                writer.WriteLine(Common.MyStat.MaxHP)
                writer.WriteLine(Common.MyStat.MP)
                writer.WriteLine(Common.MyStat.MaxMP)
                writer.WriteLine(Common.MyStat.TP)
                writer.WriteLine(Common.MyStat.MaxTP)
                writer.WriteLine(Common.MyStat.Gold)
                writer.WriteLine(Common.MyStat.EXP)
                writer.WriteLine(Common.MyStat.WalkPoint)
                writer.WriteLine(Common.MyStat.Combo)
                writer.WriteLine(Common.MyStat.Lv)
                writer.WriteLine(Common.MyStat.MaxTLOA)
                writer.WriteLine(Common.MyStat.Passive) '状態異常
                For i = 0 To 7
                    writer.WriteLine(Common.MyStat.ElementalValue(i))
                    writer.WriteLine(Common.MyStat.ElementalExp(i))
                Next
                For i = 0 To UBound(Common.MyStat.SkillLevel)
                    writer.WriteLine(Common.MyStat.SkillLevel(i))
                    writer.WriteLine(Common.MyStat.SkillExp(i))
                Next
                writer.Close()
                My.Computer.Audio.Play(Application.StartupPath & "\sound\ok.wav")
                MsgBox("セーブしました。")
                Dim ItemSave As StreamWriter = New StreamWriter(Application.StartupPath & "\text\dat2.txt", False, Encoding.UTF8)
                ItemSave.WriteLine(0)  '初期化してるかどうか してるなら 1

                For Each Item In ItemList
                    ItemSave.WriteLine(Item.ID)
                    ItemSave.WriteLine(Item.UseableTimes)
                Next
                ItemSave.Close()
            End If
        End If
    End Sub
    Private Sub EraceSaveBtn_Click(sender As Object, e As EventArgs) Handles EraceSaveBtn.Click
        If File.Exists(Application.StartupPath & "\text\dat1.txt") Then
            My.Computer.Audio.Play(Application.StartupPath & "\sound\fws_.wav")
            Dim writer As StreamWriter = New StreamWriter(Application.StartupPath & "\text\dat1.txt", False, Encoding.UTF8)
            writer.WriteLine("(貴様!よくぞ突破しやがったな！!) " & vbCrLf & "(どうぞパワープレイをお楽しみください。)")
            writer.Write(1)
            writer.Close()
            If File.Exists(Application.StartupPath & "\text\dat2.txt") Then
                Dim Items As StreamWriter = New StreamWriter(Application.StartupPath & "\text\dat2.txt", False, Encoding.UTF8)
                Items.Write(1)
                Items.Close()
            End If
            MsgBox("セーブデータを消去しました",, "完了")
        Else
            My.Computer.Audio.Play(Application.StartupPath & "\sound\error.wav")
        End If

        Me.ActiveControl = Nothing
    End Sub

    Private Sub NewStartBtn_Click(sender As Object, e As EventArgs) Handles NewStartBtn.Click
        Timer2.Enabled = False

        Init()
        'MVOpenDevice(0, Handle)
        Common.stageMIDI()

        NewStartBtn.Visible = False
        CheckBox1.Visible = False
        CheckBox2.Visible = False
        CheckBox3.Visible = False
        EraceSaveBtn.Visible = False

        WalkBtn.Visible = True
        BackBtn.Visible = True

        ItemBtn.Visible = True
        PassiveSkillCheckBtn.Visible = True

        KaifukuBtn.Visible = True
        SaveBtn.Visible = True
        TalkBtn.Visible = True
        StoryBtn.Visible = True

        Common.GamePhase = GamePhases.Playing

        Dim LoadStrs As New List(Of String)
        If File.Exists(Application.StartupPath & "\text\dat2.txt") Then
            Dim sr As New StreamReader(Application.StartupPath & "\text\dat2.txt", Encoding.GetEncoding("UTF-8"))

            While sr.Peek() > -1
                LoadStrs.Add(sr.ReadLine())
            End While
            sr.Dispose()
            If LoadStrs.Count >= 1 AndAlso LoadStrs.Item(0) = 0 Then '1行より多く行がなければロードできない、あと最初の数字が0だったらできるお
                For i = 1 To LoadStrs.Count / 2
                    Common.ItemDRRR() 'オールアイテムから引っ張ると一つのNEWした該当アイテムを使いまわしてしまうので、毎回リストを一新する
                    ItemList.Add(Common.AllItems.Item(LoadStrs.Item(1 + (i - 1) * 2) - 1))    'なぜ-1するのか、それはallitemのINDは0からとitem.IDは1から数えるから
                    ItemList.Item(i - 1).UseableTimes = LoadStrs.Item(2 + (i - 1) * 2)      'インデックスだから-1してるよ(左項)
                Next
            End If
        End If
        Me.ActiveControl = Nothing
    End Sub

    Private Sub TalkBtn_Click(sender As Object, e As EventArgs) Handles TalkBtn.Click
        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
        Units.Add(New MoveStr(Common.MyStat.TalkText))
    End Sub

    Dim WithEvents Shock As Normal_Ipad
    Private Sub SelectEvent()       'イベントを、WalkPointとStoryLineから選ぶ
        Common.EveMan = Nothing

        If Not Common.MyStat.WalkPoint Mod 100 = 0 Then
            Select Case Common.Random.Next(3)
                Case 0
                    Common.EveMan = New Normal_Ipad     '会話イベント
                    Units.Add(New MoveStr("あ、IPADだ。。。"))
                    Shock = Common.EveMan
                    AddHandler Shock.ShopOpen, AddressOf ShopOpen       'お店にリストを並べる処理を起こすイベント
                Case 1
                    Common.EveMan = New RecoverHP     '普通回復イベント
                    Units.Add(New MoveStr("ベンチを見つけた。"))
                Case 2
                    If Common.Random.Next(1, 1001) <= Common.MyStat.ElementalValue(Elemental.Night) Then '暗黒値で発生するイベント
                        Common.EveMan = New AnkokuTakarabako
                        Units.Add(New MoveStr("*ドササ"))
                    End If
            End Select
        End If



        If Common.EveMan IsNot Nothing Then
            EventStartBtn() '引数で会話とか五つボタンとか表示するのか決める、 endbtnは全部まとめて消せばいい
            Units.Add(Common.EveMan)
            My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
        End If
    End Sub
    Private Sub SelectEnemy()       '敵をステージごとに選定。
        Common.EneMan = Nothing
        Dim Level As Integer

        If Not Common.MyStat.WalkPoint Mod 100 = 0 Then
            Dim Mi As Integer = (Common.What_hundred(Common.MyStat.WalkPoint) - 100) / 20
            Dim Max As Integer = Common.What_hundred(Common.MyStat.WalkPoint) / 20
            If Mi < 1 Then
                Mi = 1
            End If
            Me.Text = "LvFeet " & Mi & "," & Max

            If Common.Random.Next(4) = 0 AndAlso Common.MyStat.WalkPoint > 100 Then
                Level = Common.Random.Next(Mi + 1, Max)
                Common.EneMan = New skyfish(Level)
            ElseIf Common.Random.Next(4) = 0 AndAlso Common.MyStat.WalkPoint > 100 Then
                Level = Common.Random.Next(Mi, Max + 1)
                Common.EneMan = New YataGarasu(Level)
            Else
                If Common.MyStat.WalkPoint Mod 2 = 0 AndAlso Common.Random.Next(4) = 0 AndAlso Common.MyStat.WalkPoint > 100 Then
                    Level = Common.Random.Next(Mi, Max - 1)
                    Common.EneMan = New Yanki(Level)
                ElseIf (Not Common.MyStat.WalkPoint Mod 2 = 0) AndAlso Common.Random.Next(6) = 0 AndAlso Common.MyStat.WalkPoint > 110 Then
                    Level = Common.Random.Next(Mi - 1, Max + 1)
                    Common.EneMan = New Mayoi(Level)
                ElseIf Common.Random.Next(Common.MyStat.Lv + 2) = 0 Then
                    Level = Common.Random.Next(Mi, Max + 1)
                    Common.EneMan = New HIYOWA(Level)
                Else
                    Level = Common.Random.Next(Mi, Max + 1)
                    If Common.Random.Next(2) = 0 AndAlso Common.MyStat.Lv < Max Then   '2分の1で自レベル絶対参照だ四
                        Level = Common.MyStat.Lv
                    End If
                    Common.EneMan = New IppanJin(Level)
                End If
            End If


        End If


        If Common.EneMan IsNot Nothing Then
            Units.Add(Common.EneMan)
            Units.Add(New MoveStr(Common.EneMan.Name & "がいる！"))
            BattleStartBtn()
            My.Computer.Audio.Play(Application.StartupPath & "\sound\enc.wav")
        Else                                                                            '敵にエンカウントしたが、そこが安全地帯だった場合。
            My.Computer.Audio.Play(Application.StartupPath & "\sound\nigeru.wav")
            Units.Add(New MoveStr("敵がいたが、何とか逃げおおせた…"))
        End If
    End Sub




    Private Sub RunFromEnemyBtn_Click(sender As Object, e As EventArgs) Handles RunFromEnemyBtn.Click
        If Common.Random.Next(100) <= 84 Then
            My.Computer.Audio.Play(Application.StartupPath & "\sound\nigeru.wav")

            Common.MyStat.WalkPoint -= 5
            If Common.MyStat.WalkPoint < 0 Then
                Common.MyStat.WalkPoint = 0
            End If

            Units.Add(New MoveStr("逃走！。。。"))


            Units.Remove(Common.EneMan)
            Common.EneMan = Nothing
            BattleEndBtn()
            StageBtn()
        Else
            '敵のターン
            My.Computer.Audio.Play(Application.StartupPath & "\sound\Pia.wav")
            Units.Add(New MoveStr("逃走失敗 ⇒  ～ターン交代～"))
            EnemyTurn()
        End If

        Me.ActiveControl = Nothing
    End Sub
    Private Sub EveBtn1_Click(sender As Object, e As EventArgs) Handles EveBtn1.Click
        EventEndBtn()

        If Common.EveMan.Events(-1) >= Common.Random.Next(1, 101) Then
            My.Computer.Audio.Play(Application.StartupPath & "\sound\ok.wav")
            Units.Add(New MoveStr(Common.EveMan.Events(1)))
            Common.EveMan.Events(6)
        Else
            My.Computer.Audio.Play(Application.StartupPath & "\sound\damage.wav")
            Units.Add(New MoveStr(Common.EveMan.Events(1, False)))
            Common.EveMan.Events(6, False)
        End If

        EventRemover()
        Me.ActiveControl = Nothing
    End Sub
    Private Sub EveBtn2_Click(sender As Object, e As EventArgs) Handles EveBtn2.Click
        EventEndBtn()

        If Common.EveMan.Events(-2) >= Common.Random.Next(1, 101) Then
            My.Computer.Audio.Play(Application.StartupPath & "\sound\ok.wav")
            Units.Add(New MoveStr(Common.EveMan.Events(2)))
            Common.EveMan.Events(7)
        Else
            My.Computer.Audio.Play(Application.StartupPath & "\sound\damage.wav")
            Units.Add(New MoveStr(Common.EveMan.Events(2, False)))
            Common.EveMan.Events(7, False)
        End If

        EventRemover()
        Me.ActiveControl = Nothing
    End Sub
    Private Sub EveBtn3_Click(sender As Object, e As EventArgs) Handles EveBtn3.Click
        EventEndBtn()

        If Common.EveMan.Events(-3) >= Common.Random.Next(1, 101) Then
            My.Computer.Audio.Play(Application.StartupPath & "\sound\ok.wav")
            Units.Add(New MoveStr(Common.EveMan.Events(3)))
            Common.EveMan.Events(8)
        Else
            My.Computer.Audio.Play(Application.StartupPath & "\sound\damage.wav")
            Units.Add(New MoveStr(Common.EveMan.Events(3, False)))
            Common.EveMan.Events(8, False)
        End If

        EventRemover()
        Me.ActiveControl = Nothing
    End Sub


    Private Sub EventStartBtn()
        EveBtn1.Visible = False
        EveBtn2.Visible = False
        EveBtn3.Visible = False
        EveBtn4.Visible = False
        EveBtn5.Visible = False

        EventPanel.Visible = True
        Select Case Common.EveMan.Events(0)
            Case 1
                EveBtn1.Visible = True
                EveBtn1.Text = Common.EveMan.Events(-6)
            Case 2
                EveBtn1.Visible = True
                EveBtn2.Visible = True
                EveBtn1.Text = Common.EveMan.Events(-6)
                EveBtn2.Text = Common.EveMan.Events(-7)
            Case 3
                EveBtn1.Visible = True
                EveBtn2.Visible = True
                EveBtn3.Visible = True
                EveBtn1.Text = Common.EveMan.Events(-6)
                EveBtn2.Text = Common.EveMan.Events(-7)
                EveBtn3.Text = Common.EveMan.Events(-8)
            Case 4
                EveBtn1.Visible = True
                EveBtn2.Visible = True
                EveBtn3.Visible = True
                EveBtn4.Visible = True
                EveBtn1.Text = Common.EveMan.Events(-6)
                EveBtn2.Text = Common.EveMan.Events(-7)
                EveBtn3.Text = Common.EveMan.Events(-8)
                EveBtn4.Text = Common.EveMan.Events(-9)
            Case 5
                EveBtn1.Visible = True
                EveBtn2.Visible = True
                EveBtn3.Visible = True
                EveBtn4.Visible = True
                EveBtn5.Visible = True
                EveBtn1.Text = Common.EveMan.Events(-6)
                EveBtn2.Text = Common.EveMan.Events(-7)
                EveBtn3.Text = Common.EveMan.Events(-8)
                EveBtn4.Text = Common.EveMan.Events(-9)
                EveBtn5.Text = Common.EveMan.Events(-10)
        End Select

        If Common.EveMan.Events(100) Then
            WalkBtn.Enabled = False
            BackBtn.Enabled = False
        End If

        ItemBtn.Enabled = False
        PassiveSkillCheckBtn.Enabled = False
    End Sub
    Private Sub EventEndBtn()
        EventPanel.Visible = False

        WalkBtn.Enabled = True
        BackBtn.Enabled = True
        ItemBtn.Enabled = True
        PassiveSkillCheckBtn.Enabled = True
    End Sub
    Private Sub EventRemover()
        If Not Common.EveMan Is Nothing Then
            Units.Remove(Common.EveMan)
            Common.EveMan = Nothing
        End If
    End Sub
    Private Sub BattleStartBtn()
        WalkBtn.Enabled = False
        BackBtn.Enabled = False

        MyturnPanel.Visible = True
        SelectSkillBtn()
        EnemyBattleBtn.Visible = False

        ItemBtn.Enabled = False
        PassiveSkillCheckBtn.Enabled = False

        BattleCode = 0
        skillDrawF = False
    End Sub
    Private Sub BattleEndBtn()
        WalkBtn.Enabled = True
        BackBtn.Enabled = True

        MyturnPanel.Visible = False

        MyBattleBtn.Visible = False
        EnemyBattleBtn.Visible = False

        ItemBtn.Enabled = True
        PassiveSkillCheckBtn.Enabled = True

        BattleCode = 0
        skillDrawF = False
    End Sub

    Private Sub EnemyTurn()
        MyturnPanel.Visible = False
        EnemyBattleBtn.Visible = True
        MyBattleBtn.Visible = False

        BattleCode = 0
        Common.EneMan.PassiveDrive()  'ここで敵のの状態異常が発生します！！！！！！！
    End Sub


    Dim MySkillIndex As Integer
    Private Function SkillUp()
        If Common.MyStat.SkillLevel(MySkillIndex) = Common.MyStat.SkillMaxLevel(MySkillIndex) Then
            Return False
        Else
            Common.MyStat.SkillExp(MySkillIndex) += 1
        End If

        If Common.MyStat.SkillExp(MySkillIndex) >= Common.MyStat.SkillMaxExp(MySkillIndex) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub Myturn(ByVal index)  '自分の技ボタンを押した処理 自ターン継続ボタンフェーズへの移行
        MySkillIndex = index
        MyturnPanel.Visible = False
        NormalSkillPanel.Visible = False
        SkySkillPanel.Visible = False
        Units.Add(New MoveStr(Common.MyStat.Name & "の" & Common.MyStat.Skill(0, MySkillIndex)))
        MyBattleBtn.Visible = True
        Common.MyStat.PassiveDrive()  'ここでA太の状態異常が発生します！！！！！！！

        '複数攻撃の回数を入れる
        Common.FukusuuSTR = False
        If Common.MyStat.Skill(15, MySkillIndex) = 1 Then '回数
            FukusuuKaisuu = Common.MyStat.Skill(13, MySkillIndex)
        ElseIf Common.MyStat.Skill(15, mySkillIndex) = 2 Then  '確率
            FukusuuKakuritu = Common.MyStat.Skill(14, MySkillIndex)
        End If
    End Sub

    Private BattleCode As Integer
    Private Sub MyBattleBtn_Click(sender As Object, e As EventArgs) Handles MyBattleBtn.Click   '自ターン　継続フェーズ
        If Common.EneMan.BattleDie AndAlso BattleCode > 0 Then
            Units.Remove(Common.EneMan)

            Dim GetExp As Integer = Utility.EXPgenerator(Common.MyStat.Lv, Common.EneMan.Lv)

            Common.MyStat.EXP += GetExp
            Common.MyStat.Gold += Common.EneMan.Gold

            My.Computer.Audio.Play(Application.StartupPath & "\sound\fws_.wav")
            Units.Add(New MoveStr(Common.EneMan.Name & "を倒した。。" & vbCrLf & Common.EneMan.Gold & "G取った。" & vbCrLf & GetExp & "exp分、成長した！"))


            'バトル終了後、回復
            Common.MyStat.HP += Common.MyStat.MaxHP * 0.05
            Common.MyStat.MP += Common.MyStat.MaxMP * 0.03

            If Common.MyStat.HP > Common.MyStat.MaxHP Then
                Common.MyStat.HP = Common.MyStat.MaxHP
            End If
            If Common.MyStat.MP > Common.MyStat.MaxMP Then
                Common.MyStat.MP = Common.MyStat.MaxMP
            End If

            Common.EneMan.Explode()



            If Utility.ElementalUp(GetExp, Common.EneMan.Elemental) Then
                BattleCode = -1
            ElseIf Common.MyStat.EXP >= Common.MyStat.MaxEXP Then
                BattleCode = -2
            Else
                Common.EneMan = Nothing
                BattleEndBtn()
            End If
        Else
            Select Case BattleCode
                Case -1
                    My.Computer.Audio.Play(Application.StartupPath & "\sound\ok.wav")
                    Units.Add(New MoveStr("【" & Common.ElementalName(Common.EneMan.Elemental) & "】が上昇"))

                    If Common.MyStat.EXP >= Common.MyStat.MaxEXP Then
                        BattleCode = -2
                    Else
                        Common.EneMan = Nothing
                        BattleEndBtn()
                    End If
                Case -2
                    My.Computer.Audio.Play(Application.StartupPath & "\sound\lvup.wav")
                    MsgBox("レベルが上がった！",, "敵死亡！")
                    Common.MyStat.EXP = 0
                    Common.MyStat.Lv += 1
                    Utility.StatesUp()
                    My.Computer.Audio.Play(Application.StartupPath & "\sound\ok.wav")
                    MsgBox("ステータスが上昇した、アイテムを手に入れた。",, "Level Up")       'この後にステータスアップの処理。


                    Common.EneMan = Nothing
                    BattleEndBtn()
                Case 0
                    If Common.MyStat.Skill(3, MySkillIndex) >= Common.Random.Next(1, 101) Then  '命中率が乱数以上だった場合。
                        If Common.MyStat.Skill(9, MySkillIndex) = Skillcategory.Attack Then
                            My.Computer.Audio.Play(Application.StartupPath & "\sound\ATK.wav")
                            Common.MyStat.Combo += 1
                            Units.Add(New MoveStr(Common.MyStat.Skill(2, MySkillIndex, Common.EneMan.Damage(Common.MyStat.Skill(1, MySkillIndex), Common.MyStat.Skill(4, MySkillIndex))) & vbCrLf & "*" & Common.TempElementalPer & "%通った！"))
                        ElseIf Common.MyStat.Skill(9, MySkillIndex) = Skillcategory.MAGIC Then
                            My.Computer.Audio.Play(Application.StartupPath & "\sound\magic.wav")
                            Common.MyStat.MAGICSKILL(MySkillIndex)
                            Units.Add(New MoveStr(Common.MyStat.Skill(2, MySkillIndex)))
                            Units.Add(New MagicRoot())
                            Common.MyStat.Combo = 0
                        End If
                    Else
                        Common.MyStat.Combo = 0 '外した場合、又は攻撃属性以外の行動をとった場合、コンボ数は0になる。
                        If Common.MyStat.Skill(11, MySkillIndex) Then
                            My.Computer.Audio.Play(Application.StartupPath & "\sound\damage.wav")
                            Units.Add(New MoveStr(Common.MyStat.Skill(12, MySkillIndex)))   '攻撃demerit存在時の文章（汎用でない）
                        Else
                            My.Computer.Audio.Play(Application.StartupPath & "\sound\h.wav")
                            Units.Add(New MoveStr("~MISS~"))
                        End If
                    End If

                    Dim Code As Integer

                    If SkillUp() Then
                        Code = -10
                    Else
                        Code = 1
                    End If


                    '複数攻撃
                    If Common.MyStat.Skill(15, MySkillIndex) = 1 AndAlso FukusuuKaisuu > 0 Then '回数
                        FukusuuKaisuu -= 1
                        Code = 0
                        Common.FukusuuSTR = True
                    ElseIf Common.MyStat.Skill(15, MySkillIndex) = 2 AndAlso FukusuuKakuritu >= Common.Random.Next(1, 101) Then  '確率
                        Code = 0
                        Common.FukusuuSTR = True
                    End If

                    BattleCode = Code
                Case -10
                    My.Computer.Audio.Play(Application.StartupPath & "\sound\ok.wav")
                    Units.Add(New MoveStr(Common.MyStat.Skill(0, MySkillIndex) & "のレベルが上がった！"))
                    Common.MyStat.SkillExp(MySkillIndex) = 0
                    Common.MyStat.SkillLevel(MySkillIndex) += 1
                    BattleCode = 1
                Case 1
                    My.Computer.Audio.Play(Application.StartupPath & "\sound\Pia.wav")
                    EnemyTurn()
                    Units.Add(New MoveStr("～ターン交代～"))
            End Select
        End If

        Me.ActiveControl = Nothing
    End Sub



    Dim EneSkillIndex As Integer
    Dim FukusuuKaisuu As Integer
    Dim FukusuuKakuritu As Integer
    Private Sub EnemyBattleBtn_Click(sender As Object, e As EventArgs) Handles EnemyBattleBtn.Click

        If Common.MyStat.HP <= 0 Then        'もし復活するのなら、ここにその処理を描く、敵も同じ

            g.FillRectangle(Brushes.Red, Common.Display.ViewArea)
            Timer1.Stop()
            My.Computer.Audio.Play(Application.StartupPath & "\sound\fws_.wav")
            MsgBox("ここで命尽きた。。",, "result")
            'MVCloseDevice
            Me.Dispose()
            'もし復活するのなら、msgboxでここにその処理を描く

        Else
            Select Case BattleCode
                Case 0
                    EneSkillIndex = Common.EneMan.SkillAI
                    My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
                    Units.Add(New MoveStr(Common.EneMan.Name & "の" & Common.EneMan.Skill(0, EneSkillIndex)))
                    BattleCode += 1
                    '複数攻撃の回数を入れる
                    Common.FukusuuSTR = False
                    If Common.EneMan.Skill(10, EneSkillIndex) = 1 Then '回数
                        FukusuuKaisuu = Common.EneMan.Skill(8, EneSkillIndex)
                    ElseIf Common.EneMan.Skill(10, EneSkillIndex) = 2 Then  '確率
                        FukusuuKakuritu = Common.EneMan.Skill(9, EneSkillIndex)
                    End If
                Case 1
                    If Common.EneMan.Skill(3, EneSkillIndex) >= Common.Random.Next(1, 101) Then  '命中率が乱数以上だった場合。
                        If Common.EneMan.Skill(4, EneSkillIndex) = Skillcategory.Attack Then    '攻撃
                            Units.Add(New MoveStr(Common.EneMan.Skill(2, EneSkillIndex, Common.MyStat.Damage(Common.EneMan.Skill(1, EneSkillIndex), Common.EneMan.Elemental)) & vbCrLf & "*" & Common.TempElementalPer & "%通った！"))
                            Units.Add(New DeathRed())
                            My.Computer.Audio.Play(Application.StartupPath & "\sound\damage.wav")
                        ElseIf Common.EneMan.Skill(4, EneSkillIndex) = Skillcategory.MAGIC Then     '魔法
                            Common.EneMan.MAGICSKILL(EneSkillIndex)
                            Units.Add(New MoveStr(Common.EneMan.Skill(2, EneSkillIndex)))
                            Units.Add(New MagicRoot())
                            My.Computer.Audio.Play(Application.StartupPath & "\sound\magic.wav")
                        ElseIf Common.EneMan.Skill(4, EneSkillIndex) = Skillcategory.Recover Then
                            Units.Add(New MoveStr(Common.EneMan.Skill(2, EneSkillIndex))) '攻撃力×回復倍率で、回復
                            Common.EneMan.HP += Math.Round(Common.EneMan.ATK * Common.EneMan.Recov)

                            If Common.EneMan.HP > Common.EneMan.MaxHP Then
                                Common.EneMan.HP = Common.EneMan.MaxHP
                            End If

                            Units.Add(New RecoverCircle)
                            My.Computer.Audio.Play(Application.StartupPath & "\sound\kaifuku.wav")

                        ElseIf Common.EneMan.Skill(4, EneSkillIndex) = Skillcategory.STATESgive Then　　'ステータス付与
                            Units.Add(New MoveStr(Common.EneMan.Skill(2, EneSkillIndex)))
                            Common.MyStat.Passive = Common.EneMan.Skill(5, EneSkillIndex)
                            Common.MyStat.passiveTime = Common.EneMan.Skill(6, EneSkillIndex)
                            Common.MyStat.passivePer = Common.EneMan.Skill(7, EneSkillIndex)

                            Units.Add(New StateMOYO)
                            My.Computer.Audio.Play(Application.StartupPath & "\sound\give.wav")
                        End If
                    Else
                        My.Computer.Audio.Play(Application.StartupPath & "\sound\h.wav")
                        Units.Add(New MoveStr("~MISS~"))
                    End If
                    BattleCode += 1

                    '複数攻撃
                    If Common.EneMan.Skill(10, EneSkillIndex) = 1 Then '回数
                        If FukusuuKaisuu > 0 Then
                            FukusuuKaisuu -= 1
                            BattleCode -= 1
                            Common.FukusuuSTR = True
                        End If
                    ElseIf Common.EneMan.Skill(10, EneSkillIndex) = 2 Then  '確率
                        If FukusuuKakuritu >= Common.Random.Next(1, 101) Then
                            BattleCode -= 1
                            Common.FukusuuSTR = True
                        End If
                    End If
                Case 2
                    My.Computer.Audio.Play(Application.StartupPath & "\sound\Pia.wav")
                    BattleStartBtn()
                    Units.Add(New MoveStr("～ターン交代～"))
            End Select
        End If

        Me.ActiveControl = Nothing
    End Sub

    Private Sub StoryBtn_Click(sender As Object, e As EventArgs) Handles StoryBtn.Click

        My.Computer.Audio.Play(Application.StartupPath & "\sound\h.wav")

        Me.Visible = False
        Dim f As New StoryForm

        f.Left = Me.Left
        f.Top = Me.Top - 5
        Timer1.Enabled = False
        f.ShowDialog(Me)

        Me.Left = f.Left
        Me.Top = f.Top
        f.Dispose()
        Timer1.Enabled = True
        Me.Visible = True


        Me.ActiveControl = Nothing
    End Sub
    Private Sub PassiveSkillCheckBtn_Click(sender As Object, e As EventArgs) Handles PassiveSkillCheckBtn.Click
        My.Computer.Audio.Play(Application.StartupPath & "\sound\h.wav")


        Me.Visible = False
        Dim f As New PassiveSkillForm

        f.Left = Me.Left + 140
        f.Top = Me.Top + 60
        Timer1.Enabled = False
        f.ShowDialog(Me)

        Me.Left = f.Left - 140
        Me.Top = f.Top - 60
        f.Dispose()
        Timer1.Enabled = True
        Me.Visible = True


        Me.ActiveControl = Nothing

    End Sub


    Private Sub ItemPanelBackBtn_Click(sender As Object, e As EventArgs) Handles ItemPanelBackBtn.Click
        ItemPanel.Visible = False

        Me.ActiveControl = Nothing

        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")

        '行動の制御
        If Not Common.GamePhase = GamePhases.BattleItem Then
            ActionBtnSeigyo(True)
        Else
            Common.GamePhase = GamePhases.Playing
            MyBattleBtn.Enabled = True
        End If
    End Sub



    Private ItemList As New List(Of ItemBase)
    Private Sub ItemBtn_Click(sender As Object, e As EventArgs) Handles ItemBtn.Click
        '行動の制御
        If Not Common.GamePhase = GamePhases.BattleItem Then
            ActionBtnSeigyo(False)
        Else
            ItemBtn.Enabled = False
            MyBattleBtn.Enabled = False
        End If

        '色戻し
        ItemStrLbl.BackColor = Color.DodgerBlue
        ItemStrLbl.ForeColor = Color.Wheat

        ItemStrLbl.Text = ""

        ItemListBox.Items.Clear()

        For Each Item In ItemList
            ItemListBox.Items.Add(Item.Name)
        Next

        ItemPanel.Visible = True

        My.Computer.Audio.Play(Application.StartupPath & "\sound\h.wav")
        Me.ActiveControl = Nothing
    End Sub

    Public Sub ActionBtnSeigyo(ByVal bool As Boolean)  '歩行盤のボタンをつかえなくしたりする。
        SaveBtn.Enabled = bool
        TalkBtn.Enabled = bool
        KaifukuBtn.Enabled = bool
        StoryBtn.Enabled = bool
        WalkBtn.Enabled = bool
        BackBtn.Enabled = bool

        ItemBtn.Enabled = bool
        PassiveSkillCheckBtn.Enabled = bool
    End Sub

    Private Sub ItemUseBtn_Click(sender As Object, e As EventArgs) Handles ItemUseBtn.Click
        If ItemListBox.SelectedIndex < 0 Then   '-1の時、アイテムリストボックスには項目がない。
            ItemStrLbl.Text = "アイテムが選ばれてません。"
            '色戻し
            ItemStrLbl.BackColor = Color.DodgerBlue
            ItemStrLbl.ForeColor = Color.Wheat
            My.Computer.Audio.Play(Application.StartupPath & "\sound\error.wav")
        ElseIf ItemList.Item(ItemListBox.SelectedIndex).ItemCate = ItemCate.Battle AndAlso Not Common.GamePhase = GamePhases.BattleItem Then
            '選択したアイテムが戦闘用で、尚且つ状況が戦闘時出なかった場合。

            ItemStrLbl.Text = "戦ってる時にしか使えません。"
            '色戻し
            ItemStrLbl.BackColor = Color.DodgerBlue
            ItemStrLbl.ForeColor = Color.Wheat
            My.Computer.Audio.Play(Application.StartupPath & "\sound\error.wav")
        Else
            Dim Temp_Str As String = ""

            My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")

            ItemList.Item(ItemListBox.SelectedIndex).UseableTimes -= 1

            If Common.GamePhase = GamePhases.BattleItem Then        '戦闘中アイテム使用の場合
                Units.Add(New MoveStr(ItemList.Item(ItemListBox.SelectedIndex).Use & "{残り回数 " & ItemList.Item(ItemListBox.SelectedIndex).UseableTimes & "}"))
            Else
                Temp_Str = ItemList.Item(ItemListBox.SelectedIndex).Use & "{残り回数 " & ItemList.Item(ItemListBox.SelectedIndex).UseableTimes & "}"
            End If

            If ItemList.Item(ItemListBox.SelectedIndex).UseableTimes <= 0 Then
                Dim Temp_Index As Integer = ItemListBox.SelectedIndex

                ItemList.RemoveAt(ItemListBox.SelectedIndex)

                ItemListBox.Items.RemoveAt(ItemListBox.SelectedIndex)

                If ItemListBox.Items.Count > 0 Then
                    If Temp_Index = 0 Then
                        ItemListBox.SelectedIndex = 0
                    Else
                        ItemListBox.SelectedIndex = Temp_Index - 1
                    End If
                End If
            End If


            ItemStrLbl.Text = Temp_Str

            '色変え
            ItemStrLbl.BackColor = Color.Cyan
            ItemStrLbl.ForeColor = Color.Green

            If Common.GamePhase = GamePhases.BattleItem Then        '戦闘中アイテム使用の場合
                ItemPanelBackBtn.PerformClick()
            End If


        End If

        Me.ActiveControl = Nothing
    End Sub

    Private Sub ItemListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ItemListBox.SelectedIndexChanged
        If ItemListBox.SelectedIndex >= 0 Then
            ItemStrLbl.Text = ItemList.Item(ItemListBox.SelectedIndex).SetumeiStr & "{rarity " & ItemList.Item(ItemListBox.SelectedIndex).rarity & ",使用可能回数" & ItemList.Item(ItemListBox.SelectedIndex).UseableTimes & "回}"
        End If

        '色戻し
        ItemStrLbl.BackColor = Color.DodgerBlue
        ItemStrLbl.ForeColor = Color.Wheat

    End Sub
    Private Sub SelectSkillBtn()
        SkySkillBtn.Visible = False
        NormalSkill2Btn.Visible = False
        NormalSkill3Btn.Visible = False
        SkySkillRecover1Btn.Visible = False
        SkySkillRecover2Btn.Visible = False

        If Common.MyStat.ElementalValue(Elemental.Normal) >= 2 Then
            NormalSkill2Btn.Visible = True '実刀コマンドは平凡2以上で使える。
        End If
        If Common.MyStat.ElementalValue(Elemental.Normal) >= 10 AndAlso Common.MyStat.ElementalValue(Elemental.Sky) >= 1 Then
            NormalSkill3Btn.Visible = True '風剣コマンドは平凡10以上と青空1以上で使える。
        End If

        If Common.MyStat.ElementalValue(Elemental.Sky) >= 2 Then
            SkySkillRecover1Btn.Visible = True '日照コマンドは青空2以上で使える。 また、ここから青空ボタンの制御
            SkySkillBtn.Visible = True
        End If

        If Common.MyStat.ElementalValue(Elemental.Sky) >= 5 Then
            SkySkillRecover2Btn.Visible = True '日照コマンドは青空5以上で使える。
        End If

    End Sub

    Private Sub NormalBackBtn_Click(sender As Object, e As EventArgs) Handles NormalBackBtn.Click
        NormalSkillPanel.Visible = False
        MyturnPanel.Visible = True
        My.Computer.Audio.Play(Application.StartupPath & "\sound\h.wav")
        Me.ActiveControl = Nothing
    End Sub
    Private Sub SkyBackBtn_Click(sender As Object, e As EventArgs) Handles SkyBackBtn.Click
        SkySkillPanel.Visible = False
        MyturnPanel.Visible = True
        My.Computer.Audio.Play(Application.StartupPath & "\sound\h.wav")
        Me.ActiveControl = Nothing
    End Sub

    Private Sub NormalSkillBtn_Click(sender As Object, e As EventArgs) Handles NormalSkillBtn.Click
        NormalSkillPanel.Visible = True
        MyturnPanel.Visible = False
        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
        Me.ActiveControl = Nothing
    End Sub
    Private Sub SkySkillBtn_Click(sender As Object, e As EventArgs) Handles SkySkillBtn.Click
        SkySkillPanel.Visible = True
        MyturnPanel.Visible = False
        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
        Me.ActiveControl = Nothing
    End Sub


    Private Sub NormalATKBtn_MouseEnter(sender As Object, e As EventArgs) Handles NormalATKBtn.MouseEnter
        SkillStatesIndex = 0
        skillDrawF = True
    End Sub
    Private Sub NormalSkill2Btn_MouseEnter(sender As Object, e As EventArgs) Handles NormalSkill2Btn.MouseEnter
        SkillStatesIndex = 1
        skillDrawF = True
    End Sub
    Private Sub NormalSkill3Btn_MouseEnter(sender As Object, e As EventArgs) Handles NormalSkill3Btn.MouseEnter
        SkillStatesIndex = 5
        skillDrawF = True
    End Sub

    Private Sub ItemSkillBtn_MouseEnter(sender As Object, e As EventArgs) Handles ItemSkillBtn.MouseEnter
        SkillStatesIndex = 2
        skillDrawF = True
    End Sub
    Private Sub SkySkillRecover1Btn_MouseEnter(sender As Object, e As EventArgs) Handles SkySkillRecover1Btn.MouseEnter
        SkillStatesIndex = 3
        skillDrawF = True
    End Sub
    Private Sub SkySkillRecover2Btn_MouseEnter(sender As Object, e As EventArgs) Handles SkySkillRecover2Btn.MouseEnter
        SkillStatesIndex = 4
        skillDrawF = True
    End Sub
    Private Sub SkillBtnS(ByVal index As Integer)   'スキル使用ボタンプロシージャに入れる処理をまとめたメゾット
        If Common.MyStat.Skill(8, index) Then
            Myturn(index)
        Else
            Units.Add(New MoveStr(Common.MyStat.Skill(10, index)))
        End If
        Me.ActiveControl = Nothing
    End Sub
    Private Sub NormalATKBtn_Click(sender As Object, e As EventArgs) Handles NormalATKBtn.Click     '平凡通常攻撃
        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
        SkillBtnS(0)
    End Sub

    Private Sub NormalSkill2Btn_Click(sender As Object, e As EventArgs) Handles NormalSkill2Btn.Click   '実刀
        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
        SkillBtnS(1)
    End Sub
    Private Sub NormalSkill3Btn_Click(sender As Object, e As EventArgs) Handles NormalSkill3Btn.Click
        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
        SkillBtnS(5)
    End Sub

    Private Sub ItemSkillBtn_Click(sender As Object, e As EventArgs) Handles ItemSkillBtn.Click 'ｱｲﾃﾑ使用技
        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
        SkillBtnS(2)
    End Sub
    Private Sub SkySkillRecover1Btn_Click(sender As Object, e As EventArgs) Handles SkySkillRecover1Btn.Click
        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
        SkillBtnS(3)
    End Sub

    Private Sub SkySkillRecover2Btn_Click(sender As Object, e As EventArgs) Handles SkySkillRecover2Btn.Click
        My.Computer.Audio.Play(Application.StartupPath & "\sound\walk.wav")
        SkillBtnS(4)
    End Sub

End Class
