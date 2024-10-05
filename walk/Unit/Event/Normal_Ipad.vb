Public Class Normal_Ipad
    Inherits UnitBase

    Dim ToumeiBr As New SolidBrush(Color.FromArgb(72, 255, 255, 255))

    Public Overrides ReadOnly Property Name As String = "Ipad 2020年製"  '名前

    Private fnt As New Font("MS UI Gothic", 13)

    Public Event ShopOpen()

    Public Sub New()


        Me.Width = 300
        Me.Height = 300 '仮


        Me.Left = (Common.Display.ViewArea.Width / 2) - Name.Length * fnt.Size / 1.5
        Me.Top = (Common.Display.ViewArea.Height / 2) - 15

    End Sub

    Public Overrides Sub Draw(g As Graphics)
        g.FillRectangle(ToumeiBr, Me.Rectangle)
        g.DrawString(Me.Name & vbCrLf & "--EVENT--", fnt, Brushes.Black, Me.Left, Me.Top)
    End Sub



    Public Overrides Function Events(ByVal what As Integer, Optional BtnBool As Boolean = True)
        Dim BtnNumber As Integer
        Dim BtnName As String
        Dim BtnHIT As Integer
        Dim resultstr(2) As String

        BtnNumber = 2
        BtnName = "読む"
        BtnHIT = 70
        resultstr(0) = "昔の記事を読んで知識を増やした。(exp_UP!)"
        resultstr(1) = "記事を読んだら端末が熱暴走した！(HP_DOWN!)"

        Select Case what
            Case -7
                Return "ﾈｯﾄｼｮｯﾌﾟ"
            Case -6
                Return BtnName
            Case -1
                Return BtnHIT
            Case -2
                Return 100
            Case 0
                Return BtnNumber
            Case 1
                If BtnBool Then
                    Return resultstr(0)
                Else
                    Return resultstr(1)
                End If
            Case 2
                Return "もう買い物終わり？ 早いねえ"
            Case 6
                If BtnBool Then
                    Dim Getexp As Integer = Utility.EXPgenerator(Common.MyStat.Lv, Common.Random.Next(1, 3))    '1～2相当のレベル分のexp貰える
                    Common.MyStat.EXP += Getexp

                    Dim EleNumber As Integer = Common.Random.Next(4)

                    If Utility.ElementalUp(Getexp, EleNumber) Then      '三属性 + 平凡のどれかが上がる
                        MsgBox("【" & Common.ElementalName(EleNumber) & "】が上昇")
                    End If

                    If Common.MyStat.EXP >= Common.MyStat.MaxEXP Then
                        My.Computer.Audio.Play(Application.StartupPath & "\sound\lvup.wav")
                        MsgBox("レベルが上がった！",, "敵死亡！")
                        Common.MyStat.EXP = 0
                        Common.MyStat.Lv += 1
                        Utility.StatesUp()
                        My.Computer.Audio.Play(Application.StartupPath & "\sound\ok.wav")
                        MsgBox("ステータスが上昇した、アイテムを手に入れた。",, "Level Up")
                    End If
                Else
                    Common.MyStat.HP -= 6 + Common.Random.Next(6)
                    Common.RequestUnitAdd(New DeathRed)
                End If


            Case 7  'ネットショップ

                'Form側でショップに並ぶ処理
                RaiseEvent ShopOpen()
            Case 100
                Return False
        End Select

        Return 0
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
