using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Immutables
{

    public class MatchNotFoundException : Exception
    {
        public MatchNotFoundException(string message) : base(message)
        {}
    }

    public interface IPatternAction<out T, in TResult>
    {
        void Then(Func<T,TResult> action);
    }

    public interface IPatternMatcher<out T, in TResult>
    {
        IPatternAction<T,TResult> Case(Func<T, bool> predicate);
        void Else(Func<T, TResult> action); 
    }

    public static class PatternMatchExtensions
    {
        
        private class PatternMatcherInfo<T, TResult> : IPatternMatcher<T, TResult>, IPatternAction<T,TResult>
        {
            private Func<T, bool> _case;            
            private Func<T,TResult> _action;
            private Func<T, TResult> _else;
            private readonly T _value;
            public PatternMatcherInfo(T value)
            {
                _value = value;
            }
            
            IPatternAction<T, TResult> IPatternMatcher<T, TResult>.Case(Func<T, bool> predicate)
            {
                _case = predicate;
                return this;
            }

            void IPatternMatcher<T, TResult>.Else(Func<T, TResult> action)
            {
                _else = action;
            }

            void IPatternAction<T, TResult>.Then(Func<T, TResult> action)
            {
                _action = action;
            }

            internal bool IsCase()
            {
                return _case != null && _case(_value);
            }

            internal bool IsElse()
            {
                return _else != null;
            }

            internal TResult Evaluate()
            {
                if (IsCase())
                    return _action(_value);
                if (IsElse())
                    return _else(_value);
                return default(TResult);
            }

        }

        public static TResult Match<T, TResult>(this T value, params Action<IPatternMatcher<T,TResult>>[] cases)
        {
            foreach (var aCase in cases)
            {
                var newCase = new PatternMatcherInfo<T, TResult>(value);
                aCase(newCase);
                if (newCase.IsCase() || newCase.IsElse())
                    return newCase.Evaluate();
            }
            throw new MatchNotFoundException("Incomplete Pattern Match");
        }
    }
}
