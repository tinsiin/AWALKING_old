Public Class mini_KIKANSYA
    Inherits ItemBase
    Public Overrides ReadOnly Property ID As Integer = 15
    Public Overrides ReadOnly Property Name As String = "ミニ機関車"
    Public Overrides ReadOnly Property rarity As Integer = 7     'アイテムのレアリティ(ショップの並びやすさ)
    Public Overrides Property Gold As Integer = 30   'アイテム購入の費用　売却は半分で売れる。
    Public Overrides Property UseableTimes As Integer = 1    'アイテム使用可能回数

    Public Overrides Function Use()
        '使用ボタンを押したときに発生する効果 返すのは効果文章

        Common.MyStat.WalkPoint += 25

        Return "ｼｭﾎﾟﾎﾟｼｭﾎﾟ　辺り一帯を巻き込みつつ25歩進んだ。"
    End Function

    Public Overrides Function SetumeiStr()
        'アイテムの説明文が返る。
        Return "空間ごと進もう！子供用の簡易機関車だよん。"
    End Function


    Public Overrides Property ItemCate As ItemCate     '属性
        Get
            Return ItemCate.Every
        End Get
        Set(ByVal value As ItemCate)
            Throw New InvalidOperationException("ItemCateに値を設定するには、派生クラスでItemCateをオーバーライドしてください。")
        End Set
    End Property


End Class
