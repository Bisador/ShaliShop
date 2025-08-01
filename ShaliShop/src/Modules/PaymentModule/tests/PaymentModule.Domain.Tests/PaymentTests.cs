using PaymentModule.Domain.Payments.Aggregates;
using PaymentModule.Domain.Payments.DomainEvents;
using PaymentModule.Domain.Payments.Enums;
using Shared.Domain;
using SharedModule.Domain.ValueObjects;

namespace PaymentModule.Domain.Tests;

public class PaymentTests
{
    [Fact]
    public void Initiating_payment_should_set_state_and_emit_PaymentInitiated()
    {
        var payment = Payment.Initiate(Guid.NewGuid(), Guid.NewGuid(), Money.From(150m));

        payment.Status.Should().Be(PaymentStatus.Pending);
        payment.InitiatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));

        payment.DomainEvents.Any(e =>
                e is PaymentInitiated pi && pi.AggregateId == payment.Id)
            .Should().BeTrue();
    }

    [Fact]
    public void Succeeding_payment_should_set_status_and_raise_PaymentSucceeded()
    {
        var payment = Payment.Initiate(Guid.NewGuid(), Guid.NewGuid(), Money.From(250m));
        var txnId = "stripe_txn_xyz";

        payment.Succeed(txnId);

        payment.Status.Should().Be(PaymentStatus.Succeeded);
        payment.TransactionId.Should().Be(txnId);
        payment.CompletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));

        payment.DomainEvents.Any(e =>
                e is PaymentSucceeded ps && ps.AggregateId == payment.Id && ps.TransactionId == txnId)
            .Should().BeTrue();
    }

    [Fact]
    public void Failing_payment_should_set_status_and_raise_PaymentFailed()
    {
        var payment = Payment.Initiate(Guid.NewGuid(), Guid.NewGuid(), Money.From(80m));

        payment.Fail("Card declined");

        payment.Status.Should().Be(PaymentStatus.Failed);
        payment.CompletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));

        payment.DomainEvents.Any(e =>
                e is PaymentFailed pf && pf.AggregateId == payment.Id && pf.Reason == "Card declined")
            .Should().BeTrue();
    }

    [Fact]
    public void Refund_should_only_be_allowed_for_succeeded_payments()
    {
        var payment = Payment.Initiate(Guid.NewGuid(), Guid.NewGuid(), Money.From(99m));

        Action act = () => payment.IssueRefund("Customer request");

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Refunding_succeeded_payment_should_set_status_and_raise_RefundIssued()
    {
        var payment = Payment.Initiate(Guid.NewGuid(), Guid.NewGuid(), Money.From(120m));
        payment.Succeed("txn-abc");

        payment.IssueRefund("Returned item");

        payment.Status.Should().Be(PaymentStatus.Refunded);

        payment.DomainEvents.Any(e =>
                e is RefundIssued r && r.AggregateId == payment.Id && r.Reason == "Returned item")
            .Should().BeTrue();
    }

    [Fact]
    public void Cannot_succeed_a_failed_payment()
    {
        var payment = Payment.Initiate(Guid.NewGuid(), Guid.NewGuid(), Money.From(60m));
        payment.Fail("Network error");

        Action act = () => payment.Succeed("txn-late");

        act.Should().Throw<DomainException>();
    }
}