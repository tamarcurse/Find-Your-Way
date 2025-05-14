import React from "react";
import { Link } from "react-router-dom";
export default function Links() {
    return (
        <>
            <ul>
                <li><Link to="/provider/privateProvider">פרטים אישיים</Link></li>
                <li><Link to="/provider/privateFactory">פרטי מפעל</Link></li>
                
                <li><Link to="/provider/shops/updateShop">חנויות</Link></li>
                <li><Link to="/provider/trucks">משאיות</Link></li>
                <li><Link to="/login/driver">כניסה נהג</Link></li>
                <li><Link to="/login/provider">כניסה ספק</Link></li>
                <li><Link to="/provider/truck/privateTruck/5378">פרטי משאית</Link></li>
                <li><Link to="/provider/shop/privateShop/20">פרטי חנות</Link></li>
                <li><Link to="/provider/trucks">משאיות</Link></li>
                <li><Link to="/driver/4567">הצגת המסלול לנהג</Link></li>
                <li><Link to="provider/map">הצגת המסלול לספק</Link></li>


            </ul>
        </>
    )
}