namespace ShipmentTracker.Domain.Shipments;

public class Shipment
{
    public int Id { get; set; }
    public ShipmentType ShipmentType { get; set; }
    public required string Origin { get; set; }
    public required string Destination { get; set; }
}