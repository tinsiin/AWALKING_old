Public Class Psi_1up_Normal_1down
    Inherits ItemBase
    Public Overrides ReadOnly Property ID As Integer = 11
    Public Overrides ReadOnly Property Name As String = "名物浮き団子"

    Public Overrides ReadOnly Property rarity As Integer = 10    'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property gold As Integer = 27

    Public Overrides Property UseableTimes As Integer = 1   'アイテム使用可能回数

    Public Overrides Function Use()
        Common.MyStat.ElementalValue(Elemental.Normal) -= 1
        Common.MyStat.ElementalValue(Elemental.PSI) += 1

        Return "異能属性が1増えた。平凡が1減った。"
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "異能の力が込められた団子、但し異能は「普通ではない」ということなので、異能は1上がるが平凡が1下がります。"
    End Function


End Class
