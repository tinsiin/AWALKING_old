Public Class Mayoi
    Inherits UnitBase

    Dim ToumeiBr As New SolidBrush(Color.FromArgb(72, 255, 255, 255))

    Public Overrides Property Passive As Passive = Passive.Neutral   '本人の状態異常状態
    Public Overrides Property HP As Integer   'HP 体力のこと　これがなくなると死ぬ
    Public Overrides Property MaxHP As Integer   'HP上限
    Public Overrides Property ATK As Integer   '攻撃力
    Public Overrides Property MaxMP As Integer     'MP上限
    Public Overrides Property MaxTP As Integer   'TP上限
    Public Overrides Property Gold As Integer = Common.Random.Next(2, 5)  '持ってる金 2~4G持ってる。
    Public Overrides Property Lv As Integer  'レベル
    Public Overrides ReadOnly Property Name As String = "迷い人"  '名前

    Private fnt As New Font("MS UI Gothic", 13)
    Private Br1 As New SolidBrush(Color.Black)


    Public Sub New(ByVal Le As Integer)
        Me.Lv = Le

        ATK = 9 + (Lv - 1) * 1.8
        MaxHP = 10 + (Lv - 1) * 8

        Me.MaxMP = Lv / 3
        Me.MaxTP = 4 + Lv / 10


        Me.HP = Me.MaxHP    'HP初期化

        Me.MP = Me.MaxMP
        Me.TP = Common.Random.Next(MaxTP + 1)

        Me.Width = 300
        Me.Height = 300 '仮


        Me.Left = (Common.Display.ViewArea.Width / 2) - Name.Length * fnt.Size / 1.5
        Me.Top = (Common.Display.ViewArea.Height / 2) - 15

    End Sub

    Public Overrides Sub Draw(g As Graphics)
        If Me.HP <= Me.MaxHP / 5 Then
            Br1.Color = Color.DimGray
            If Me.HP <= 0 Then
                Br1.Color = Color.Red
            End If
        Else
            Br1.Color = Color.Black
        End If

        Dim Str As String = ""
        Dim PassiveString As String = ""
        Dim MSTR As String = Me.Name & " Lv." & Me.Lv & vbCrLf & "HP:" & Me.HP & "/" & Me.MaxHP & vbCrLf & "MP:" & Me.MP & "/" & Me.MaxMP & " TP:" & Me.TP & "/" & Me.MaxTP & vbCrLf & "【" & Common.ElementalName(Me.Elemental) & "】"

        If Me.Passive > 0 Then
            PassiveString = Common.PassiveText(Me.Passive, 1)
        End If

        If DamageTime > 0 Then
            If DamageTime Mod 5 = 0 Then
                Str = MSTR
            End If
            DamageTime -= 1
        Else
            Str = MSTR
        End If

        g.FillRectangle(ToumeiBr, Me.Rectangle)
        g.DrawString(Str & vbCrLf & PassiveString, fnt, Br1, Me.Left, Me.Top)
    End Sub

    Public Overrides Function BattleDie() As Boolean
        If Me.HP <= 0 Then
            Return True
        Else
            Return False
        End If
    End Function



    Public Overrides Function Skill(ByVal what As Integer, ByVal Index As Integer, Optional Resultdmg As Integer = 0)
        Dim Name As String = "【error】該当スキルがありません。"
        Dim dmg As Integer
        Dim Resultstr As String = ""
        Dim HIT As Integer
        Dim category As Skillcategory
        Dim Fukusuu As Integer = 0
        Dim FukusuuNumber As Integer = 0
        Dim FukusuuKakuritu As Integer = 0


        Select Case Index
            Case 0      '平凡攻撃
                Name = "抵抗"
                dmg = ATK / 4
                Resultstr = Resultdmg & "ダメージ受けた"
                HIT = 47
                category = Skillcategory.Attack
            Case 1
                Name = "ハイントネイス"        '強魔法
                Resultstr = Resultdmg & "ダメージ受けた"
                HIT = 99
                category = Skillcategory.MAGIC
            Case 2
                Name = "コミューン"
                Resultstr = Name & "は少し回復した。" & vbCrLf & Resultdmg & "ダメージ受けた"
                HIT = 86
                category = Skillcategory.MAGIC
            Case 3
                Name = "ハル" 'コスパ悪い三連魔法
                Resultstr = Resultdmg & "ダメージ受けた"
                HIT = 90
                category = Skillcategory.MAGIC
                Fukusuu = 1
                FukusuuNumber = 3
            Case 4
                Name = "MP吸収"   '該当mpがない場合hpを必要値の半分分吸われる。
                Resultstr = ""
                HIT = 87
                category = Skillcategory.MAGIC
            Case 5
                Name = "ボール"   'コスパのいい技、コミューンより少し強い
                Resultstr = ""
                HIT = 92
                category = Skillcategory.MAGIC
            Case 6      '平凡攻撃
                Name = "強い抵抗"
                dmg = ATK / 2
                Resultstr = Resultdmg & "ダメージ受けた"
                HIT = 53
                category = Skillcategory.Attack

        End Select


        Select Case what
            Case 0
                Return Name
            Case 1
                Return dmg
            Case 2
                If Me.TP < Me.MaxTP Then
                    Me.TP += 1
                End If
                Return Resultstr
            Case 3
                MP_TP_Use(Index)
                Return HIT
            Case 4
                Return category
            Case 8
                Return FukusuuNumber
            Case 9
                Return FukusuuKakuritu
            Case 10
                Return Fukusuu
        End Select

        Return 0
    End Function

    Public Overrides Sub MAGICSKILL(ByVal index As Integer)
        Select Case index
            Case 1 'ハイントネイス
                Common.MyStat.HP -= ATK * 2
                Me.MaxMP -= Me.Lv
            Case 2 'コミューン
                Common.MyStat.HP -= ATK * 0.7
                Me.HP += Me.Lv / 2
            Case 3 'ハル
                Common.MyStat.HP -= ATK / 4
            Case 4 'MP吸収
                Common.MyStat.MP -= ATK / 3
                Me.MP += ATK / 3
                If MP > MaxMP Then
                    MP = MaxMP
                End If
                If Common.MyStat.MP < 0 Then
                    Common.MyStat.MP = 0 - Common.MyStat.MP
                    Common.MyStat.HP -= Common.MyStat.MP / 2
                    Common.MyStat.MP = 0
                End If
            Case 5 'ボール
                Common.MyStat.HP -= ATK

        End Select

    End Sub


    Private Sub MP_TP_Use(ByVal index As Integer)
        Select Case index
            Case 1  'ハイントネイス
                MP -= 20
            Case 2  'コミューン
                MP -= 6
            Case 3  'ハル
                MP -= 2         '実質 6
            Case 4  'mp吸収
                MP -= 1
                TP -= 10
            Case 5  'ボール
                MP -= 3
            Case 6  '強い抵抗
                TP -= 2
        End Select
    End Sub


    Public Overrides Function SkillAI()
        Dim index As Integer    '初期値0は攻撃

        If TP >= 2 AndAlso Common.Random.Next(3) = 0 Then
            index = 6
        End If

        If MP >= 20 AndAlso Common.Random.Next(4) = 0 Then  'ハイントネイス
            index = 1
        ElseIf MP >= 6 AndAlso Common.Random.Next(10) <= 2 Then 'コミューン
            index = 2
        ElseIf MP >= 6 AndAlso Common.Random.Next(4) = 1 Then     'ハル
            index = 3
        End If

        If Common.Random.Next(3) = 0 AndAlso MP >= 3 Then   'ボール
            index = 5
        ElseIf MP >= 1 AndAlso MP < 10 AndAlso TP >= 10 AndAlso Common.Random.Next(2) = 0 Then  'mp吸収
            index = 4
        End If

            Return index
    End Function



    Public Overrides Property BelongTo As BelongTo
        Get
            Return BelongTo.EventObject
        End Get
        Set(ByVal value As BelongTo)
            MyBase.BelongTo = value
        End Set
    End Property



End Class
