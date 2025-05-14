import produce from 'immer'

const initialState =
{
    providerPrivate: {
        ProviderId: null,
        ProviderName: null,
        PasswordProvider: null,
    }
    , factoryPrivate: {
        FactoryName: null,
        LeavingTime: null,
        placeGoogleMapsId: null,
        Lat: null,
        Lang: null,
        placeAddress: null,
        CrateVolume: null,
        CrateWeight: null,
        MaxGoodsInCrate: null
    }
};
// axios.get("https://localhost:44345/api/Provider/getAll").then((res)=>{
//      initialState =res.data;
//     console.log(res.data);

// }).catch(error=>{
//      initialState =null;
//     console.log(error);

// })

const reducer = produce((state, action) => {
    switch (action.type) {
        case 'UPDATE_ALL_PROVIDER':
            {

                state.providerPrivate.ProviderId = action.payLoad.ProviderId
                state.providerPrivate.ProviderName = action.payLoad.ProviderName
                state.providerPrivate.PasswordProvider = action.payLoad.PasswordProvider
                state.factoryPrivate.FactoryName = action.payLoad.FactoryName
                state.factoryPrivate.LeavingTime = action.payLoad.LeavingTime
                state.factoryPrivate.CrateVolume = action.payLoad.CrateVolume
                state.factoryPrivate.CrateWeight = action.payLoad.CrateWeight
                state.factoryPrivate.placeGoogleMapsId = action.payLoad.placeGoogleMapsId
                state.factoryPrivate.Lat = action.payLoad.Lat
                state.factoryPrivate.Lang = action.payLoad.Lang

                state.factoryPrivate.placeAddress = action.payLoad.placeAddress
                state.factoryPrivate.MaxGoodsInCrate = action.payLoad.MaxGoodsInCrate

            }
            break;
        case 'UPDATE_FACTORYP_RIVATE':
            {
                state.factoryPrivate.FactoryName = action.payLoad.FactoryName
                state.factoryPrivate.LeavingTime = action.payLoad.LeavingTime
                state.factoryPrivate.CrateVolume = action.payLoad.CrateVolume
                state.factoryPrivate.CrateWeight = action.payLoad.CrateWeight
                state.factoryPrivate.placeGoogleMapsId = action.payLoad.placeGoogleMapsId
                state.factoryPrivate.Lat = action.payLoad.Lat
                state.factoryPrivate.Lang = action.payLoad.Lang

                state.factoryPrivate.placeAddress = action.payLoad.placeAddress
                state.factoryPrivate.MaxGoodsInCrate = action.payLoad.MaxGoodsInCrate
            }
            break
        case 'UPDATE_PROVIDER_PRIVATE':
            {
                state.providerPrivate.ProviderId = action.payLoad.ProviderId
                state.providerPrivate.ProviderName = action.payLoad.ProviderName
                state.providerPrivate.PasswordProvider = action.payLoad.PasswordProvider

            }


    }

}, initialState)
export default reducer
// axios.get("")