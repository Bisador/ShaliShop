using Shared.Domain;

namespace ShippingModule.Domain.Shipments.Exceptions;

public class MaximumDeliveryAttemptsExceededException() : BusinessRuleValidationException("Maximum delivery attempts exceeded.");