//שקופית4 צד ימין

//import 'privateFactory.css';
import React, { useEffect, useRef, useState } from "react";
import { updatePrivateProvider, updateFactoryPrivate, updateShops, updateTrucks } from "../../redux/actions";
import { CheckID, CheckName } from "../normality"
import { connect } from "react-redux"
import ReactGoogleAutocomplete from "react-google-autocomplete";
//import {Redirect} from 'react-router-dom'
import key from "../key";
import { Client } from "../Client";
import { Redirect } from "react-router-dom/cjs/react-router-dom.min";
import NavBar from "../home/navbar";
import { Form } from "react-bootstrap";
import Box from '@mui/material/Box';
import "../../App.css"
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';
import Modal from '@mui/material/Modal';
import Button from '@mui/material/Button';
function mapStateToProps(state) {

    return {
        currentProvider: state.currentProvider
    }
}
export default connect(mapStateToProps)(
    function PrivateFactory(props) {


        // const { }= props
        // const nameRef = useRef('')
        // const addressRef = useRef('')
        // const timeRef = useRef('')
        // const crateVolumeRef = useRef('')
        // const crateWeightRef = useRef('')
        // const maxGoodsInCrateRef = useRef('')
        const [crateWeight, setCrateWeight] = useState("")
        const [crateWeightError, setCrateWeightError] = useState(false)
        const [time, setTime] = useState("06:00")
        const [address, setAddress] = useState("")
        const newProvider = (props.show == true) ? true : false
        const [crateVolume, setCrateVolume] = useState('')
        const [crateVolumeError, setCrateVolumeError] = useState(false)

        const [maxGoodsInCrate, setMaxGoodsInCrate] = useState(0)
        const [nameFactory, setNameFactory] = useState("")
        const [nameFactoryError, setNameFactoryError] = useState(false)
        const [maxGoodsInCrateError, setMaxGoodsInCrateError] = useState(false)
        //  const [addressError, setAddressError] = useState(false)
        const [toValidate, setToValidate] = useState(false)
        // const [tov]
        // const [value,setValue ]=useState()
        // const [trec,set_load]
        //const okPasswordRef = useRef('')
        const [provider, setProvider] = useState(null)

        const { currentProvider, dispatch, status, setShow } = props
        const factoryPrivate = currentProvider.factoryPrivate
        const [RedirectFlag, setRedirectFlag] = useState(false)
        const style = {
            position: 'absolute',
            top: '50%',
            left: '50%',
            transform: 'translate(-50%, -50%)',
            width: 400,
            bgcolor: 'background.paper',
            border: '2px solid #000',
            boxShadow: 24,
            p: 4,
            direction: "rtl"
        };
        const [showModal, setShowModal] = useState(false)
        useEffect(() => {
            if (status == "UPDATE" && factoryPrivate.FactoryName) {
                //nameRef.current.value = factoryPrivate.FactoryName
                setNameFactory(factoryPrivate.FactoryName)

                //addressRef.current.value = factoryPrivate.placeAddress
                setAddress(factoryPrivate.placeAddress)
                //timeRef.current.value = factoryPrivate.LeavingTime
                // crateVolumeRef.current.value = factoryPrivate.CrateVolume.toString()
                setCrateVolume(factoryPrivate.CrateVolume)
                // crateWeightRef.current.value = factoryPrivate.CrateWeight.toString()
                setCrateWeight(factoryPrivate.CrateWeight)
                // maxGoodsInCrateRef.current.value = factoryPrivate.MaxGoodsInCrate.toString()
                setMaxGoodsInCrate(factoryPrivate.MaxGoodsInCrate)
                let MinutesString = factoryPrivate.LeavingTime.Minutes
                if (factoryPrivate.LeavingTime.Minutes < 10)
                    MinutesString = "0" + MinutesString
                let houreString = factoryPrivate.LeavingTime.Hours
                if (factoryPrivate.LeavingTime.Hours < 10)
                    houreString = "0" + houreString
                // timeRef.current.value = houreString + ":" + MinutesString
                setTime(houreString + ":" + MinutesString)
            }

        },
            [])
        async function checkAndSet() {
            let test = true
            setToValidate(true)

            // var reWhiteSpace = new RegExp("\\s+");
            // if (!nameRef.current.value || reWhiteSpace.test(nameRef.current.value) == false) {
            //     //תשלח שגיאה שם שדה חובה
            //     setNameFactory("")
            //     return;
            // }
            // if (!addressRef.current.value || !reWhiteSpace.test(addressRef.current.value)) {
            //     //תשלח שגיאה כתובת שדה חובה
            //     setAddress("")
            //     return;
            // }

            if (!CheckName(nameFactory)) {
                setNameFactoryError(true)
                test = false
            }
            else
                setNameFactoryError(false)
            if (isNaN(crateVolume)) {
                setCrateVolumeError(true)
                test = false
            }
            if (isNaN(crateWeight)) {
                setCrateWeightError(true)

                test = false
            }
            if (maxGoodsInCrate <= 0) {
                setMaxGoodsInCrateError(maxGoodsInCrate <= 0)
                test = false
            }
            if (!test)
                return
            if (nameFactory.length == 0 || address.length == 0 || crateVolume.length == 0 || crateVolume.length == 0 || crateWeight.length == 0)
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
            let res
            if (status == "UPDATE") {
                res = await Client.put('/Provider/update?LeavingTime=' + time, providerUpdate)
                // Client.get('/Provider/getById?id=' + currentProvider.providerPrivate.ProviderId).then(res => {
                //redaxעדכון פרטי הספק הנוכחי ב 
            }
            else {
                res = await Client.post('/Provider/add?LeavingTime=' + time, providerUpdate)
            }
            dispatch(updateFactoryPrivate({
                FactoryName: res.data.FactoryName,
                LeavingTime: res.data.LeavingTime,
                placeGoogleMapsId: res.data.placeGoogleMapsId,
                placeAddress: res.data.placeAddress,
                CrateVolume: res.data.CrateVolume,
                CrateWeight: res.data.CrateWeight,
                MaxGoodsInCrate: res.data.MaxGoodsInCrate

            }))
            if (status == "UPDATE")
                setShowModal(true)
            else {
                dispatch(updateShops([]))
                dispatch(updateTrucks([]))
                setRedirectFlag(true)
            }
            //setRedirectFlag(true)
            // })



            //תנווט לדף הראשי


        }
        if (RedirectFlag)
            return <Redirect to="/provider"></Redirect>
        if (props.show == false)
            return (<></>)
        return (
            <>
                <Modal
                    keepMounted
                    open={showModal}
                    // onClose={handleClose}
                    aria-labelledby="keep-mounted-modal-title"
                    aria-describedby="keep-mounted-modal-description"
                >
                    <Box sx={style}>
                        <Typography id="keep-mounted-modal-title" variant="h6" component="h2">
                            עדכון נתוני המפעל
                        </Typography>
                        <Typography id="keep-mounted-modal-description" sx={{ mt: 2 }}>
                            הפעולה התבצעה בהצלחה
                        </Typography>
                        <Button variant="primary" dir="rtl" onClick={() => {
                            setShowModal(false)
                        }} >אישור</Button>

                    </Box>
                </Modal>
                {(!newProvider) ? <NavBar Mypage="privateFactory" /> : ""}

                <div
                    className="form"
                >
                    <Form id="privateProvider" className="box" >

                        <h4> פרטי המפעל</h4>
                        <TextField

                            id={"outlined-required"}
                            label="הזן שם המפעל"
                            defaultValue="הזן שם המפעל"

                            required
                            //ref={nameRef}
                            value={nameFactory}
                            onChange={e => { setNameFactory(e.target.value); }}
                            className="m-1"
                        />
                        <TextField

                            id={"outlined-required"}
                            label={"הזן כתובת"}
                            defaultValue="הזן כתובת"
                            required
                            // ref={addressRef}
                            value={address}
                            onChange={e => { setAddress(e.target.value); }}
                            className="m-1"
                        />
                        <TextField

                            id={"outlined-required"}
                            label="הזן את שעת היציאה של המשאיות"
                            defaultValue="הזן את שעת היציאה של המשאיות"

                            required
                            // ref={timeRef}
                            value={time}
                            onChange={e => { setTime(e.target.value); }}
                            className="m-1"
                            type="time"
                        />
                        <TextField
                            error={crateVolumeError}
                            id={crateVolumeError ? "outlined-error-helper-text" : "outlined-required"}
                            label={crateVolumeError ? "Error" : "הזן נפח ארגז סחורה"}
                            defaultValue="הזן נפח ארגז סחורה"
                            helperText={crateVolumeError ? "הכנס מספר בלבד" : ""}
                            required
                            // ref={crateVolumeRef}
                            value={crateVolume}
                            onChange={e => { setCrateVolume(e.target.value); }}
                            className="m-1"
                        />
                        <TextField
                            error={maxGoodsInCrateError}
                            id={maxGoodsInCrateError ? "outlined-error-helper-text" : "outlined-required"}
                            label={maxGoodsInCrateError ? "Error" : "הזן כמות מוצרים בארגז"}
                            defaultValue="הזן כמות מוצרים בארגז"
                            helperText={maxGoodsInCrateError ? "הכנס מספר חיובי בלבד" : ""}
                            required
                            //  ref={maxGoodsInCrateRef}
                            value={maxGoodsInCrate}
                            onChange={e => { setMaxGoodsInCrate(e.target.value); }}
                            className="m-1"
                            type="number"
                        />
                        <TextField
                            error={crateWeightError}
                            id={crateWeightError ? "outlined-error-helper-text" : "outlined-required"}
                            label={crateWeightError ? "Error" : 'הזן משקל ארגז (בק"ג)'}
                            defaultValue='הזן משקל ארגז (בק"ג)'
                            helperText={crateWeightError ? "הכנס מספר בלבד" : ""}
                            required
                            //ref={crateWeightRef}
                            value={crateWeight}
                            onChange={e => { setCrateWeight(e.target.value); }}
                            className="m-1"
                        />
                        <br />
                        {/* <Form.Group className="mb-3" >
                            <Form.Label>שם המפעל</Form.Label>
                            <Form.Control type="text" placeholder="הזן שם המפעל" ref={nameRef} value={nameFactory} onChange={e => { setNameFactory(e.target.value); }} />
                            <Form.Text className="text-error" style={{ visibility: toValidate && nameFactory.length == 0 ? "visible" : "hidden" }}>
                                שדה חובה
                            </Form.Text>
                            <Form.Text className="text-error" style={{ visibility: toValidate && crateVolume.length != 0 && nameFactoryError ? "visible" : "hidden" }}>
                                שם שגוי
                            </Form.Text>
                        </Form.Group>

                        <Form.Group className="mb-3" >
                            <Form.Label>כתובת</Form.Label>
                            <Form.Control type="text" placeholder="הזן כתובת" ref={addressRef} value={address} onChange={e => { setAddress(e.target.value); }} />
                            <Form.Text className="text-muted text-error" style={{ visibility: toValidate && address.length == 0 ? "visible" : "hidden" }}>
                                שדה חובה
                            </Form.Text>
                        </Form.Group>

                        <Form.Group className="mb-3" >
                            <Form.Label>שעת יציאת המשאיות</Form.Label>
                            <Form.Control type="time" placeholder="הזן את שעת היציאה של המשאיות" ref={timeRef} />

                        </Form.Group>
                        <Form.Group className="mb-3" >
                            <Form.Label> נפח ארגז סחורה (בקמ"ר)</Form.Label>
                            <Form.Control type="text" placeholder="הזן נפח ארגז סחורה" ref={crateVolumeRef} value={address} onChange={e => { setAddress(e.target.value); }} />
                            <Form.Text className="text-muted text-error" style={{ visibility: toValidate && crateVolume.length == 0 ? "visible" : "hidden" }}>
                                שדה חובה
                            </Form.Text>
                            <Form.Text className="text-muted text-error" style={{ visibility: toValidate && crateVolume.length != 0 && crateVolumeError ? "visible" : "hidden" }}>
                                הכנס מספר בלבד                        </Form.Text>
                        </Form.Group>
                        <Form.Group className="mb-3" >
                            <Form.Label> כמות מוצרים בארגז</Form.Label>
                            <Form.Control type="text" placeholder="הזן כמות מוצרים בארגז" value={nameFactory} onChange={e => { setNameFactory(e.target.value); }} />
                            <Form.Text className="text-muted text-error" style={{ visibility: toValidate && maxGoodsInCrate.length == 0 ? "visible" : "hidden" }}>
                                שדה חובה
                            </Form.Text>
                            <Form.Text className="text-muted text-error" style={{ visibility: toValidate && crateVolume.length != 0 && maxGoodsInCrateError ? "visible" : "hidden" }}>
                                הכנס מספר בלבד                        </Form.Text>
                        </Form.Group>

                        <Form.Group className="mb-3" >
                            <Form.Label> משקל ארגז (בק"ג)</Form.Label>
                            <Form.Control type="text" placeholder="הזן משקל ארגז" ref={crateWeightRef} value={crateWeight} onChange={e => { setCrateWeight(e.target.value); }} />
                            <Form.Text className="text-muted text-error" style={{ visibility: toValidate && crateWeight.length == 0 ? "visible" : "hidden" }}>
                                שדה חובה
                            </Form.Text>
                            <Form.Text className="text-muted text-error" style={{ visibility: toValidate && crateWeight.length != 0 && crateWeightError ? "visible" : "hidden" }}>
                                הכנס מספר בלבד                        </Form.Text>
                        </Form.Group> */}
                        <Button variant="primary" dir="rtl" disabled={!nameFactory || !address || !maxGoodsInCrate || !time || crateVolume.length == 0 || crateWeight.length == 0 || nameFactory.length == 0 || address.length == 0 || maxGoodsInCrate.length == 0 || time.length == 0 || crateVolume.length == 0 || crateWeight.length == 0}
                            onClick={() => { checkAndSet() }} >אישור
                        </Button>
                        {/* <Button variant="primary" onClick={checkAndSet}>
                            אישור
                        </Button> */}
                    </Form>
                </div>

                {/* <label>שם המפעל</label>
                <input ref={nameRef} value={nameFactory} onChange={e => { setNameFactory(e.target.value); }} />
                <div style={{ visibility: toValidate && nameFactory.length == 0 ? "visible" : "hidden" }}>שדה חובה  </div>
                <div style={{ visibility: toValidate && crateVolume.length != 0 && nameFactoryError ? "visible" : "hidden" }}>תוכן שגוי</div>

                <label>כתובת</label> */}
                {/* 
                <ReactGoogleAutocomplete
                    apiKey={key}
                    onPlaceSelected={(place) => {
                        console.log(place);
                    }}
                    ref={addressRef} value={address} onChange={e => { setAddress(e.target.value); }}
                ></ReactGoogleAutocomplete> */}

                {/* <input ref={addressRef} value={address} onChange={e => { setAddress(e.target.value); }} />
                {/* <div style={{ visibility: toValidate && crateVolume.length != 0 && addressError ? "visible" : "hidden" }}>תוכן שגוי</div> */}

                {/* <div style={{ visibility: toValidate && address.length == 0 ? "visible" : "hidden" }}>שדה חובה  </div>

                <label>השעה שבה יוצאות המשאיות</label>
                <input type="time" ref={timeRef} />

                <label>  נפח ארגז סחורה (בקמ"ר)</label>
                <input ref={crateVolumeRef} value={crateVolume} onChange={e => { setCrateVolume(e.target.value); }} />
                <div style={{ visibility: toValidate && crateVolume.length == 0 ? "visible" : "hidden" }}>שדה חובה  </div>
                <div style={{ visibility: toValidate && crateVolume.length != 0 && crateVolumeError ? "visible" : "hidden" }}>תוכן שגוי</div>

                <label>כמות מוצרים בארגז</label>
                <input type="number" ref={maxGoodsInCrateRef} value={maxGoodsInCrate} onChange={e => { setMaxGoodsInCrate(e.target.value); }} />
                <div style={{ visibility: toValidate && maxGoodsInCrate.length == 0 ? "visible" : "hidden" }}>שדה חובה  </div>
                <div style={{ visibility: toValidate && crateVolume.length != 0 && maxGoodsInCrateError ? "visible" : "hidden" }}>תוכן שגוי</div>

                <label>משקל ארגז (בק"ג)</label>
                <input type="text" ref={crateWeightRef} value={crateWeight} onChange={e => { setCrateWeight(e.target.value); }} />
                <div style={{ visibility: toValidate && crateWeight.length == 0 ? "visible" : "hidden" }}>שדה חובה  </div>
                <div style={{ visibility: toValidate && crateWeight.length != 0 && crateWeightError ? "visible" : "hidden" }}>תוכן שגוי </div>

            <button onClick={checkAndSet}>שמור</button>

            </>*/}
            </>
        )
    }
)