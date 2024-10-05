Public Class RecoverHP
    Inherits UnitBase

    Dim ToumeiBr As New SolidBrush(Color.FromArgb(72, 255, 255, 255))


    Public Overrides ReadOnly Property Name As String = "ベンチ"  '名前

    Private fnt As New Font("MS UI Gothic", 13)


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
        Select Case what
            Case -6
                Return "座る"
            Case -1
                Return 90
            Case 0
                Return 1
            Case 1
                If BtnBool Then
                    Return "座って安らいだ。(HP回復少し)"
                Else
                    Return "誰かが座って来たから座るのを止めた。。"
                End If
            Case 6
                If BtnBool Then
                    Common.MyStat.HP += Common.MyStat.MaxHP * 0.15       '回復ってわけ
                    If Common.MyStat.HP > Common.MyStat.MaxHP Then
                        Common.MyStat.HP = Common.MyStat.MaxHP
                    End If
                End If
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
