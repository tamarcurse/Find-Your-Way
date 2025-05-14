//שורה המייצגת חנות בשקופיות 6 ו-7
import React, { useState } from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom/cjs/react-router-dom.min";

import UpdateIcon from '@mui/icons-material/Update';

import { Card } from "react-bootstrap";
import DeleteIcon from '@mui/icons-material/Delete';
import shop from "../../../images/shop.jpg"
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';


export default function ShopInTable(props) {

    const { currentShop, deleteMyShop } = props
    //האם נבחרה חנות לעדכון
    const [flagUpdate, setFlagUpdate] = useState(false)
    function deleteShop() {
        //תוציא הודעה האם אתה בטוח שאתה רוצה למחוק?
        //אם כן
        deleteMyShop(currentShop.ShopId)

    }
    if (flagUpdate)
        //לשלוח לו איזה חנות אני רוצה לעדכן
        return <Redirect to={"/provider/shop/privateShop/" + currentShop.ShopId} />
    if (currentShop)
        return (
            <div className=" me-auto">
                <Card.Img variant="top" src={shop} />
                <Card.Body>
                    <Card.Title className="fw-bold">{currentShop.ShopName}</Card.Title>

                    <Card.Text>{currentShop.placeAddress}</Card.Text>
                    <Card.Text>כמות סחורה שהוזמנה
                        :
                        {currentShop.GoodsRequired}</Card.Text>
                    <Card.Subtitle>
                        <Tooltip title="עדכן">
                            <IconButton onClick={() => { setFlagUpdate(true) }}>
                                <UpdateIcon></UpdateIcon>
                            </IconButton>
                        </Tooltip>
                        <Tooltip title="מחק">
                            <IconButton onClick={deleteShop}>
                                <DeleteIcon></DeleteIcon>
                            </IconButton>
                        </Tooltip>
                    </Card.Subtitle>

                </Card.Body>
            </div>
        )

}
