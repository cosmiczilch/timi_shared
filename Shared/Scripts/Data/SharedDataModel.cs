using TimiShared.Service;

public class SharedDataModel : IService {

    public static SharedDataModel Instance {
        get {
            return ServiceLocator.Service<SharedDataModel>();
        }
    }

    #region Data
    // Nothing here yet
    #endregion

    public void LoadData() {
        // No data to load yet
    }
}
