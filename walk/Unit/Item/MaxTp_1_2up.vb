Public Class MaxTp_1_2up
    Inherits ItemBase

    Public Overrides ReadOnly Property ID As Integer = 12
    Public Overrides ReadOnly Property Name As String = "ジム一日無料券"

    Public Overrides ReadOnly Property rarity As Integer = 7    'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property gold As Integer = 23

    Public Overrides Property UseableTimes As Integer = 1   'アイテム使用可能回数

    Public Overrides Function Use()
        Dim Value As Integer = Common.Random.Next(1, 3) '1~2

        Common.MyStat.MaxTP += Value

        Return "ジムで鍛えて、最大TPが" & Value & "増えた。"
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "どんな人でもバキバキにするAサレイジムの一日体験券貰ったんですけど要らないからお譲りします！" & vbCrLf & "最大Tpが1か2ぐらい増えます。"
    End Function


End Class
