### 地图设计文档

地图采用2D横版像素风，形式为整张式的大地图（玩家的视野范围只占地图的很小一部分），用Tilemap的方式绘制而成（想了想随机地图还是有点难整，既然只要展示demo的话我觉得demo里画个固定地图就可以了，这和游戏设定的随机地图应该不矛盾）



#### 一、物体类型及分层（自底向上）

**Layer1**  背景层：主要放背景图片

**Layer2**  装饰物层：主要放一些地图上可见但没有碰撞体积且不可被破坏的装饰物（树、石头一类）

**Layer3**  平台层：主要放地图上站立的平台（地形构筑的关键）

**Layer4**  梯子层：主要放连接各种平台的不可破坏道具（梯子是一种）

**Layer5**  可破坏物层：主要放地图上可以破坏的物体



#### 二、一些问题

目前给的素材感觉契合主题的不多，而且素材局限性太强（比如我现在用的这个，斜坡下都是黑色的会覆盖背景，然后箱子无法堆叠）。我自己找了一些素材也碰到多多少少一些问题（比如素材摆放对不齐这种，不知道用Tilemap搭建地图对于这种不规则的物体有什么好的解决办法...），如果之后能找到更合适的素材替换现在的自然是最好的，或者自己画也可以，这个就靠你们了。