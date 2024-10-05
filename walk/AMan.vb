Imports System.IO
Imports System.Text
Public Class AMan
    Inherits UnitBase
    Public Property WalkPoint As Integer
    Public Property MAX_WalkPoint As Integer = 100

    Public Overrides Property HP As Integer = 0   'HP 体力のこと　これがなくなると死ぬ
    Public Overrides Property MaxHP As Integer = 30    'HP上限
    ' Public Overrides Property ATK As Integer  '攻撃力  A太は攻撃力がElementalとTLOAのパワーで変わるので、これは使わない。
    Public Overrides Property MP As Integer   'MP
    Public Overrides Property MaxMP As Integer = 2   'MP上限
    Public Overrides Property TP As Integer   'TP
    Public Overrides Property MaxTP As Integer = 1    'TP上限
    Public Overrides Property Gold As Integer = 5 '持ってる金
    Public Property EXP As Integer  '経験値
    Public Property MaxEXP As Integer = 100  '上限経験値

    Public Property Combo As Integer 'コンボ数　3以降から出る。　異能×コンボ数で追加ダメージ入る。


    Public Overrides Property Lv As Integer = 1  'レベル
    Public Overrides ReadOnly Property Name As String = "??"  '名前

    Public Property TLOApower As Integer    'TLOAの力
    Public Property MaxTLOA As Integer = 5  'TLOAの力
    Public Property TLOAName As String = "???"  'TLOAの名前

    Public ElementalValue() As Integer = {0, 0, 0, 1, 0, 0, 0, 0}

    Public ElementalExp(7) As Integer


    '左から、　平凡通常攻撃
    Public SkillLevel(5) As Integer                 'スキル追記するたびに個々の数に入力！
    Public SkillMaxLevel() As Integer = {5, 3, 1, 2, 4, 12}

    Public SkillExp(UBound(SkillLevel)) As Integer
    Public SkillMaxExp() As Integer = {15, 10, 1, 231, 17, 15}


    Public ElementalEXPstep As Integer = 5  '属性expの増えていく値

    Private LoadStrs As New List(Of String)        'セーブデータ達

    Public Sub New()
        Me.HP = Me.MaxHP    'ニューゲームの初期化　　saveあるのなら、下にセーブデータからの搬入を行う
        Me.MP = Me.MaxMP

        For i = 0 To UBound(SkillLevel)      'スキルレベルを初期化(1と入れる。)
            SkillLevel(i) = 1
            SkillExp(i) = 0
        Next

        'セーブデータ読込
        If File.Exists(Application.StartupPath & "\text\dat1.txt") Then
            Dim sr As New StreamReader(Application.StartupPath & "\text\dat1.txt", Encoding.GetEncoding("UTF-8"))

            While sr.Peek() > -1
                LoadStrs.Add(sr.ReadLine())
            End While
            sr.Dispose()
            '初期化されたものでなければそのまま入れる
            If LoadStrs.Count >= 3 AndAlso LoadStrs.Item(2) = 0 Then
                Me.HP = LoadStrs.Item(3)
                Me.MaxHP = LoadStrs.Item(4)
                Me.MP = LoadStrs.Item(5)
                Me.MaxMP = LoadStrs.Item(6)
                Me.TP = LoadStrs.Item(7)
                Me.MaxTP = LoadStrs.Item(8)
                Me.Gold = LoadStrs.Item(9)
                Me.EXP = LoadStrs.Item(10)
                Me.WalkPoint = LoadStrs.Item(11)
                Me.Combo = LoadStrs.Item(12)
                Me.Lv = LoadStrs.Item(13)
                Me.MaxTLOA = LoadStrs.Item(14)
                Me.Passive = LoadStrs.Item(15)
                For i = 0 To 7
                    Me.ElementalValue(i) = LoadStrs.Item(16 + i * 2)
                    Me.ElementalExp(i) = LoadStrs.Item(17 + i * 2)
                Next
                For i = 0 To UBound(Me.SkillLevel)
                    Me.SkillLevel(i) = LoadStrs.Item(32 + i * 2)
                    Me.SkillExp(i) = LoadStrs.Item(33 + i * 2)
                Next
            End If
            LoadStrs.Clear()
        End If


    End Sub


    Public Function TalkText()
        Dim str As String = "進め"


        Return str
    End Function

    Public Sub TLOACalc()
        Me.TLOApower = Math.Ceiling(Me.MaxTLOA * (Me.HP / Me.MaxHP))
    End Sub



    ''' <summary>
    ''' 引数に0を指定するとスキル名、1だと素ダメージ、2だと当てた時の文章、3だと命中率、4だと該当スキルの属性、5だとスキル説明、6だとスキル計算式文字列
    ''' 7だと必要なものの文章、8だとスキルの使用可能判定Bool、9だとスキルの種類、10だとスキルの使用失敗文章、11だと攻撃ミスデメリット判定、12だと攻撃ミスデメリット存在時のミス文章、
    ''' 13だと複数攻撃回数、14だと複数攻撃確率、15だと複数攻撃の種類が返ります。
    ''' </summary>
    ''' <returns>string,integer</returns>
    ''' <remarks></remarks>
    Public Overrides Function Skill(ByVal what As Integer, ByVal Index As Integer, Optional Resultdmg As Integer = 0)
        Dim Name As String = "【error】該当スキルがありません。"
        Dim dmg As Integer
        Dim TLOA As Decimal
        Dim EleV As Integer
        Dim HIT As Integer
        Dim Resultstr As String = ""
        Dim SkillEle As Elemental
        Dim category As Skillcategory
        Dim SkillStatesStr As String = ""
        Dim SkillCalcStr As String = ""
        Dim NeedStr As String = ""
        Dim SkillBool As Boolean 'スキル使用判定用
        Dim SkillMistakeStr As String = "入力されていない。" 'スキル失敗文章
        Dim SkillMissdemeritBool As Boolean '　スキルミスデメリット存在判定
        Dim SkillMissdemeritStr As String = "" '　スキルミスデメリット文章
        Dim Fukusuu As Integer = 0
        Dim FukusuuNumber As Integer = 0
        Dim FukusuuKakuritu As Integer = 0


        Select Case Index
            Case 0      '平凡攻撃
                Select Case SkillLevel(Index)
                    Case 1
                        TLOA = 0.3
                        HIT = 70
                    Case 2
                        TLOA = 0.35
                        HIT = 75
                    Case 3
                        TLOA = 0.4
                        HIT = 80
                    Case 4
                        TLOA = 0.45
                        HIT = 85
                    Case 5
                        TLOA = 0.5
                        HIT = 90
                End Select

                Name = "攻撃"
                dmg = (TLOApower * TLOA) + ElementalValue(Elemental.Normal)
                Resultstr = Resultdmg & "ダメージ与えた"
                SkillEle = Elemental.Normal
                category = Skillcategory.Attack

                SkillCalcStr = "[平凡]+TLOA×" & Math.Floor(TLOA * 100) & "%"
                SkillStatesStr = "あなたの強い攻撃の意思が" & vbCrLf & "「TLOA」を通じて被害を与えます。"

                SkillBool = True '判定基準ナシ！　無償で使える。
            Case 1      '平凡強攻撃　実刀
                Select Case SkillLevel(Index)
                    Case 1
                        TLOA = 0.7
                        HIT = 70
                    Case 2
                        TLOA = 0.75
                        HIT = 77
                    Case 3
                        TLOA = 0.8
                        HIT = 86
                End Select

                Name = "実刀"
                dmg = (TLOApower * TLOA) + ElementalValue(Elemental.Normal) * 1.2

                Resultstr = Resultdmg & "ダメージ与えた"
                SkillEle = Elemental.Normal
                category = Skillcategory.Attack

                SkillCalcStr = "[平凡]×1.2+TLOA×" & Math.Floor(TLOA * 100) & "%"
                SkillStatesStr = "本物の刀で切る。危ない。"
                NeedStr = "必要TP: 1"
                SkillMistakeStr = "TPが足りない。"
                SkillMissdemeritBool = True
                SkillMissdemeritStr = "^Miss^(自分にダメージ)"

                If Me.TP >= 1 Then
                    SkillBool = True
                End If
            Case 2      '平凡 アイテムを使う
                HIT = 100

                Name = "ｱｲﾃﾑ"

                Resultstr = "アイテムを使用中"
                category = Skillcategory.MAGIC

                SkillCalcStr = ""
                SkillStatesStr = "戦闘中にアイテムを使用するには、" & vbCrLf & "相手の動きをある程度把握しろ!"
                NeedStr = "必要TP: 1"
                SkillMistakeStr = "TPが足りない。"
                SkillMissdemeritBool = False

                If Me.TP >= 1 Then
                    SkillBool = True
                End If
            Case 3      '青空 　日照　　(回復)
                Select Case SkillLevel(Index)
                    Case 1
                        HIT = 65
                        SkillCalcStr = "[青空]+[太陽]÷5,MP 80%回復"
                        Resultstr = "日光が体に染み込む..." & vbCrLf & " 敵に" & Me.ElementalValue(Elemental.Sky) + Math.Round(Me.ElementalValue(Elemental.Sun) / 5) & "ダメージ与えた。。。"
                    Case 2
                        HIT = 99
                        SkillCalcStr = "[青空]+[太陽],MP 90%回復"
                        Resultstr = "日光が体に染み込む..." & vbCrLf & " 敵に" & Me.ElementalValue(Elemental.Sky) + Me.ElementalValue(Elemental.Sun) & "ダメージ与えた。。。"
                End Select

                Name = "日照"

                category = Skillcategory.MAGIC

                SkillEle = Elemental.Sky
                SkillStatesStr = "溢れる魔力が日光を吸収する。" & vbCrLf & "青空に輝く太陽が貴方を助けます。"
                NeedStr = "必要MP: ある分だけ"
                SkillMistakeStr = "MPがない。"
                SkillMissdemeritBool = False

                If Me.MP >= 1 Then
                    SkillBool = True
                End If
            Case 4      '青空 　コミューン　　(微回復)
                Select Case SkillLevel(Index)
                    Case 1
                        HIT = 75
                        SkillCalcStr = "[青空]+TLOA×40%  , Lv/3回復   "
                    Case 2
                        HIT = 77
                        SkillCalcStr = "[青空]+TLOA×43%  , Lv/2回復   "
                    Case 3
                        HIT = 83
                        SkillCalcStr = "[青空]+TLOA×46%  , Lv/2回復   "
                    Case 4
                        HIT = 93
                        SkillCalcStr = "[青空]+TLOA×63%  , Lv/2回復   "
                End Select

                Resultstr = "バァアア"

                Name = "コミューン"

                category = Skillcategory.MAGIC

                SkillEle = Elemental.Sky
                SkillStatesStr = "青空は世界中の人々が属する。" & vbCrLf & "意識の多いそこから力を借りた魔法。"
                NeedStr = "必要MP: 2"
                SkillMistakeStr = "MPが足りない。"
                SkillMissdemeritBool = False

                If Me.MP >= 2 Then
                    SkillBool = True
                End If
            Case 5      '平凡　ウィンディ　　(風剣)
                Select Case SkillLevel(Index)
                    Case 1
                        TLOA = 0.2
                        HIT = 100
                        EleV = 0.9
                    Case 2
                        TLOA = 0.24
                        HIT = 100
                        EleV = 1
                    Case 3
                        TLOA = 0.27
                        HIT = 100
                        EleV = 1.1
                    Case 4
                        TLOA = 0.3
                        HIT = 99
                        EleV = 1.5
                    Case 5
                        TLOA = 0.33
                        HIT = 99
                        EleV = 2.5
                    Case 6
                        TLOA = 0.36
                        HIT = 99
                        EleV = 2.5
                    Case 7
                        TLOA = 0.39
                        HIT = 98
                        EleV = 3.1
                    Case 8
                        TLOA = 0.41
                        HIT = 98
                        EleV = 3.4
                    Case 9
                        TLOA = 0.42
                        HIT = 98
                        EleV = 3.8
                    Case 10
                        TLOA = 0.45
                        HIT = 96
                        EleV = 4.3
                    Case 11
                        TLOA = 0.5
                        HIT = 97
                        EleV = 4.9
                    Case 12 '最終進化系
                        TLOA = 0.6
                        HIT = 97
                        EleV = 5
                End Select

                Dim wi As String = ""
                Name = "ウィンディ"
                dmg = (TLOApower * TLOA) + ElementalValue(Elemental.Normal) * EleV
                If Common.FukusuuSTR Then
                    wi = "ビュオウ" & vbCrLf
                End If
                Resultstr = wi & Resultdmg & "ダメージ与えた"
                SkillEle = Elemental.Normal
                category = Skillcategory.Attack

                SkillCalcStr = "[平凡]×" & EleV & "+TLOA×" & Math.Floor(TLOA * 100) & "%  ,HP3回復"
                SkillStatesStr = "風を操る風剣。上手く風に沿って！"
                NeedStr = "必要TP: 5　MP: 3"
                SkillMistakeStr = "TP,MPが足りない。"
                SkillMissdemeritBool = False
                Fukusuu = 2
                FukusuuKakuritu = 9

                If Me.TP >= 5 AndAlso Me.MP >= 3 Then
                    SkillBool = True
                End If
        End Select

        Dim GOODSTR As String = ""
        If SkillEle = Me.Elemental Then 'タイプ一致
            If Me.Elemental = Elemental.Normal Then '平凡の場合だけ1.2倍
                dmg *= 1.2
                GOODSTR = "[得意]"
            Else                                         'その他は1.5倍
                dmg *= 1.5
                GOODSTR = "[得意]"
            End If
        End If

        If category = Skillcategory.Attack AndAlso Common.MyStat.Combo >= 3 Then
            Dim Combodmg As Decimal = 1.0
            If SkillEle = Me.Elemental Then 'タイプ一致
                Combodmg = 1.3
            End If
            dmg += Me.ElementalValue(Elemental.PSI) * Combodmg * (Me.Combo - 2)
        End If


        Select Case what
            Case 0
                Return Name
            Case 1
                Return dmg
            Case 2
                If Me.TP < Me.MaxTP Then
                    Me.TP += 1
                End If
                MP_TP_anyUse(Index)
                SubSkill(Index)
                Return GOODSTR + Resultstr
            Case 3
                Return HIT
            Case 4
                Return SkillEle
            Case 5
                Return SkillStatesStr
            Case 6
                Return SkillCalcStr
            Case 7
                Return NeedStr
            Case 8
                Return SkillBool
            Case 9
                Return category
            Case 10
                Return SkillMistakeStr
            Case 11
                Return SkillMissdemeritBool
            Case 12
                Missdemerit(Index)
                Return SkillMissdemeritStr
            Case 13
                Return FukusuuNumber
            Case 14
                Return FukusuuKakuritu
            Case 15
                Return Fukusuu
        End Select

        Return 0
    End Function

    Private Sub Missdemerit(ByVal index As Integer)
        Select Case index
            Case 1
                Me.HP -= Me.Damage(Me.Skill(1, 1) / 2, Me.Skill(4, 1))  '半分のダメージを自分に諸計算込みで喰らう。
        End Select
    End Sub
    Private Sub MP_TP_anyUse(ByVal index As Integer)
        Select Case index
            Case 1      '実刀
                Me.TP -= 1
            Case 2      'アイテム使用
                Me.TP -= 1
            Case 3  '青空　日照
                Me.MP = 0
            Case 4 '青空　コミューン
                Me.MP -= 2
            Case 5 ' 平凡　ウィンディ
                Me.MP -= 3
                Me.TP -= 5
        End Select
    End Sub

    'とても烏滸がましいですがこれは、攻撃属性の追加効果です。 　攻撃中でこれを使う敵が出たらこれ入れましょお
    Private Sub SubSkill(ByVal index As Integer)
        Select Case index
            Case 5      '平凡　ウィンディの追加効果
                Me.HP += 3
        End Select

        If Me.HP > Me.MaxHP Then
            Me.HP = Me.MaxHP
        End If
    End Sub

    Public Overrides Sub MAGICSKILL(ByVal index As Integer)
        Select Case index
            Case 2      'アイテム使用
                Common.GamePhase = GamePhases.BattleItem
                Form1.ItemBtn.Enabled = True
                Form1.ItemBtn.PerformClick()        'アイテムボタン実行
            Case 3 '　青空　日照
                Select Case SkillLevel(3)
                    Case 1
                        Me.HP += Me.MP * 0.8
                        Common.EneMan.HP -= Me.ElementalValue(Elemental.Sky) + Math.Round(Me.ElementalValue(Elemental.Sun) / 5)
                    Case 2
                        Me.HP += Me.MP * 0.9
                        Common.EneMan.HP -= Me.ElementalValue(Elemental.Sky) + Me.ElementalValue(Elemental.Sun)
                End Select

                If HP > MaxHP Then
                    HP = MaxHP
                End If
            Case 4  '青空　コミューン
                Select Case SkillLevel(index)
                    Case 1
                        Me.HP += Me.Lv / 3
                        Common.EneMan.HP -= Me.ElementalValue(Elemental.Sky) + TLOApower * 0.4
                    Case 2
                        Me.HP += Me.Lv / 2
                        Common.EneMan.HP -= Me.ElementalValue(Elemental.Sky) + TLOApower * 0.43
                    Case 2
                        Me.HP += Me.Lv / 2
                        Common.EneMan.HP -= Me.ElementalValue(Elemental.Sky) + TLOApower * 0.46
                    Case 2
                        Me.HP += Me.Lv / 2
                        Common.EneMan.HP -= Me.ElementalValue(Elemental.Sky) + TLOApower * 0.63
                End Select

                If HP > MaxHP Then
                    HP = MaxHP
                End If
        End Select

    End Sub


    Public Overrides Property Elemental As Elemental = Elemental.Normal     '本人の属性

    Public Overrides Property Passive As Passive = Passive.Neutral   '本人の状態異常状態




    Public Sub AreaBtn(ByVal Boll As Boolean)
        If Boll Then
            Form1.KaifukuBtn.Visible = True
            Form1.SaveBtn.Visible = True
            Form1.TalkBtn.Visible = True
            Form1.StoryBtn.Visible = True
        Else
            Form1.KaifukuBtn.Visible = False
            Form1.SaveBtn.Visible = False
            Form1.TalkBtn.Visible = False
            Form1.StoryBtn.Visible = False

        End If
    End Sub
End Class
