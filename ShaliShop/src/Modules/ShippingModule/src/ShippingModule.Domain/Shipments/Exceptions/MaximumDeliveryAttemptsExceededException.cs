using Shared.Domain;

namespace ShippingModule.Domain.Shipments.Exceptions;

public class MaximumDeliveryAttemptsExceededException() : DomainException("Maximum delivery attempts exceeded.");