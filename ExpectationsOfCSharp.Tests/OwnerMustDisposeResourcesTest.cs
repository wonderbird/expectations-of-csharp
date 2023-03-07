using System;
using Xunit;

namespace ExpectationsOfCSharp.Tests;

/// <summary>
/// Classes holding a reference to an IDisposable instance are responsible for its cleanup.
/// </summary>
/// <see href="https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose">
/// Microsoft Learn: Implement a Dispose method
/// </see>
public class OwnerMustDisposeResourcesTest
{
    [Fact]
    public void DisposeMustBeCalledForOwnedMember()
    {
        var isDisposed = false;
        void NotifyDisposed() => isDisposed = true;

        using (new OwnerOfDisposable(NotifyDisposed)) { }

        Assert.True(isDisposed);
    }

    private sealed class OwnerOfDisposable : IDisposable
    {
        private readonly OwnedDisposable _ownedDisposable;

        public OwnerOfDisposable(Action disposeAction) => _ownedDisposable = new OwnedDisposable(disposeAction);

        public void Dispose() => _ownedDisposable.Dispose();
    }

    private sealed class OwnedDisposable : IDisposable
    {
        private readonly Action _disposeAction;

        public OwnedDisposable(Action disposeAction) => _disposeAction = disposeAction;

        public void Dispose() => _disposeAction();
    }
}
