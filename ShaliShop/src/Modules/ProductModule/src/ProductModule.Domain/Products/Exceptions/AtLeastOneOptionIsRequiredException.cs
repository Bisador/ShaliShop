using Shared.Domain;

namespace ProductModule.Domain.Products.Exceptions;

public class AtLeastOneOptionIsRequiredException() : BusinessRuleValidationException("At least one option is required.");