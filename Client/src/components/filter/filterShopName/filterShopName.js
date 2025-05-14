import React, { useRef } from "react";
import { connect } from "react-redux";
import TextField from '@mui/material/TextField';
function mapStateToProps(state) {
    return {
        shops: state.shopsByProvider.shops
    }
}
export default connect(mapStateToProps)(
    function FilterNameShop(props) {

        const { setArrShops, shops } = props

        function filterkey(e) {

            setArrShops(shops.filter(s => s.ShopName.includes(e)))
        }
        return (

            <div className="margin-4">
                <TextField type="search" onChange={(e) => filterkey(e.target.value)} id="outlined-search" label="חיפוש שם חנות" variant="outlined"></TextField>

            </div>
        )
    })