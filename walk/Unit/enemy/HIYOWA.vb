Public Class HIYOWA
    Inherits UnitBase

    Dim ToumeiBr As New SolidBrush(Color.FromArgb(72, 255, 255, 255))

    Public Overrides Property Passive As Passive = Passive.Neutral   '本人の状態異常状態

    Public Overrides Property HP As Integer   'HP 体力のこと　これがなくなると死ぬ
    Public Overrides Property MaxHP As Integer   'HP上限
    Public Overrides Property ATK As Integer   '攻撃力
    Public Overrides Property MaxMP As Integer     'MP上限
    Public Overrides Property MaxTP As Integer   'TP上限
    Public Overrides Property Gold As Integer = Common.Random.Next(1, 2)  '持ってる金 1～2G持ってる。
    Public Overrides Property Lv As Integer  'レベル
    Public Overrides ReadOnly Property Name As String = "ひ弱"  '名前

    Private fnt As New Font("MS UI Gothic", 13)
    Private Br1 As New SolidBrush(Color.Black)

    Public Sub New(ByVal Le As Integer)
        Me.Lv = Le

        ATK = 1 + (Lv - 1) * 0.66
        MaxHP = 8 + (Lv - 1) * 2.3
        MaxMP = Lv

        Me.HP = Common.Random.Next(2, Me.MaxHP)    'HP初期化

        Me.MP = MaxMP


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

        Select Case Index
            Case 0      '平凡攻撃
                Name = "攻撃"
                dmg = ATK
                Resultstr = Resultdmg & "ダメージ受けた"
                HIT = 80
                category = Skillcategory.Attack
            Case 1
                Name = "コミューン"
                Resultstr = Name & "は少し回復した。" & vbCrLf & Resultdmg & "ダメージ受けた"
                HIT = 95
                category = Skillcategory.MAGIC
            Case 2
                Name = "ガスガン"       '何分の一かの確率でHPが最大HP三分の一減る
                Resultstr = "ブァドッ... 急所にHIT!"
                HIT = 33
                category = Skillcategory.MAGIC

        End Select


        Select Case what
            Case 0
                Return Name
            Case 1
                Return dmg
            Case 2
                MP_TP_Use(Index)
                Return Resultstr
            Case 3
                If Index = 2 Then       'ガスガンのための特筆処理　失敗しても必ずMPを消費します。
                    MP -= 1
                End If
                Return HIT
            Case 4
                Return category
        End Select

        Return 0
    End Function
    Public Overrides Sub MAGICSKILL(ByVal index As Integer)
        Select Case index
            Case 1  'コミューン
                Common.MyStat.HP -= ATK * 0.8
                Me.HP += Me.Lv / 2
            Case 2  'ガスガン
                Common.MyStat.HP -= Common.MyStat.MaxHP / 3
        End Select

    End Sub
    Private Sub MP_TP_Use(ByVal index As Integer)
        Select Case index
            Case 1  'コミューン
                MP -= 3
            Case 2 'ガスガン
                '    MP -= 1　必ず消費するために失敗しても使われるプロシージャに書きます
        End Select
    End Sub





    Public Overrides Function SkillAI()
        Dim index As Integer

        If Common.Random.Next(3) = 0 AndAlso MP > 0 Then    'ガスガン三分の一
            index = 2
        ElseIf Common.Random.Next(5) <= 1 AndAlso MP >= 3 Then    'コミューン 5分の二
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
