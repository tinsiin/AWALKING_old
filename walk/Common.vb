Public Class Common

    Public Shared Random As New Random
    Public Shared Display As DisplayInfo  '画面の領域。
    Public Shared GamePhase As GamePhases

    Public Shared MyStat As AMan  '全体で使いまわす主人公のステータスク
    Public Shared EneMan As UnitBase  '全体で使いまわす敵クラス
    Public Shared EveMan As UnitBase  '全体で使いまわすイベントクラス

    Public Shared ElementalName() As String = {"夜暗黒", "太陽", "青空", "平凡", "火", "異能", "ATOM", "宇宙"}

    Public Shared MIDI As Boolean = True
    Public Shared Hakai As Boolean
    Public Shared FukusuuSTR As Boolean

    Public Shared AllItems As New List(Of ItemBase)  'すべてのアイテム
    Public Shared TempElementalPer As Integer

    Public Shared Event UnitAddRequested(ByVal unit As UnitBase)
    Public Shared Event ItemAddRequested(ByVal item As ItemBase)

    Shared Sub New()
        GamePhase = GamePhases.WaitingForStart
        ItemDRRR()
    End Sub
    Public Shared Sub ItemDRRR()
        AllItems.Clear()
        AllItems.AddRange({New TLOA_1UP, New Space_2up_anyDown, New Sky_1up, New Recover_mp3, New Recover_mp15, New Recover_hp7_mp5, New Recover_5, New Recover_30, New Recover_3, New Recover_10, New Psi_1up_Normal_1down, New MaxTp_1_2up, New MaxMp_2up, New HalfBlade, New mini_KIKANSYA, New mini_ANTI_KIKANSYA})
    End Sub
    Public Shared Sub RequestUnitAdd(ByVal unit As UnitBase)
        RaiseEvent UnitAddRequested(unit)
    End Sub
    Public Shared Sub RequestItemAdd(ByVal item As ItemBase)
        RaiseEvent ItemAddRequested(item)
    End Sub

    Public Shared Sub stageMIDI()
        If MIDI Then
            'MVWaitMusic
            'MVLoadMusic(Application.StartupPath & "\sound\w.mid")
            'MVPlayMusic
        End If
    End Sub

    Public Shared Function PassiveText(ByVal index As Passive, ByVal what As Integer)
        Dim Text As String = ""
        Dim Name As String = ""
        Select Case index
            Case Passive.Blood
                Name = "血"
                Text = "戦うたび、血が流れて辛い"
            Case Passive.Dream
                Name = "夢"
                Text = "目覚めてもまだ夢の中にいます。"
            Case Passive.DarkNess
                Name = "暗黒"
                Text = "暗黒が周りを覆ってくれるけど、MPを取られる。"
        End Select

        Select Case what
            Case 0
                Return Text
            Case Else
                Return Name
        End Select
    End Function
    Public Shared Function StageIndex_to_FileIndex(ByVal Index As Integer) As Integer
        Dim FileIndex As Integer

        FileIndex = Index + (0 - (Index / 2 - 1))

        Return FileIndex
    End Function

    Public Shared Function What_hundred(ByVal Number As Integer)

        Do
            Number += 1
        Loop Until Number Mod 100 = 0

        Return Number
    End Function

End Class


Public Enum GamePhases
    WaitingForStart     'タイトル画面
    Playing
    BattleItem   'バトル中アイテム使用判定
End Enum

    Public Class Utility

    Public Function GetDistance(ByVal pos1 As Point, ByVal pos2 As Point) As Double

        Dim xDistance As Double = pos1.X - pos2.X
        Dim yDistance As Double = pos1.Y - pos2.Y

        Dim result As Double

        '三平方の定理(ピタゴラスの定理)
        result = Math.Sqrt(xDistance ^ 2 + yDistance ^ 2)

        Return result
    End Function


    Public Shared Function ElementalMagic(ByVal attacker As Elemental, ByVal Under_Attacker As Elemental) As Decimal
        'under_attackerが攻撃されちゃう側ダヨ！！！

        If attacker = Elemental.Normal AndAlso Under_Attacker = Elemental.Normal Then  '平凡同士へ
            Return 1.0
        End If
        If attacker = Elemental.Normal AndAlso Under_Attacker = Elemental.Sky Then  '平凡から青空へ 88～92%
            Return 0.88 + Common.Random.Next(5) / 100
        End If
        If attacker = Elemental.Sky AndAlso Under_Attacker = Elemental.Normal Then  '青空から平凡へ 94% 6%気化するからね
            Return 0.94
        End If
        If attacker = Elemental.Normal AndAlso Under_Attacker = Elemental.Sun Then  '平凡から太陽へ 70% 
            Return 0.7
        End If
        If attacker = Elemental.Sun AndAlso Under_Attacker = Elemental.Normal Then  '太陽から平凡へ 50～60% 青空が守ってくれるからね
            Return 0.5 + Common.Random.Next(11) / 100
        End If

        Return 0
    End Function

    Public Shared Function EXPgenerator(ByVal KillerLv As Integer, ByVal Under_KillerLv As Integer) As Integer  '経験値算出　敵属性も考慮しようかな。
        Dim exp As Integer
        Dim Plus_Minus_exp As Integer

        Plus_Minus_exp = Under_KillerLv - KillerLv
        exp = Common.Random.Next(15 + Plus_Minus_exp * 3, 27 + Plus_Minus_exp * 3)

        If KillerLv > Under_KillerLv Then '敵がこっちより低いレベルの場合
            If exp <= 0 Then
                exp = 1
            End If

            exp -= Common.Random.Next(exp)
        End If

        Return exp
    End Function

    Public Shared Function ElementalUp(ByVal Getexp As Integer, ByVal Ele As Elemental)
        Common.MyStat.ElementalExp(Ele) += Getexp

        If 20 + Common.MyStat.ElementalValue(Ele) * Common.MyStat.ElementalEXPstep <= Common.MyStat.ElementalExp(Ele) Then
            '最大属性expが上昇

            Common.MyStat.ElementalExp(Ele) = 0

            Common.MyStat.ElementalValue(Ele) += 1

            Return True
        End If

        Return False
    End Function

    Public Shared Sub StatesUp()
        Common.MyStat.MaxHP += Common.Random.Next(5, 9)
        Common.MyStat.MaxTP += Common.Random.Next(1, 3)
        Common.MyStat.MaxMP += Common.Random.Next(1, 5)

        'レベルアップ後、回復する。
        Common.MyStat.HP = Common.MyStat.MaxHP
        Common.MyStat.MP = Common.MyStat.MaxMP
        ' Common.MyStat.TP = Common.MyStat.MaxTP

        'TLOAアイテムゲット
        Common.RequestItemAdd(New TLOA_1UP)
    End Sub

End Class

