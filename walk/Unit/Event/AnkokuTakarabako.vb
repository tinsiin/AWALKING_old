Public Class AnkokuTakarabako
    Inherits UnitBase

    Dim ToumeiBr As New SolidBrush(Color.FromArgb(72, 255, 255, 255))


    Public Overrides ReadOnly Property Name As String = "闇の旋風"  '名前

    Private fnt As New Font("MS UI Gothic", 13)

    Dim Item(2) As ItemBase

    Public Sub New()


        Me.Width = 300
        Me.Height = 300 '仮


        Me.Left = (Common.Display.ViewArea.Width / 2) - Name.Length * fnt.Size / 1.5
        Me.Top = (Common.Display.ViewArea.Height / 2) - 15


        Item(0) = Common.AllItems.Item(Common.Random.Next(Common.AllItems.Count))
        Item(1) = Common.AllItems.Item(Common.Random.Next(Common.AllItems.Count))

        Dim ItemBool As Boolean
        Do     '選定メインループ
            Dim KariItem As ItemBase = Common.AllItems.Item(Common.Random.Next(Common.AllItems.Count))
            ItemBool = False

            If KariItem.ItemCate = ItemCate.Battle Then
                ItemBool = True
            End If
            If Common.Random.Next(70) = 29 Then
                ItemBool = True
            End If

            Item(1) = KariItem
        Loop Until ItemBool


    End Sub

    Public Overrides Sub Draw(g As Graphics)
        g.FillRectangle(ToumeiBr, Me.Rectangle)
        g.DrawString(Me.Name & vbCrLf & "--EVENT(夜暗黒)--", fnt, Brushes.Black, Me.Left, Me.Top)
    End Sub



    Public Overrides Function Events(ByVal what As Integer, Optional BtnBool As Boolean = True)
        Select Case what
            Case -6
                Return "手を入れる"
            Case -7
                Return "剣を入れる"
            Case -8
                Return "切る"
            Case -1
                Return 80
            Case -2
                Return 80
            Case -3
                Return 89
            Case 0
                Return 3
            Case 1
                If BtnBool Then
                    Return "アイテムをゲット！" & vbCrLf & "(" & Item(0).Name & ")"
                Else
                    Return "【暗黒】状態になってしまった。。"
                End If
            Case 2
                If BtnBool Then
                    Return "アイテムをゲット！" & vbCrLf & "(" & Item(1).Name & ")"
                Else
                    Return "【夢】状態になってしまった。。"
                End If
            Case 3
                If BtnBool Then
                    Return "切り崩した(適性経験値GET)"
                Else
                    Return "切り損ねた"
                End If
            Case 6
                If BtnBool Then
                    Common.RequestItemAdd(Item(0))
                Else
                    Common.MyStat.Passive = Passive.DarkNess
                End If
            Case 7
                If BtnBool Then
                    Common.RequestItemAdd(Item(1))
                Else
                    Common.MyStat.Passive = Passive.Dream
                End If
            Case 8
                If BtnBool Then
                    Dim Getexp As Integer = Utility.EXPgenerator(Common.MyStat.Lv, Common.Random.Next(Common.MyStat.Lv, Common.MyStat.Lv + 3))    '1～2相当のレベル分のexp貰える
                    Common.MyStat.EXP += Getexp

                    Dim EleNumber As Integer = Elemental.Night

                    If Utility.ElementalUp(Getexp, EleNumber) Then      '夜暗黒が上がる
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
                End If
            Case 100
                Return True
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
