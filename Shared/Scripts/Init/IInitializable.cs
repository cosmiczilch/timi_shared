using System;

namespace TimiShared.Init {
    public interface IInitializable {
        void StartInitialize();
        bool IsFullyInitialized();
    }
}