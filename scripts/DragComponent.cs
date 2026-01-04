using Godot;

public partial class DragComponent : Area2D
{
    [Signal] public delegate void DragStartedEventHandler();
    [Signal] public delegate void DragEndedEventHandler();

    [Export] public bool Enabled { get; set; } = true;
    [Export] public MouseButton DragButton { get; set; } = MouseButton.Left;

    public bool IsDragging { get; private set; }

    private Node2D _target;
    private Vector2 _dragOffset;

    public override void _Ready()
    {
        _target = GetParent() as Node2D;

        if (_target == null)
        {
            GD.PrintErr("DragComponent 必须附加到 Node2D 节点上");
            return;
        }

        // 自动创建碰撞形状（如果不存在）
        if (GetChildCount() == 0)
        {
            var shape = new CollisionShape2D();
            shape.Shape = new RectangleShape2D { Size = new Vector2(100, 100) };
            AddChild(shape);
        }

        // 设置 Area2D 属性
        InputPickable = true;
        Monitoring = true;
        Monitorable = true;
    }

    // 处理输入事件（Area2D 的方法）
    public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
    {
        if (!Enabled || _target == null) return;

        if (@event is InputEventMouseButton mouseButton &&
            mouseButton.ButtonIndex == DragButton)
        {
            if (mouseButton.Pressed)
            {
                // 开始拖动
                IsDragging = true;
                _dragOffset = _target.GlobalPosition - mouseButton.GlobalPosition;
                EmitSignal(SignalName.DragStarted);

                // C# 中阻止事件传递的方法
                GetViewport().SetInputAsHandled();
            }
        }
    }

    public override void _Process(double delta)
    {
        if (IsDragging && _target != null)
        {
            // 更新位置
            var mousePos = GetGlobalMousePosition();
            _target.GlobalPosition = mousePos + _dragOffset;
        }
    }

    public override void _Input(InputEvent @event)
    {
        // 处理鼠标释放
        if (@event is InputEventMouseButton mouseButton &&
            mouseButton.ButtonIndex == DragButton &&
            !mouseButton.Pressed &&
            IsDragging)
        {
            IsDragging = false;
            EmitSignal(SignalName.DragEnded);
        }
    }

    // 获取全局鼠标位置
    private Vector2 GetGlobalMousePosition()
    {
        // 方法1：如果有相机，使用相机的全局鼠标位置
        if (GetViewport()?.GetCamera2D() is Camera2D camera)
        {
            return camera.GetGlobalMousePosition();
        }

        // 方法2：否则使用视口鼠标位置（可能需要转换）
        return GetViewport().GetMousePosition();
    }
}