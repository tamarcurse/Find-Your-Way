//שקופית4 צד ימין

import React, { useEffect, useRef, useState } from "react";
import { updatePrivateProvider, updateFactoryPrivate } from "../../redux/actions";
import { CheckID, CheckName } from "../normality"
import { connect } from "react-redux"
import ReactGoogleAutocomplete from "react-google-autocomplete";
import key from "../key";
import { Client } from "../Client";
import { Redirect } from "react-router-dom/cjs/react-router-dom.min";
function mapStateToProps(state) {
    ;
    return { currentProvider: state.currentProvider }
}
export default connect(mapStateToProps)(
    function PrivateFactory(props) {


        const status = props.status
        const nameRef = useRef('')
        const addressRef = useRef('')
        const timeRef = useRef('')
        const crateVolumeRef = useRef('')
        const crateWeightRef = useRef('')
        const maxGoodsInCrateRef = useRef('')
        const [crateWeight, setCrateWeight] = useState("")
        const [crateWeightError, setCrateWeightError] = useState()
        const [time, setTime] = useState("")
        const [address, setAddress] = useState("")

        const [crateVolume, setCrateVolume] = useState(false)
        const [crateVolumeError, setCrateVolumeError] = useState(false)

        const [maxGoodsInCrate, setMaxGoodsInCrate] = useState(0)
        const [nameFactory, setNameFactory] = useState("")
        const [nameFactoryError, setNameFactoryError] = useState(false)
        const [maxGoodsInCrateError, setMaxGoodsInCrateError] = useState(false)
        const [toValidate, setToValidate] = useState(false)

        const [provider, setProvider] = useState(null)

        const { currentProvider, dispatch } = props
        const factoryPrivate = currentProvider.factoryPrivate
        const [RedirectFlag, setRedirectFlag] = useState(false)

        useEffect(() => {
            if (status == "UPDATE") {
                nameRef.current.value = factoryPrivate.FactoryName
                setNameFactory(factoryPrivate.FactoryName)

                addressRef.current.value = factoryPrivate.placeAddress
                setAddress(factoryPrivate.placeAddress)
                //timeRef.current.value = factoryPrivate.LeavingTime
                crateVolumeRef.current.value = factoryPrivate.CrateVolume
                setCrateVolume(factoryPrivate.CrateVolume)
                crateWeightRef.current.value = factoryPrivate.CrateWeight
                setCrateWeight(factoryPrivate.CrateWeight)
                maxGoodsInCrateRef.current.value = factoryPrivate.MaxGoodsInCrate
                setMaxGoodsInCrate(factoryPrivate.MaxGoodsInCrate)
                let MinutesString = factoryPrivate.LeavingTime.Minutes
                if (factoryPrivate.LeavingTime.Minutes < 10)
                    MinutesString = "0" + MinutesString
                let houreString = factoryPrivate.LeavingTime.Hours
                if (factoryPrivate.LeavingTime.Hours < 10)
                    houreString = "0" + houreString
                timeRef.current.value = houreString + ":" + MinutesString





            }

        },
            [])
        function checkAndSet() {
            setToValidate(true)
            if (nameFactory.length == 0 && address.length == 0 && crateVolume.length == 0 && crateVolume.length == 0 && crateWeight.length == 0)
                return
           

            if (!CheckName(nameFactory))
                setNameFactoryError(true)
            else
                setNameFactoryError(false)

            setCrateVolumeError(isNaN(crateVolume))


            setCrateWeightError(isNaN(crateWeight))
            setMaxGoodsInCrateError(maxGoodsInCrate <= 0)
            if (maxGoodsInCrateError || crateVolumeError || nameFactoryError || crateWeightError || crateVolumeError || maxGoodsInCrateError)
                return




            //עדכון פרטי הספק הנוכחי כולו במסד נתונים
            const providerUpdate = {

                placeAddress: address,
                placeGoogleMapsId: factoryPrivate.placeGoogleMapsId,
                ProviderName: currentProvider.providerPrivate.ProviderName,
                PasswordProvider: currentProvider.providerPrivate.PasswordProvider,
                FactoryName: nameFactory,
                LeavingTime: {},
                ProviderId: currentProvider.providerPrivate.ProviderId,
                CrateWeight: crateWeight,
                CrateVolume: crateVolume,
                MaxGoodsInCrate: maxGoodsInCrate
            }
            Client.put('/Provider/update?LeavingTime=' + timeRef.current.value, providerUpdate).then(res => {
                //redaxעדכון פרטי הספק הנוכחי ב 

                dispatch(updateFactoryPrivate({
                    FactoryName: res.data.FactoryName,
                    LeavingTime: res.data.LeavingTime,
                    placeGoogleMapsId: res.data.placeGoogleMapsId,
                    placeAddress: res.data.placeAddress,
                    CrateVolume: res.data.CrateVolume,
                    CrateWeight: res.data.CrateWeight,
                    MaxGoodsInCrate: res.data.MaxGoodsInCrate

                }))
                console.log(res.data.MaxGoodsInCrate)
            })
            setRedirectFlag(true)
            // })



            //תנווט לדף הראשי


        }
        if (RedirectFlag)
            return <Redirect to="/provider/trucks"></Redirect>
        return (
            <>
                <h4> פרטי המפעל</h4>
                <label>שם המפעל</label>
                <input ref={nameRef} value={nameFactory} onChange={e => { setNameFactory(e.target.value); }} />
                <div style={{ visibility: toValidate && nameFactory.length == 0 ? "visible" : "hidden" }}>שדה חובה  </div>
                <div style={{ visibility: toValidate && crateVolume.length != 0 && nameFactoryError ? "visible" : "hidden" }}>תוכן שגוי</div>

                <label>כתובת</label>
               
                <input ref={addressRef} value={address} onChange={e => { setAddress(e.target.value); }} />

                <div style={{ visibility: toValidate && address.length == 0 ? "visible" : "hidden" }}>שדה חובה  </div>

                <label>השעה שבה יוצאות המשאיות</label>
                <input type="time" ref={timeRef} />

                <label>נפח ארגז סחורה</label>
                <input ref={crateVolumeRef} value={crateVolume} onChange={e => { setCrateVolume(e.target.value); }} />
                <div style={{ visibility: toValidate && crateVolume.length == 0 ? "visible" : "hidden" }}>שדה חובה  </div>
                <div style={{ visibility: toValidate && crateVolume.length != 0 && crateVolumeError ? "visible" : "hidden" }}>תוכן שגוי</div>

                <label>כמות מוצרים בארגז</label>
                <input type="number" ref={maxGoodsInCrateRef} value={maxGoodsInCrate} onChange={e => { setMaxGoodsInCrate(e.target.value); }} />
                <div style={{ visibility: toValidate && maxGoodsInCrate.length == 0 ? "visible" : "hidden" }}>שדה חובה  </div>
                <div style={{ visibility: toValidate && crateVolume.length != 0 && maxGoodsInCrateError ? "visible" : "hidden" }}>תוכן שגוי</div>

                <label>משקל ארגז</label>
                <input type="text" ref={crateWeightRef} value={crateWeight} onChange={e => { setCrateWeight(e.target.value); }} />
                <div style={{ visibility: toValidate && crateWeight.length == 0 ? "visible" : "hidden" }}>שדה חובה  </div>
                <div style={{ visibility: toValidate && crateWeight.length != 0 && crateWeightError ? "visible" : "hidden" }}>תוכן שגוי </div>

                <button onClick={checkAndSet}>שמור</button>

            </>
        )

    }
)