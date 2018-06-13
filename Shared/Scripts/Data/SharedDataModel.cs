using System;
using TimiShared.Service;

public class SharedDataModel : DataModelBase, IService {

    public static SharedDataModel Instance {
        get {
            return ServiceLocator.Service<SharedDataModel>();
        }
    }

    #region DataModelBase
    protected override string GetDataModelRootPath {
        get {
            return "SharedDataModels/";
        }
    }

    protected override Type[] DataModelTypes {
        get {
            return this._dataModelTypes;
        }
    }
    #endregion

    private Type[] _dataModelTypes = {
        // Add data types here
    };
}
