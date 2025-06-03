using System.ComponentModel.DataAnnotations;

namespace LoanManagment.Api.Model;

public record CreateCreditRequest([Required] long PlanId);

