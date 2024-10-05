Public Class Recover_mp15
    Inherits ItemBase
    Public Overrides ReadOnly Property ID As Integer = 5
    Public Overrides ReadOnly Property Name As String = "マジカルボーイ"

    Public Overrides ReadOnly Property rarity As Integer = 4    'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property gold As Integer = 14

    Public Overrides Property UseableTimes As Integer = 2   'アイテム使用可能回数

    Public Overrides Function Use()
        Common.MyStat.MP += 3
        If Common.MyStat.MP > Common.MyStat.MaxMP Then
            Common.MyStat.MP = Common.MyStat.MaxMP
        End If
        Return "少し苦い、粉っぽい。。。" & vbCrLf & "MP15が補充された。"
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "すくすく育つお子様もきっと喜ぶ、甘くて美味しいココア飲料。" & vbCrLf & "MP15回復します。"
    End Function


End Class
