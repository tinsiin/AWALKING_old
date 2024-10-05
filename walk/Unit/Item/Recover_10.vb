Public Class Recover_10
    Inherits ItemBase

    Public Overrides ReadOnly Property ID As Integer = 10
    Public Overrides ReadOnly Property Name As String = "鶏肉ねぎうどん"

    Public Overrides ReadOnly Property rarity As Integer = 2    'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property gold As Integer = 8

    Public Overrides Property UseableTimes As Integer = 1   'アイテム使用可能回数

    Public Overrides Function Use()
        Common.MyStat.HP += 10
        If Common.MyStat.HP > Common.MyStat.MaxHP Then
            Common.MyStat.HP = Common.MyStat.MaxHP
        End If

        Return "食べてHPを5回復した！" & vbCrLf & "(鶏肉ねぎうどん大好きだ！)"
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "鶏肉とうどんとねぎは良く合います。[お勧め商品]" & vbCrLf & "回復します、HP10"
    End Function


End Class
