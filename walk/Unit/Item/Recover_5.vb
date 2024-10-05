Public Class Recover_5
    Inherits ItemBase

    Public Overrides ReadOnly Property ID As Integer = 7
    Public Overrides ReadOnly Property Name As String = "冷凍今川焼き"

    Public Overrides ReadOnly Property rarity As Integer = 1    'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property gold As Integer = 5

    Public Overrides Property UseableTimes As Integer = 1   'アイテム使用可能回数

    Public Overrides Function Use()
        Common.MyStat.HP += 5
        If Common.MyStat.HP > Common.MyStat.MaxHP Then
            Common.MyStat.HP = Common.MyStat.MaxHP
        End If
        Dim str As String

        'フレーバーてきすとです
        If Common.Random.Next(2) = 0 Then
            str = "カスタードだった、邪道だけどやっぱ美味しい。"
        Else
            str = "あんこだ。安らかな甘みで安らぐ。"
        End If

        Return "食べてHPを5回復した！" & vbCrLf & str
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "冷凍食品であの今川焼きがお手軽に食べれます！" & vbCrLf & "回復します、HP5分"
    End Function


End Class
