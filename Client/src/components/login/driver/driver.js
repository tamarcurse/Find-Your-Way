//path=/login/driver

import React, { useRef, useState } from "react";
import axios from "axios";
import { CheckID, CheckName } from "../../normality"
import store from "../../../redux/store";
import { Action } from "redux";
import { updateAllProvider } from "../../../redux/actions";
import { Redirect } from "react-router-dom";
import { connect } from "react-redux";
import { Modal } from "react-bootstrap"
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import { Client } from "../../Client";
function mapStateToProps(state) {
    return {

    }
}
export default connect(mapStateToProps)(
    function DriverLogIn(props) {

        const { dispatch } = props
        const nameRef = useRef('')
        const idRef = useRef('')
        const [provider, setProvider] = useState(null)
        const [truck, setTruck] = useState(null)
        const [flagLogIn, setFlagLogIn] = useState(false)
        //const [RedirectFlag, setRedirectFlag] = useState(false)
        const [toValidate, set_toValidate] = useState(false)
        const [nameError, setNameError] = useState(false)
        const [Name, setName] = useState('')
        const [id, setId] = useState('')
        const [tzError, setTzError] = useState(false)
        const [show, setShow] = useState(true);
        const handleClose = () => setShow(false);
        const [loginProvider, setLoginProvider] = useState(false)

        function checkTruck(Truck) {
            if (!Truck.LicensePlate) {
                setTzError(true)
                return false
            }

            if (Truck.NameOfDriver != Name) {
                setNameError(true)
                return false
            }
            return true;



        }
        async function checkAndSet() {
            let test = true

            if (!CheckID(id)) { //תשלח שגיאה ותודיע לו שהבעיה בת.ז
                setTzError(true)
                test = false
            }
            else {
                setTzError(false)
            }
            if (!CheckName(Name)) { //תשלח שגיאה ותודיע לו שהבעיה בשם
                setNameError(true)
                test = false
            }
            else {
                setNameError(false)
            }
            if (test) {
                Client.get(`/Truck/getByDriverId?id=` + id).then((res) => {

                    setTruck(res.data)
                    if (checkTruck(res.data)) {



                        Client.get(`/Provider/getById?id=` + res.data.ProviderId).then((res2) => {
                            setProvider(res2.data)
                                ;
                            dispatch(updateAllProvider(res2.data))
                            setFlagLogIn(true)
                            // }

                        }).catch(() => {

                            console.log("cath");
                            setTzError(true)
                            //תודיע שהפקודה לא הצליחה

                        })
                    }
          
                }
                ).catch(() => {

                    console.log("cath");
                    setTzError(true)
                    //תודיע שהפקודה לא הצליחה

                })
            }
            setShow(true)


        }
        if (loginProvider)
            return <Redirect to="/login/provider"></Redirect>
        if (flagLogIn)
            return <Redirect to={"/driver/" + truck.LicensePlate} />
        return (
            <>
                <Modal show={show} onHide={handleClose}>
                    <Modal.Header closeButton>
                        <Modal.Title>התחברות נהג</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <TextField
                            error={nameError}
                            id={nameError ? "outlined-error-helper-text" : "outlined-required"}
                            label={nameError ? "Error" : "הזן שם "}
                            defaultValue="הזן שם "
                            helperText={nameError ? "השם לא קיים במערכת" : ""}
                            required
                            ref={nameRef}
                            value={Name}
                            onChange={e => { setName(e.target.value); }}
                            className="m-1"
                        />
                        <TextField
                            error={tzError}
                            id={tzError ? "outlined-error-helper-text" : "outlined-required"}
                            label={tzError ? "Error" : "הזן מספר תעודת זהות"}
                            defaultValue="הזן מספר תעודת זהות"
                            helperText={tzError ? "מספר ת.ז. שגוי" : ""}
                            required
                            ref={idRef}
                            value={id}
                            onChange={e => { setId(e.target.value); }}
                            className="m-1"
                        />

              
                    </Modal.Body>
                    <Modal.Footer>


                        <Button variant="primary" dir="rtl" onClick={() => { set_toValidate(true); checkAndSet() }}
                            disabled={!Name || !id || Name.length == 0 || id.length == 0}
                        >אישור</Button>
                        <Button variant="outlined" dir="rtl" onClick={() => { setLoginProvider(true) }}>התחברות ספק</Button>

                    </Modal.Footer>
                </Modal>
            </>
        )

    })
