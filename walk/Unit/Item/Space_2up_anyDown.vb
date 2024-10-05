Public Class Space_2up_anyDown
    Inherits ItemBase
    Public Overrides ReadOnly Property ID As Integer = 2
    Public Overrides ReadOnly Property Name As String = "ダークマター"

    Public Overrides ReadOnly Property rarity As Integer = 13    'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property gold As Integer = 50

    Public Overrides Property UseableTimes As Integer = 1   'アイテム使用可能回数

    Public Overrides Function Use()
        Common.MyStat.ElementalValue(Elemental.Space) += 2

        Dim str As String
        Dim Val As Integer

        Select Case Common.Random.Next(9) 'case 7~8は、外れ
            Case 0
                Val = Common.Random.Next(Common.MyStat.EXP)
                Common.MyStat.EXP -= Val

                str = "経験値が" & Val & "下がった。"
            Case 1
                Val = Common.Random.Next(Common.MyStat.HP)
                Common.MyStat.HP -= Val

                str = "HPが" & Val & "下がった。"
            Case 2
                Val = Common.Random.Next(4) '0~3
                Common.MyStat.MaxHP -= Val

                str = "最大HPが" & Val & "下がった。"
            Case 3
                Val = Common.Random.Next(Common.MyStat.MP)
                Common.MyStat.MP -= Val

                str = "MPが" & Val & "下がった。"
            Case 4
                Val = Common.Random.Next(2) '0~2
                Common.MyStat.MaxMP -= Val

                str = "最大MPが" & Val & "下がった。"
            Case 5
                Val = Common.Random.Next(Common.MyStat.TP)
                Common.MyStat.TP -= Val

                str = "TPが" & Val & "下がった。"
            Case 6
                Val = Common.Random.Next(5) '0~4
                Common.MyStat.MaxTP -= Val

                str = "最大TPが" & Val & "下がった。"
            Case Else
                If Common.Random.Next(2) = 0 Then
                    str = "何か体調悪くなった気がしたけど、それだけ。"
                Else
                    str = "何も起こらなかった。"
                End If
        End Select

        Return "宇宙属性が2増えた。" & vbCrLf & str
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "ダークマター、宇宙の片鱗を取り込むとデメリットもあるよ。(ステータス何か下がります。)"
    End Function


End Class
