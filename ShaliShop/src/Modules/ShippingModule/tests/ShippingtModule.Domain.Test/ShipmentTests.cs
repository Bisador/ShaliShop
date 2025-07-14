using Shared.Domain;
using ShippingModule.Domain.Shipments.Aggregates;
using ShippingModule.Domain.Shipments.DomainEvents;
using ShippingModule.Domain.Shipments.Enums;
using ShippingModule.Domain.Shipments.Exceptions;

namespace ShippingtModule.Domain.Test;

public class ShipmentTests
{
    [Fact]
    public void Creating_shipment_should_initialize_state_and_raise_ShipmentCreated()
    {
        var shipment = Shipment.Create(
            orderId: Guid.NewGuid());

        shipment.Status.Should().Be(ShipmentStatus.Created);
        shipment.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        shipment.Events.OfType<ShipmentCreated>().Should().ContainSingle();
    }

    [Fact]
    public void Dispatching_shipment_should_update_status_and_raise_event()
    {
        var shipment = ShipmentFixture.Create();

        shipment.Dispatch(carrier: "UPS", trackingNumber: "XYZ-789");

        shipment.Status.Should().Be(ShipmentStatus.Dispatched);
        shipment.DispatchedAt.Should().NotBeNull();
        shipment.Events.OfType<ShipmentDispatched>().Should().ContainSingle();
    }

    [Fact]
    public void Confirming_delivery_should_update_status_and_raise_event()
    {
        var shipment = ShipmentFixture.CreateAndDispatch();

        shipment.ConfirmDelivery();

        shipment.Status.Should().Be(ShipmentStatus.Delivered);
        shipment.DeliveredAt.Should().NotBeNull();
        shipment.Events.OfType<ShipmentDelivered>().Should().ContainSingle();
    }

    [Fact]
    public void Cannot_dispatch_twice()
    {
        var shipment = ShipmentFixture.CreateAndDispatch();

        FluentActions.Invoking(() => shipment.Dispatch(carrier: "UPS", trackingNumber: "XYZ-789"))
            .Should().Throw<OnlyNewlyCreatedShipmentsCanBeDispatchedException>();
    }

    [Fact]
    public void Cannot_confirm_delivery_without_dispatch()
    {
        var shipment = ShipmentFixture.Create();

        FluentActions.Invoking(() => shipment.ConfirmDelivery())
            .Should().Throw<ShipmentMustBeDispatchedBeforeDeliveryException>()
            .WithMessage("*must be dispatched*");
    }

    [Fact]
    public void Canceling_shipment_should_prevent_further_transitions()
    {
        var shipment = ShipmentFixture.Create();
        shipment.Cancel();

        FluentActions.Invoking(() => shipment.Dispatch(carrier: "UPS", trackingNumber: "XYZ-789"))
            .Should().Throw<OnlyNewlyCreatedShipmentsCanBeDispatchedException>();

        shipment.Status.Should().Be(ShipmentStatus.Canceled);
    }

    [Fact]
    public void Failing_delivery_more_than_3_times_should_throw()
    {
        var shipment = ShipmentFixture.CreateAndDispatch();

        for (var i = 0; i < 3; i++)
            shipment.MarkDeliveryFailed();

        FluentActions.Invoking(() => shipment.MarkDeliveryFailed())
            .Should().Throw<BusinessRuleValidationException>()
            .WithMessage("*maximum delivery attempts*");
    }
}