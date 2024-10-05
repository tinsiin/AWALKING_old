Public Class MaxMp_2up
    Inherits ItemBase

    Public Overrides ReadOnly Property ID As Integer = 13
    Public Overrides ReadOnly Property Name As String = "世界不思議解明本"

    Public Overrides ReadOnly Property rarity As Integer = 8    'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property gold As Integer = 25

    Public Overrides Property UseableTimes As Integer = 1   'アイテム使用可能回数

    Public Overrides Function Use()

        Common.MyStat.MaxMP += 2

        Return "最大MPが2増えた。"
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "世界の不思議を理解して最大MP2上げる。"
    End Function


End Class
