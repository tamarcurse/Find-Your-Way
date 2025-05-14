//path=login/newProvide

import React, { useRef, useState } from "react";
import { Client } from "../../Client";
import { CheckID, CheckName } from "../../normality"

import { useEffect } from "react";
import { updateAllProvider, updatePrivateProvider, updateShops, updateSizeContians, updateTrucks } from "../../../redux/actions";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";
import { Modal } from "react-bootstrap"
import Button from '@mui/material/Button';
import PrivateFactory from "../../factory/privateFactory";
import TextField from '@mui/material/TextField';
function mapStateToProps(state) {
    return {}
}
export default connect(mapStateToProps)(
    function LoginNewProvider(props) {

        const [show, setShow] = useState(false);
        const [password, setPassword] = useState('');
        const [passwordOk, setPasswordOk] = useState('');

        const [firstName, setFirstName] = useState('');
        const [id, setId] = useState('')
        const handleClose = () => setShow(false);
        const handleShow = () => setShow(true);
        const { dispatch } = props
        const nameRef = useRef('')
        const idRef = useRef('')
        const passwordRef = useRef('')
        const passwordOkRef = useRef('')

        const [RedirectFlag, setRedirectFlag] = useState(false)
        const [toValidate, set_toValidate] = useState(false)
        const [passwordEror, setPasswordEror] = useState(false)

        const [nameError, setNameError] = useState(false)
        const [tzError, setTzError] = useState(false)

        useEffect(() => {
            handleShow()
        }, [])
        function checkProvider(provider) {
            if (provider == null) {
                setTzError(true)
                return false
            }
            setTzError(false)
            if (provider.PasswordProvider != passwordRef.current.value || provider.ProviderName != nameRef.current.value) {
                setPasswordEror(true)
                return false
            }
            setPasswordEror(false)
            return true

        }
        async function checkAndSet() {


            if (!CheckID(idRef.current.value)) {

                //תשלח שגיאה ותודיע לו שהבעיה בת.ז
                setTzError(true)

            }
            else
                setTzError(false)
            if (!CheckName(nameRef.current.value)) { //תשלח שגיאה ותודיע לו שהבעיה בשם
                setNameError(true)

            }
            else
                setNameError(false)

            setPasswordEror(false)
            if (password != passwordOk) {
                setPasswordEror(true)
                return
            }
            if (!nameError && !tzError && password.length != 0 && passwordOk.length != 0) {


                
                
                let privateProvider = {
                    ProviderId: id,
                    ProviderName: firstName,
                    PasswordProvider: password,
                }
                await dispatch(updatePrivateProvider(privateProvider))
            
                // תנווט לקומפננטה הבאה
                setRedirectFlag(true)
            }
            else {
            }
        }


        if (RedirectFlag)
            return <PrivateFactory status="ADD" />



        return (
            <>
                <Modal show={show} onHide={handleClose}>
                    <Modal.Header closeButton>
                        <Modal.Title>הצטרפות ספק חדש</Modal.Title>
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
                            value={firstName}
                            onChange={e => { setFirstName(e.target.value); }}
                            className="m-1"
                        />
                        <TextField
                            error={tzError}
                            id={tzError ? "outlined-error-helper-text" : "outlined-required"}
                            label={tzError ? "Error" : "הזן שם פרטי"}
                            defaultValue="הזן שם פרטי"
                            helperText={tzError ? "השם לא קיים במערכת" : ""}
                            required
                            ref={idRef}
                            value={id}
                            onChange={e => { setId(e.target.value); }}
                            className="m-1"
                        />
                        <TextField
                            // error={passwordEror}
                            id={passwordEror ? "outlined-error-helper-text outlined-password-input" : "outlined-required"}
                            label={passwordEror ? "Error" : "הזן סיסמת מנהל"}
                            defaultValue="הזן סיסמת מנהל"
                            // helperText={password ? "סיסמא שגויה" : ""}
                            required
                            ref={passwordRef}
                            value={password}
                            onChange={e => { setPassword(e.target.value); }}
                            className="m-1"
                        />
                        <TextField
                            error={passwordEror}
                            id={passwordEror ? "outlined-error-helper-text outlined-password-input" : "outlined-required"}
                            label={passwordEror ? "Error" : "אשר סיסמא"}
                            defaultValue="אשר סיסמא"
                            helperText={password ? "סיסמא שגויה" : ""}
                            required
                            ref={passwordRef}
                            value={passwordOk}
                            onChange={e => { setPasswordOk(e.target.value); }}
                            className="m-1"
                        />
                       
                    </Modal.Body>
                    <Modal.Footer>
                    
                        <Button variant="primary" dir="rtl" disabled={!firstName || !id || !password || firstName.length == 0 || id.length == 0 || password.length == 0}
                            onClick={() => { set_toValidate(true); checkAndSet() }} >אישור
                        </Button>
                    </Modal.Footer>
                </Modal>

            </>)
    })