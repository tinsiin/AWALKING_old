Public Class HalfBlade
    Inherits ItemBase
    Public Overrides ReadOnly Property ID As Integer = 14
    Public Overrides ReadOnly Property Name As String = "‼ハーフブレード"
    Public Overrides ReadOnly Property rarity As Integer = 5     'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property Gold As Integer = 16   'アイテム購入の費用　売却は半分で売れる。
    Public Overrides Property UseableTimes As Integer = 3    'アイテム使用可能回数

    Public Overrides Function Use()
        '使用ボタンを押したときに発生する効果 返すのは効果文章
        Dim dmg As Integer = 10 + Common.MyStat.Gold / 2
        Dim Money As Integer = Common.MyStat.Gold / 4

        Common.EneMan.HP -= dmg
        Common.MyStat.Gold -= Money

        If Common.MyStat.Gold < 0 Then
            Common.MyStat.Gold = 0
        End If
        Return "ガショオオン" & vbCrLf & dmg & "ダメージ与えた！" & vbCrLf & "ピピピ、使用料金は" & Money & "Gとなりマス。"
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "お手軽使い捨ての剣！、[10]+[手持ちのお金の半分]だけ直接ダメージを与えられる。" & vbCrLf & "その代わり持ち金の4分の一は情報になって製造販売元へ使用料金として支払われる。"
    End Function


    Public Overrides Property ItemCate As ItemCate     '属性
        Get
            Return ItemCate.Battle
        End Get
        Set(ByVal value As ItemCate)
            Throw New InvalidOperationException("ItemCateに値を設定するには、派生クラスでItemCateをオーバーライドしてください。")
        End Set
    End Property


End Class
