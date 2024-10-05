Public Class TLOA_1UP
    Inherits ItemBase
    Public Overrides ReadOnly Property ID As Integer = 1

    Public Overrides ReadOnly Property Name As String = "LVUPボーナス"

    Public Overrides ReadOnly Property rarity As Integer = 100    'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property gold As Integer = 200

    Public Overrides Property UseableTimes As Integer = 1   'アイテム使用可能回数

    Public Overrides Function Use()
        Common.MyStat.MaxTLOA += 1
        Return "鉄の剣が強くなった。"
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "精神の成長" & vbCrLf
    End Function


End Class
