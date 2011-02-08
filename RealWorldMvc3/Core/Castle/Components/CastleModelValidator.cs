using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Castle.Components.Validator;

namespace RealWorldMvc3.Core.Castle.Components
{
    public class CastleModelValidator : ModelValidator
    {
        private readonly IValidatorRegistry validatorRegistry;

        public IValidator Validator { get; set; }

        public CastleModelValidator(ModelMetadata metadata, ControllerContext controllerContext, IValidatorRegistry validatorRegistry, IValidator validator)
            : base(metadata, controllerContext)
        {
            Validator = validator;
            this.validatorRegistry = validatorRegistry;
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            if (IsValid(container))
                return Enumerable.Empty<ModelValidationResult>();

            return new[]
                       {
                           new ModelValidationResult
                               {
                                   Message = Validator.ErrorMessage
                               }
                       };
        }

        private bool IsValid(object container)
        {
            PropertyInfo propertyInfo = GetPropertyInfo();
            InitializeValidator(propertyInfo);

            return Validator.IsValid(container, Metadata.Model);
        }

        private void InitializeValidator(PropertyInfo propertyInfo)
        {
            Validator.Initialize(validatorRegistry, propertyInfo);
        }

        private PropertyInfo GetPropertyInfo()
        {
            return Metadata.ContainerType.GetProperty(Metadata.PropertyName);
        }
    }

    public abstract class CastleModelValidator<TValidator> : CastleModelValidator where TValidator : IValidator
    {
        public new TValidator Validator { get; private set; }

        protected CastleModelValidator(ModelMetadata metadata, ControllerContext controllerContext, IValidatorRegistry validatorRegistry, TValidator validator)
            : base(metadata, controllerContext, validatorRegistry, validator)
        {
            Validator = validator;
        }
    }
}