using Shared.Domain;

namespace ShippingService.Domain.Shipments.Exceptions;

public class MaximumDeliveryAttemptsExceededException() : DomainException("Maximum delivery attempts exceeded.");