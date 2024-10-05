Public Class Yanki
    Inherits UnitBase

    Dim ToumeiBr As New SolidBrush(Color.FromArgb(162, 255, 255, 255))

    Public Overrides Property Passive As Passive = Passive.Neutral   '本人の状態異常状態
    Public Overrides Property HP As Integer   'HP 体力のこと　これがなくなると死ぬ
    Public Overrides Property MaxHP As Integer   'HP上限
    Public Overrides Property ATK As Integer   '攻撃力
    Public Overrides Property MaxMP As Integer     'MP上限
    Public Overrides Property MaxTP As Integer   'TP上限
    Public Overrides Property Gold As Integer = Common.Random.Next(1, 8)  '持ってる金 1～7G持ってる。
    Public Overrides Property Lv As Integer  'レベル
    Public Overrides ReadOnly Property Name As String = "ヤンキー"  '名前

    Private fnt As New Font("MS UI Gothic", 13)
    Private Br1 As New SolidBrush(Color.Black)

    Public Sub New(ByVal Le As Integer)
        Me.Lv = Le


        ATK = 1 + (Lv - 1) * 3.2
        MaxHP = 35 + (Lv - 1) * 4
        MaxTP = Lv
        MaxMP = Lv / 2

        Me.TP = Common.Random.Next(Me.MaxTP)
        Me.MP = Common.Random.Next(Me.MaxMP)


        Me.HP = Me.MaxHP    'HP初期化

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
            Case 0
                Name = "攻撃"       'ノーマル
                dmg = ATK
                Resultstr = Resultdmg & "ダメージ受けた"
                HIT = 69
                category = Skillcategory.Attack
            Case 1
                Name = "シンナー"       '攻撃力増強　HP回復
                Resultstr = "アンパンっていうらしい。" & vbCrLf & "攻撃力が上がり" & vbCrLf & "HPが" & Math.Round(Lv * 2.3) & "回復してしまいました！"
                HIT = 88
                category = Skillcategory.MAGIC
            Case 2
                Name = "たこ殴り"       '確率複数回攻撃
                Resultstr = "ドカドカドカ" & Resultdmg & "ダメージ受けた"
                dmg = ATK - Common.Random.Next(3, 5) '3～4引かれる
                HIT = 88
                category = Skillcategory.Attack
                Fukusuu = 2
                FukusuuKakuritu = 57
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
                MP_TP_Use(Index)
                Return Resultstr
            Case 3
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
            Case 1  'シンナー
                HP += Math.Round(Lv * 2.3)
                ATK += Common.Random.Next(1 + Lv / 3, 5 + Lv / 3) '2~5
                If MaxHP < HP Then
                    HP = MaxHP
                End If
        End Select

    End Sub



    Private Sub MP_TP_Use(ByVal index As Integer)
        Select Case index
            Case 1      'シンナー
                MP -= 2
                TP -= 1
            Case 2      'たこ殴り(確率複数攻撃)
                TP -= 2
        End Select
    End Sub


    Public Overrides Function SkillAI()
        Dim index As Integer = 0    '攻撃

        If Common.Random.Next(3) = 0 AndAlso TP >= 2 Then    'たこ殴り
            index = 2
        End If

        If HP < MaxHP * 0.7 AndAlso TP >= 1 AndAlso MP >= 2 AndAlso Common.Random.Next(4) = 0 Then  '少し食らったら、シンナー
            index = 1
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
