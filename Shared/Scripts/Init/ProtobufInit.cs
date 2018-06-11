﻿using ProtoBuf.Meta;
using UnityEngine;

namespace TimiShared.Init {
    public class ProtobufInit : MonoBehaviour, IInitializable {

        #region IInitializable
        public void StartInitialize() {
            this.RegisterCustomTypes();
            this.IsFullyInitialized = true;
        }

        public bool IsFullyInitialized {
            get; private set;
        }
        #endregion

        private void RegisterCustomTypes() {
            // Add vector2 de/serialization support:
            if (!ProtoBuf.Meta.RuntimeTypeModel.Default.IsDefined(typeof(Vector2))) {
                MetaType metaType = ProtoBuf.Meta.RuntimeTypeModel.Default.Add(typeof(Vector2), applyDefaultBehaviour: false);
                metaType.Add("x", "y");
            }

            // Add vector3 de/serialization support:
            if (!ProtoBuf.Meta.RuntimeTypeModel.Default.IsDefined(typeof(Vector3))) {
                MetaType metaType = ProtoBuf.Meta.RuntimeTypeModel.Default.Add(typeof(Vector3), applyDefaultBehaviour: false);
                metaType.Add("x", "y", "z");
            }

            // Add vector4 de/serialization support:
            if (!ProtoBuf.Meta.RuntimeTypeModel.Default.IsDefined(typeof(Vector4))) {
                MetaType metaType = ProtoBuf.Meta.RuntimeTypeModel.Default.Add(typeof(Vector4), applyDefaultBehaviour: false);
                metaType.Add("x", "y", "z", "w");
            }

            // Append any other types here as needed
        }
    }
}
