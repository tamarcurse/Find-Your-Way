//שקופית 7
//path="/provider/shops"
import React, { useEffect, useState } from "react";
import { Redirect } from "react-router-dom/cjs/react-router-dom.min";
import FilterAddressShop from "../../filter/filterAddressShop/filterAddressShop";
import FilterNameShop from "../../filter/filterShopName/filterShopName";
import ShopInTable from "../ShopInTable/shopInTable";
import { connect } from "react-redux";
import { updateShops } from "../../../redux/actions";
import { Client } from "../../Client";
import Button from '@mui/material/Button';
import NavBar from "../../home/navbar";
import Icon from '@mui/material/Icon';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';
import "../../../App.css"
import { Card } from "react-bootstrap";
import shop from "../../../images/shop.jpg"
import { createTheme } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import Typography from '@mui/material/Typography';
import truckDriverImg from "../../../images/truck.png"
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
function mapStateToProps(state) {
    return { shops: state.shopsByProvider.shops }
}
export default connect(mapStateToProps)(
    function UpdateShops(props) {
        const { dispacth } = props
        const [show, setShow] = useState(false);
        const [deleteOk, setDeleteOk] = useState(false)
        const handleClose = () => setShow(false);
        const [shopDelete, setShopDelete] = useState()
        const [addShopFlag, setAddShopFlag] = useState(false)
        const [arrShops, setArrShops] = useState([{}])
        useEffect(() => {
            setArrShops(props.shops
            )
            const theme = createTheme({
                palette: {
                    primary: {
                        light: '#757ce8',
                        main: '#ffffff',
                        dark: '#002884',
                        contrastText: '#fff',
                    },
                    secondary: {
                        light: '#ff7961',
                        main: '#f44336',
                        dark: '#ba000d',
                        contrastText: '#000',
                    },
                },
            });
        }, [])
        function deleteMyShop(shopId) {
            setShopDelete(shopId)
            setShow(true)

            if (deleteOk) {
                Client.delete('/Shop/delete?id=' + shopId).then((req, res) => {
                    //תשלח הודעה החנות נמחקה בהצלחה
                    if (res.data != null)
                        dispacth(updateShops(res.data))
                    setArrShops(res.data)

                }).catch(() => {
                    //תודיע שקרתה בעיה בזמן המחיקה
                })
            }

        }
        if (addShopFlag)
            return <Redirect to="/provider/shop/privateShop/add" />
        return (
            <>
                <Modal
                    keepMounted

                    open={show}
                    onClose={handleClose}
                    aria-labelledby="keep-mounted-modal-title"
                    aria-describedby="keep-mounted-modal-description"
                >
                    <Box sx={style}>
                        <Typography id="keep-mounted-modal-title" variant="h6" component="h2">
                            מחיקת חנות
                        </Typography>
                        <Typography id="keep-mounted-modal-description" sx={{ mt: 2 }}>
                            האם אתה בטוח שברצונך למחוק מהמערכת חנות זו?
                        </Typography>
                        <Button variant="primary" dir="rtl" onClick={() => {
                            setDeleteOk(true)
                            deleteMyShop(shopDelete)
                            setShow(false)
                        }} >אישור</Button>
                        <Button variant="primary" dir="rtl" onClick={() => {
                            setDeleteOk(false)
                            setShow(false)
                        }} >ביטול</Button>
                    </Box>
                </Modal>

                <NavBar Mypage="updateShop"></NavBar>
                <div>
                    <div className="box filters filterShop">
                        {/* <h3 className="margin-4">חיפושים</h3> */}
                        <FilterNameShop setArrShops={setArrShops} />
                        <FilterAddressShop setArrShops={setArrShops} />

                    </div>
                    <div className="cards">
                        <div numbered className=" container cards">
                            <Card className="card">
                                <Card.Img variant="top" src={shop} />
                                <Card.Body>
                                    <Tooltip title="הוסף חנות">
                                        <IconButton onClick={() => { setAddShopFlag(true) }}>
                                            <Icon sx={{ fontSize: 140 }}  >add_circle</Icon>
                                        </IconButton>
                                    </Tooltip></Card.Body>

                            </Card>
                            {/* החיפוש לא עובד arrshops הניה פתאום לא מוגדר והתכנית נופלת */}
                            {arrShops ?
                                arrShops.map(
                                    (shop, index) => (<Card

                                        className="card"
                                        key={index}>
                                        <ShopInTable currentShop={shop} deleteMyShop={deleteMyShop} />
                                    </Card>))
                                :
                                "retriving data"}

                        </div>

                    </div>

                </div>


            </>
        )


    })