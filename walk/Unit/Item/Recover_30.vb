Public Class Recover_30
    Inherits ItemBase

    Public Overrides ReadOnly Property ID As Integer = 8
    Public Overrides ReadOnly Property Name As String = "健康ドロップ飴"

    Public Overrides ReadOnly Property rarity As Integer = 3    'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property gold As Integer = 24

    Public Overrides Property UseableTimes As Integer = 1   'アイテム使用可能回数

    Public Overrides Function Use()
        Common.MyStat.HP += 30
        If Common.MyStat.HP > Common.MyStat.MaxHP Then
            Common.MyStat.HP = Common.MyStat.MaxHP
        End If

        Return "30回復する。"
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "三開社の30年続く人気商品。" & vbCrLf & "HPを30回復する。"
    End Function


End Class
