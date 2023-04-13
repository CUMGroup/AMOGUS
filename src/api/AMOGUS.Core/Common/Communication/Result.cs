
namespace AMOGUS.Core.Common.Communication {
    public enum ResultState : byte {
        Faulted,
        Success
    }

    public readonly struct Result<A> : IEquatable<Result<A>>, IComparable<Result<A>> {

        internal readonly ResultState State;
        public readonly A Value;
        readonly Exception? exception;

        public Result(A value) {
            State = ResultState.Success;
            this.Value = value;
            exception = null;
        }

        public Result(Exception ex) {
            State = ResultState.Faulted;
            this.exception = ex;
            Value = default;
        }

        public static implicit operator Result<A>(A value) => new(value);

        public static implicit operator Result<A>(Exception ex) => new(ex);

        public static implicit operator Exception(Result<A> result) => ExceptionOrDefault(result.exception);

        public static implicit operator Result(Result<A> res) =>
            res.IsFaulted ? new(ExceptionOrDefault(res.exception)) : new();


        public bool IsFaulted => State == ResultState.Faulted;

        public bool IsSuccess => State == ResultState.Success;

        public A ValueOrDefault(A defaultValue) {
            return IsSuccess ? Value : defaultValue;
        }

        public A ValueOrDefault(Func<A> defaultValueProvider) {
            return IsSuccess ? Value : defaultValueProvider();
        }

        public async Task<A> ValueOrDefaultAsync(Func<Task<A>> defaultValueProvider) {
            return IsSuccess ? Value : await defaultValueProvider();
        }

        public override string ToString() =>
            IsFaulted ?
                exception?.ToString() ?? "Undefined Exception!" :
                Value?.ToString() ?? "(null)";

        public bool Equals(Result<A> b) {
            if (IsFaulted && !b.IsFaulted || !IsFaulted && b.IsFaulted)
                return false;
            if (IsFaulted && b.IsFaulted) {
                if (exception == null && b.exception == null)
                    return true;
                return exception?.Equals(b.exception) ?? false;
            }
            if (Value == null && b.Value == null)
                return true;
            return Value?.Equals(b.Value) ?? false;
        }

        public static bool operator ==(Result<A> a, Result<A> b) =>
            Equals(a, b);

        public static bool operator !=(Result<A> a, Result<A> b) =>
            !(a == b);

        public A IfFail(A defaultValue) =>
            IsFaulted ? defaultValue : Value;

        public A IfFail(Func<Exception, A> f) =>
            IsFaulted ? f(ExceptionOrDefault(exception)) : Value;

        public void IfSucc(Action<A> f) {
            if (IsSuccess)
                f(Value);
        }

        public R Match<R>(Func<A, R> Succ, Func<Exception, R> Fail) =>
            IsFaulted ? Fail(ExceptionOrDefault(exception)) : Succ(Value);

        public Result<B> Map<B>(Func<A, B> map) =>
            IsFaulted ? new Result<B>(ExceptionOrDefault(exception)) : new Result<B>(map(Value));

        public async Task<Result<B>> MapAsync<B>(Func<A, Task<B>> map) =>
            IsFaulted ? new Result<B>(ExceptionOrDefault(exception)) : new Result<B>(await map(Value));

        public int CompareTo(Result<A> b) {
            if (IsFaulted && b.IsFaulted)
                return 0;
            if (IsFaulted && !b.IsFaulted)
                return -1;
            if (!IsFaulted && b.IsFaulted)
                return 1;
            return Comparer<A>.Default.Compare(Value, b.Value);
        }

        public static bool operator <(Result<A> a, Result<A> b) =>
            a.CompareTo(b) < 0;
        public static bool operator <=(Result<A> a, Result<A> b) =>
            a.CompareTo(b) <= 0;
        public static bool operator >(Result<A> a, Result<A> b) =>
            a.CompareTo(b) > 0;
        public static bool operator >=(Result<A> a, Result<A> b) =>
            a.CompareTo(b) >= 0;

        public override bool Equals(object? obj) {
            return obj != null && obj is Result<A> && Equals((Result<A>) obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(IsSuccess, Value, exception);
        }

        private static Exception ExceptionOrDefault(Exception? ex) =>
            ex ?? new Exception("Undefined Exception!");
    }

    public readonly struct Result : IEquatable<Result>, IComparable<Result> {

        internal readonly ResultState State;
        readonly Exception? exception;

        public Result() {
            State = ResultState.Success;
            exception = null;
        }

        public Result(Exception ex) {
            State = ResultState.Faulted;
            this.exception = ex;
        }

        public static implicit operator Result(bool value) => value ? new() : new(new Exception("Undefined Exception!"));

        public static implicit operator Result(Exception ex) => new(ex);

        public static implicit operator Exception(Result result) => ExceptionOrDefault(result.exception);


        public static implicit operator Result<bool>(Result res) =>
            res.IsFaulted ? new(ExceptionOrDefault(res.exception)) : new(true);


        public bool IsFaulted => State == ResultState.Faulted;

        public bool IsSuccess => State == ResultState.Success;

        public override string ToString() =>
            IsFaulted ?
                exception?.ToString() ?? "Undefined Exception!" :
                "Successful";

        public bool Equals(Result b) {
            if (IsFaulted && !b.IsFaulted || !IsFaulted && b.IsFaulted)
                return false;
            if (IsFaulted && b.IsFaulted) {
                if (exception == null && b.exception == null)
                    return true;
                return exception?.Equals(b.exception) ?? false;
            }
            return true;
        }

        public static bool operator ==(Result a, Result b) =>
            Equals(a, b);

        public static bool operator !=(Result a, Result b) =>
            !(a == b);

        public void IfFail(Action<Exception> f) {
            if (IsFaulted)
                f(ExceptionOrDefault(exception));
        }
        public void IfFail(Action f) {
            if (IsFaulted)
                f();
        }

        public void IfSucc(Action f) {
            if (IsSuccess)
                f();
        }

        public R Match<R>(Func<R> Succ, Func<Exception, R> Fail) =>
            IsFaulted ? Fail(ExceptionOrDefault(exception)) : Succ();

        public int CompareTo(Result b) {
            if (IsFaulted && b.IsFaulted)
                return 0;
            if (IsFaulted && !b.IsFaulted)
                return -1;
            if (!IsFaulted && b.IsFaulted)
                return 1;
            return 0;
        }

        public static bool operator <(Result a, Result b) =>
            a.CompareTo(b) < 0;
        public static bool operator <=(Result a, Result b) =>
            a.CompareTo(b) <= 0;
        public static bool operator >(Result a, Result b) =>
            a.CompareTo(b) > 0;
        public static bool operator >=(Result a, Result b) =>
            a.CompareTo(b) >= 0;

        private static Exception ExceptionOrDefault(Exception? ex) =>
            ex ?? new Exception("Undefined Exception!");

        public override bool Equals(object? obj) {
            return obj != null && obj is Result && Equals((Result) obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(IsSuccess, exception);
        }
    }
}
