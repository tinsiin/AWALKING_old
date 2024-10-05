Public Class K_Yanki
    Inherits UnitBase

    Dim cm As New Imaging.ColorMatrix()
    Dim ia As New Imaging.ImageAttributes()
    Dim ToumeiBr As New SolidBrush(Color.FromArgb(162, 255, 255, 255))
    Dim Port As Bitmap

    Public Overrides Property Passive As Passive = Passive.Neutral   '本人の状態異常状態
    Public Overrides Property HP As Integer   'HP 体力のこと　これがなくなると死ぬ
    Public Overrides Property MaxHP As Integer   'HP上限
    Public Overrides Property ATK As Integer   '攻撃力
    Public Overrides Property MaxMP As Integer     'MP上限
    Public Overrides Property MaxTP As Integer   'TP上限
    Public Overrides Property Gold As Integer = Common.Random.Next(4, 11)  '持ってる金 4～10G持ってる。
    Public Overrides Property Lv As Integer  'レベル
    Public Overrides ReadOnly Property Name As String = "K官に憧れた若者"  '名前

    Private fnt As New Font("MS UI Gothic", 13)
    Private Br1 As New SolidBrush(Color.Black)

    Public Sub New()
        Select Case Common.Random.Next(5)
            Case 0 To 2
                Lv = 4
                ATK = 9
                MaxHP = 47
                MaxMP = 2
            Case 3 To 4
                Lv = 6
                ATK = 11
                MaxHP = 67
                MaxMP = 21
        End Select

        Me.MaxTP = 12
        Me.TP = Common.Random.Next(3, 7)


        Me.HP = Me.MaxHP    'HP初期化

        Me.MP = Me.MaxMP - Common.Random.Next(4)       '


        Me.Width = 300
        Me.Height = 300 '仮


        Me.Left = (Common.Display.ViewArea.Width / 2) - Name.Length * fnt.Size / 1.5
        Me.Top = (Common.Display.ViewArea.Height / 2) - 15


        cm.Matrix00 = 1
        cm.Matrix11 = 1
        cm.Matrix22 = 1
        cm.Matrix33 = 0.9F  'この値を変更
        cm.Matrix44 = 1
        ia.SetColorMatrix(cm)
        Port = Image.FromFile(Application.StartupPath & "\image\yankii_K.jpg")
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

        g.DrawImage(Port, New Rectangle(0, 0, 250, 250), 0, 0, 250, 250, GraphicsUnit.Pixel, ia)
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
            Case 0      '血を伴う平凡攻撃
                Name = "凸撃"
                dmg = ATK + Common.Random.Next(4, 10)
                Resultstr = "敵は血を流す覚悟で突っ込んできた。" & vbCrLf & Resultdmg & "ダメージ受けた"
                HIT = 97
                category = Skillcategory.Attack
            Case 1
                Name = "ガスガン"       '何分の一かの確率でHPが最大HP三分の一減る
                Resultstr = "ブァドッ... 急所にHIT!"
                HIT = 40
                category = Skillcategory.MAGIC
            Case 2
                Name = "コミューン"       '青空魔法　
                Resultstr = "バァアア"
                HIT = 100
                category = Skillcategory.MAGIC
            Case 3
                Name = "攻撃"       'ノーマル
                dmg = ATK
                Resultstr = Resultdmg & "ダメージ受けた"
                HIT = 100
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
                SubSkill(Index)
                Return Resultstr
            Case 3
                If Index = 1 Then       'ガスガンのための特筆処理　失敗しても必ずMPを消費します。
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
            Case 1  'ガスガン
                Common.MyStat.HP -= Common.MyStat.MaxHP / 3
            Case 2  'コミューン
                Common.MyStat.HP -= ATK * 0.8
                Me.HP += Me.Lv / 2
        End Select

    End Sub

    Private Sub SubSkill(ByVal index As Integer)
        Select Case index
            Case 0 '凸撃
                Me.Passive = Passive.Blood
                Me.passiveTime = 3
                Me.passivePer = 67
        End Select

    End Sub


    Private Sub MP_TP_Use(ByVal index As Integer)
        Select Case index
            Case 0 '凸撃
                TP -= 4
            Case 1
            '    MP -= 1　必ず消費するために失敗しても使われるプロシージャに書きます
            Case 2
                MP -= 2
        End Select
    End Sub


    Public Overrides Function SkillAI()
        Dim index As Integer = 3

        If Common.Random.Next(3) = 0 AndAlso MP > 0 Then    'ガスガン三分の一
            index = 1
        ElseIf Common.Random.Next(2) = 0 AndAlso MP > 1 Then    'コミューン
            index = 2
        End If

        If HP < MaxHP / 3 AndAlso TP >= 4 Then  '最終手段　凸撃
            index = 0
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
