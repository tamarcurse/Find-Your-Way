//שקופית 5
//path="/shop/privateShop"
import React, { useEffect, useRef, useState } from "react";
import { connect } from "react-redux";
import { useParams } from "react-router-dom/cjs/react-router-dom.min";
import { CheckName } from "../../normality";
import { Client } from "../../Client";
import { updateOneShop, updateShops } from "../../../redux/actions";
import { Redirect } from "react-router-dom";
import NavBar from "../../home/navbar";
import { Form } from "react-bootstrap";
import Button from '@mui/material/Button';
function mapStateToProps(state) {
    return {
        shops: state.shopsByProvider.shops
        ,
        providerId: state.currentProvider.providerPrivate.ProviderId /*|| "326238821"*/
    }
}
export default connect(mapStateToProps)(
    function PrivateShop(props) {

        let { ShopId } = useParams()
        const nameRef = useRef()
        const addressRef = useRef()
        const demandRef = useRef()
        const fromTimeRef = useRef()
        const ToTimeRef = useRef()
        const { dispatch, providerId } = props
        const [nameShop, setNameShop] = useState("")
        const [address, setAddress] = useState("")
        const [demand, setDemand] = useState(0)
        const [toValidate, setToValidate] = useState(false)
        const [flagRedirect, setFlagRedirect] = useState(false)

        let currentShop
        async function SaveShopInDBAndRedux() {
            //אם אתה במצב עדכון


            if (ShopId != "add") {
                currentShop = {
                    ShopId: ShopId,
                    ShopName: nameRef.current.value,
                    placeAddress: addressRef.current.value,
                    placeGoogleMapsId: null,
                    GoodsRequired: demandRef.current.value,
                    ProviderId: providerId,
                    HourDayStart: {},
                    HourDayFinish: {}
                }



                let res1 = await Client.put('/Shop/update?startTime=' + fromTimeRef.current.value + '&finishTime=' + ToTimeRef.current.value, currentShop)

                dispatch(updateShops(res1.data))
                //תודיע שהעדכון הושלם בהצלחה
                setFlagRedirect(true)






            }
            else {
                currentShop = {
                    ShopId: 0,
                    ShopName: nameRef.current.value,
                    placeAddress: addressRef.current.value,
                    placeGoogleMapsId: null,
                    GoodsRequired: demandRef.current.value,
                    ProviderId: providerId,
                    HourDayStart: {},
                    HourDayFinish: {}
                }

                Client.post('/Shop/add?startTime=' + fromTimeRef.current.value + '&finishTime=' + ToTimeRef.current.value, currentShop).then((res) => {
                    //Client.get('/Shop/getAll').then(res1 => {
                    dispatch(updateShops(res.data))
                    //תודיע שהעדכון הושלם בהצלחה
                    setFlagRedirect(true)
                    // })

                }).catch(() => {
                    console.log("error");//תודיע שחלה בעיה בעדכון })

                })
            }
        }

        function checkAndSet() {

            setToValidate(true)
            if (demand.length == 0 || nameShop.length == 0 || address.length == 0)
                return
            if ((parseInt(demand) <= 0) && demand.toString.length == 0) {
                //תודיע שיש שגיאה בדרישה
                return
            }
            SaveShopInDBAndRedux()






        }
        useEffect(FillTextBox, () => { }, [])
        function FillTextBox() {
            if (ShopId != "add") {
                currentShop = props.shops.find(s => s.ShopId == ShopId)
                nameRef.current.value = currentShop.ShopName
                addressRef.current.value = currentShop.placeAddress
                demandRef.current.value = currentShop.GoodsRequired
                let MinutesString = currentShop.HourDayStart.Minutes
                if (currentShop.HourDayStart.Minutes < 10)
                    MinutesString = "0" + MinutesString
                let houreString = currentShop.HourDayStart.Hours
                if (currentShop.HourDayStart.Hours < 10)
                    houreString = "0" + houreString

                fromTimeRef.current.value = houreString + ":" + MinutesString
                MinutesString = currentShop.HourDayFinish.Minutes
                if (MinutesString < 10)
                    MinutesString = "0" + MinutesString
                houreString = currentShop.HourDayFinish.Hours
                if (houreString < 10)
                    houreString = "0" + houreString
                ToTimeRef.current.value = houreString + ":" + MinutesString
                setAddress(addressRef.current.value)
                setNameShop(nameRef.current.value)
                setDemand(demandRef.current.value)

            }
        }
        if (flagRedirect)
            return <Redirect to='/provider/shops'></Redirect>
        return (
            <>
                <NavBar Mypage="updateShop"></NavBar>
                <Form className="form box">
                    <h4 className="titlePrivate">פרטי חנות</h4>
                    <div className="form">
                        <Form.Group className="inputIN">

                            <Form.Label>שם החנות</Form.Label>

                            <Form.Control type="text" ref={nameRef} value={nameShop} onChange={e => { setNameShop(e.target.value); }}></Form.Control>
                            <Form.Text style={{ visibility: toValidate && nameShop.length == 0 ? "visible" : "hidden" }}>שדה חובה </Form.Text>
                        </Form.Group>

                        <Form.Group className="inputIN">

                            <Form.Label>כתובת</Form.Label>
                            <Form.Control type="text" ref={addressRef} value={address} onChange={e => { setAddress(e.target.value); }} ></Form.Control>
                            <Form.Text style={{ visibility: toValidate && address.length == 0 ? "visible" : "hidden" }}>שדה חובה </Form.Text>
                        </Form.Group>
                        <Form.Group className="inputIN">
                            <Form.Label>כמות סחורה נדרשת</Form.Label>
                            <Form.Control type='number' ref={demandRef}></Form.Control>
                            <Form.Text style={{ visibility: toValidate && (parseInt(demandRef.current.value) <= 0) && demandRef.current.value.toString.length == 0 ? "visible" : "hidden" }}>תוכן  שגוי</Form.Text>

                        </Form.Group>
                        <Form.Group className="inputIN">
                            <Form.Label>השעה המוקדמת ביותר בה ניתן להגיע לחנות</Form.Label>
                            <Form.Control type='time' ref={fromTimeRef}></Form.Control>
                        </Form.Group>
                        <Form.Group className="inputIN">
                            <Form.Label>השעה המאוחרת ביותר בה ניתן להגיע לחנות</Form.Label>
                            <Form.Control type='time' ref={ToTimeRef}></Form.Control>
                        </Form.Group>
                    </div>
                    <Button className="button-ok" variant="contained" color="success" onClick={checkAndSet}>שמור</Button>
                </Form>

            </>

        )
    }
)
