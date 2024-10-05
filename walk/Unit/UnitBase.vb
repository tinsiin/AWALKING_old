Public Class UnitBase
    Public Left As Integer
    Public Top As Integer
    Public Width As Integer
    Public Height As Integer


    Public Overridable Property HP As Integer   'HP 体力のこと　これがなくなると死ぬ
    Public Overridable Property MaxHP As Integer    'HP上限
    Public Overridable Property ATK As Integer  '攻撃力
    Public Overridable Property Recov As Decimal  '回復倍率
    Public Overridable Property MP As Integer   'MP
    Public Overridable Property MaxMP As Integer    'MP上限
    Public Overridable Property TP As Integer   'ｔｅｃｈｎｉｃａｌポイント
    Public Overridable Property MaxTP As Integer    'TP上限
    Public Overridable Property Gold As Integer  '持ってる金
    Public Overridable Property Lv As Integer  'レベル

    Public DamageTime As Integer


    Public Overridable ReadOnly Property Name As String = ""  '名前



    Public Overridable Sub Explode()
        Common.RequestUnitAdd(New DeathExplode())
    End Sub

    Public Overridable Function SkillAI()
        'スキルを状況によって自動で選ぶ
        Return 0
    End Function


    Public Sub New()
        'なんか使えないからコピペ　なぜoverridesした値がここで適用されないのか謎　またはその他原因。
    End Sub


    Public Overridable Sub Draw(ByVal g As Graphics)
        '描画
        'g.DrawImage(mainimage, Me.Rectangle, 0, 0, mainimage.Width, mainimage.Height, GraphicsUnit.Pixel, Attr)
    End Sub



    ''' <summary>
    ''' 引数に0を指定するとスキル名、1だと素ダメージ、2だと当てた時の文章、3だと命中率、4だとスキルの種類、5だと与える状態異常、6だと状態異常のターン数、7だと状態異常の解除確率、8だと複数攻撃回数、9だと複数攻撃確率、10だと複数攻撃の種類が返ります。
    ''' </summary>
    ''' <returns>string,integer</returns>
    ''' <remarks></remarks>
    Public Overridable Function Skill(ByVal what As Integer, ByVal Index As Integer, Optional Resultdmg As Integer = 0)

        Return 0
    End Function

    Public Overridable Function Damage(ByVal dmg As Integer, ByVal enemyElemental As Elemental)        '自らがダメージを受けるメゾット
        Dim resultdmg As Integer
        Dim Per As Decimal = Utility.ElementalMagic(enemyElemental, Me.Elemental)
        resultdmg = dmg * Per
        Common.TempElementalPer = Per * 100     'メイン画面で表示する用。
        Me.HP -= resultdmg
        DamageTime = 30
        Return resultdmg
    End Function


    Public Overridable Sub MAGICSKILL(ByVal index As Integer)
        '魔法スキル
    End Sub

    Public passiveTime As Integer '-1を指定すると、確率で効果が消える(ナンバーがゼロになる。)
    Public passivePer As Integer '-1を指定すると、確率で効果が消える(ナンバーがゼロになる。)
    '状態異常
    Public Overridable Sub PassiveDrive()
        If passiveTime > 0 Then
            passiveTime -= 1
        End If

        '正常(0)出ないとき、その状態異常の効果が出る。
        Select Case Me.Passive

            Case Passive.Blood      '血
                Me.HP -= Me.Lv
            Case Passive.DarkNess
                Me.MP -= 1
        End Select

        If passiveTime = 0 OrElse Common.Random.Next(1, 101) <= passivePer Then 'もし状態異常の効果時間が切れたとき、状態異常が正常(0)になる。
            Me.Passive = Passive.Neutral
        End If
    End Sub


    Public Overridable Function IsDead() As Boolean
        '  If Me.HP > 0 Then
        ' Return False
        'Else
        'Return True
        'End If
        Return False
    End Function
    Public Overridable Function BattleDie() As Boolean      'バトルでの死
        '  If Me.HP > 0 Then
        ' Return False
        'Else
        'Return True
        'End If
        Return False
    End Function

    ''' <summary>
    ''' 引数に-1を指定すると一つ目のボタンの成功確率、以後-5まで同様、 -6～-10を指定すると1～5のボタンの名前が返る。0を指定するとイベントボタン数、1～5だとそれぞれのボタンの可否文章、6～10だとそれぞれのスキル効果が返ります。二つ目はボタンの成功か失敗かの判定です（True or False）
    ''' １００だと行動が制御されるか決まる。
    ''' </summary>
    ''' <returns>string,integer</returns>
    ''' <remarks></remarks>
    Public Overridable Function Events(ByVal what As Integer, Optional BtnBool As Boolean = True)    'イベントプロシージャ
        Return False
    End Function


    Public Overridable ReadOnly Property Rectangle() As Rectangle
        Get
            Return New Rectangle(Left, Top, Width, Height)
        End Get
    End Property

    Public Overridable ReadOnly Property Bottom() As Integer
        Get
            Return Top + Height
        End Get
    End Property

    Public Overridable ReadOnly Property Right() As Integer
        Get
            Return Left + Width
        End Get
    End Property

    Public Overridable Property BelongTo As BelongTo        'エフェクトはオーバライドの必要なし！
        Get
            Return BelongTo.Effect
        End Get
        Set(ByVal value As BelongTo)
            Throw New InvalidOperationException("BelongToに値を設定するには、派生クラスでBelongToをオーバーライドしてください。")
        End Set
    End Property

    Public Overridable Property Elemental As Elemental      '本人の属性
        Get
            Return Elemental.Normal
        End Get
        Set(ByVal value As Elemental)
            Throw New InvalidOperationException("Elementalに値を設定するには、派生クラスでElementalをオーバーライドしてください。")
        End Set
    End Property
    Public Overridable Property Passive As Passive    '本人の状態異常状態
        Get
            Return Passive.Neutral
        End Get
        Set(ByVal value As Passive)
            Throw New InvalidOperationException("Passiveに値を設定するには、派生クラスでPassiveをオーバーライドしてください。")
        End Set
    End Property

    Public Overridable Property boss As Boolean = False     'ボスかどうか。


End Class

Public Enum BelongTo
    ''' <summary>イベントのユニット、進むか戻ると消える。</summary>
    EventObject
    ''' <summary>テキスト系のユニット</summary>
    Text
    ''' <summary>エフェクト系</summary>
    Effect

End Enum

Public Enum Passive
    Neutral         '正常
    DarkNess         ' 暗黒
    Dream        '夢状態
    Blood　      '血
End Enum
Public Enum Elemental
    Night
    Sun
    Sky
    Normal
    Fire
    PSI
    ATOM
    Space
End Enum
Public Enum Skillcategory
    Attack  '攻撃
    Recover '回復
    MAGIC '魔法
    STATESgive  '状態付与
End Enum

