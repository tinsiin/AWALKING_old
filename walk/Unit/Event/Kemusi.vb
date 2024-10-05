Public Class Kemusi
    Inherits UnitBase

    Public Overrides ReadOnly Property Name As String = "太い毛虫"  '名前

    Private fnt As New Font("MS UI Gothic", 13)

    Dim Item As ItemBase = Common.AllItems.Item(Common.Random.Next(Common.AllItems.Count))

    Public Sub New()


        Me.Width = Name.Length * fnt.Size
        Me.Height = 30 '仮

        Me.Left = (Common.Display.ViewArea.Width / 2) - Me.Width / 1.5
        Me.Top = (Common.Display.ViewArea.Height / 2) - Me.Height / 2

    End Sub

    Public Overrides Function Events(ByVal what As Integer, Optional BtnBool As Boolean = True)

        Select Case what
            Case 1
                If BtnBool Then
                    Return "スルーした。"
                Else
                    Return "うわあ！ 踏んで毒汁が飛んできた！" & "-3ダメージGet!"
                End If
            Case 2
                If BtnBool Then
                    Return "潰した Expを4_Get!"
                Else
                    Return "うわあ！ 踏んで毒汁が飛んできた！" & vbCrLf & "最大HP4%分のダメージGet!"
                End If
            Case 3
                Return "↑↑↑↑↑↑↑↑"
            Case 4
                If BtnBool Then
                    Return "旨い虫で回復しました。"
                Else
                    Dim str As String
                    If Common.Random.Next(2) = 0 Then
                        str = "不味い！"
                    Else
                        str = "毛が刺さる！"
                    End If

                    Return str & vbCrLf & "ダメージを喰らった！"
                End If
            Case 5
                If BtnBool Then
                    Return "トゲをとってアイテムに変えてやったぜ！" & vbCrLf & "(度胸試し成功！いいことあるさ)"
                Else
                    Return "刺さった痛い！" & vbCrLf & "毒になった。"
                End If
            Case 6
                If BtnBool = False Then
                    Common.MyStat.HP -= 3
                    If Common.MyStat.HP < 0 Then
                        Common.MyStat.HP = 0
                    End If
                End If
            Case 7
                If BtnBool Then
                    Common.MyStat.EXP += 4

                    Dim EleNumber As Integer = Common.Random.Next(5)

                    If Utility.ElementalUp(4, EleNumber) Then      '三属性 + 平凡のどれかが上がる
                        MsgBox("【" & Common.ElementalName(EleNumber) & "】が上昇")
                    End If

                    If Common.MyStat.EXP >= Common.MyStat.MaxEXP Then
                        MsgBox("レベルが上がった！",, "敵死亡！")
                        Common.MyStat.EXP = 0
                        Common.MyStat.Lv += 1
                        Utility.StatesUp()
                        MsgBox("ステータスが上昇した。",, "Level Up")       'この後にステータスアップの処理。
                    End If

                Else
                    Common.MyStat.HP -= Common.MyStat.MaxHP * 0.04
                    If Common.MyStat.HP < 0 Then
                        Common.MyStat.HP = 0
                    End If
                End If
            Case 8
                '戦いへのフェーズ変更
            Case 0
                Return 5 'ボタンをフルに使う！
            Case -1
                Return 90
            Case -2
                Return 66
            Case -3
                Return 100
            Case -4
                Return 50
            Case -5
                Return 60
            Case -6
                Return "避ける"
            Case -7
                Return "潰す"
            Case -8
                Return "戦う"
            Case -9
                Return "食べる"
            Case -10
                Return "毛を抜く"
            Case 100
                Return True
        End Select

        Return 0
    End Function


    Public Overrides Sub Draw(g As Graphics)
        g.DrawString(Me.Name & vbCrLf & "--EVENT--", fnt, Brushes.Black, Me.Left, Me.Top)
    End Sub


    Public Overrides Property BelongTo As BelongTo
        Get
            Return BelongTo.EventObject
        End Get
        Set(ByVal value As BelongTo)
            MyBase.BelongTo = value
        End Set
    End Property

End Class
