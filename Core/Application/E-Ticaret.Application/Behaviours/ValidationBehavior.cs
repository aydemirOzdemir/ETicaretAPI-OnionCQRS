using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Behaviours;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>  // Mediator da yazmış olduğum yapıda ırequestlerim tip olarak içinde dönen responselarlada işaretlenirler
{
    private readonly IEnumerable<IValidator<TRequest>> validator;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
    {
        this.validator = validator;
    }
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failtures=validator.Select(v=>v.Validate(context))//buradaki yapıda gelen requesttin içine giriyoruz
            .SelectMany(v=>v.Errors)//Buradaki tüm errorleri getireceğim grup bayla sadece bir tanesini seçiyorum daha sonra null olmayanları alıp listeliyorum.
            .GroupBy(e=>e.ErrorMessage)
            .Select(x=>x.First())
            .Where(x=>x != null)
            .ToList();
        if (failtures.Any())
            throw new ValidationException(failtures);
        return next();
        
    }
}
