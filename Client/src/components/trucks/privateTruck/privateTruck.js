//שקופית 5
//path="/truck/privateTruck/:LicensePlate"
import React, { useEffect, useRef, useState } from "react";
import { connect } from "react-redux";
import { useParams } from "react-router-dom/cjs/react-router-dom.min";
import { CheckID, CheckName } from "../../normality";
import { Client } from "../../Client";
import { updateShops, updateSizeContians, updateTrucks } from "../../../redux/actions";
import { Redirect } from "react-router-dom";
import Box from '@mui/material/Box';
import "../../../App.css"
import NavBar from "../../home/navbar";
import { Form } from "react-bootstrap";
import Button from '@mui/material/Button';

function mapStateToProps(state) {
    return {
        trucks: state.TrucksByProvider.trucks,
        sizeContain: state.sizeContianByProvider.sizeContain,
        providerId: state.currentProvider.providerPrivate.ProviderId 
    }
}
export default connect(mapStateToProps)(
    function PrivateTruck(props) {

        let { LicensePlate } = useParams()

        const nameDriverRef = useRef()
        const LicensePlateRef = useRef()
        const driverIdRef = useRef()
        const VolumeSqmrRef = useRef()
        const WeightSizeRef = useRef()
        const { dispatch, providerId, sizeContain } = props
        let currentTruck
        let currentSizeContian

        const [nameDriver, setNameDriver] = useState('')
        const [WeightSize, setWeightSize] = useState(0)
        const [VolumeSqmr, setVolumeSqmr] = useState(0)
        const [toValidate, setToValidate] = useState(false)
        const [driverId, setDriverId] = useState('')
        const [flagRedirect, setFlagRedirect] = useState(false)
        const [sizeId, setSizeId] = useState()
        const [WeightSize_src, setWeightSize_src] = useState()
        const [VolumeSqmr_src, setVolumeSqmr_src] = useState()


        async function SaveShopInDBAndRedux() {
            //אם אתה במצב עדכון
            if (LicensePlate != "add") {
                //let sizeId = currentTruck.SizeId

                currentTruck = {
                    LicensePlate: LicensePlateRef.current.value,
                    SizeId: sizeId,
                    NameOfDriver: nameDriverRef.current.value,
                    IdOfDriver: driverIdRef.current.value,
                    ProviderId: providerId
                }
                if (WeightSizeRef.current.value != WeightSize_src || VolumeSqmrRef.current.value != VolumeSqmr_src) {
                    currentSizeContian = {
                        SizeContainId: 0,
                        WeightSize: parseFloat(WeightSizeRef.current.value),
                        VolumeSqmr: parseFloat(VolumeSqmrRef.current.value)
                    }
                    let res = await Client.post('/SizeContian/add', currentSizeContian)

                    // let res = await Client.get('/SizeContian/getAll')
                    if (res) {
                        dispatch(updateSizeContians(res.data))
                        currentTruck.SizeId = res.data.find(s => s.WeightSize == currentSizeContian.WeightSize && s.VolumeSqmr == currentSizeContian.VolumeSqmr).SizeContainId
                    }

                }
                await Client.put('/Truck/update', currentTruck).then((res) => {

                    dispatch(updateTrucks(res.data))
                    setFlagRedirect(true)

                })


            }
            else {
                currentTruck = {
                    LicensePlate: LicensePlateRef.current.value,
                    SizeId: 0,
                    NameOfDriver: nameDriverRef.current.value,
                    IdOfDriver: driverIdRef.current.value,
                    ProviderId: providerId
                }
                currentSizeContian = {
                    SizeContainId: 0,
                    WeightSize: parseFloat(WeightSizeRef.current.value),
                    VolumeSqmr: VolumeSqmrRef.current.value
                }
                await Client.post('/SizeContian/add', currentSizeContian)
                let res1 = await Client.get('/SizeContian/getAll')
                if (res1) {
                    dispatch(updateSizeContians(res1.data))
                    currentTruck.SizeId = res1.data.find(s => s.WeightSize == currentSizeContian.WeightSize && s.VolumeSqmr == currentSizeContian.VolumeSqmr).SizeContainId
                }

                console.log(currentTruck.SizeId);
                await Client.post('Truck/add', currentTruck)
                let res = await Client.get('/Truck/getAll')
                if (res) {
                    await dispatch(updateTrucks(res.data))
                    setFlagRedirect(true)

                }


            }
        }
        function checkAndSet() {
            setToValidate(true)
           
            if (driverId.length == 0 || nameDriver.length == 0 || WeightSize.length == 0 || VolumeSqmr.length == 0)
                return
            if (!CheckName(nameDriver) || !CheckID(driverId) || isNaN(WeightSize) || VolumeSqmr <= 0 || parseFloat(WeightSize) < 0)
                return


            SaveShopInDBAndRedux()





        }
        useEffect(FillTextBox, [])
        function FillTextBox() {
            if (LicensePlate != "add") {
                currentTruck = props.trucks.find(s => s.LicensePlate == LicensePlate)
                currentSizeContian = props.sizeContain.find(s => s.SizeContainId == currentTruck.SizeId) || {
                    "SizeContainId": 47,
                    "WeightSize": 10,
                    "VolumeSqmr": 11
                }
                nameDriverRef.current.value = currentTruck.NameOfDriver
                driverIdRef.current.value = currentTruck.IdOfDriver
                LicensePlateRef.current.value = currentTruck.LicensePlate
                WeightSizeRef.current.value = currentSizeContian.WeightSize
                VolumeSqmrRef.current.value = currentSizeContian.VolumeSqmr
                setDriverId(driverIdRef.current.value)

                setNameDriver(currentTruck.NameOfDriver)
                setVolumeSqmr(currentSizeContian.VolumeSqmr)
                setWeightSize(currentSizeContian.WeightSize)
                setSizeId(currentTruck.SizeId)
                setVolumeSqmr_src(currentSizeContian.VolumeSqmr)
                setWeightSize_src(currentSizeContian.WeightSize)
            }

        }
        if (flagRedirect)
            return <Redirect to='/provider/trucks' ></Redirect>
        return (
            <>
                <NavBar Mypage="trucks" />

                <Form className="form box">

                    <h4 className="titlePrivate">פרטי משאית</h4>
                    <div className="form">
                        <Form.Group className="inputIN">

                            <Form.Label>לוחית רישוי</Form.Label>
                            <Form.Control ref={LicensePlateRef} disabled={LicensePlate != "add"}></Form.Control>

                        </Form.Group>
                        <Form.Group className="inputIN">

                            <Form.Label>שם הנהג</Form.Label>
                            <Form.Control ref={nameDriverRef} value={nameDriver} onChange={e => { setNameDriver(e.target.value); }}></Form.Control>
                            <Form.Text style={{ visibility: toValidate && nameDriver.length == 0 ? "visible" : "hidden" }}>שדה חובה</Form.Text>
                            <Form.Text style={{ visibility: ((toValidate && nameDriver.length != 0) && ((!CheckName(nameDriver)))) ? "visible" : "hidden" }}>תוכן  שגוי</Form.Text>
                        </Form.Group>

                        <Form.Group className="inputIN">

                            <Form.Label>ת.ז</Form.Label>
                            <Form.Control ref={driverIdRef} value={driverId} onChange={e => { setDriverId(e.target.value); }}></Form.Control>
                            <Form.Text style={{ visibility: toValidate && driverId.length == 0 ? "visible" : "hidden" }}>שדה חובה</Form.Text>
                            <Form.Text style={{ visibility: ((toValidate && driverId.length != 0) && ((!CheckID(driverId)))) ? "visible" : "hidden" }}>תוכן  שגוי</Form.Text>
                        </Form.Group>
                        <Form.Group className="inputIN">

                            <Form.Label> הגבלת משקל (בק"ג)</Form.Label>
                            <Form.Control ref={WeightSizeRef} value={WeightSize} onChange={e => { setWeightSize(e.target.value); }}></Form.Control>
                            <Form.Text style={{ visibility: toValidate && WeightSize.length == 0 ? "visible" : "hidden" }}>שדה חובה</Form.Text>
                            <Form.Text style={{ visibility: ((toValidate && WeightSize.length != 0) && ((isNaN(WeightSize) || parseFloat(WeightSize) < 0))) ? "visible" : "hidden" }}>תוכן  שגוי</Form.Text>

                        </Form.Group>
                        <Form.Group className="inputIN">

                            <Form.Label> הגבלת נפח (בסמ"ר)</Form.Label>
                            <Form.Control ref={VolumeSqmrRef} type="number" value={VolumeSqmr} onChange={e => { setVolumeSqmr(e.target.value); }} ></Form.Control>
                            <Form.Text style={{ visibility: (toValidate && ((VolumeSqmr <= 0))) ? "visible" : "hidden" }}>תוכן  שגוי</Form.Text>
                        </Form.Group>
                    </div>
                    <Button className="button-ok" variant="contained" color="success" onClick={checkAndSet}>שמור</Button>

                </Form>


            </>
        )
    }
)
