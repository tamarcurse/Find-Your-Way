//path="provider/trucks"

//שקופית 9

import React, { useState } from "react";
import { Redirect } from "react-router-dom/cjs/react-router-dom.min";
import { connect } from "react-redux";
import { updateTrucks } from "../../../redux/actions";
import { Client } from "../../Client";
import FilterdriverId from "../../filter/filterdriverId/filterdriverId";
import FilterDriverName from "../../filter/filterDriverName/filterDriverName";
import FilterLicensePlate from "../../filter/filterLicensePlate/filterLicensePlate";
import OneTruckInTable from "../oneTruckInTable/oneTruckInTable";
import { ListGroup, Card } from "react-bootstrap"
import NavBar from "../../home/navbar";
import Button from '@mui/material/Button';
import Icon from '@mui/material/Icon';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import Typography from '@mui/material/Typography';
import truckDriverImg from "../../../images/truck.png"
import CircularProgress from '@mui/material/CircularProgress';

import "../../../App.css"
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
    return { trucks: state.TrucksByProvider.trucks }
}
export default connect(mapStateToProps)(
    function TrucksAll(props) {
        const { dispatch, trucks } = props
        const [truckDelete, setTruckDetlete] = useState()
        const [addShopFlag, setAddShopFlag] = useState(false)
        const [arrTrucks, setArrTrucks] = useState(trucks)
        const [show, setShow] = useState(false);
        let deleteOk = false
        const handleClose = () => setShow(false);

        function deleteMyTruck(truckId) {
            setTruckDetlete(truckId)
            setShow(true)
            debugger
            if (deleteOk) {
                Client.delete('/Truck/delete?id=' + truckId).then((res) => {
                    if (res.data != null && res.data != "") { dispatch(updateTrucks(res.data)) }
                    setArrTrucks(res.data)
                    deleteOk = false

                }).catch(() => {
                    //תודיע שקרתה בעיה בזמן המחיקה
                    console.log("catch");
                })
            }

        }
        function IsLicensePlateExist(LicensePlate) {
            return (trucks.includes(LicensePlate))
        }
        function AddShop() {
            setAddShopFlag(true)
        }
        if (addShopFlag)
            return <Redirect to="/provider/truck/privateTruck/add" />
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
                            מחיקת משאית
                        </Typography>
                        <Typography id="keep-mounted-modal-description" sx={{ mt: 2 }}>
                            האם אתה בטוח שברצונך למחוק מהמערכת משאית זו?
                        </Typography>
                        <Button variant="primary" dir="rtl" onClick={() => {
                            deleteOk = true
                            deleteMyTruck(truckDelete)
                            setShow(false)
                        }} >אישור</Button>
                        <Button variant="primary" dir="rtl" onClick={() => {
                            // deleteOk=false
                            setShow(false)
                        }} >ביטול</Button>
                    </Box>
                </Modal>
             
                <NavBar Mypage="trucks"></NavBar>
                <div>
                    <div className="box filters">
                        {/* <h3 className="margin-4">חיפושים</h3> */}
                        <FilterdriverId setArrTrucks={setArrTrucks} />
                        <FilterDriverName setArrTrucks={setArrTrucks} />
                        <FilterLicensePlate setArrTrucks={setArrTrucks} />

                    </div>
                    <div className="cards">
                        <div numbered className=" container cards">

                            {<Card className="card">
                                <Card.Img variant="top" src={truckDriverImg} />
                                <Card.Body>
                                    <Tooltip title="הוסף משאית">
                                        <IconButton onClick={AddShop}>
                                            <Icon sx={{ fontSize: 166 }}  >add_circle</Icon>
                                        </IconButton>
                                    </Tooltip>
                                </Card.Body>

                            </Card>}
                            {(arrTrucks) ? arrTrucks.map((truck, index) => (<Card

                                className="card"
                                key={index}><OneTruckInTable currentTruck={truck} deleteMyTruck={deleteMyTruck}/>

                            </Card>)) : <CircularProgress />}
                        </div>

                    </div>

                </div>
           
           
            </>
        )


    })