Public Class Sky_1up
    Inherits ItemBase
    Public Overrides ReadOnly Property ID As Integer = 3
    Public Overrides ReadOnly Property Name As String = "トウモロコシ"

    Public Overrides ReadOnly Property rarity As Integer = 11    'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property gold As Integer = 30

    Public Overrides Property UseableTimes As Integer = 1   'アイテム使用可能回数

    Public Overrides Function Use()
        Common.MyStat.ElementalValue(Elemental.Sky) += 1

        Return "青空属性が1増えた。"
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "お空の光を一杯に浴びて育った発色のいい黄色のトウモロコシ。"
    End Function


End Class
