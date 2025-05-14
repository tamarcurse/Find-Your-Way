import React, { useRef, useState } from "react";
import { Client } from "../../Client";
import { CheckID, CheckName } from "../../normality"

import { useEffect } from "react";
import { updateAllProvider, updateShops, updateSizeContians, updateTrucks } from "../../../redux/actions";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";
import { Modal } from "react-bootstrap"
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';

function mapStateToProps(state) {
    return {}
}
export default connect(mapStateToProps)(
    function ProviderLogIn(props) {

        const [show, setShow] = useState(false);
        const [password, setPassword] = useState('');
        const [firstName, setFirstName] = useState('');
        const [id, setId] = useState('')
        const handleClose = () => setShow(false);
        const handleShow = () => setShow(true);
        const { dispatch } = props
        const nameRef = useRef('')
        const idRef = useRef('')
        const passwordRef = useRef('')
        const [provider, setProvider] = useState(null)
        const [RedirectFlag, setRedirectFlag] = useState(false)
        const [toValidate, set_toValidate] = useState(false)
        const [passwordEror, setPasswordEror] = useState(false)

        const [nameError, setNameError] = useState(false)
        const [tzError, setTzError] = useState(false)
        const [temp, setTemp] = useState(false)
        const [NewProviderFlag, setNewProviderFlag] = useState(false)
        const [tested, stetTested] = useState(false)
        useEffect(() => {
            handleShow()
        }, [])

        function checkProvider(provider) {
            if (provider == null || provider == "") {
                setTzError(true)
                return false
            }
            setTzError(false)
            if (provider.PasswordProvider != password) {
                setPasswordEror(true)
                return false
            } if (provider.ProviderName != firstName) {
                setNameError(true)
                return false
            }
            setPasswordEror(false)
            return true

        }

        async function checkAndSet() {

            let test = true
            if (!CheckID(id)) {

                //תשלח שגיאה ותודיע לו שהבעיה בת.ז
                setTzError(true)
                test = false

            }
            else {
                stetTested(true)
                setTzError(false)


            }
            if (!CheckName(firstName)) { //תשלח שגיאה ותודיע לו שהבעיה בשם
                setNameError(true)
                test = false
            }
            else
                setNameError(false)
            setPasswordEror(false)
            if (test && !nameError && !tzError && password.length != 0) {


                let res1 = await Client.get(`/Provider/getById?id=` + id)
                await setProvider(res1.data)
                if (checkProvider(res1.data)) {
                    await dispatch(updateAllProvider(res1.data))
                    let res2 = await Client.get(`/Shop/getAllByProviderID?id=` + res1.data.ProviderId)
                    dispatch(updateShops(res2.data))

                    let res3 = await Client.get(`/Truck/getAllByProviderId?providerId=` + res1.data.ProviderId)
                    dispatch(updateTrucks(res3.data))
                    let res4 = await Client.get(`/SizeContian/getAll`)
                    dispatch(updateSizeContians(res4.data))
                    // תנווט לקומפננטה הבאה
                    setRedirectFlag(true)
                }

            }
            handleShow()
        }

        if (RedirectFlag)
            return <Redirect to="/provider"></Redirect>

        if (NewProviderFlag)
            // return <Redirect to="/login/newProvider" />
            return <Redirect to="/newProvider" />

        return (
            <>

                <Modal show={show} onHide={handleClose}>
                    <Modal.Header closeButton>
                        <Modal.Title>התחברות ספק</Modal.Title>
                        {/* <button onClick={() => { setNewProviderFlag(true) }}>ספק חדש</button> */}
                    </Modal.Header>
                    <Modal.Body>


                        <TextField
                            error={nameError}
                            id={nameError ? "outlined-error-helper-text" : "outlined-required"}
                            label={nameError ? "Error" : "הזן שם פרטי"}
                            defaultValue="הזן שם פרטי"
                            helperText={nameError ? "השם לא קיים במערכת" : ""}
                            required
                            ref={nameRef}
                            value={firstName}
                            onChange={e => { setFirstName(e.target.value); }}
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
                        <TextField
                            error={passwordEror}
                            id={passwordEror ? "outlined-error-helper-text outlined-password-input" : "outlined-required"}
                            label={passwordEror ? "Error" : "הזן סיסמת מנהל"}
                            defaultValue="הזן סיסמת מנהל"
                            helperText={passwordEror ? "סיסמא שגויה" : ""}
                            required
                            ref={passwordRef}
                            value={password}
                            onChange={e => { setPassword(e.target.value); }}
                            className="m-1"
                            type="password"
                        />
                        
                    </Modal.Body>
                    <Modal.Footer>
               
                        <Button variant="primary" dir="rtl" disabled={!firstName || !id || !password || firstName.length == 0 || id.length == 0 || password.length == 0}
                            onClick={() => { set_toValidate(true); checkAndSet() }} >אישור
                        </Button>
                        <Button onClick={() => { setNewProviderFlag(true) }} variant="outlined">ספק חדש</Button>

                    </Modal.Footer>
                </Modal>

            </>)

    })
