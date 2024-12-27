using Coupon.Core.Abstractions;

namespace Coupon.Application.Query.Client;

public sealed record GetByIdClient(Guid id) : IQuery;
