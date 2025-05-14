import React from "react";
import { connect } from "react-redux";
import TextField from '@mui/material/TextField';
//import { grey } from '@mui/material/colors';
// import theme from "../../shops/deleteShops/deleteShops"
function mapStateToProps(state) {
    return {
        shops: state.shopsByProvider.shops 
    }
}
export default connect(mapStateToProps)(
    function FilterAddressShop(props) {
        const { setArrShops, shops } = props

        function filterkey(e) {

            setArrShops(shops.filter(s => s.placeAddress.includes(e)))

        }
        return (
            <div className="margin-4">

                <TextField onChange={(e) => { filterkey(e.target.value) }} id="outlined-search" label="חיפוש כתובת" variant="outlined" type="search"></TextField>
            </div>
        )
    })






