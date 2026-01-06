using Godot;

public partial class DraggableColorRect : ColorRect
{
    private bool isDragging = false;
    private Vector2 dragOffset;

    private Label label;
    public override void _Ready()
    {
        MouseFilter = Control.MouseFilterEnum.Pass;
        GuiInput += OnGuiInput;
        label = GetNode<Label>("Label");
    }

    private void OnGuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Left)
        {
            if (mouseButton.Pressed)
            {
                // 开始拖动
                isDragging = true;
                // 保存点击位置与当前位置的偏移
                dragOffset = Position - GetViewport().GetMousePosition();
                label.Text = "我好像被拖动了QwQ";
            }
            else
            {
                // 停止拖动
                isDragging = false;
                label.Text = "我是个Label";
            }
        }
    }

    public override void _Process(double delta)
    {
        if (isDragging)
        {
            // 直接使用视口鼠标位置
            Position = GetViewport().GetMousePosition() + dragOffset;
        }
    }
}