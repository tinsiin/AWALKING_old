Public Class ItemBase
    Public Overridable ReadOnly Property Name As String
    Public Overridable ReadOnly Property ID As Integer
    Public Overridable ReadOnly Property rarity As Integer      'アイテムのレアリティ(ショップの並びやすさ)
    Public Overridable Property Gold As Integer    'アイテム購入の費用　売却は半分で売れる。
    Public Overridable Property UseableTimes As Integer    'アイテム使用可能回数

    Public Overridable Function Use()
        '使用ボタンを押したときに発生する効果 返すのは効果文章
        Return 0
    End Function

    Public Overridable Function SetumeiStr()
        'アイテムの説明文が返る。
        Return 0
    End Function


    Public Overridable Property ItemCate As ItemCate     '属性
        Get
            Return ItemCate.Every
        End Get
        Set(ByVal value As ItemCate)
            Throw New InvalidOperationException("ItemCateに値を設定するには、派生クラスでItemCateをオーバーライドしてください。")
        End Set
    End Property


End Class
Public Enum ItemCate
    ''' <summary>汎用アイテム、いつでも使える</summary>
    Every
    ''' <summary>戦闘専用アイテム、通常時は使えない</summary>
    Battle

End Enum

