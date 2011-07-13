using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Immutables
{
    public class PatternMatch<T,TResult>
    {
        private readonly T _value;
        private readonly List<Tuple<Predicate<T>, Func<T, TResult>>> _cases = new List<Tuple<Predicate<T>, Func<T, TResult>>>();
        private Func<T, TResult> _elseFunc;

        public PatternMatch(T value)
        {
            _value = value;
        }

        public PatternMatch<T, TResult> With(Predicate<T> condition, Func<T, TResult> result)
        {
            _cases.Add(Tuple.Create(condition, result));
            return this;
        }

        public PatternMatch<T, TResult> Else(Func<T, TResult> result)
        {
            if(_elseFunc != null)
                throw new InvalidOperationException("Cannot have more than one else condition");
            _elseFunc = result;
            return this;
        }

        public TResult Do()
        {
            if (_elseFunc != null)
                _cases.Add(Tuple.Create<Predicate<T>, Func<T, TResult>>(x => true, _elseFunc));
            foreach (var item in _cases)
                if (item.Item1(_value))
                    return item.Item2(_value);

            throw new MatchNotFoundException("Incomplete pattern match");
        }
    }

    public class MatchNotFoundException : Exception
    {
        public MatchNotFoundException(string message) : base(message)
        {}
    }

    public class PatternMatchContext<T>
    {
        private readonly T _value;

        public PatternMatchContext(T value)
        {
            _value = value;
        }

        public PatternMatch<T, TResult> With<TResult>(Predicate<T> condition, Func<T, TResult> result)
        {
            var match = new PatternMatch<T, TResult>(_value);
            return match.With(condition, result);
        }
    }

    public static class PatternMatchExceptions
    {
        public static PatternMatchContext<T> Match<T>(this T value)
        {
            return new PatternMatchContext<T>(value);
        }
    }
}
