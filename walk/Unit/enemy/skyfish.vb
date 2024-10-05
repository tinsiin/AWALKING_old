Public Class skyfish
    Inherits UnitBase

    Dim ToumeiBr As New SolidBrush(Color.FromArgb(72, 255, 255, 255))

    Public Overrides Property Passive As Passive = Passive.Neutral   '本人の状態異常状態
    Public Overrides Property HP As Integer   'HP 体力のこと　これがなくなると死ぬ
    Public Overrides Property MaxHP As Integer   'HP上限
    Public Overrides Property ATK As Integer   '攻撃力
    Public Overrides Property recov As Decimal   '回復力
    Public Overrides Property MaxMP As Integer     'MP上限
    Public Overrides Property MaxTP As Integer   'TP上限
    Public Overrides Property Gold As Integer = Common.Random.Next(1, 3)  '持ってる金 1~2G持ってる。
    Public Overrides Property Lv As Integer  'レベル
    Public Overrides ReadOnly Property Name As String = "スカイフィッシュ"  '名前

    Private fnt As New Font("MS UI Gothic", 13)
    Private Br1 As New SolidBrush(Color.SkyBlue)

    Public Overrides Property boss As Boolean = False   'ボスかどうか。

    Public Sub New(ByVal Le As Integer)
        Me.Lv = Le

        ATK = (Lv - 1) * 2 - 5
        MaxHP = 20 + Lv * 10 - 1

        Me.MaxMP = Lv / 4
        Me.MaxTP = Lv / 3

        Me.recov = 1.3

        Me.HP = Me.MaxHP    'HP初期化

        Me.MP = Common.Random.Next(MaxMP)
        Me.TP = Common.Random.Next(MaxTP)


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
            Br1.Color = Color.SkyBlue
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

        Select Case Index
            Case 0      '平凡攻撃
                Name = "攻撃"
                dmg = ATK
                Resultstr = Resultdmg & "ダメージ受けた"
                HIT = 87
                category = Skillcategory.Attack
            Case 1
                Name = "気丈"
                Resultstr = Name & "は奮い立って、" & vbCrLf & "MPを増やした"
                HIT = 100
                category = Skillcategory.MAGIC
            Case 2
                Name = "回復"
                Resultstr = Name & "は霧めいて回復してしまった！！"
                HIT = 97
                category = Skillcategory.Recover
            Case 3
                Name = "霧魔法"
                Resultstr = "魚の魔法で" & Resultdmg & "ダメージ！"
                dmg = ATK * 2
                HIT = 97
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
                MP_TP_Use(Index)
                Return Resultstr
            Case 3
                Return HIT
            Case 4
                Return category
        End Select

        Return 0
    End Function

    Public Overrides Sub MAGICSKILL(ByVal index As Integer)
        Select Case index
            Case 1
                MP += Common.Random.Next(Lv / 5)
        End Select

        If MP > MaxMP Then
            MP = MaxMP
        End If
    End Sub


    Private Sub MP_TP_Use(ByVal index As Integer)
        Select Case index
            Case 1
                TP -= 5
            Case 2
                TP -= 6
                MP -= 2
            Case 3
                MP -= 20
        End Select
    End Sub


    Public Overrides Function SkillAI()
        Dim index As Integer    '初期値0は攻撃

        If TP >= 5 AndAlso Common.Random.Next(3) = 0 Then   '三分の一でMP溜め　それを回避してTP2以上あるなら5分の3で回復
            index = 1
        ElseIf TP >= 6 AndAlso Common.Random.Next(5) <= 3 AndAlso MP >= 2 Then   '回復
            index = 2
        End If
        If MP >= 20 AndAlso Common.Random.Next(3) <= 1 Then 'MPが5以上　３分の一で霧魔法
            index = 3
        End If

        If HP <= MaxHP / 3 AndAlso MP >= 2 AndAlso TP >= 6 AndAlso Common.Random.Next(2) = 0 Then  'HPが三分の一以下で　該当あるなら回復　２分の一で
            index = 2
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

    Public Overrides Property Elemental As Elemental      '本人の属性
        Get
            Return Elemental.Sky
        End Get
        Set(ByVal value As Elemental)
            Throw New InvalidOperationException("Elementalに値を設定するには、派生クラスでElementalをオーバーライドしてください。")
        End Set
    End Property


End Class
