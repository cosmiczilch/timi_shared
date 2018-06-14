using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ProtoBuf;
using TimiShared.Loading;
using TimiShared.Extensions;
using UnityEngine;

public abstract class DataModelBase : MonoBehaviour {

    private const string APPDATAMODEL_EXTENSION = ".pb";

    // This is the root of the app data models inside streaming assets
    protected abstract string GetDataModelRootPath {
        get;
    }

    protected abstract Type[] DataModelTypes {
        get;
    }

    private Dictionary<Type, object> _dataModels = new Dictionary<Type, object>();
    // TODO: Remove this getter
    protected object GetDataModelForType(Type dataModelType) {
        return this._dataModels.GetOrDefault(dataModelType);
    }

    private int _numDataModelsLoaded = 0;

    public Coroutine LoadDataAsync() {
        return this.StartCoroutine(this.LoadDataInternal());
    }

    private IEnumerator LoadDataInternal() {
        this._numDataModelsLoaded = 0;

        for (int i = 0; i < this.DataModelTypes.Length; ++i) {
            string filePath = Path.Combine(this.GetDataModelRootPath, this.DataModelTypes[i].ToString() + APPDATAMODEL_EXTENSION);
            int i_copy = i;
            AssetLoader.Instance.GetStreamFromStreamingAssets(filePath, (Stream stream) => {
                this.OnStreamLoaded(stream, this.DataModelTypes[i_copy]);
            });
        }

        // Wait for all data models to be loaded
        while (this._numDataModelsLoaded < this.DataModelTypes.Length) {
            yield return null;
        }
    }

    private void OnStreamLoaded(Stream stream, Type dataModelType) {
        if (stream != null) {
            object dataModel = Serializer.NonGeneric.Deserialize(dataModelType, stream);
            this._dataModels.Add(dataModelType, dataModel);
            AssetLoader.Instance.CloseStream(stream);

            ++this._numDataModelsLoaded;
        }
    }


}
