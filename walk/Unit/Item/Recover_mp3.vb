Public Class Recover_mp3
    Inherits ItemBase
    Public Overrides ReadOnly Property ID As Integer = 4
    Public Overrides ReadOnly Property Name As String = "ビターチョコ2つ"

    Public Overrides ReadOnly Property rarity As Integer = 0    'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property gold As Integer = 7

    Public Overrides Property UseableTimes As Integer = 2   'アイテム使用可能回数

    Public Overrides Function Use()
        Common.MyStat.MP += 3
        If Common.MyStat.MP > Common.MyStat.MaxMP Then
            Common.MyStat.MP = Common.MyStat.MaxMP
        End If
        Return "ひとつ食べてMPを3回復した！"
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "ビターチョコ大好き！そんなに苦くないよ。。" & vbCrLf & "MP3回復します。"
    End Function


End Class
