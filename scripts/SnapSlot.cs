using Godot;

public partial class SnapSlot : ColorRect
{
    [Export] private Color normalColor = new Color(0.3f, 0.3f, 0.3f, 0.5f);
    [Export] private Color occupiedColor = new Color(0.1f, 0.7f, 0.1f, 0.7f);
    [Export] private float snapDistance = 100.0f;

    public DraggableColorRect OccupiedRect { get; private set; }

    public override void _Ready()
    {
        Color = normalColor;
    }

    public override void _Process(double delta)
    {
        // 检查附近是否有可拖动的矩形
        CheckForNearbyRect();
    }

    private void CheckForNearbyRect()
    {
        // 获取所有可拖动的矩形
        var draggableRects = GetTree().GetNodesInGroup("DraggableRects");

        foreach (var node in draggableRects)
        {
            if (node is DraggableColorRect rect && !rect.isDragging)
            {
                var distance = rect.Position.DistanceTo(Position);

                if (distance < snapDistance)
                {
                    // 吸附到槽中心
                    rect.Position = Position;
                    OccupiedRect = rect;
                    Color = occupiedColor;
                    return;
                }
            }
        }

        // 如果没有矩形在附近
        if (OccupiedRect != null && OccupiedRect.Position.DistanceTo(Position) > snapDistance)
        {
            OccupiedRect = null;
        }

        Color = normalColor;
    }
}