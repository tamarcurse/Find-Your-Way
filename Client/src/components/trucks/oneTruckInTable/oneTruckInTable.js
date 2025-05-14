//מייצג משאית אחת ברשימת המשאיות שבשקופית 9
//שורה המייצגת חנות בשקופיות 6 ו-7
import React, { useState } from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom/cjs/react-router-dom.min";
import garbage from "../../../images/garbage.png"
import update from "../../../images/update.png"
import truckDriverImg from "../../../images/truck.png"
import UpdateIcon from '@mui/icons-material/Update';
import { Image } from "react-bootstrap";
import { Card } from "react-bootstrap";
import DeleteIcon from '@mui/icons-material/Delete';

import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';
export default function OneTruckInTable(props) {

    const { currentTruck, deleteMyTruck } = props
    //האם נבחרה משאית לעדכון
    const [flagUpdate, setFlagUpdate] = useState(false)
    function deleteTruck() {
        deleteMyTruck(currentTruck.LicensePlate)
    }
    if (flagUpdate)
        //לשלוח לו איזה משאית אני רוצה לעדכן
        return <Redirect to={"/provider/truck/privateTruck/" + currentTruck.LicensePlate} />
    if (currentTruck)
        return (
            <>
                <div className="me-auto">

                    <Card.Img variant="top" src={truckDriverImg} />
                    {/* <span>נהג: </span> */}
                    <Card.Body>
                        <Card.Title className="fw-bold">
                            לוחית רישוי:
                            {currentTruck.LicensePlate}
                        </Card.Title>


                        <Card.Text>
                            שם הנהג:
                            {currentTruck.NameOfDriver}
                        </Card.Text>
                        <Card.Text>
                            ת.ז.:
                            {currentTruck.IdOfDriver}
                        </Card.Text>
                        <Card.Subtitle>
                            <Tooltip title="עדכן">
                                <IconButton onClick={() => { setFlagUpdate(true) }}>
                                    <UpdateIcon></UpdateIcon>
                                </IconButton>
                            </Tooltip>
                            <Tooltip title="מחק">
                                <IconButton onClick={deleteTruck}>
                                    <DeleteIcon></DeleteIcon>
                                </IconButton>
                            </Tooltip>
                        </Card.Subtitle>

                    </Card.Body>




                </div>

            </>
        )

}
    //)