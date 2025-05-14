//מוצגת לנהג שאין לו מסלול
import React from "react";
import logo from "./../../images/logo.png"

export default function HaveNotTruck() {
    return (
        <>
            <div className="text-have-not">בשלב זה אין  צורך בנהג נוסף</div>
            <img className="bigLogo" src={logo}></img>
        </>
    )
}