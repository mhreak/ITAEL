using MD.PersianDateTime;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace Web.ModelBinder
{
    public class PersianDateModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            DateTime? dt;

            try
            {
                var value = valueResult.FirstValue;
                if (!string.IsNullOrEmpty(value))
                {
                    var persianDateTime = PersianDateTime.Parse(value);
                    dt = persianDateTime.ToDateTime();
                }
                else
                {
                    dt = null;
                }

            }
            catch (Exception e)
            {
                dt = null;
            }

            if (dt == null)
            {
                //bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Jalali date is not valid");
            }
            else if (Nullable.GetUnderlyingType(bindingContext.ModelType) == typeof(DateTime))
            {
                bindingContext.Result = ModelBindingResult.Success(dt);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Success(dt.Value);
            }

            return Task.CompletedTask;
        }

        public class PersianDateModelBinderProvider : IModelBinderProvider
        {
            private readonly IModelBinder _binder = new PersianDateModelBinder();

            public IModelBinder GetBinder(ModelBinderProviderContext context)
            {
                var isDateTimeNullableType = context.Metadata.ModelType == typeof(DateTime?);
                var isDateTimeType = context.Metadata.ModelType == typeof(DateTime);

                return isDateTimeType || isDateTimeNullableType ? _binder : null;
            }
        }
    }
}