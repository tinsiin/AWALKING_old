<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.WalkBtn = New System.Windows.Forms.Button()
        Me.BackBtn = New System.Windows.Forms.Button()
        Me.KaifukuBtn = New System.Windows.Forms.Button()
        Me.SaveBtn = New System.Windows.Forms.Button()
        Me.NewStartBtn = New System.Windows.Forms.Button()
        Me.TalkBtn = New System.Windows.Forms.Button()
        Me.RunFromEnemyBtn = New System.Windows.Forms.Button()
        Me.NormalATKBtn = New System.Windows.Forms.Button()
        Me.EnemyBattleBtn = New System.Windows.Forms.Button()
        Me.MyturnPanel = New System.Windows.Forms.Panel()
        Me.SkySkillBtn = New System.Windows.Forms.Button()
        Me.NormalSkillBtn = New System.Windows.Forms.Button()
        Me.MyBattleBtn = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.EraceSaveBtn = New System.Windows.Forms.Button()
        Me.EventPanel = New System.Windows.Forms.Panel()
        Me.EveBtn5 = New System.Windows.Forms.Button()
        Me.EveBtn4 = New System.Windows.Forms.Button()
        Me.EveBtn3 = New System.Windows.Forms.Button()
        Me.EveBtn2 = New System.Windows.Forms.Button()
        Me.EveBtn1 = New System.Windows.Forms.Button()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.StoryBtn = New System.Windows.Forms.Button()
        Me.ItemListBox = New System.Windows.Forms.ListBox()
        Me.ItemPanel = New System.Windows.Forms.Panel()
        Me.ItemStrLbl = New System.Windows.Forms.Label()
        Me.ItemUseBtn = New System.Windows.Forms.Button()
        Me.ItemPanelBackBtn = New System.Windows.Forms.Button()
        Me.ItemLabel = New System.Windows.Forms.Label()
        Me.ShopPanel = New System.Windows.Forms.Panel()
        Me.ShopStrLbl = New System.Windows.Forms.Label()
        Me.BuyBtn = New System.Windows.Forms.Button()
        Me.ShopPanelBackBtn = New System.Windows.Forms.Button()
        Me.ShopListBox = New System.Windows.Forms.ListBox()
        Me.ShopLbl = New System.Windows.Forms.Label()
        Me.ShopRankLbl = New System.Windows.Forms.Label()
        Me.ShopMoneyLbl = New System.Windows.Forms.Label()
        Me.ItemBtn = New System.Windows.Forms.Button()
        Me.NormalSkillPanel = New System.Windows.Forms.Panel()
        Me.NormalSkill3Btn = New System.Windows.Forms.Button()
        Me.NormalBackBtn = New System.Windows.Forms.Button()
        Me.NormalSkill2Btn = New System.Windows.Forms.Button()
        Me.ItemSkillBtn = New System.Windows.Forms.Button()
        Me.SkySkillPanel = New System.Windows.Forms.Panel()
        Me.SkySkillRecover2Btn = New System.Windows.Forms.Button()
        Me.SkyBackBtn = New System.Windows.Forms.Button()
        Me.SkySkillRecover1Btn = New System.Windows.Forms.Button()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.PassiveSkillCheckBtn = New System.Windows.Forms.Button()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.MyturnPanel.SuspendLayout()
        Me.EventPanel.SuspendLayout()
        Me.ItemPanel.SuspendLayout()
        Me.ShopPanel.SuspendLayout()
        Me.NormalSkillPanel.SuspendLayout()
        Me.SkySkillPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 6
        '
        'WalkBtn
        '
        Me.WalkBtn.Location = New System.Drawing.Point(2, 272)
        Me.WalkBtn.Margin = New System.Windows.Forms.Padding(2)
        Me.WalkBtn.Name = "WalkBtn"
        Me.WalkBtn.Size = New System.Drawing.Size(36, 82)
        Me.WalkBtn.TabIndex = 0
        Me.WalkBtn.TabStop = False
        Me.WalkBtn.Text = "一歩前進"
        Me.WalkBtn.UseVisualStyleBackColor = True
        Me.WalkBtn.Visible = False
        '
        'BackBtn
        '
        Me.BackBtn.BackColor = System.Drawing.Color.White
        Me.BackBtn.Location = New System.Drawing.Point(43, 272)
        Me.BackBtn.Margin = New System.Windows.Forms.Padding(2)
        Me.BackBtn.Name = "BackBtn"
        Me.BackBtn.Size = New System.Drawing.Size(36, 82)
        Me.BackBtn.TabIndex = 1
        Me.BackBtn.TabStop = False
        Me.BackBtn.Text = "二歩後退"
        Me.BackBtn.UseVisualStyleBackColor = False
        Me.BackBtn.Visible = False
        '
        'KaifukuBtn
        '
        Me.KaifukuBtn.Location = New System.Drawing.Point(135, 330)
        Me.KaifukuBtn.Margin = New System.Windows.Forms.Padding(2)
        Me.KaifukuBtn.Name = "KaifukuBtn"
        Me.KaifukuBtn.Size = New System.Drawing.Size(53, 28)
        Me.KaifukuBtn.TabIndex = 2
        Me.KaifukuBtn.TabStop = False
        Me.KaifukuBtn.Text = "休憩"
        Me.KaifukuBtn.UseVisualStyleBackColor = True
        Me.KaifukuBtn.Visible = False
        '
        'SaveBtn
        '
        Me.SaveBtn.Location = New System.Drawing.Point(193, 330)
        Me.SaveBtn.Margin = New System.Windows.Forms.Padding(2)
        Me.SaveBtn.Name = "SaveBtn"
        Me.SaveBtn.Size = New System.Drawing.Size(53, 28)
        Me.SaveBtn.TabIndex = 3
        Me.SaveBtn.TabStop = False
        Me.SaveBtn.Text = "セーブ"
        Me.SaveBtn.UseVisualStyleBackColor = True
        Me.SaveBtn.Visible = False
        '
        'NewStartBtn
        '
        Me.NewStartBtn.BackColor = System.Drawing.Color.White
        Me.NewStartBtn.Location = New System.Drawing.Point(430, 213)
        Me.NewStartBtn.Margin = New System.Windows.Forms.Padding(2)
        Me.NewStartBtn.Name = "NewStartBtn"
        Me.NewStartBtn.Size = New System.Drawing.Size(91, 39)
        Me.NewStartBtn.TabIndex = 5
        Me.NewStartBtn.TabStop = False
        Me.NewStartBtn.Text = "始める"
        Me.NewStartBtn.UseVisualStyleBackColor = False
        '
        'TalkBtn
        '
        Me.TalkBtn.Location = New System.Drawing.Point(193, 297)
        Me.TalkBtn.Name = "TalkBtn"
        Me.TalkBtn.Size = New System.Drawing.Size(53, 28)
        Me.TalkBtn.TabIndex = 6
        Me.TalkBtn.TabStop = False
        Me.TalkBtn.Text = "会話"
        Me.TalkBtn.UseVisualStyleBackColor = True
        Me.TalkBtn.Visible = False
        '
        'RunFromEnemyBtn
        '
        Me.RunFromEnemyBtn.Location = New System.Drawing.Point(3, 114)
        Me.RunFromEnemyBtn.Name = "RunFromEnemyBtn"
        Me.RunFromEnemyBtn.Size = New System.Drawing.Size(53, 28)
        Me.RunFromEnemyBtn.TabIndex = 9
        Me.RunFromEnemyBtn.TabStop = False
        Me.RunFromEnemyBtn.Text = "逃げる"
        Me.RunFromEnemyBtn.UseVisualStyleBackColor = True
        '
        'NormalATKBtn
        '
        Me.NormalATKBtn.Location = New System.Drawing.Point(3, 7)
        Me.NormalATKBtn.Name = "NormalATKBtn"
        Me.NormalATKBtn.Size = New System.Drawing.Size(53, 28)
        Me.NormalATKBtn.TabIndex = 10
        Me.NormalATKBtn.TabStop = False
        Me.NormalATKBtn.Text = "攻"
        Me.NormalATKBtn.UseVisualStyleBackColor = True
        '
        'EnemyBattleBtn
        '
        Me.EnemyBattleBtn.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point)
        Me.EnemyBattleBtn.Location = New System.Drawing.Point(266, 10)
        Me.EnemyBattleBtn.Name = "EnemyBattleBtn"
        Me.EnemyBattleBtn.Size = New System.Drawing.Size(64, 113)
        Me.EnemyBattleBtn.TabIndex = 11
        Me.EnemyBattleBtn.TabStop = False
        Me.EnemyBattleBtn.Text = "↺" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.EnemyBattleBtn.UseVisualStyleBackColor = True
        Me.EnemyBattleBtn.Visible = False
        '
        'MyturnPanel
        '
        Me.MyturnPanel.BackColor = System.Drawing.Color.LightSteelBlue
        Me.MyturnPanel.Controls.Add(Me.SkySkillBtn)
        Me.MyturnPanel.Controls.Add(Me.NormalSkillBtn)
        Me.MyturnPanel.Controls.Add(Me.RunFromEnemyBtn)
        Me.MyturnPanel.Location = New System.Drawing.Point(414, 319)
        Me.MyturnPanel.Margin = New System.Windows.Forms.Padding(2)
        Me.MyturnPanel.Name = "MyturnPanel"
        Me.MyturnPanel.Size = New System.Drawing.Size(64, 151)
        Me.MyturnPanel.TabIndex = 12
        Me.MyturnPanel.Visible = False
        '
        'SkySkillBtn
        '
        Me.SkySkillBtn.Location = New System.Drawing.Point(3, 41)
        Me.SkySkillBtn.Name = "SkySkillBtn"
        Me.SkySkillBtn.Size = New System.Drawing.Size(53, 28)
        Me.SkySkillBtn.TabIndex = 23
        Me.SkySkillBtn.TabStop = False
        Me.SkySkillBtn.Text = "青空"
        Me.SkySkillBtn.UseVisualStyleBackColor = True
        '
        'NormalSkillBtn
        '
        Me.NormalSkillBtn.Location = New System.Drawing.Point(3, 7)
        Me.NormalSkillBtn.Name = "NormalSkillBtn"
        Me.NormalSkillBtn.Size = New System.Drawing.Size(53, 28)
        Me.NormalSkillBtn.TabIndex = 11
        Me.NormalSkillBtn.TabStop = False
        Me.NormalSkillBtn.Text = "平凡"
        Me.NormalSkillBtn.UseVisualStyleBackColor = True
        '
        'MyBattleBtn
        '
        Me.MyBattleBtn.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point)
        Me.MyBattleBtn.Location = New System.Drawing.Point(266, 10)
        Me.MyBattleBtn.Name = "MyBattleBtn"
        Me.MyBattleBtn.Size = New System.Drawing.Size(64, 113)
        Me.MyBattleBtn.TabIndex = 13
        Me.MyBattleBtn.TabStop = False
        Me.MyBattleBtn.Text = "↺" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.MyBattleBtn.UseVisualStyleBackColor = True
        Me.MyBattleBtn.Visible = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.BackColor = System.Drawing.Color.White
        Me.CheckBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CheckBox1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.CheckBox1.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.CheckBox1.Location = New System.Drawing.Point(444, 272)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(89, 19)
        Me.CheckBox1.TabIndex = 14
        Me.CheckBox1.TabStop = False
        Me.CheckBox1.Text = "終了確認"
        Me.CheckBox1.UseVisualStyleBackColor = False
        '
        'EraceSaveBtn
        '
        Me.EraceSaveBtn.BackColor = System.Drawing.Color.White
        Me.EraceSaveBtn.Location = New System.Drawing.Point(417, 181)
        Me.EraceSaveBtn.Margin = New System.Windows.Forms.Padding(2)
        Me.EraceSaveBtn.Name = "EraceSaveBtn"
        Me.EraceSaveBtn.Size = New System.Drawing.Size(104, 20)
        Me.EraceSaveBtn.TabIndex = 15
        Me.EraceSaveBtn.TabStop = False
        Me.EraceSaveBtn.Text = "データ消去"
        Me.EraceSaveBtn.UseVisualStyleBackColor = False
        '
        'EventPanel
        '
        Me.EventPanel.BackColor = System.Drawing.SystemColors.Info
        Me.EventPanel.Controls.Add(Me.EveBtn5)
        Me.EventPanel.Controls.Add(Me.EveBtn4)
        Me.EventPanel.Controls.Add(Me.EveBtn3)
        Me.EventPanel.Controls.Add(Me.EveBtn2)
        Me.EventPanel.Controls.Add(Me.EveBtn1)
        Me.EventPanel.Location = New System.Drawing.Point(915, 433)
        Me.EventPanel.Margin = New System.Windows.Forms.Padding(2)
        Me.EventPanel.Name = "EventPanel"
        Me.EventPanel.Size = New System.Drawing.Size(55, 194)
        Me.EventPanel.TabIndex = 16
        Me.EventPanel.Visible = False
        '
        'EveBtn5
        '
        Me.EveBtn5.Font = New System.Drawing.Font("MS UI Gothic", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.EveBtn5.Location = New System.Drawing.Point(0, 146)
        Me.EveBtn5.Margin = New System.Windows.Forms.Padding(2)
        Me.EveBtn5.Name = "EveBtn5"
        Me.EveBtn5.Size = New System.Drawing.Size(55, 38)
        Me.EveBtn5.TabIndex = 4
        Me.EveBtn5.TabStop = False
        Me.EveBtn5.Text = "Button1"
        Me.EveBtn5.UseVisualStyleBackColor = True
        '
        'EveBtn4
        '
        Me.EveBtn4.Font = New System.Drawing.Font("MS UI Gothic", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.EveBtn4.Location = New System.Drawing.Point(0, 110)
        Me.EveBtn4.Margin = New System.Windows.Forms.Padding(2)
        Me.EveBtn4.Name = "EveBtn4"
        Me.EveBtn4.Size = New System.Drawing.Size(55, 38)
        Me.EveBtn4.TabIndex = 3
        Me.EveBtn4.TabStop = False
        Me.EveBtn4.Text = "Button1"
        Me.EveBtn4.UseVisualStyleBackColor = True
        '
        'EveBtn3
        '
        Me.EveBtn3.Font = New System.Drawing.Font("MS UI Gothic", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.EveBtn3.Location = New System.Drawing.Point(0, 74)
        Me.EveBtn3.Margin = New System.Windows.Forms.Padding(2)
        Me.EveBtn3.Name = "EveBtn3"
        Me.EveBtn3.Size = New System.Drawing.Size(55, 38)
        Me.EveBtn3.TabIndex = 2
        Me.EveBtn3.TabStop = False
        Me.EveBtn3.Text = "Button1"
        Me.EveBtn3.UseVisualStyleBackColor = True
        '
        'EveBtn2
        '
        Me.EveBtn2.Font = New System.Drawing.Font("MS UI Gothic", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.EveBtn2.Location = New System.Drawing.Point(0, 40)
        Me.EveBtn2.Margin = New System.Windows.Forms.Padding(2)
        Me.EveBtn2.Name = "EveBtn2"
        Me.EveBtn2.Size = New System.Drawing.Size(55, 38)
        Me.EveBtn2.TabIndex = 1
        Me.EveBtn2.TabStop = False
        Me.EveBtn2.Text = "Button1"
        Me.EveBtn2.UseVisualStyleBackColor = True
        '
        'EveBtn1
        '
        Me.EveBtn1.Font = New System.Drawing.Font("MS UI Gothic", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.EveBtn1.Location = New System.Drawing.Point(0, 2)
        Me.EveBtn1.Margin = New System.Windows.Forms.Padding(2)
        Me.EveBtn1.Name = "EveBtn1"
        Me.EveBtn1.Size = New System.Drawing.Size(55, 38)
        Me.EveBtn1.TabIndex = 0
        Me.EveBtn1.TabStop = False
        Me.EveBtn1.Text = "Button1"
        Me.EveBtn1.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.BackColor = System.Drawing.Color.White
        Me.CheckBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CheckBox3.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.CheckBox3.ForeColor = System.Drawing.Color.Crimson
        Me.CheckBox3.Location = New System.Drawing.Point(0, 295)
        Me.CheckBox3.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(135, 19)
        Me.CheckBox3.TabIndex = 26
        Me.CheckBox3.TabStop = False
        Me.CheckBox3.Text = "悲劇の災禍モード"
        Me.CheckBox3.UseVisualStyleBackColor = False
        '
        'StoryBtn
        '
        Me.StoryBtn.Location = New System.Drawing.Point(135, 297)
        Me.StoryBtn.Margin = New System.Windows.Forms.Padding(2)
        Me.StoryBtn.Name = "StoryBtn"
        Me.StoryBtn.Size = New System.Drawing.Size(53, 28)
        Me.StoryBtn.TabIndex = 17
        Me.StoryBtn.TabStop = False
        Me.StoryBtn.Text = "ｽﾄｰﾘｰ"
        Me.StoryBtn.UseVisualStyleBackColor = True
        Me.StoryBtn.Visible = False
        '
        'ItemListBox
        '
        Me.ItemListBox.FormattingEnabled = True
        Me.ItemListBox.ItemHeight = 15
        Me.ItemListBox.Location = New System.Drawing.Point(12, 5)
        Me.ItemListBox.Name = "ItemListBox"
        Me.ItemListBox.Size = New System.Drawing.Size(108, 199)
        Me.ItemListBox.TabIndex = 18
        '
        'ItemPanel
        '
        Me.ItemPanel.BackColor = System.Drawing.Color.Linen
        Me.ItemPanel.Controls.Add(Me.ItemStrLbl)
        Me.ItemPanel.Controls.Add(Me.ItemUseBtn)
        Me.ItemPanel.Controls.Add(Me.ItemPanelBackBtn)
        Me.ItemPanel.Controls.Add(Me.ItemListBox)
        Me.ItemPanel.Controls.Add(Me.ItemLabel)
        Me.ItemPanel.Location = New System.Drawing.Point(0, 0)
        Me.ItemPanel.Name = "ItemPanel"
        Me.ItemPanel.Size = New System.Drawing.Size(250, 250)
        Me.ItemPanel.TabIndex = 19
        Me.ItemPanel.Visible = False
        '
        'ItemStrLbl
        '
        Me.ItemStrLbl.BackColor = System.Drawing.Color.DodgerBlue
        Me.ItemStrLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ItemStrLbl.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.ItemStrLbl.ForeColor = System.Drawing.Color.Wheat
        Me.ItemStrLbl.Location = New System.Drawing.Point(126, 5)
        Me.ItemStrLbl.Name = "ItemStrLbl"
        Me.ItemStrLbl.Size = New System.Drawing.Size(120, 196)
        Me.ItemStrLbl.TabIndex = 22
        Me.ItemStrLbl.Text = "TLOAがあなたの心そのものであるならば、その性質で自らの状況が制御されつつあるのは、仕方のないこと。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "オレとあなたは表裏一体です。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'ItemUseBtn
        '
        Me.ItemUseBtn.Location = New System.Drawing.Point(173, 210)
        Me.ItemUseBtn.Name = "ItemUseBtn"
        Me.ItemUseBtn.Size = New System.Drawing.Size(73, 37)
        Me.ItemUseBtn.TabIndex = 21
        Me.ItemUseBtn.TabStop = False
        Me.ItemUseBtn.Text = "使用"
        Me.ItemUseBtn.UseVisualStyleBackColor = True
        '
        'ItemPanelBackBtn
        '
        Me.ItemPanelBackBtn.Location = New System.Drawing.Point(3, 210)
        Me.ItemPanelBackBtn.Name = "ItemPanelBackBtn"
        Me.ItemPanelBackBtn.Size = New System.Drawing.Size(73, 37)
        Me.ItemPanelBackBtn.TabIndex = 20
        Me.ItemPanelBackBtn.TabStop = False
        Me.ItemPanelBackBtn.Text = "×"
        Me.ItemPanelBackBtn.UseVisualStyleBackColor = True
        '
        'ItemLabel
        '
        Me.ItemLabel.AutoSize = True
        Me.ItemLabel.Font = New System.Drawing.Font("ＭＳ 明朝", 18.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point)
        Me.ItemLabel.Location = New System.Drawing.Point(-10, 17)
        Me.ItemLabel.Name = "ItemLabel"
        Me.ItemLabel.Size = New System.Drawing.Size(28, 120)
        Me.ItemLabel.TabIndex = 19
        Me.ItemLabel.Text = "I" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "T" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "E" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "M" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'ShopPanel
        '
        Me.ShopPanel.BackColor = System.Drawing.Color.PaleGreen
        Me.ShopPanel.Controls.Add(Me.ShopStrLbl)
        Me.ShopPanel.Controls.Add(Me.BuyBtn)
        Me.ShopPanel.Controls.Add(Me.ShopPanelBackBtn)
        Me.ShopPanel.Controls.Add(Me.ShopListBox)
        Me.ShopPanel.Controls.Add(Me.ShopLbl)
        Me.ShopPanel.Controls.Add(Me.ShopRankLbl)
        Me.ShopPanel.Controls.Add(Me.ShopMoneyLbl)
        Me.ShopPanel.Location = New System.Drawing.Point(724, 12)
        Me.ShopPanel.Name = "ShopPanel"
        Me.ShopPanel.Size = New System.Drawing.Size(250, 250)
        Me.ShopPanel.TabIndex = 21
        Me.ShopPanel.Visible = False
        '
        'ShopStrLbl
        '
        Me.ShopStrLbl.BackColor = System.Drawing.Color.DodgerBlue
        Me.ShopStrLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ShopStrLbl.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.ShopStrLbl.ForeColor = System.Drawing.Color.Wheat
        Me.ShopStrLbl.Location = New System.Drawing.Point(126, 5)
        Me.ShopStrLbl.Name = "ShopStrLbl"
        Me.ShopStrLbl.Size = New System.Drawing.Size(120, 196)
        Me.ShopStrLbl.TabIndex = 22
        Me.ShopStrLbl.Text = "ランダムアイテムの説明文 （所持数）"
        '
        'BuyBtn
        '
        Me.BuyBtn.Location = New System.Drawing.Point(173, 210)
        Me.BuyBtn.Name = "BuyBtn"
        Me.BuyBtn.Size = New System.Drawing.Size(73, 37)
        Me.BuyBtn.TabIndex = 21
        Me.BuyBtn.TabStop = False
        Me.BuyBtn.Text = "購入"
        Me.BuyBtn.UseVisualStyleBackColor = True
        '
        'ShopPanelBackBtn
        '
        Me.ShopPanelBackBtn.Location = New System.Drawing.Point(3, 210)
        Me.ShopPanelBackBtn.Name = "ShopPanelBackBtn"
        Me.ShopPanelBackBtn.Size = New System.Drawing.Size(73, 37)
        Me.ShopPanelBackBtn.TabIndex = 20
        Me.ShopPanelBackBtn.TabStop = False
        Me.ShopPanelBackBtn.Text = "×"
        Me.ShopPanelBackBtn.UseVisualStyleBackColor = True
        '
        'ShopListBox
        '
        Me.ShopListBox.FormattingEnabled = True
        Me.ShopListBox.ItemHeight = 15
        Me.ShopListBox.Location = New System.Drawing.Point(12, 5)
        Me.ShopListBox.Name = "ShopListBox"
        Me.ShopListBox.Size = New System.Drawing.Size(108, 199)
        Me.ShopListBox.TabIndex = 18
        '
        'ShopLbl
        '
        Me.ShopLbl.AutoSize = True
        Me.ShopLbl.Font = New System.Drawing.Font("ＭＳ 明朝", 18.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point)
        Me.ShopLbl.Location = New System.Drawing.Point(-10, 17)
        Me.ShopLbl.Name = "ShopLbl"
        Me.ShopLbl.Size = New System.Drawing.Size(43, 150)
        Me.ShopLbl.TabIndex = 19
        Me.ShopLbl.Text = "E" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "C" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "サ" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "イ" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ト"
        '
        'ShopRankLbl
        '
        Me.ShopRankLbl.AutoSize = True
        Me.ShopRankLbl.Font = New System.Drawing.Font("MS UI Gothic", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.ShopRankLbl.Location = New System.Drawing.Point(87, 211)
        Me.ShopRankLbl.Name = "ShopRankLbl"
        Me.ShopRankLbl.Size = New System.Drawing.Size(80, 13)
        Me.ShopRankLbl.TabIndex = 23
        Me.ShopRankLbl.Text = "ショップRank:1"
        '
        'ShopMoneyLbl
        '
        Me.ShopMoneyLbl.AutoSize = True
        Me.ShopMoneyLbl.Font = New System.Drawing.Font("MS UI Gothic", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.ShopMoneyLbl.Location = New System.Drawing.Point(87, 228)
        Me.ShopMoneyLbl.Name = "ShopMoneyLbl"
        Me.ShopMoneyLbl.Size = New System.Drawing.Size(77, 13)
        Me.ShopMoneyLbl.TabIndex = 24
        Me.ShopMoneyLbl.Text = "値段:1209円"
        '
        'ItemBtn
        '
        Me.ItemBtn.Location = New System.Drawing.Point(393, 66)
        Me.ItemBtn.Margin = New System.Windows.Forms.Padding(2)
        Me.ItemBtn.Name = "ItemBtn"
        Me.ItemBtn.Size = New System.Drawing.Size(68, 41)
        Me.ItemBtn.TabIndex = 20
        Me.ItemBtn.TabStop = False
        Me.ItemBtn.Text = "アイテム"
        Me.ItemBtn.UseVisualStyleBackColor = True
        Me.ItemBtn.Visible = False
        '
        'NormalSkillPanel
        '
        Me.NormalSkillPanel.BackColor = System.Drawing.Color.LightSteelBlue
        Me.NormalSkillPanel.Controls.Add(Me.NormalSkill3Btn)
        Me.NormalSkillPanel.Controls.Add(Me.NormalBackBtn)
        Me.NormalSkillPanel.Controls.Add(Me.NormalATKBtn)
        Me.NormalSkillPanel.Controls.Add(Me.NormalSkill2Btn)
        Me.NormalSkillPanel.Controls.Add(Me.ItemSkillBtn)
        Me.NormalSkillPanel.Location = New System.Drawing.Point(585, 319)
        Me.NormalSkillPanel.Margin = New System.Windows.Forms.Padding(2)
        Me.NormalSkillPanel.Name = "NormalSkillPanel"
        Me.NormalSkillPanel.Size = New System.Drawing.Size(64, 208)
        Me.NormalSkillPanel.TabIndex = 22
        Me.NormalSkillPanel.Visible = False
        '
        'NormalSkill3Btn
        '
        Me.NormalSkill3Btn.Location = New System.Drawing.Point(3, 52)
        Me.NormalSkill3Btn.Name = "NormalSkill3Btn"
        Me.NormalSkill3Btn.Size = New System.Drawing.Size(53, 28)
        Me.NormalSkill3Btn.TabIndex = 24
        Me.NormalSkill3Btn.TabStop = False
        Me.NormalSkill3Btn.Text = "風剣"
        Me.NormalSkill3Btn.UseVisualStyleBackColor = True
        '
        'NormalBackBtn
        '
        Me.NormalBackBtn.Location = New System.Drawing.Point(3, 177)
        Me.NormalBackBtn.Name = "NormalBackBtn"
        Me.NormalBackBtn.Size = New System.Drawing.Size(53, 28)
        Me.NormalBackBtn.TabIndex = 9
        Me.NormalBackBtn.TabStop = False
        Me.NormalBackBtn.Text = "モドル"
        Me.NormalBackBtn.UseVisualStyleBackColor = True
        '
        'NormalSkill2Btn
        '
        Me.NormalSkill2Btn.Location = New System.Drawing.Point(3, 31)
        Me.NormalSkill2Btn.Name = "NormalSkill2Btn"
        Me.NormalSkill2Btn.Size = New System.Drawing.Size(53, 28)
        Me.NormalSkill2Btn.TabIndex = 11
        Me.NormalSkill2Btn.TabStop = False
        Me.NormalSkill2Btn.Text = "実刀"
        Me.NormalSkill2Btn.UseVisualStyleBackColor = True
        '
        'ItemSkillBtn
        '
        Me.ItemSkillBtn.Location = New System.Drawing.Point(3, 132)
        Me.ItemSkillBtn.Name = "ItemSkillBtn"
        Me.ItemSkillBtn.Size = New System.Drawing.Size(53, 28)
        Me.ItemSkillBtn.TabIndex = 23
        Me.ItemSkillBtn.TabStop = False
        Me.ItemSkillBtn.Text = "ｱｲﾃﾑ"
        Me.ItemSkillBtn.UseVisualStyleBackColor = True
        '
        'SkySkillPanel
        '
        Me.SkySkillPanel.BackColor = System.Drawing.Color.LightSteelBlue
        Me.SkySkillPanel.Controls.Add(Me.SkySkillRecover2Btn)
        Me.SkySkillPanel.Controls.Add(Me.SkyBackBtn)
        Me.SkySkillPanel.Controls.Add(Me.SkySkillRecover1Btn)
        Me.SkySkillPanel.Location = New System.Drawing.Point(727, 319)
        Me.SkySkillPanel.Margin = New System.Windows.Forms.Padding(2)
        Me.SkySkillPanel.Name = "SkySkillPanel"
        Me.SkySkillPanel.Size = New System.Drawing.Size(64, 151)
        Me.SkySkillPanel.TabIndex = 23
        Me.SkySkillPanel.Visible = False
        '
        'SkySkillRecover2Btn
        '
        Me.SkySkillRecover2Btn.Location = New System.Drawing.Point(3, 27)
        Me.SkySkillRecover2Btn.Name = "SkySkillRecover2Btn"
        Me.SkySkillRecover2Btn.Size = New System.Drawing.Size(53, 28)
        Me.SkySkillRecover2Btn.TabIndex = 11
        Me.SkySkillRecover2Btn.TabStop = False
        Me.SkySkillRecover2Btn.Text = "コミューン"
        Me.SkySkillRecover2Btn.UseVisualStyleBackColor = True
        '
        'SkyBackBtn
        '
        Me.SkyBackBtn.Location = New System.Drawing.Point(3, 114)
        Me.SkyBackBtn.Name = "SkyBackBtn"
        Me.SkyBackBtn.Size = New System.Drawing.Size(53, 28)
        Me.SkyBackBtn.TabIndex = 9
        Me.SkyBackBtn.TabStop = False
        Me.SkyBackBtn.Text = "モドル"
        Me.SkyBackBtn.UseVisualStyleBackColor = True
        '
        'SkySkillRecover1Btn
        '
        Me.SkySkillRecover1Btn.Location = New System.Drawing.Point(3, 7)
        Me.SkySkillRecover1Btn.Name = "SkySkillRecover1Btn"
        Me.SkySkillRecover1Btn.Size = New System.Drawing.Size(53, 28)
        Me.SkySkillRecover1Btn.TabIndex = 10
        Me.SkySkillRecover1Btn.TabStop = False
        Me.SkySkillRecover1Btn.Text = "日照"
        Me.SkySkillRecover1Btn.UseVisualStyleBackColor = True
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 1000
        '
        'PassiveSkillCheckBtn
        '
        Me.PassiveSkillCheckBtn.Location = New System.Drawing.Point(453, 66)
        Me.PassiveSkillCheckBtn.Margin = New System.Windows.Forms.Padding(2)
        Me.PassiveSkillCheckBtn.Name = "PassiveSkillCheckBtn"
        Me.PassiveSkillCheckBtn.Size = New System.Drawing.Size(68, 41)
        Me.PassiveSkillCheckBtn.TabIndex = 24
        Me.PassiveSkillCheckBtn.TabStop = False
        Me.PassiveSkillCheckBtn.Text = "パッシブ"
        Me.PassiveSkillCheckBtn.UseVisualStyleBackColor = True
        Me.PassiveSkillCheckBtn.Visible = False
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.BackColor = System.Drawing.Color.White
        Me.CheckBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CheckBox2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.CheckBox2.ForeColor = System.Drawing.Color.MediumBlue
        Me.CheckBox2.Location = New System.Drawing.Point(430, 158)
        Me.CheckBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(84, 19)
        Me.CheckBox2.TabIndex = 25
        Me.CheckBox2.TabStop = False
        Me.CheckBox2.Text = "MIDI_Off"
        Me.CheckBox2.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(986, 605)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.PassiveSkillCheckBtn)
        Me.Controls.Add(Me.SkySkillPanel)
        Me.Controls.Add(Me.NormalSkillPanel)
        Me.Controls.Add(Me.ShopPanel)
        Me.Controls.Add(Me.EventPanel)
        Me.Controls.Add(Me.EraceSaveBtn)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.NewStartBtn)
        Me.Controls.Add(Me.SaveBtn)
        Me.Controls.Add(Me.KaifukuBtn)
        Me.Controls.Add(Me.BackBtn)
        Me.Controls.Add(Me.WalkBtn)
        Me.Controls.Add(Me.TalkBtn)
        Me.Controls.Add(Me.MyturnPanel)
        Me.Controls.Add(Me.EnemyBattleBtn)
        Me.Controls.Add(Me.StoryBtn)
        Me.Controls.Add(Me.MyBattleBtn)
        Me.Controls.Add(Me.ItemPanel)
        Me.Controls.Add(Me.ItemBtn)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WALKING , 散歩風Rpgゲーム"
        Me.MyturnPanel.ResumeLayout(False)
        Me.EventPanel.ResumeLayout(False)
        Me.ItemPanel.ResumeLayout(False)
        Me.ItemPanel.PerformLayout()
        Me.ShopPanel.ResumeLayout(False)
        Me.ShopPanel.PerformLayout()
        Me.NormalSkillPanel.ResumeLayout(False)
        Me.SkySkillPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents WalkBtn As Button
    Friend WithEvents BackBtn As Button
    Public WithEvents KaifukuBtn As Button
    Public WithEvents SaveBtn As Button
    Friend WithEvents NewStartBtn As Button
    Public WithEvents TalkBtn As Button
    Public WithEvents RunFromEnemyBtn As Button
    Public WithEvents NormalATKBtn As Button
    Public WithEvents EnemyBattleBtn As Button
    Friend WithEvents MyturnPanel As Panel
    Public WithEvents MyBattleBtn As Button
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents EraceSaveBtn As Button
    Friend WithEvents EventPanel As Panel
    Friend WithEvents EveBtn1 As Button
    Friend WithEvents EveBtn5 As Button
    Friend WithEvents EveBtn4 As Button
    Friend WithEvents EveBtn3 As Button
    Friend WithEvents EveBtn2 As Button
    Friend WithEvents ItemPanel As Panel
    Friend WithEvents ItemLabel As Label
    Friend WithEvents ItemUseBtn As Button
    Friend WithEvents ItemPanelBackBtn As Button
    Friend WithEvents ItemStrLbl As Label
    Public WithEvents StoryBtn As Button
    Friend WithEvents ItemListBox As ListBox
    Friend WithEvents ShopPanel As Panel
    Friend WithEvents ShopStrLbl As Label
    Friend WithEvents BuyBtn As Button
    Friend WithEvents ShopPanelBackBtn As Button
    Friend WithEvents ShopListBox As ListBox
    Friend WithEvents ShopLbl As Label
    Friend WithEvents ShopRankLbl As Label
    Friend WithEvents ShopMoneyLbl As Label
    Public WithEvents NormalSkillBtn As Button
    Friend WithEvents NormalSkillPanel As Panel
    Public WithEvents NormalBackBtn As Button
    Public WithEvents NormalSkill2Btn As Button
    Public WithEvents ItemBtn As Button
    Public WithEvents ItemSkillBtn As Button
    Public WithEvents SkySkillBtn As Button
    Friend WithEvents SkySkillPanel As Panel
    Public WithEvents SkyBackBtn As Button
    Public WithEvents SkySkillRecover1Btn As Button
    Friend WithEvents Timer2 As Timer
    Public WithEvents SkySkillRecover2Btn As Button
    Public WithEvents PassiveSkillCheckBtn As Button
    Friend WithEvents CheckBox2 As CheckBox
    Public WithEvents NormalSkill3Btn As Button
    Friend WithEvents CheckBox3 As CheckBox
End Class
