import React from "react"
import axios from "axios"
const CurrentProviderMiddleWare = (store) => (next) => (action) => {
    if (action.type == 'UPDATE_PROVIDER_PRIVATE')
        if (action.payload.status) {

            axios.post(`https://localhost:44345/api/Provider/add`,
                {
                    placeNmae: store.getState().currentProvider.factoryPrivate.placeNmae,
                    placeAddress: store.getState().currentProvider.factoryPrivate.placeAddress,
                    placeGoogleMapsId: store.getState().currentProvider.factoryPrivate.placeGoogleMapsId,
                    ProviderName: action.payload.ProviderName,
                    PasswordProvider: action.payload.PasswordProvider,
                    FactoryName: store.getState().currentProvider.factoryPrivate.FactoryName,
                    LeavingTime: store.getState().currentProvider.factoryPrivate.LeavingTime,
                    ProviderId: action.payload.ProviderId,
                    CrateWeight: store.getState().currentProvider.factoryPrivate.CrateWeight,
                    CrateVolume: store.getState().currentProvider.factoryPrivate.CrateVolume
                })
                .then((res) => {


                    return next(action)

                }).catch(() => {

                    console.log("cath");
                    //תודיע שהפקודה לא הצליחה

                })
        }
        else {
            axios.put(`https://localhost:44345/api/Provider/update`,
                {
                    placeNmae: store.getState().currentProvider.factoryPrivate.placeNmae,
                    placeAddress: store.getState().currentProvider.factoryPrivate.placeAddress,
                    placeGoogleMapsId: store.getState().currentProvider.factoryPrivate.placeGoogleMapsId,
                    ProviderName: action.payload.ProviderName,
                    PasswordProvider: action.payload.PasswordProvider,
                    FactoryName: store.getState().currentProvider.factoryPrivate.FactoryName,
                    LeavingTime: store.getState().currentProvider.factoryPrivate.LeavingTime,
                    ProviderId: action.payload.ProviderId,
                    CrateWeight: store.getState().currentProvider.factoryPrivate.CrateWeight,
                    CrateVolume: store.getState().currentProvider.factoryPrivate.CrateVolume
                })
                .then((res) => {


                    return next(action)

                }).catch(() => {

                    console.log("cath");
                    //תודיע שהפקודה לא הצליחה
                })
        }



}

export default CurrentProviderMiddleWare