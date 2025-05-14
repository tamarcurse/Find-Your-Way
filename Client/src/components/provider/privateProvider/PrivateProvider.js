//שקופית 2 הריבוע הראשון
import React, { useEffect, useRef, useState } from "react";
import { updatePrivateProvider } from "../../../redux/actions";
import { CheckID, CheckName } from "../../normality";
import { connect } from "react-redux";
import TextField from '@mui/material/TextField';
// import { Redirect } from 'react-router-dom'
import { Redirect } from "react-router-dom";
import { Button } from "@mui/material";

function mapStateToProps(state) {
    ;
    return { providerPrivate: state.currentProvider.providerPrivate }
}
export default connect(mapStateToProps)(
    function PrivateProvider(props) {

        const [myDelete, setMyDelete] = useState(false)
        const status = props.status

        const [provider, setProvider] = useState(null)
        const [Name, setName] = useState('')
        const [Passward, setPassward] = useState('')
        const [PasswardOk, setPasswardOk] = useState('')
        const [Id, setId] = useState('')

        const [NameError, setNameError] = useState(false)
        const [PasswardError, setPasswardError] = useState(false)
        const [PasswardOkError, setPasswardOkError] = useState(false)
        const [IdError, setIdError] = useState(false)

        const providerPrivate = props.providerPrivate
        const dispatch = props.dispatch
        useEffect(() => {
            if (status == "UPDATE") {
                setId(providerPrivate.ProviderId) //|| "326238821"
                setName(providerPrivate.ProviderName) //|| "b"
                setPassward(providerPrivate.password)
            }

        },
            [])
        function checkAndSet() {
            let test = true

            // var reWhiteSpace = new RegExp("\\s+");
            // if (!idRef.current.value || reWhiteSpace.test(idRef.current.value) == false) {
            //     //תשלח שגיאה תעודת זהות שדה חובה
            //     return;
            // }
            // if (!nameRef.current.value || reWhiteSpace.test(nameRef.current.value) == false) {
            //     //תשלח שגיאה שם שדה חובה
            //     return;
            // }
            // if (!passwordRef.current.value || reWhiteSpace.test(passwordRef.current.value) == false) {
            //     //תשלח שגיאה ותודיע לו שחובה למלא סיסמא
            // }
            if (!CheckID(Id)) { //תשלח שגיאה ותודיע לו שהבעיה בת.ז
                test = false
                setIdError(true)
            }
            if (!CheckName(Name)) { //תשלח שגיאה ותודיע לו שהבעיה בשם
                test = false
                setNameError(true)
            }
            if (Passward != PasswardOk) {
                //תשלח שגיאה שהסיסמא שונה
                //return <Redirect to="/login/provider"></Redirect>
                test = false
                setPasswardOk(true)
            }
            if (test) {


                //redaxעדכון פרטי הספק הנוכחי ב
                dispatch(updatePrivateProvider({
                    ProviderId: Id,
                    ProviderName: Name,
                    PasswordProvider: Passward,
                    status: status === "UPDATE" ? true : false

                }))
                //עדכון פרטי הספק הנוכחי כולו במסד נתונים

                //תנווט לדף הראשי
                props.setShow(false)
            }
        }
        // if (myDelete)
        //     return <Redirect to="/login/provider"></Redirect>
        if (!props.show)
            return (<></>)
        return (


            <div className="box boxPrivate">
                <h4>פרטים אישיים</h4>
                <TextField
                    //error={nameError}
                    id="outlined-required margin-normal"
                    label="הזן שם "
                    placeholder="הזן שם "
                    //helperText={nameError ? "השם לא קיים במערכת" : ""}
                    required
                    className="textFailed"
                    value={Name}
                    onChange={e => { setName(e.target.value); }}
                    color="success"
                />
                <br />
                <TextField
                    //error={nameError}
                    id="outlined-required margin-normal"
                    label="הזן מספר ת.ז. "
                    placeholder="הזן מספר ת.ז. "
                    //helperText={nameError ? "השם לא קיים במערכת" : ""}
                    required
                    className="textFailed"
                    color="success"
                    value={Id}

                    onChange={e => { setId(e.target.value); }}
                />
                <br />
                <TextField
                    //error={nameError}
                    id="outlined-required margin-normal"
                    label="הזן סיסמא "
                    placeholder="הזן סיסמא "
                    //helperText={nameError ? "השם לא קיים במערכת" : ""}
                    required
                    type="password"
                    className="textFailed"
                    color="success"
                    value={Passward}
                    onChange={e => { setPassward(e.target.value); }}

                />
                <br />
                <TextField
                    //error={nameError}
                    id="outlined-required"
                    label="אשר סיסמא "
                    placeholder="אשר סיסמא  "
                    //helperText={nameError ? "השם לא קיים במערכת" : ""}
                    required
                    type="password"
                    className="textFailed"
                    color="success"

                    value={PasswardOk}
                    onChange={e => { setPasswardOk(e.target.value); }}
                />
                <br />
                <Button onClick={checkAndSet}
                    Submit
                    disabled={(Name.length == 0 || Passward.length == 0 || PasswardOk.length == 0 || Id.length == 0)}
                    variant="outlined"
                    color="success"
                >שמור</Button>

                {/* <label>שם</label>
                <input ref={nameRef}></input>
                <label>מספר תעודת זהות</label>
                <input ref={idRef} ></input>
                <label>סיסמא</label>
                <input type='password' ref={passwordRef}></input>
                <label>אשר סיסמא</label>
                <input type='password' ref={okPasswordRef}></input>
                <button onClick={checkAndSet}>שמור</button>

                <button onClick={() => {
                    setMyDelete(true)

                }}>עבור</button> */}



            </div>
        )

    })