namespace Framework.Business
{
    using EnsureThat;
    using FluentValidation;
    using Framework.Business.Extension;
    using Framework.Business.Models;
    //using Framework.Entity.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Core.Objects.DataClasses;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ValidationHelpers" />.
    /// </summary>
    public static class ValidationHelpers
    {

        /// <summary>
        /// The IsPreciseToDay.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset}"/>.</param>
        /// <param name="fractionalDayNotAllowedErrorCode">The fractionalDayNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset> IsPreciseToDay<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder, TErrorCode fractionalDayNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => x.Hour == 0 && x.Minute == 0 && x.Second == 0 && x.Millisecond == 0).WithErrorEnum(fractionalDayNotAllowedErrorCode).WithMessage("Fractional minutes are not allowed.");
        }

        /// <summary>
        /// The IsPreciseToDay.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset?}"/>.</param>
        /// <param name="fractionalDayNotAllowedErrorCode">The fractionalDayNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset?}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset?> IsPreciseToDay<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset?> ruleBuilder, TErrorCode fractionalDayNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => !x.HasValue || (x.Value.Hour == 0 && x.Value.Minute == 0 && x.Value.Second == 0 && x.Value.Millisecond == 0)).WithErrorEnum(fractionalDayNotAllowedErrorCode).WithMessage("Fractional hours are not allowed.");
        }

        /// <summary>
        /// The IsPreciseToHours.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset}"/>.</param>
        /// <param name="fractionalHoursNotAllowedErrorCode">The fractionalHoursNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset> IsPreciseToHours<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder, TErrorCode fractionalHoursNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => x.Minute == 0 && x.Second == 0 && x.Millisecond == 0).WithErrorEnum(fractionalHoursNotAllowedErrorCode).WithMessage("Fractional minutes are not allowed.");
        }

        /// <summary>
        /// The IsPreciseToHours.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset?}"/>.</param>
        /// <param name="fractionalHoursNotAllowedErrorCode">The fractionalHoursNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset?}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset?> IsPreciseToHours<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset?> ruleBuilder, TErrorCode fractionalHoursNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => !x.HasValue || (x.Value.Minute == 0 && x.Value.Second == 0 && x.Value.Millisecond == 0)).WithErrorEnum(fractionalHoursNotAllowedErrorCode).WithMessage("Fractional hours are not allowed.");
        }

        /// <summary>
        /// The IsPreciseToMinutes.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset}"/>.</param>
        /// <param name="fractionalMinutesNotAllowedErrorCode">The fractionalMinutesNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset> IsPreciseToMinutes<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder, TErrorCode fractionalMinutesNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => x.Second == 0 && x.Millisecond == 0).WithErrorEnum(fractionalMinutesNotAllowedErrorCode).WithMessage("Fractional minutes are not allowed.");
        }

        /// <summary>
        /// The IsPreciseToMinutes.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset?}"/>.</param>
        /// <param name="fractionalMinutesNotAllowedErrorCode">The fractionalMinutesNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset?}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset?> IsPreciseToMinutes<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset?> ruleBuilder, TErrorCode fractionalMinutesNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => !x.HasValue || (x.Value.Second == 0 && x.Value.Millisecond == 0)).WithErrorEnum(fractionalMinutesNotAllowedErrorCode).WithMessage("Fractional minutes are not allowed.");
        }

        /// <summary>
        /// The IsPreciseToSeconds.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset}"/>.</param>
        /// <param name="fractionalSecondsNotAllowedErrorCode">The fractionalSecondsNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset> IsPreciseToSeconds<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder, TErrorCode fractionalSecondsNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => x.Millisecond == 0).WithErrorEnum(fractionalSecondsNotAllowedErrorCode).WithMessage("Fractional seconds are not allowed.");
        }

        /// <summary>
        /// The IsPreciseToSeconds.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset?}"/>.</param>
        /// <param name="fractionalSecondsNotAllowedErrorCode">The fractionalSecondsNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset?}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset?> IsPreciseToSeconds<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset?> ruleBuilder, TErrorCode fractionalSecondsNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => !x.HasValue || x.Value.Millisecond == 0).WithErrorEnum(fractionalSecondsNotAllowedErrorCode).WithMessage("Fractional seconds are not allowed.");
        }



        /// <summary>
        /// The GetPropertyMessageName.
        /// </summary>
        /// <param name="propertyName">The propertyName<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string GetPropertyMessageName(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return "Key";
            }
            else
            {
                return propertyName;
            }
        }

        /// <summary>
        /// The GetPropertyName.
        /// </summary>
        /// <typeparam name="T1">.</typeparam>
        /// <typeparam name="T2">.</typeparam>
        /// <param name="nameExpression">The nameExpression<see cref="Expression{Func{T1, T2}}"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string GetPropertyName<T1, T2>(Expression<Func<T1, T2>> nameExpression)
        {
            if (nameExpression?.Body is UnaryExpression unaryExpression)
            {
                if (unaryExpression.Operand is MemberExpression memberExpression)
                {
                    return memberExpression.Member.Name;
                }
            }
            else if (nameExpression?.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            return string.Empty;
        }

        /// <summary>
        /// The GetPropertyName.
        /// </summary>
        /// <typeparam name="T1">.</typeparam>
        /// <typeparam name="T2">.</typeparam>
        /// <typeparam name="T3">.</typeparam>
        /// <typeparam name="T4">.</typeparam>
        /// <param name="nameExpression1">The nameExpression1<see cref="Expression{Func{T1, T2}}"/>.</param>
        /// <param name="nameExpression2">The nameExpression2<see cref="Expression{Func{T3, T4}}"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string GetPropertyName<T1, T2, T3, T4>(Expression<Func<T1, T2>> nameExpression1, Expression<Func<T3, T4>> nameExpression2)
        {
            string propertyName;
            if (nameExpression1 != null)
            {
                propertyName = GetPropertyName(nameExpression1);
            }
            else
            {
                propertyName = GetPropertyName(nameExpression2);
            }

            return propertyName;
        }

        /// <summary>
        /// The AllIndexesOf.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="items">The items<see cref="IEnumerable{T}"/>.</param>
        /// <param name="item">The item<see cref="T"/>.</param>
        /// <returns>The <see cref="IEnumerable{int}"/>.</returns>
        private static IEnumerable<int> AllIndexesOf<T>(this IEnumerable<T> items, T item)
        {
            return items.Select((x, i) => x.Equals(item) ? i : -1).Where(x => x != -1);
        }

        //public static async Task<ErrorRecords<Tkey, TErrorCode>> ExistsValidationAsync<Tkey, TModel, TCompareType, TErrorCode, TEntity>(Func<TModel, Task<TEntity>> databaseSelector, TModel model, Expression<Func<IIndexedItem<TModel>, TCompareType>> modelPredicateExpression, TErrorCode keyDoesNotExist, Expression<Func<TModel, object>> propertyNameExpression = null)
        //   where TErrorCode : struct, Enum
        //    where TEntity : IEntityWithId<Tkey>
        //    where TModel : IModelWithKey<Tkey>
        //{
        //    EnsureArg.IsNotNull(databaseSelector, nameof(databaseSelector));
        //    EnsureArg.IsNotNull(modelPredicateExpression, nameof(modelPredicateExpression));

        //    var modelPredicate = modelPredicateExpression.Compile();
        //    var propertyName = GetPropertyName(propertyNameExpression, modelPredicateExpression);
        //    var propertyMessageName = GetPropertyMessageName(propertyName);
        //    var message = $"{propertyMessageName} does not exist.";

        //    var errorRecords = new ErrorRecords<Tkey, TErrorCode>();
        //    var existingKeys = await databaseSelector(model).ConfigureAwait(false);
        //    if (existingKeys.Id == model.Id)
        //    {
        //        errorRecords.Add(model.CreateErrorMessage(propertyName, keyDoesNotExist, message, missingKey));
        //    }
        //    return errorRecords;
        //}
    }
}
