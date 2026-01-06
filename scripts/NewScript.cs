using Godot;
using System;

public partial class NewScript : ColorRect
{
    private DragComponent dragComponent;

    public override void _Ready()
    {
        // 创建并添加组件
        dragComponent = GetNode<DragComponent>("DragComponent");

        // 你也可以添加其他组件
        // AddChild(new HoverEffectComponent());
        // AddChild(new ClickSoundComponent());
    }
}
