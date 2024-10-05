Public Class Recover_3
    Inherits ItemBase

    Public Overrides ReadOnly Property ID As Integer = 9
    Public Overrides ReadOnly Property Name As String = "チョコチップパン"

    Public Overrides ReadOnly Property rarity As Integer = 0    'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property gold As Integer = 2

    Public Overrides Property UseableTimes As Integer = 1   'アイテム使用可能回数

    Public Overrides Function Use()
        Common.MyStat.HP += 3
        If Common.MyStat.HP > Common.MyStat.MaxHP Then
            Common.MyStat.HP = Common.MyStat.MaxHP
        End If
        Return "食べてHPを3回復した！"
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "チョコチップが散りばめられたスティックパン。" & vbCrLf
    End Function


End Class
