Public Class Recover_hp7_mp5
    Inherits ItemBase
    Public Overrides ReadOnly Property ID As Integer = 6
    Public Overrides ReadOnly Property Name As String = "とろろ牛丼"

    Public Overrides ReadOnly Property rarity As Integer = 2    'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property gold As Integer = 13

    Public Overrides Property UseableTimes As Integer = 1   'アイテム使用可能回数

    Public Overrides Function Use()
        Common.MyStat.HP += 7
        Common.MyStat.MP += 5
        If Common.MyStat.HP > Common.MyStat.MaxHP Then
            Common.MyStat.HP = Common.MyStat.MaxHP
        End If
        If Common.MyStat.MP > Common.MyStat.MaxMP Then
            Common.MyStat.MP = Common.MyStat.MaxMP
        End If


        Return "バクバク、旨い。" & vbCrLf & "HPを7、mpを5回復しました。"
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "あの押野屋が牛丼お持ち帰りサービスを始めました。" & vbCrLf & "体力もつくし精力もつく、とろろトッピング付き！"
    End Function


End Class
