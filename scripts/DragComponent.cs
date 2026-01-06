using Godot;

public partial class DragComponent : Control
{
    [Signal] public delegate void DragStartedEventHandler();
    [Signal] public delegate void DragEndedEventHandler();

    [Export] public bool Enabled { get; set; } = true;
    [Export] public MouseButton DragButton { get; set; } = MouseButton.Left;

    private Control target;
    private bool isDragging = false;
    private Vector2 dragOffset;

    public override void _Ready()
    {
        // 默认目标是父节点（必须是Control）
        if (GetParent() is Control control)
        {
            target = control;
            target.MouseFilter = Control.MouseFilterEnum.Pass;

            // 连接到目标的GuiInput信号
            target.GuiInput += OnTargetGuiInput;
        }
        else
        {
            GD.PrintErr("DragComponent必须附加到Control节点上");
            Enabled = false;
        }
    }

    private void OnTargetGuiInput(InputEvent @event)
    {
        if (!Enabled) return;

        if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == DragButton)
        {
            if (mouseButton.Pressed)
            {
                StartDrag();
            }
            else if (isDragging)
            {
                EndDrag();
            }
        }
    }

    public override void _Process(double delta)
    {
        if (isDragging && target != null)
        {
            target.Position = target.GetViewport().GetMousePosition() + dragOffset;
        }
    }

    private void StartDrag()
    {
        if (isDragging) return;

        isDragging = true;
        dragOffset = target.Position - target.GetViewport().GetMousePosition();
        _ = EmitSignal(SignalName.DragStarted);
    }

    private void EndDrag()
    {
        if (!isDragging) return;

        isDragging = false;
        EmitSignal(SignalName.DragEnded);
    }

    public void SetTarget(Control newTarget)
    {
        if (isDragging) EndDrag();

        // 断开旧目标
        if (target != null)
        {
            target.GuiInput -= OnTargetGuiInput;
        }

        // 连接新目标
        target = newTarget;
        if (target != null)
        {
            target.MouseFilter = Control.MouseFilterEnum.Pass;
            target.GuiInput += OnTargetGuiInput;
        }
    }
}