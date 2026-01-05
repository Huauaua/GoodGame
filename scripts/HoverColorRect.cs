using Godot;

public partial class HoverColorRect : ColorRect
{
    [Export] private Color normalColor = new Color(0.2f, 0.2f, 0.2f, 1);
    [Export] private Color hoverColor = new Color(0.4f, 0.6f, 0.8f, 1);
    private Label Label;

    public override void _Ready()
    {
        base._Ready();
        Label = GetNode<Label>("Label");
        Color = normalColor;

        // 启用鼠标检测
        MouseFilter = Control.MouseFilterEnum.Pass;

        // 连接信号
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
    }

    private void OnMouseEntered()
    {
        Color = hoverColor;
        Label.Text = new string("感觉有人在我上面");
    }

    private void OnMouseExited()
    {
        Color = normalColor;
        Label.Text = new string("我是个Label");
    }
}