using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.Components.Validator;
using RealWorldMvc3.Core.Castle.Components;
using Storm.Utils;

public delegate ModelValidator CastleModelValidationFactory(ModelMetadata metadata, ControllerContext context, IValidatorRegistry validatorRegistry, IValidator validator);

public class CastleModelValidatorProvider : AssociatedValidatorProvider
{
    private readonly IValidatorRegistry validationRegistry;

    private TypeKeyCache<CastleModelValidationFactory> validatorFactories =
        new TypeKeyCache<CastleModelValidationFactory>((m, c, r, v) => new CastleModelValidator(m, c, r, v))
                {
                    {typeof (NonEmptyValidator), RequiredValidatorWrapper.Create},
                    {typeof (EmailValidator), EmailValidatorWrapper.Create},
                    {typeof (DoubleValidator), NumberValidatorWrapper<DoubleValidator>.Create},
                    {typeof (IntegerValidator), NumberValidatorWrapper<IntegerValidator>.Create},
                    {typeof (SameAsValidator), SameAsValidatorWrapper.Create},
                    {typeof (RegularExpressionValidator), RegexValidatorWrapper.Create},
                    {typeof (DateTimeValidator), DateTimeValidatorWrapper.Create},

                };

    public CastleModelValidatorProvider()
    {
        validationRegistry = new CachedValidationRegistry();
    }

    protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
    {
        foreach (AbstractValidationAttribute attribute in attributes.OfType<AbstractValidationAttribute>())
        {
            CastleModelValidationFactory cntr;
            IValidator validator = attribute.Build();

            if (!validatorFactories.TryGetValue(validator.GetType(), out cntr))
            {
                cntr = validatorFactories.Default;
            }

            yield return cntr(metadata, context, validationRegistry, validator);
        }
    }



    public class RequiredValidatorWrapper : CastleModelValidator<NonEmptyValidator>
    {
        public RequiredValidatorWrapper(ModelMetadata metadata, ControllerContext controllerContext, IValidatorRegistry validatorRegistry, NonEmptyValidator validator)
            : base(metadata, controllerContext, validatorRegistry, validator)
        {
        }

        internal static ModelValidator Create(ModelMetadata metadata, ControllerContext context, IValidatorRegistry validatorRegistry, IValidator validator)
        {
            return new RequiredValidatorWrapper(metadata, context, validatorRegistry, (NonEmptyValidator)validator);
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            yield return new ModelClientValidationRequiredRule(Validator.ErrorMessage);
        }
    }

    public class RegexValidatorWrapper : CastleModelValidator<RegularExpressionValidator>
    {
        public RegexValidatorWrapper(ModelMetadata metadata, ControllerContext controllerContext, IValidatorRegistry validatorRunner, RegularExpressionValidator validator)
            : base(metadata, controllerContext, validatorRunner, validator)
        {
        }

        internal static ModelValidator Create(ModelMetadata metadata, ControllerContext context, IValidatorRegistry validatorRunner, IValidator validator)
        {
            return new RegexValidatorWrapper(metadata, context, validatorRunner, (RegularExpressionValidator)validator);
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            yield return new ModelClientValidationRegexRule(Validator.ErrorMessage, Validator.Expression);
        }
    }

    public class EmailValidatorWrapper : CastleModelValidator<EmailValidator>
    {
        public EmailValidatorWrapper(ModelMetadata metadata, ControllerContext controllerContext, IValidatorRegistry validatorRunner, EmailValidator validator)
            : base(metadata, controllerContext, validatorRunner, validator)
        {
        }

        internal static ModelValidator Create(ModelMetadata metadata, ControllerContext context, IValidatorRegistry validatorRunner, IValidator validator)
        {
            return new EmailValidatorWrapper(metadata, context, validatorRunner, (EmailValidator)validator);
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            yield return new ModelClientValidationRule
                             {
                                 ErrorMessage = Validator.ErrorMessage,
                                 ValidationType = "email"
                             };
        }
    }

    public class SameAsValidatorWrapper : CastleModelValidator<SameAsValidator>
    {
        public SameAsValidatorWrapper(ModelMetadata metadata, ControllerContext controllerContext, IValidatorRegistry validatorRegistry, SameAsValidator validator)
            : base(metadata, controllerContext, validatorRegistry, validator)
        {
        }

        internal static ModelValidator Create(ModelMetadata metadata, ControllerContext context, IValidatorRegistry validatorregistry, IValidator validator)
        {
            return new SameAsValidatorWrapper(metadata, context, validatorregistry, (SameAsValidator)validator);
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = Validator.ErrorMessage,
                ValidationType = "equalTo_" + Validator.PropertyToCompare
            };
        }
    }

    public class NumberValidatorWrapper<TValidator> : CastleModelValidator<TValidator> where TValidator : IValidator
    {
        public NumberValidatorWrapper(ModelMetadata metadata, ControllerContext controllerContext, IValidatorRegistry validatorRunner, TValidator validator)
            : base(metadata, controllerContext, validatorRunner, validator)
        {
        }

        internal static ModelValidator Create(ModelMetadata metadata, ControllerContext context, IValidatorRegistry validatorRunner, IValidator validator)
        {
            return new NumberValidatorWrapper<TValidator>(metadata, context, validatorRunner, (TValidator)validator);
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = Validator.ErrorMessage,
                ValidationType = "number"
            };
        }
    }

    public class DateTimeValidatorWrapper : CastleModelValidator<DateTimeValidator>
    {
        public DateTimeValidatorWrapper(ModelMetadata metadata, ControllerContext controllerContext, IValidatorRegistry validatorRegistry, DateTimeValidator validator)
            : base(metadata, controllerContext, validatorRegistry, validator)
        {
        }

        internal static ModelValidator Create(ModelMetadata metadata, ControllerContext context, IValidatorRegistry validatorregistry, IValidator validator)
        {
            return new DateTimeValidatorWrapper(metadata, context, validatorregistry, (DateTimeValidator)validator);
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            yield return new ModelClientValidationRule()
                             {
                                 ErrorMessage = Validator.ErrorMessage,
                                 ValidationType = "date"
                             };
        }
    }
}